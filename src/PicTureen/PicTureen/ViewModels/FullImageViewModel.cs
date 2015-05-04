using System.Diagnostics;
using System.Windows.Input;
using Caliburn.PresentationFramework;
using Database;
using PicTureen.Support;
using Xceed.Wpf.Toolkit.Zoombox;

namespace PicTureen.ViewModels
{
    public class FullImageViewModel: PropertyChangedBase
    {
        private double _rotateAngle;
        private double _scaleFactor;
        private double _moveX;
        private double _moveY;
        public Image Image { get; set; }
        public DelegateCommand RotateLeftCommand { get; set; }
        public DelegateCommand RotateRightCommand { get; set; }
        public DelegateCommand NextImageCommand { get; set; }
        public DelegateCommand PrevImageCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand WheelUpCommand { get; set; }
        public DelegateCommand WheelDownCommand { get; set; }
        public double MoveX
        {
            get { return _moveX; }
            set
            {
                _moveX = value; 
                NotifyOfPropertyChange(()=> MoveX);
            }
        }

        public double MoveY
        {
            get { return _moveY; }
            set
            {
                _moveY = value;  
                NotifyOfPropertyChange(()=>MoveY);
            }
        }

        public double ScaleFactor
        {
            get { return _scaleFactor; }
            set
            {
                _scaleFactor = value;
                NotifyOfPropertyChange(() => ScaleFactor);
            }
        }

        public double RotateAngle
        {
            get { return _rotateAngle; }
            set
            {
                _rotateAngle = value;
                NotifyOfPropertyChange(()=>RotateAngle);
            }
        }

        public DelegateCommand MoveCommand { get; set; }

        public FullImageViewModel()
        {
            FitCommand = new DelegateCommand(Fit);
            RotateLeftCommand = new DelegateCommand(RotateLeft);
            RotateRightCommand = new DelegateCommand(RotateRight);
            NextImageCommand = new DelegateCommand(NextImage);
            PrevImageCommand = new DelegateCommand(PrevImage);
            DeleteCommand = new DelegateCommand(DeleteImage);
            WheelUpCommand = new DelegateCommand(ScaleUp);
            WheelDownCommand = new DelegateCommand(ScaleDown);
            MoveCommand = new DelegateCommand(Move);
            RotateAngle = 0;
            ScaleFactor = 1;
        }

        private void Fit(object obj)
        {
            (obj as Zoombox).FitToBounds();
        }

        public DelegateCommand MouseDownCommand { get; set; }

        public DelegateCommand FitCommand { get; set; }

        private void Move(object obj)
        {
            Debugger.Break();
            MouseEventArgs args = (MouseEventArgs) obj;
            if (args.LeftButton == MouseButtonState.Pressed)
            {
              //  MoveX += args.MouseDevice.GetPosition();
            }
        }

        private void ScaleUp()
        {
            ScaleFactor += 0.1;
        }

        private void ScaleDown()
        {
            ScaleFactor -= 0.1;
        }

        private void DeleteImage()
        {
            throw new System.NotImplementedException();
        }

        private void PrevImage()
        {
            throw new System.NotImplementedException();
        }

        private void NextImage()
        {
            throw new System.NotImplementedException();
        }

        private void RotateRight()
        {
            RotateAngle -= 90;
        }

        private void Rotate(double angle)
        {
            
        }
        private void RotateLeft()
        {
            RotateAngle += 90;
        }
    }
}
