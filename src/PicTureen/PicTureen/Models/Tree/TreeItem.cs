using Caliburn.PresentationFramework;

namespace PicTureen.Models.Tree
{
    public abstract class TreeItem: PropertyChangedBase
    {
        public string Title { get; set; }
    }

    public class ImageTreeItem : TreeItem
    {
        
    }

    public class DirectoryItem : TreeItem
    {
        public BindableCollection<TreeItem> Children { get; set; }
 
    }
}
