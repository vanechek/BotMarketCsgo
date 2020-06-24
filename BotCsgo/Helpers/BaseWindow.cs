using BotCsgo.Model;
using System.Windows;

namespace BotCsgo.Helpers
{
    public abstract class BaseWindow
    {
        public double WindowMinHeight { get; set; } = 270;
        public double WindowMinWidth { get; set; } = 1070;

        public int cornerRadius { get; set; } = 10;

        public int CornerRadius
        {
            get { return mWindow.WindowState == WindowState.Maximized ? 0 : cornerRadius; }
            set { cornerRadius = value; }
        }
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(CornerRadius); } }

        public double WindowCaptionHeight { get; set; } = 50;

        private int ResizeBorderThickness { get; set; } = 6;
        public Thickness WindowResizeBorderThickness { get { return new Thickness(ResizeBorderThickness); } }

        public int windowPaddingSize = 10;

        public int titleHeightLenght = 50;
        public GridLength TitleHeightLenght { get { return new GridLength(titleHeightLenght); } }

        private Window mWindow;
    }
}
