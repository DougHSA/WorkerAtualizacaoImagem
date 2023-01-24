using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Models.Magento;

namespace Repository.Interfaces
{
    public interface IImageMagentoService : IImageMagentoRepository
    {
        List<MediaGalleryEntry> MediaGalleryResolver(List<FileInfo> fileList);
    }
}