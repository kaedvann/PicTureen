using System.Linq;
using Caliburn.PresentationFramework;
using PicTureen.Annotations;
using PicTureen.Services;
using PicTureen.Support;

namespace PicTureen.ViewModels.Search
{
    [UsedImplicitly]
    public class SearchViewModel : PropertyChangedBase
    {
        private readonly IAppState _appState;
        private readonly IContextProvider _contextProvider;
        private readonly SearchParameters _searchParameters = new SearchParameters();

        public SearchViewModel(IAppState appState, IContextProvider contextProvider)
        {
            _appState = appState;
            _contextProvider = contextProvider;
            SearchCommand = new DelegateCommand(Search);
        }

        public DelegateCommand SearchCommand { get; set; }
        public string SearchName { get; set; }
        public string SearchTags { get; set; }
        public string SearchDescription { get; set; }
        public string SearchMinRating { get; set; }
        public string SearchMaxRating { get; set; }
        public string SearchMinWidth { get; set; }
        public string SearchMinHeight { get; set; }
        public string SearchMaxWidth { get; set; }
        public string SearchMaxHeight { get; set; }

        public void Search()
        {
            UpdateParameters();
            if (_searchParameters.Valid)
            {
                var list = _contextProvider.GetDbContext().Images.ToList();
                var result = list.Where(i => _searchParameters.Check(i));
                _appState.RequestImagesDisplay(result);
            }
        }

        private void UpdateParameters()
        {
            _searchParameters.Name = SearchName;
            _searchParameters.Description = SearchDescription;
            _searchParameters.Tags = SearchTags;
            int parameter;
            _searchParameters.RatingMin = int.TryParse(SearchMinRating, out parameter) ? (int?) parameter : null;

            _searchParameters.RatingMax = int.TryParse(SearchMaxRating, out parameter) ? (int?) parameter : null;
            _searchParameters.HeightMin = int.TryParse(SearchMinHeight, out parameter) ? (int?) parameter : null;
            _searchParameters.HeightMax = int.TryParse(SearchMaxHeight, out parameter) ? (int?) parameter : null;
            _searchParameters.WidthMin = int.TryParse(SearchMinWidth, out parameter) ? (int?) parameter : null;
            _searchParameters.WidthMax = int.TryParse(SearchMaxWidth, out parameter) ? (int?) parameter : null;
        }
    }
}