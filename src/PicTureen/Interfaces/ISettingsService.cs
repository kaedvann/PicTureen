namespace Interfaces
{
    public interface ISettingsService
    {
        string DatabasePath { get; set; }
        string ImagesDirectory { get; set; }
    }
}
