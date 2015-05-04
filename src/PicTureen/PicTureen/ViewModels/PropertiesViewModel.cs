using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.PresentationFramework;
using Database;
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
        private string _rating;
        private string _description;
        private double _size;
        private DateTime _creationDate;
        private PixelFormat _colourDepth;
        private float _verticalDpi;
        private float _horizontalDpi;
        private Image _image = new Image();
        private string _tags;
        private bool _isChanged;

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

        public string Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                NotifyOfPropertyChange(()=>Tags);
                IsChanged = true;
            }
        }

        public string Rating
        {
            get { return _rating; }
            set
            {
                if (value == _rating) return;
                _rating = value;
                IsChanged = true;
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
                IsChanged = true;
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
            _appState = appState;
            _contextProvider = contextProvider;
            SaveCommand = new DelegateCommand(SaveImage, () => IsChanged);

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
                _image = value ?? new Image();
                Name = System.IO.Path.GetFileName(_image.Path);
                Path = _image.Path;
                Description = _image.Description;
                Width = _image.Width;
                Height = _image.Height;
                Rating = _image.Rating.ToString();
                Tags = TagsToString(_image.Tags);
                if (!string.IsNullOrEmpty(_image.Path))
                    LoadAdditionalData();
                IsChanged = false;
                NotifyOfPropertyChange(()=>Image);
                NotifyOfPropertyChange(() => IsImageSelected);
            }
        }

        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value; 
                NotifyOfPropertyChange(()=>IsChanged);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string TagsToString(IEnumerable<Tag> tags )
        {
            return tags.Aggregate("", (s, tag) => s += tag.Name + "; ");
        }

        public bool IsImageSelected
        {
            get { return !string.IsNullOrEmpty(Image.Path); }
        }


        // name, rating, tags, description
        private void SaveImage()
        {
            try
            {
                int rating = int.Parse(Rating);
                if (rating < 0 || rating > 5)
                    throw new ArgumentException();
                var tags = StringToTags(Tags);
                _image.Tags.Clear();
                foreach (var tag in tags)
                {
                    _image.Tags.Add(tag);
                }
                _image.Description = Description;
                _image.Rating = rating;
                _contextProvider.GetDbContext().SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect parameters");
            }
        }

        private IEnumerable<Tag> StringToTags(string tags)
        {
            var context = _contextProvider.GetDbContext();
            var splitTags = tags.Split(';').Select(t => t.Trim().ToLower()).Where(s => !string.IsNullOrWhiteSpace(s));
            var result = new List<Tag>();

                foreach (var splitTag in splitTags)
                {
                    var dbTag =
                        context.Tags.FirstOrDefault(
                            t => t.Name== splitTag);
                    if (dbTag == null)
                    {
                        dbTag = context.Tags.Create();
                        dbTag.Name = splitTag;
                    }
                    result.Add(dbTag);
                }
      
            return result;
        }


        private void LoadAdditionalData()
        {
            FileInfo inf = new FileInfo(Path);
            Size = inf.Length >> 10;
            CreationDate = inf.CreationTime;
            System.Drawing.Image im = System.Drawing.Image.FromFile(Path);
            ColourDepth = im.PixelFormat;
            VerticalDpi = im.VerticalResolution;
            HorizontalDpi = im.HorizontalResolution;
        }
    }
}
