using Interfaces;

namespace PicTureen.Services
{
    public class SettingsService: ISettingsService
    {
        public string DatabasePath { get; set; }
        public string ImagesDirectory { get; set; }
    }
}
