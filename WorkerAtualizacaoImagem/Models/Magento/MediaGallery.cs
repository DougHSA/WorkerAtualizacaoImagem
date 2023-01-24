namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class MediaGallery
    {
        public int Id { get; set; }
        public string MediaType { get; set; }
        public object Label { get; set; }
        public int Position { get; set; }
        public bool Disabled { get; set; }
        public string[] Types { get; set; }
        public string File { get; set; }
    }
}