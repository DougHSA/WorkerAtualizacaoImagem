using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;
using WorkerAtualizacaoImagem.Models.Magento;

namespace Repository
{
    public class ImageMagentoRepository : IImageMagentoRepository
    {
        private readonly string _endpoint = "rest/V1/products";
        private readonly ILogger<ImageMagentoRepository> _logger;
        private readonly IMagentoConnection _m2;

        public ImageMagentoRepository(ILogger<ImageMagentoRepository> loggerFactory, IMagentoConnection m2)
        {
            _logger = loggerFactory;
            _m2 = m2;
        }

        public async Task<int?> Create(MediaGalleryEntry mediaGallery, string sku)
        {
            try
            {
                var oldMedia = await GetBySku(mediaGallery, sku);

                if (oldMedia != null && oldMedia.Count>0)
                {
                    mediaGallery.Label = oldMedia[0].Label;
                    if (oldMedia.Exists(i => i.File.Contains(mediaGallery.Content.Name.Split(".")[0])))
                        return -1;
                }

                JToken mediaGallery_ = JToken.FromObject(mediaGallery);

                var model = new { entry = mediaGallery_ };

                var json = JsonConvert.SerializeObject(model);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await _m2.POST(_endpoint + "/" + sku + "/media", content);

                if (result != null)
                    return JsonConvert.DeserializeObject<int?>(result);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + "\n");
            }

            return null;
        }
        public async Task<bool> Delete(MediaGalleryEntry mediaGallery, string sku)
        {
            try
            {
                var oldMedia = await GetBySku(mediaGallery, sku);
                if (oldMedia == null || oldMedia.Count == 0)
                    throw new Exception("Could not find sku product.");

                mediaGallery.Id = oldMedia.FirstOrDefault(i => i.File.Contains(mediaGallery.Content.Name.Split(".")[0])).Id;
                var result = await _m2.DELETE(_endpoint + "/" + sku + "/media/" + mediaGallery.Id);

                if (result != null)
                    return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + "\n");
            }
            return false;
        }
        public async Task<List<MediaGalleryEntry>> GetBySku(MediaGalleryEntry mediaGallery, string sku)
        {
            try
            {
                var result = await _m2.GET(_endpoint + "/" + sku + "/media");
                if (result != null)
                    return JsonConvert.DeserializeObject<List<MediaGalleryEntry>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }  
        public async Task<bool> Update(MediaGalleryEntry mediaGallery, string sku)
        {
            try
            {
                var oldMedia = await GetBySku(mediaGallery, sku);
                if (oldMedia == null || oldMedia.Count == 0)
                    throw new Exception("Could not find sku product.");

                mediaGallery = MapperMedia(mediaGallery, oldMedia);

                JToken mediaGallery_ = JToken.FromObject(mediaGallery);

                var model = new { entry = mediaGallery_ };

                var json = JsonConvert.SerializeObject(model);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _m2.PUT(_endpoint + "/" + sku + "/media/" + mediaGallery.Id, content);

                if (response != null)
                    return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace + "\n");
            }
            return false;
        }
        public async Task<string[]> GetProductsWithoutImage()
        {
            string[] skus = { };
            try
            {
                var result = await _m2.GET(_endpoint + "-without-image");
                if (result != null)
                {
                    var products = JsonConvert.DeserializeObject<ProductWithoutImage>(result);
                    if(products.Status)
                        skus = products.Skus;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace + "\n");
            }
            return skus;
        }

        private MediaGalleryEntry MapperMedia(MediaGalleryEntry mediaGallery, List<MediaGalleryEntry> oldMedia)
        {
            try
            {
                mediaGallery.Id = oldMedia.FirstOrDefault(m => m.File.Contains(mediaGallery.Content.Name.Split(".")[0])).Id;
                mediaGallery.Label = oldMedia.FirstOrDefault(m => m.File.Contains(mediaGallery.Content.Name.Split(".")[0])).Label;
                mediaGallery.Position = oldMedia.FirstOrDefault(m => m.File.Contains(mediaGallery.Content.Name.Split(".")[0])).Position;
            }
            catch (Exception ex)
            {
                return new MediaGalleryEntry();
            }

            return mediaGallery;
        }
    }
}