using Interfaces;
using PicTureen.Properties;

namespace PicTureen.Services
{
    public class SettingsService: ISettingsService
    {
        public string DatabasePath { get; set; }

        public string ImagesDirectory
        {
            get { return Settings.Default.RootDirectory; }
            set
            {
                Settings.Default.RootDirectory = value;
                Settings.Default.Save();
            }
        }
    }
}
