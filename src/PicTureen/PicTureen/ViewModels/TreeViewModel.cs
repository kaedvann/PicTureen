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
        private readonly INavigationService _navigationService;

        public BindableCollection<TreeItem> Items { get; set; }

        public DelegateCommand OpenImageCommand { get; set; }

        public DelegateCommand HandleSelectionChangeCommand { get; set; }
        public TreeViewModel(IAppState appState, IContextProvider contextProvider, ISettingsService settingsService, INavigationService navigationService)
        {
            Items = new BindableCollection<TreeItem>();
            _appState = appState;
            _contextProvider = contextProvider;
            _settingsService = settingsService;
            _navigationService = navigationService;

            OpenImageCommand = new DelegateCommand(OpenImage);
            HandleSelectionChangeCommand = new DelegateCommand(HandleSelectionChange);
            BuildTree();

            _appState.DbChanged += AppStateOnDbChanged;
            RequestRootDisplay();
        }

        private void RequestRootDisplay()
        {
            _appState.RequestImagesDisplay(((DirectoryItem) Items.First()).Children.OfType<ImageTreeItem>().Select(i => i.Image));
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
                else
                {
                    var image = args.NewValue as ImageTreeItem;
                    if (image != null) _appState.CurrentImage = image.Image;
                }
            }
        }
        private void OpenImage(object obj)
        {
            var image = (ImageTreeItem)obj;
            _navigationService.ShowImage(image.Image);
        }
        private void AppStateOnDbChanged(object sender, EventArgs eventArgs)
        {
            BuildTree();
            RequestRootDisplay();
        }

        public void BuildTree()
        {
            var root = new DirectoryItem(_settingsService.ImagesDirectory);
            root.AddRange(_contextProvider.GetDbContext().Images.AsEnumerable());
            Items.Clear();
            Items.Add(root);
        }
    }
}
