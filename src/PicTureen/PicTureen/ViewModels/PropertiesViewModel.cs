using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows;
using PicTureen.Annotations;
using Database;
using PicTureen.Support;
namespace PicTureen.ViewModels
{
    class PropertiesViewModel : INotifyPropertyChanged
    {
        private string _path;
        private string _name;
        private int _width;
        private int _height;
        private int _rating;
        private string _description;
        private double _size;
        private DateTime _creationDate;
        private byte _colourDepth;
        private int _verticalDpi;
        private int _horizontalDpi;
        private ObservableCollection<Tag> _tags;
        public event PropertyChangedEventHandler PropertyChanged;
        private DelegateCommand _showMoreCommand;
        private ModelState _state;
        private bool _additionalDataLoaded = false;
        private Image _image;
        private DelegateCommand _saveCommand;

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
                OnPropertyChanged();
            }
        }

        public DelegateCommand ShowMoreCommand
        {
            get { return _showMoreCommand; }
            set
            {
                if (Equals(value, _showMoreCommand)) return;
                _showMoreCommand = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SaveCommand
        {
            get { return _saveCommand; }
            set
            {
                if (Equals(value, _saveCommand)) return;
                _saveCommand = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #region Properties
        public string Path
        {
            get { return _path; }
            set
            {
                if (value == _path) return;
                _path = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                _width = value;
                OnPropertyChanged();
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                _height = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Tag> Tags
        {
            get { return _tags; }
            set
            {
                if (Equals(value, _tags)) return;
                _tags = value;
                OnPropertyChanged();
            }
        }

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (value == _rating) return;
                _rating = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public double Size
        {
            get { return _size; }
            set
            {
                if (value.Equals(_size)) return;
                _size = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (value.Equals(_creationDate)) return;
                _creationDate = value;
                OnPropertyChanged();
            }
        }

        public string ColourDepth
        {
            get { return _colourDepth; }
            set
            {
                if (value == _colourDepth) return;
                _colourDepth = value;
                OnPropertyChanged();
            }
        }

        public float VerticalDpi
        {
            get { return _verticalDpi; }
            set
            {
                if (value == _verticalDpi) return;
                _verticalDpi = value;
                OnPropertyChanged();
            }
        }

        public float HorizontalDpi
        {
            get { return _horizontalDpi; }
            set
            {
                if (value == _horizontalDpi) return;
                _horizontalDpi = value;
                OnPropertyChanged();
            }
        }
        #endregion

        // Name, Resolution, Rating, Tags, Description, Size, Creation date, Colour depth, vert dpi, horiz dpi
        public PropertiesViewModel(Image image)
        {
            _image = image;
            Name = System.IO.Path.GetFileName(image.Path);
            Path = image.Path;
            Description = image.Description;
            Width = image.Width;
            Height = image.Height;
            Tags = new ObservableCollection<Tag>(image.Tags);
            Rating = image.Rating;
            ShowMoreCommand = new DelegateCommand(ShowMore);
            SaveCommand = new DelegateCommand(SaveImage);
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
            ColourDepth = im.PixelFormat.ToString();
            VerticalDpi = im.VerticalResolution;
            HorizontalDpi = im.HorizontalResolution;
            _additionalDataLoaded = true;
        }
    }
}
