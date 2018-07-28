using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace IT.Nereu.CustomWindow.Base
{
    public class TitleBar : System.Windows.Controls.Control
    {
        public static readonly double BUTTON_WIDTH  = 50;
        public static readonly double BUTTON_HEIGHT = 25;
        public static readonly GridLength GRID_WIDTH = new GridLength(BUTTON_WIDTH);

        private const string CLOSE_ICON    = "<Canvas xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Width=\"9\" Height=\"9\"><Path Stroke=\"White\" Width=\"9\" Height=\"9\" Stretch=\"Fill\" StrokeThickness=\"1\"><Path.Data><PathGeometry Figures=\"M9 0l-9 9m0 -9l9 9\"/></Path.Data></Path></Canvas>";
        private const string RESTORE_ICON  = "<Canvas xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Width=\"9\" Height=\"9\"><Path Stroke=\"White\" Width=\"9\" Height=\"9\" Stretch=\"Fill\" StrokeThickness=\"1\"><Path.Data><PathGeometry Figures=\"M0 2l6 0 0 7 -6 0 0 -7zm3 -2l6 0 0 7 -3 0 0 -5 -3 0 0 -2z\"/></Path.Data></Path></Canvas>";
        private const string MINIMIZE_ICON = "<Canvas xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Width=\"9\" Height=\"2\"><Line Stroke=\"White\" StrokeThickness=\"1\" X1=\"0\" X2=\"9\"/></Canvas>";
        private const string MAXIMIZE_ICON = "<Canvas xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Width=\"9\" Height=\"9\"><Polygon Stroke=\"White\" Width=\"9\" Height=\"9\" Stretch=\"Fill\" StrokeThickness=\"1\" Points=\"0,0 9,0 9,9 0,9\"/></Canvas>";

        TitleButton closeButton;
        TitleButton maximizeButton;
        TitleButton minimizeButton;

        public TitleBar()
        {
            MouseLeftButtonDown += new MouseButtonEventHandler(OnTitleBarLeftButtonDown);
            MouseDoubleClick    += new MouseButtonEventHandler(TitleBar_MouseDoubleClick);
            Loaded              += new RoutedEventHandler(TitleBar_Loaded);
        }

        static TitleBar()
        {
            DefaultStyleKeyProperty
                .OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
        }

        void TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxButton_Click(sender, e);
        }

        private Canvas GetCanvas(string xaml)
        {
            return (Canvas) XamlReader.Load(
                XmlReader.Create(
                    new StringReader(xaml)));
        }

        void TitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            closeButton     = (TitleButton)Template.FindName("CloseButton", this);
            minimizeButton  = (TitleButton)Template.FindName("MinButton", this);
            maximizeButton  = (TitleButton)Template.FindName("MaxButton", this);

            closeButton.Icon    = GetCanvas(CLOSE_ICON);
            minimizeButton.Icon = GetCanvas(MINIMIZE_ICON);
            maximizeButton.Icon = GetCanvas(MAXIMIZE_ICON);

            closeButton.Click       += new RoutedEventHandler(CloseButton_Click);
            minimizeButton.Click    += new RoutedEventHandler(MinButton_Click);
            maximizeButton.Click    += new RoutedEventHandler(MaxButton_Click);
        }

        #region Event Handlers
        void OnTitleBarLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (TemplatedParent is Window window)
                window.DragMove();
        }

        void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (TemplatedParent is Window window)
                window.Close();
        }

        void MinButton_Click(object sender, RoutedEventArgs e)
        {
            if (TemplatedParent is Window window)
                window.WindowState = WindowState.Minimized;
        }

        void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            if (TemplatedParent is Window window)
            {
                window.WindowState =
                    (window.WindowState == WindowState.Maximized)
                        ? WindowState.Normal
                        : WindowState.Maximized;

                maximizeButton.Icon =
                    (window.WindowState == WindowState.Maximized)
                        ? GetCanvas(RESTORE_ICON)
                        : GetCanvas(MAXIMIZE_ICON);
            }
        }

        #endregion
    }
}
