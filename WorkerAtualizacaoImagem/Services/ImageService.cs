using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Repository.Interfaces;
using WorkerAtualizacaoImagem.Services.Interfaces;

namespace WorkerAtualizacaoImagem.Services
{
    public class ImageService : IImageService
    {
        private BrunskerSettings _settings { get; }
        private readonly IImageRepository _imageRepository;


        public ImageService(IOptions<BrunskerSettings> settings, IImageRepository imageRepository)
        {
            _settings = settings.Value;
            _imageRepository = imageRepository;
        }


        public async Task UpdateCreationTimeImages(string[] skus)
        {
            try
            {
                var directoryInfo = new DirectoryInfo(_settings.ImagesFolder);
                var files = directoryInfo.GetFiles("*.jpg");
                
                Parallel.ForEach(files, file =>
                {
                    if (skus.FirstOrDefault(s => file.Name.Contains(s)) != null)
                        file.CreationTime = DateTime.Now;
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AppendImageNameTxt(string imageName, string type)
        {
            try
            {
                string path = _settings.ImagesFolder + @"\Reinserir Imagens";
                switch(type)
                {
                    case ("Insert"):
                        path += @"\ErrorInsertImages.txt";
                        break;

                    case ("Update"):
                        path += @"\ErrorUpdateImages.txt";
                        break;

                    case ("Delete"):
                        path += @"\ErrorDeleteImages.txt";
                        break;

                    default:
                        throw new Exception("Operation could not be found.");
                }

                var inserted = File.ReadAllLines(path);

                if(!inserted.Contains(imageName))
                    using (StreamWriter sw = File.AppendText(path))
                        sw.WriteLine(imageName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<string>> ReadImageError(string type)
        {
            string path = _settings.ImagesFolder + @"\Reinserir Imagens";
            string[] returnArray = Array.Empty<string>();
            try
            {
                switch (type)
                {
                    case ("Insert"):
                        path += @"\ErrorInsertImages.txt";
                        break;

                    case ("Update"):
                        path += @"\ErrorUpdateImages.txt";
                        break;

                    case ("Delete"):
                        path += @"\ErrorDeleteImages.txt";
                        break;

                    default:
                        throw new Exception("Operation could not be found.");
                }
                returnArray = File.ReadAllLines(path);
                File.WriteAllText(path,"");
            }
            catch (Exception)
            {
                throw;
            }
            return returnArray.ToList();
        }


        public async Task<string[]> GetSkus() => await _imageRepository.GetSkus();
        public async Task CreateNecessaryFiles() => await _imageRepository.CreateNecessaryFiles();
        public async Task EraseSkusFromTxt() => await _imageRepository.EraseSkusFromTxt();
        public async Task<List<FileInfo>> SearchImages() => await _imageRepository.SearchImages();
        public async Task<List<FileInfo>> SearchInsertImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroInsertImages)
            => await _imageRepository.SearchInsertImages(actualImages, previousList, erroInsertImages);
        public async Task<List<FileInfo>> SearchUpdateImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroUpdateImages)
            => await _imageRepository.SearchUpdateImages(actualImages, previousList, erroUpdateImages);
        public async Task<List<FileInfo>> SearchDeleteImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroDeleteImages) 
            => await _imageRepository.SearchDeleteImages(actualImages, previousList, erroDeleteImages);
    }
}
