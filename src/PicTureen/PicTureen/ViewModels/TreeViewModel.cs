using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Caliburn.PresentationFramework;
using Interfaces;
using PicTureen.Models.Tree;
using PicTureen.Services;
using PicTureen.Support;

namespace PicTureen.ViewModels
{
    public class TreeViewModel: PropertyChangedBase
    {
        private readonly IAppState _appState;
        private readonly IContextProvider _contextProvider;
        private readonly ISettingsService _settingsService;

        public BindableCollection<TreeItem> Items { get; set; }
        public DelegateCommand HandleSelectionChangeCommand { get; set; }
        public TreeViewModel(IAppState appState, IContextProvider contextProvider, ISettingsService settingsService)
        {
            _appState = appState;
            _contextProvider = contextProvider;
            _settingsService = settingsService;

            HandleSelectionChangeCommand = new DelegateCommand(HandleSelectionChange);
            BuildTree();

            _appState.DbChanged += AppStateOnDbChanged;
            _appState.RequestImagesDisplay(((DirectoryItem)Items.First()).Children.OfType<ImageTreeItem>().Select(i => i.Image));
        }

        private async void HandleSelectionChange(object obj)
        {
            var args = obj as RoutedPropertyChangedEventArgs<Object>;
            if (args != null)
            {
                if (args.NewValue is DirectoryItem)
                {
                    var dir = args.NewValue as DirectoryItem;
                    _appState.RequestImagesDisplay(dir.Children.OfType<ImageTreeItem>().Select(i => i.Image));
                }
            }
        }

        private void AppStateOnDbChanged(object sender, EventArgs eventArgs)
        {
            BuildTree();
        }

        public void BuildTree()
        {
            var root = new DirectoryItem(_settingsService.ImagesDirectory);
            root.AddRange(_contextProvider.GetDbContext().Images.AsEnumerable());
            Items = new BindableCollection<TreeItem>();
            Items.Add(root);
        }
    }
}
