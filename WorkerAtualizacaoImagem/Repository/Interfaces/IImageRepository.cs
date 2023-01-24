using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerAtualizacaoImagem.Repository.Interfaces
{
    public interface IImageRepository
    {
        Task<List<FileInfo>> SearchImages();
        Task<List<FileInfo>> SearchInsertImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroInsertImages);
        Task<List<FileInfo>> SearchUpdateImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroUpdateImages);
        Task<List<FileInfo>> SearchDeleteImages(List<FileInfo> actualImages, List<FileInfo> previousList, List<string> erroDeleteImages);
        Task<string[]> GetSkus();
        Task CreateNecessaryFiles();
        Task EraseSkusFromTxt();
    }
}
