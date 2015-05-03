using System.Collections.Generic;
using System.IO;
using System.Linq;
using Caliburn.PresentationFramework;
using Database;

namespace PicTureen.Models.Tree
{
    public abstract class TreeItem: PropertyChangedBase
    {
        protected TreeItem(string fullPath)
        {
            FullPath = fullPath;
        }

        public virtual string Title { get { return new DirectoryInfo(FullPath).Name; } }
        public string FullPath { get; set; }
        
    }

    public class ImageTreeItem : TreeItem
    {
        public override string Title { get { return Path.GetFileNameWithoutExtension(FullPath); } }
        public ImageTreeItem(Image image) : base(image.Path)
        {
        }
    }

    public class DirectoryItem : TreeItem
    {
        public BindableCollection<TreeItem> Children { get; set; }

        public void AddRange(IEnumerable<Image> images)
        {
            Children.IsNotifying = false;
            foreach (var image in images)
            {
                Add(image);
            }
            Children.IsNotifying = true;
            Children.Refresh();
        }

        public void Add(Image item)
        {
            if (Path.GetDirectoryName(item.Path) == FullPath)
                Children.Add(new ImageTreeItem(item));
            else
            {
                var dir = Children.FirstOrDefault(t => item.Path.StartsWith(t.FullPath) && item.Path.Remove(0, t.FullPath.Length).StartsWith("\\")) as DirectoryItem;
                if (dir != null)
                {
                    dir.Add(item);
                }
                else
                {
                    var title = FullPath + Path.GetDirectoryName(item.Path.Remove(0, FullPath.Length));
                    var newDir = new DirectoryItem(title);
                    newDir.Add(item);
                    Children.Add(newDir);
                }
            }
        }

        public DirectoryItem(string fullPath) : base(fullPath)
        {
            Children = new BindableCollection<TreeItem>();
        }
    }
}
