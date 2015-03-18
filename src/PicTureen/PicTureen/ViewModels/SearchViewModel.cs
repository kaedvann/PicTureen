using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PicTureen.Annotations;
using PicTureen.Support;

namespace PicTureen.ViewModels
{
    class SearchViewModel : INotifyPropertyChanged
    {
        private string _searchString;
        private string _searchMinWidth;
        private string _searchMaxWidth;
        private string _searchMinHeight;
        private string _searchMaxHeight;
        private int _searchMinRating;
        private int _searchMaxRating;
        private DateTime _searchStartDate;
        private DateTime _searchEndDate;
        private int _searchMinSize;
        private int _searchMaxSize;
        private SelectionStatus _tagSelectionStatus;
        private SelectionStatus _nameSelectionStatus;
        private SelectionStatus _descriptionSelectionStatus;
        private bool _inSelectedFolder;
        private bool _isRegexp;
        private bool _showDimensions;
        private bool _showRating;
        private DelegateCommand _showHideDimensionsCommand;
        private DelegateCommand _showHideRatingCommand;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<String> _searchHistoryList; 

        public SearchViewModel()
        {
            _showDimensions = false;
            _showRating = false;
            _searchHistoryList = new List<string>();
            ShowRatingCommand = new DelegateCommand(ShowHideRating);
            ShowHideDimensionsCommand = new DelegateCommand(ShowHideDimensions);
        }

        public void Search()
        {
            if (!String.IsNullOrEmpty(_searchString))
            {
                _searchHistoryList.Add(_searchString);
            }
        }

        public DelegateCommand ShowHideDimensionsCommand
        {
            get { return _showHideDimensionsCommand; }
            set
            {
                if (Equals(value, _showHideDimensionsCommand)) return;
                _showHideDimensionsCommand = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand ShowRatingCommand
        {
            get { return _showHideRatingCommand; }
            set
            {
                if (Equals(value, _showHideRatingCommand)) return;
                _showHideRatingCommand = value;
                OnPropertyChanged();
            }
        }

        public void ShowHideDimensions()
        {
            _showDimensions = !_showDimensions;

        }

        public void ShowHideRating()
        {
            _showRating = !_showRating;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Included - search the SearchString in type; Excluded - don't search in type; Missing - type not specified for image
        /// </summary>
        public enum SelectionStatus
        {
            Included,
            Excluded,
            Missing
        }

        public bool ShowDimensions
        {
            get { return _showDimensions; }
            set
            {
                if (value.Equals(_showDimensions)) return;
                _showDimensions = value;
                OnPropertyChanged();
            }
        }

        public bool ShowRating
        {
            get { return _showRating; }
            set
            {
                if (value.Equals(_showRating)) return;
                _showRating = value;
                OnPropertyChanged();
            }
        }

        public SelectionStatus TagSelectionStatus
        {
            get { return _tagSelectionStatus; }
            set
            {
                if (value == _tagSelectionStatus) return;
                _tagSelectionStatus = value;
                OnPropertyChanged();
            }
        }

        public SelectionStatus NameSelectionStatus
        {
            get { return _nameSelectionStatus; }
            set
            {
                if (value == _nameSelectionStatus) return;
                _nameSelectionStatus = value;
                OnPropertyChanged();
            }
        }

        public SelectionStatus DescriptionSelectionStatus
        {
            get { return _descriptionSelectionStatus; }
            set
            {
                if (value == _descriptionSelectionStatus) return;
                _descriptionSelectionStatus = value;
                OnPropertyChanged();
            }
        }

        public bool InSelectedFolder
        {
            get { return _inSelectedFolder; }
            set
            {
                if (value.Equals(_inSelectedFolder)) return;
                _inSelectedFolder = value;
                OnPropertyChanged();
            }
        }

        public bool IsRegexp
        {
            get { return _isRegexp; }
            set
            {
                if (value.Equals(_isRegexp)) return;
                _isRegexp = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// String to search
        /// </summary>
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (value == _searchString) return;
                _searchString = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Minimal width of an image
        /// </summary>
        public string SearchMinWidth
        {
            get { return _searchMinWidth; }
            set
            {
                if (value == _searchMinWidth) return;
                _searchMinWidth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Maximal width of an image
        /// </summary>
        public string SearchMaxWidth
        {
            get { return _searchMaxWidth; }
            set
            {
                if (value == _searchMaxWidth) return;
                _searchMaxWidth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Minimal height of an image
        /// </summary>
        public string SearchMinHeight
        {
            get { return _searchMinHeight; }
            set
            {
                if (value == _searchMinHeight) return;
                _searchMinHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Maximal heigth of an image
        /// </summary>
        public string SearchMaxHeight
        {
            get { return _searchMaxHeight; }
            set
            {
                if (value == _searchMaxHeight) return;
                _searchMaxHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Minimal rating of an image
        /// </summary>
        public int SearchMinRating
        {
            get { return _searchMinRating; }
            set
            {
                if (value == _searchMinRating) return;
                _searchMinRating = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Maximal rating of image
        /// </summary>
        public int SearchMaxRating
        {
            get { return _searchMaxRating; }
            set
            {
                if (value == _searchMaxRating) return;
                _searchMaxRating = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Minimal size of of an image
        /// </summary>
        public int SearchMinSize
        {
            get { return _searchMinSize; }
            set
            {
                if (value == _searchMinSize) return;
                _searchMinSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Maximal size of an image
        /// </summary>
        public int SearchMaxSize
        {
            get { return _searchMaxSize; }
            set
            {
                if (value == _searchMaxSize) return;
                _searchMaxSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Earliest creation date of an image
        /// </summary>
        public DateTime SearchStartDate
        {
            get { return _searchStartDate; }
            set
            {
                if (value.Equals(_searchStartDate)) return;
                _searchStartDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Latest creation date of an image
        /// </summary>
        public DateTime SearchEndDate
        {
            get { return _searchEndDate; }
            set
            {
                if (value.Equals(_searchEndDate)) return;
                _searchEndDate = value;
                OnPropertyChanged();
            }
        }
    }
}
