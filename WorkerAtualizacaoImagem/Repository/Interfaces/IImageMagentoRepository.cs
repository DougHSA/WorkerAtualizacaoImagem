using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Models.Magento;

namespace Repository.Interfaces
{
    public interface IImageMagentoRepository
    {
        Task<int?> Create(MediaGalleryEntry mediaGallery, string sku);
        Task<bool> Delete(MediaGalleryEntry mediaGallery, string sku);
        Task<List<MediaGalleryEntry>> GetBySku(MediaGalleryEntry mediaGallery, string sku);
        Task<bool> Update(MediaGalleryEntry mediaGallery, string sku);
    }
}