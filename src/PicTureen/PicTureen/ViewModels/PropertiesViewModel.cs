using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using Caliburn.PresentationFramework;
using Database;
using PicTureen.Annotations;
using PicTureen.Services;
using PicTureen.Support;

namespace PicTureen.ViewModels
{
    public class PropertiesViewModel : PropertyChangedBase
    {
        private readonly IAppState _appState;
        private readonly IContextProvider _contextProvider;
        private string _path;
        private string _name;
        private int _width;
        private int _height;
        private int _rating;
        private string _description;
        private double _size;
        private DateTime _creationDate;
        private PixelFormat _colourDepth;
        private float _verticalDpi;
        private float _horizontalDpi;
        private DelegateCommand _showMoreCommand;
        private ModelState _state;
        private bool _additionalDataLoaded = false;
        private Image _image;

        public enum ModelState
        {
            ShowingAll,
            ShowingLittle
        };

        public ModelState State
        {
            get { return _state; }
            set
            {
                if (value == _state) return;
                _state = value;
                NotifyOfPropertyChange(()=>State);
            }
        }

        public DelegateCommand ShowMoreCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        #region Properties
        public string Path
        {
            get { return _path; }
            set
            {
                if (value == _path) return;
                _path = value;
                NotifyOfPropertyChange(() => Path);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                NotifyOfPropertyChange(()=>Name);
            }
        }

        public int Width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                _height = value;
                NotifyOfPropertyChange(()=>Height);
            }
        }

        public BindableCollection<Tag> Tags { get; set; }

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (value == _rating) return;
                _rating = value;
                NotifyOfPropertyChange(()=>Rating);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                NotifyOfPropertyChange(()=>Description);
            }
        }

        public double Size
        {
            get { return _size; }
            set
            {
                if (value.Equals(_size)) return;
                _size = value;
                NotifyOfPropertyChange(()=>Size);
            }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (value.Equals(_creationDate)) return;
                _creationDate = value;
                NotifyOfPropertyChange(()=>CreationDate);
            }
        }

        public PixelFormat ColourDepth
        {
            get { return _colourDepth; }
            set
            {
                if (value == _colourDepth) return;
                _colourDepth = value;
                NotifyOfPropertyChange(()=>ColourDepth);
            }
        }

        public float VerticalDpi
        {
            get { return _verticalDpi; }
            set
            {
                _verticalDpi = value;
                NotifyOfPropertyChange(()=>VerticalDpi);
            }
        }

        public float HorizontalDpi
        {
            get { return _horizontalDpi; }
            set
            {
                _horizontalDpi = value;
                NotifyOfPropertyChange(() => HorizontalDpi);
            }
        }
        #endregion

        // Name, Resolution, Rating, Tags, Description, Size, Creation date, Colour depth, vert dpi, horiz dpi
        public PropertiesViewModel(IAppState appState, IContextProvider contextProvider)
        {
            Tags = new BindableCollection<Tag>();
            _appState = appState;
            _contextProvider = contextProvider;

            ShowMoreCommand = new DelegateCommand(ShowMore);
            SaveCommand = new DelegateCommand(SaveImage);

            _appState.CurrentImageChanged += AppStateOnCurrentImageChanged;
        }

        private void AppStateOnCurrentImageChanged(object sender, EventArgs eventArgs)
        {
            Image = _appState.CurrentImage;
        }

        public Image Image
        {
            get { return _image; }
            set
            {
                if (value != null)
                    _image = value;
                else
                {
                    _image = new Image();
                } 
                Name = System.IO.Path.GetFileName(_image.Path);
                Path = _image.Path;
                Description = _image.Description;
                Width = _image.Width;
                Height = _image.Height;
                Tags.Clear();
                Tags.AddRange(_image.Tags);
                Rating = _image.Rating;
                NotifyOfPropertyChange(()=>Image);
            }
        }


        // name, rating, tags, description
        private void SaveImage()
        {
            throw new NotImplementedException();
        }


        private void ShowMore()
        {
            if (_state == ModelState.ShowingAll)
            {
                _state = ModelState.ShowingLittle;
            }
            else
            {
                if (!_additionalDataLoaded)
                    LoadAdditionalData();
                _state = ModelState.ShowingAll;
            }
        }

        // Size, Creation date, Colour depth, vert dpi, horiz dpi
        private void LoadAdditionalData()
        {
            FileInfo inf = new FileInfo(Path);
            Size = inf.Length >> 10;
            CreationDate = inf.CreationTime;
            System.Drawing.Image im = System.Drawing.Image.FromFile(Path);
            ColourDepth = im.PixelFormat;
            VerticalDpi = im.VerticalResolution;
            HorizontalDpi = im.HorizontalResolution;
            _additionalDataLoaded = true;
        }
    }
}
