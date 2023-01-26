using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Models.Magento;
using WorkerAtualizacaoImagem.Repository.Interfaces;
using WorkerAtualizacaoImagem.Services.Interfaces;

namespace WorkerAtualizacaoImagem
{
    public class Worker : BackgroundService
    {
        private readonly IImageService _imageService;
        private readonly ILogger<Worker> _logger;
        private readonly IImageMagentoService _m2;

        private List<FileInfo> previousList = new List<FileInfo>();
        private List<FileInfo> actualImages = new List<FileInfo>();
        private List<string> erroInsertImages = new List<string>();
        private List<string> erroUpdateImages = new List<string>();
        private List<string> erroDeleteImages = new List<string>();

        public Worker(ILogger<Worker> logger, IImageMagentoService m2, IImageService imageService)
        {
            _logger = logger;
            _m2 = m2;
            _imageService = imageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _imageService.CreateNecessaryFiles();
            await ReadTxtsErrors();
            actualImages = await _imageService.SearchImages();
            previousList = actualImages;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Beginning at: {time} \n", DateTime.Now);

                    actualImages = await _imageService.SearchImages();

                    await ReinsertImagesFolder();

                    await InsertImagesMagento();

                    await UpdateImagesMagento();

                    await DeleteImagesMagento();

                    previousList = actualImages;

                    _logger.LogInformation("Finishing at: {time} \n", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message + ex.StackTrace);
                }

                await Task.Delay(10*1000, stoppingToken);
            }
        }

        private async Task ReadTxtsErrors()
        {
            try
            {
                erroInsertImages = await _imageService.ReadImageError("Insert");
                erroUpdateImages = await _imageService.ReadImageError("Update");
                erroDeleteImages = await _imageService.ReadImageError("Delete");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + ex.StackTrace);
            }
        }
        private async Task ReinsertImagesFolder()
        {
            try
            {
                var insertSkus = await _m2.GetProductsWithoutImage();
                foreach (var sku in insertSkus)
                {
                    await _imageService.AppendImageNameTxt(sku, "Reinsert");
                }
                var skus = await _imageService.GetSkus();
                if (skus.Length==0 )
                    return;

                _logger.LogInformation("\n------------------------\n" +
                        "Beginning Image Reinsert at: {time}" +
                        "\n------------------------\n", DateTime.Now);

                await _imageService.UpdateCreationTimeImages(skus);
                await _imageService.EraseSkusFromTxt();

                _logger.LogInformation("\n------------------------\n" +
                        "Finishing Image Reinsert at: {time}" +
                        "\n------------------------\n", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
            }
        }
        private async Task InsertImagesMagento()
        {
            try
            {
                var insertImages = await _imageService.SearchInsertImages(actualImages, previousList,erroInsertImages);
                erroInsertImages.Clear();

                if (insertImages.Count > 0)
                {
                    _logger.LogInformation("\n------------------------\n" + 
                        "Beginning Image Insert at: {time}"+
                        "\n------------------------\n", DateTime.Now);

                    var imagesList = _m2.MediaGalleryResolver(insertImages);

                    foreach(var image in insertImages)
                    {
                        try 
                        {
                            var sku = image.Name.Split(".")[0];
                            var insertedImage = await _m2.Create(imagesList.FirstOrDefault(i=>i.Content.Name.Equals(image.Name)), sku);

                            if (insertedImage == (-1))
                            {
                                erroUpdateImages.Add(image.Name);
                                _logger.LogInformation($"Image {image.Name} for Update at - {DateTime.Now} \n");
                            }
                            else if (insertedImage != null)
                                _logger.LogInformation($"Image Inserted: {image.Name} at - {DateTime.Now} \n");
                            else
                            {
                                erroInsertImages.Add(image.Name);
                                await _imageService.AppendImageNameTxt(image.Name, "Insert");
                                _logger.LogWarning($"Image {image.Name} could not be Inserted.\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Image {image.Name} could not be Inserted.\n");
                        }
                        
                    }

                    _logger.LogInformation("\n------------------------\n" + 
                        "Finishing Image Insert at: {time}" +
                        "\n------------------------\n", DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
            }
        }
        private async Task UpdateImagesMagento()
        {
            try
            {
                var updateImages = await _imageService.SearchUpdateImages(actualImages, previousList, erroUpdateImages);
                erroUpdateImages.Clear();

                if (updateImages.Count > 0)
                {
                    _logger.LogInformation("\n------------------------\n" + 
                        "Beginning Image Update at: {time}" +
                        "\n------------------------\n", DateTime.Now);

                    var imagesList =_m2.MediaGalleryResolver(updateImages);

                    foreach(var image in updateImages)
                    {
                        var sku = image.Name.Split(".")[0];
                        var updatedImage = await _m2.Update(imagesList.FirstOrDefault(i => i.Content.Name.Equals(image.Name)), sku);
                        
                        if(updatedImage)
                        {
                            _logger.LogInformation($"Image Updated: {image.Name} at - {DateTime.Now} \n");
                        }
                        else
                        {
                            erroUpdateImages.Add(image.Name);
                            await _imageService.AppendImageNameTxt(image.Name, "Update");
                            _logger.LogWarning($"Image {image.Name} could not be Updated.\n");
                        }
                        
                    }
                    _logger.LogInformation("\n------------------------\n" +
                        "Finishing Image Update at: {time}" +
                        "\n------------------------\n", DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
            }
        }
        private async Task DeleteImagesMagento()
        {
            try
            {
                var deleteImages = await _imageService.SearchDeleteImages(actualImages, previousList, erroDeleteImages);
                erroDeleteImages.Clear();
                if (deleteImages.Count > 0)
                {
                    _logger.LogInformation("\n------------------------\n" + 
                        "Beginning Image Delete at: {time}" +
                        "\n------------------------\n", DateTime.Now);

                    var imagesList = _m2.MediaGalleryResolver(deleteImages);

                    foreach (var image in deleteImages)
                    {
                        var sku = image.Name.Split(".")[0];
                        var deletedImage = await _m2.Delete(imagesList.FirstOrDefault(i => i.Content.Name.Equals(image.Name)), sku);

                        if(deletedImage)
                        {
                            _logger.LogInformation($"Image Deleted: {image.Name} at - {DateTime.Now}\n");
                        }
                        else
                        {
                            erroDeleteImages.Add(image.Name);
                            await _imageService.AppendImageNameTxt(image.Name, "Delete");
                            _logger.LogWarning($"Image {image.Name} could not be Deleted.\n");
                        }
                    }

                    _logger.LogInformation("\n------------------------\n" +
                        "Finishing Delete Image Update at: {time}" +
                        "\n------------------------\n", DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
            }
        }
    }
}