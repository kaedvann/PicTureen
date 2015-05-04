using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PicTureen.ViewModels;

namespace PicTureen.Views
{
    /// <summary>
    /// Interaction logic for FullImageView.xaml
    /// </summary>
    public partial class FullImageView : Window
    {
        public FullImageView()
        {
            InitializeComponent();
        }

        private bool isMoving;
        private double dX, dY;
        private Point _startPoint;
        private void DispImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition((IInputElement) sender);
            isMoving = true;
        }

        private void DispImage_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving && e.LeftButton == MouseButtonState.Pressed)
            {
                Point newPoint = e.MouseDevice.GetPosition((IInputElement) sender);
                if (Math.Abs(newPoint.X - _startPoint.X) >= 1 || Math.Abs(newPoint.Y - _startPoint.Y) >= 1)
                {
                    TranslateTransform.X = newPoint.X - _startPoint.X;
                    TranslateTransform.Y = newPoint.Y - _startPoint.Y;
                    _startPoint = newPoint;
                }
            }

        }

        private void DispImage_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMoving = false;
        }
    }
}
