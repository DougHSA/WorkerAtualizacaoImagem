using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Repository.Interfaces;

namespace WorkerAtualizacaoImagem.Services.Interfaces
{
    public interface IImageService : IImageRepository
    {
        Task UpdateCreationTimeImages(string[] skus);
        Task AppendImageNameTxt(string imageName, string type);
        Task<List<string>> ReadImageError(string type);
    }
}
