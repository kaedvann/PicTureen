using System.Collections.Generic;

namespace Database
{
    public class Image
    {
        public Image()
        {
            Tags = new HashSet<Tag>();
        }
        public long Id { get; set; }
        public string Path { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
         
    }
}