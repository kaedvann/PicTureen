using Caliburn.PresentationFramework;
using PicTureen.Models.Tree;
using PicTureen.Services;

namespace PicTureen.ViewModels
{
    public class TreeViewModel: PropertyChangedBase
    {
        private readonly IAppState _appState;

        public BindableCollection<TreeItem> Items { get; set; } 
        public TreeViewModel(IAppState appState)
        {
            _appState = appState;
            BuildTree();
        }

        public void BuildTree()
        {
            Items = new BindableCollection<TreeItem>();
            Items.AddRange(new TreeItem[]{new DirectoryItem {Title = "test1", Children = new BindableCollection<TreeItem>(){new ImageTreeItem(){Title = "test2"}, new ImageTreeItem(){Title = "test3"}}} , new ImageTreeItem(){Title = "test"}});
        }
    }
}
