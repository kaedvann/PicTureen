using System;
using System.Linq;
using Caliburn.PresentationFramework;
using Interfaces;
using PicTureen.Models.Tree;
using PicTureen.Services;

namespace PicTureen.ViewModels
{
    public class TreeViewModel: PropertyChangedBase
    {
        private readonly IAppState _appState;
        private readonly IContextProvider _contextProvider;
        private readonly ISettingsService _settingsService;

        public BindableCollection<TreeItem> Items { get; set; } 
        public TreeViewModel(IAppState appState, IContextProvider contextProvider, ISettingsService settingsService)
        {
            _appState = appState;
            _contextProvider = contextProvider;
            _settingsService = settingsService;
            BuildTree();

            _appState.DbChanged += AppStateOnDbChanged;
        }

        private void AppStateOnDbChanged(object sender, EventArgs eventArgs)
        {
            BuildTree();
        }

        public void BuildTree()
        {
            var root = new DirectoryItem(_settingsService.ImagesDirectory);
            Items = new BindableCollection<TreeItem>();
            Items.Add(root);
            root.AddRange(_contextProvider.GetDbContext().Images.AsEnumerable());
        }
    }
}
