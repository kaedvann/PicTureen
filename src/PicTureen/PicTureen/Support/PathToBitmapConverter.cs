﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PicTureen.Support
{
    public class PathToBitmapConverter : IValueConverter
    {
        public int PixelSize { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.DecodePixelWidth = PixelSize;
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(value.ToString());
            bi.EndInit();
            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}