using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Models.Magento;

namespace WorkerAtualizacaoImagem.Services
{
    public class ImageMagentoService : IImageMagentoService
    {
        private BrunskerSettings _settings { get; }
        private readonly IImageMagentoRepository _m2;
        public ImageMagentoService(IOptions<BrunskerSettings> settings, IImageMagentoRepository imageMagentoService)
        {
            _settings = settings.Value;
            _m2 = imageMagentoService;
        }

        public List<MediaGalleryEntry> MediaGalleryResolver (List<FileInfo> fileList)
        {
            try
            {
                var medias = new List<MediaGalleryEntry>();
                foreach (FileInfo file in fileList)
                {
                    if (fileList.Any())
                    {

                        string[] types = { };

                        string extension = Path.GetExtension(file.FullName).Replace(".", "");

                        string[] image = file.FullName.ToLower()
                            .Substring(file.FullName.LastIndexOf("\\"))
                            .Substring(1)
                            .Split(".");

                        string image64 = ParseToBase64(image[0]);

                        string imageType = extension.ToLower() == "jpg" ? "jpeg" : extension;

                        types = new string[] { "image", "small_image", "thumbnail", "swatch_image" };

                        Content content = new Content
                        {
                            Base64EncodedData = image64,
                            Type = "image/" + imageType,
                            Name = image[0] + "." + image[1]
                        };
                        var media = new MediaGalleryEntry
                        {
                            Position = 1,
                            Disabled = false,
                            Label = "",
                            MediaType = "image",
                            Types = types,
                            Content = content
                        };
                        medias.Add(media);
                        types = new string[] { };
                    }
                }
                return medias;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        private string ParseToBase64(string name)
        {
            string file = _settings.ImagesFolder + name + ".jpg";

            if (File.Exists(file)) return Convert.ToBase64String(File.ReadAllBytes(file));

            else return "";
        }

        public Task<int?> Create(MediaGalleryEntry mediaGallery, string sku) => _m2.Create(mediaGallery, sku);
        public Task<bool> Delete(MediaGalleryEntry mediaGallery, string sku) => _m2.Delete(mediaGallery, sku);
        public Task<List<MediaGalleryEntry>> GetBySku(MediaGalleryEntry mediaGallery, string sku) => _m2.GetBySku(mediaGallery, sku);
        public Task<bool> Update(MediaGalleryEntry mediaGallery, string sku) => _m2.Update(mediaGallery, sku);
        public Task<string[]> GetProductsWithoutImage()=>_m2.GetProductsWithoutImage();
    }
}
