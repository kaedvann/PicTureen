using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;

namespace PicTureen.ViewModels.Search
{
    public class SearchParameters
    {
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public int? RatingMin { get; set; }
        public int? RatingMax { get; set; }
        public int? WidthMin { get; set; }
        public int? WidthMax { get; set; }
        public int? HeightMin { get; set; }
        public int? HeightMax { get; set; }

        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Tags) || !string.IsNullOrEmpty(Description) ||
                       RatingMax != null || RatingMin != null || WidthMin != null || WidthMax != null ||
                       HeightMax != null ||
                       HeightMin != null;
            }
        }

        public bool Check(Image image)
        {
            return CheckName(image.Path) && CheckTags(image.Tags) && CheckDescription(image.Description) && CheckRating(image.Rating) && CheckWidth(image.Width) && CheckHeight(image.Height);
        }

        private bool CheckWidth(int width)
        {
            bool result = true;
            if (WidthMin.HasValue)
                result &= WidthMin.Value < width;
            if (WidthMax.HasValue)
                result &= WidthMax.Value > width;
            return result;
        }

        private bool CheckHeight(int height)
        {
            bool result = true;
            if (HeightMin.HasValue)
                result &= HeightMin.Value < height;
            if (HeightMax.HasValue)
                result &= HeightMax.Value > height;
            return result;
        }

        private bool CheckRating(int rating)
        {
            bool result = true;
            if (RatingMin.HasValue)
                result &= RatingMin.Value < rating;
            if (RatingMax.HasValue)
                result &= RatingMax.Value > rating;
            return result;
        }

        private bool CheckDescription(string description)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(description))
                return true;
            return description.Contains(Description);
        }

        private bool CheckTags(ICollection<Tag> tags)
        {

            var splitTags = Tags.Split(';').Select(t => t.Trim().ToLower()).Where(s => !string.IsNullOrWhiteSpace(s));
            return splitTags.All(tag => tags.Any(t => t.Name == tag));
        }

        private bool CheckName(string path)
        {
            if (string.IsNullOrEmpty(Name))
                return true;
            var fileName = Path.GetFileName(path);
            return fileName != null && fileName.Contains(Name);
        }
    }
}