using System.Collections.Generic;
using Database;

namespace PicTureen.EventArguments
{
    public class ImagesEventArgs: System.EventArgs
    {
        public IEnumerable<Image> Images { get; private set; }
        public ImagesEventArgs(params Image[] images )
        {
            Images = images;
        }
    }
}
