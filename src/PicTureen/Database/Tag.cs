using System.Collections.Generic;

namespace Database
{
    public class Tag
    {
        public Tag()
        {
            Images = new HashSet<Image>();
        }
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Image> Images { get; set; } 
    }
}