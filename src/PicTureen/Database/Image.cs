using System;
using System.Collections.Generic;

namespace Database
{
    /// <summary>
    /// The class used for keeping info about image in database
    /// </summary>
    public class Image : IEquatable<Image>
    {
        public Image()
        {
// ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Tags = new HashSet<Tag>();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Image other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Path, other.Path) && Id == other.Id;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Image) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Path != null ? Path.GetHashCode() : 0)*397) ^ Id.GetHashCode();
            }
        }

        public long Id { get; set; }
        
        /// <summary>
        /// Path to the file with the image
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Image description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// User-provided image rating
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Image width in pixels
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Image height in pixels
        /// </summary>
        public int Height{ get; set; }

        /// <summary>
        /// Tags, associated with the image
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }
         
    }
}