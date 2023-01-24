using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Repository.Interfaces;

namespace WorkerAtualizacaoImagem.Repository
{
    public class ImageRepository : IImageRepository
    {
        private BrunskerSettings _settings { get; }

        public ImageRepository(IOptions<BrunskerSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<List<FileInfo>> SearchImages()
        {
            var directoryInfo = new DirectoryInfo(_settings.ImagesFolder);
            return directoryInfo.GetFiles("*.jpg").Where(i=>i.Name.Contains("_")).ToList();
        }

        public async Task<List<FileInfo>> SearchInsertImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroInsertImages)
        {
            List<FileInfo> insertImages = new List<FileInfo>();
            try
            {
                Parallel.ForEach(actualImages, image =>
                {
                    if (!previousList.Exists(i => i.FullName.Equals(image.FullName))
                        || previousList.Exists(i => i.FullName.Equals(image.FullName) && i.CreationTime < image.CreationTime)
                        || erroInsertImages.Contains(image.Name))
                        insertImages.Add(image);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
            return insertImages;
        }

        public async Task<List<FileInfo>> SearchUpdateImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroUpdateImages)
        {
            List<FileInfo> updateImages = new List<FileInfo>();
            try
            {
                Parallel.ForEach(actualImages, image =>
                {
                    if (previousList.Exists(i => i.FullName.Equals(image.FullName) && i.LastWriteTime < image.LastWriteTime)
                        || erroUpdateImages.Contains(image.Name))
                        updateImages.Add(image);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
            return updateImages;
        }

        public async Task<List<FileInfo>> SearchDeleteImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroDeleteImages)
        {
            List<FileInfo> deleteImages = new List<FileInfo>();
            try
            {
                Parallel.ForEach(previousList, image =>
                {
                    if (!actualImages.Exists(i => i.FullName.Equals(image.FullName)))
                        deleteImages.Add(image);
                });

                erroDeleteImages.ForEach(i=> File.Create(_settings.ImagesFolder + i).Close());

                var searchDeletedImage = await SearchImages();

                Parallel.ForEach(searchDeletedImage, image =>
                {
                    if (!actualImages.Exists(i => i.Name.Equals(image.Name)))
                        deleteImages.Add(image);
                });

                erroDeleteImages.ForEach(i => File.Delete(_settings.ImagesFolder + i));
            }
            catch (Exception ex)
            {
                throw;
            }
            return deleteImages;
        }

        public async Task<string[]> GetSkus()
        {
            string[] skus = { };
            try
            {
                skus = File.ReadAllLines(_settings.ImagesFolder + @"\Reinserir Imagens\Sku's.txt");

                for (int i = 0; i < skus.Length; i++)
                    skus[i] = skus[i].Replace("\"", "")
                                        .Replace("'", "")
                                        .Replace(",", "")
                                        .Replace(" ", "")
                                        .Trim();

                return skus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateNecessaryFiles()
        {
            try
            {
                Directory.CreateDirectory(_settings.ImagesFolder + @"\Reinserir Imagens");

                var directoryInfo = new DirectoryInfo(_settings.ImagesFolder + @"\Reinserir Imagens");
                var files = directoryInfo.GetFiles("*.txt");

                if (files.FirstOrDefault(f => f.Name.Equals("Sku's.txt")) == null)
                    File.Create(_settings.ImagesFolder + @"\Reinserir Imagens\Sku's.txt").Close();

                if (files.FirstOrDefault(f => f.Name.Equals("ErrorInsertImages.txt")) == null)
                    File.Create(_settings.ImagesFolder + @"\Reinserir Imagens\ErrorInsertImages.txt").Close();

                if (files.FirstOrDefault(f => f.Name.Equals("ErrorUpdateImages.txt")) == null)
                    File.Create(_settings.ImagesFolder + @"\Reinserir Imagens\ErrorUpdateImages.txt").Close();

                if (files.FirstOrDefault(f => f.Name.Equals("ErrorDeleteImages.txt")) == null)
                    File.Create(_settings.ImagesFolder + @"\Reinserir Imagens\ErrorDeleteImages.txt").Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EraseSkusFromTxt()
        {
            try
            {
                File.WriteAllText(_settings.ImagesFolder + @"\Reinserir Imagens\Sku's.txt", "");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
