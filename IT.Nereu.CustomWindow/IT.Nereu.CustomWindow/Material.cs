using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace IT.Nereu.CustomWindow
{
    public partial class Material
    {

        #region Sizing Event Handlers
        private WindowInteropHelper GetHelper(object sender)
        {
            var element = (FrameworkElement)sender;
            if (element.TemplatedParent is Window window)
            {
                return new WindowInteropHelper(window);
            }

            return null;
        }

        void OnSize(object sender, SizingAction diraction)
        {
            WindowInteropHelper helper = GetHelper(sender);
            if (helper != null)
                DragSize(helper.Handle, diraction);

        }

        void OnSizeSouth(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.South);
        }

        void OnSizeNorth(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.North);
        }

        void OnSizeEast(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.East);
        }

        void OnSizeWest(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.West);
        }

        void OnSizeNorthWest(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.NorthWest);
        }

        void OnSizeNorthEast(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.NorthEast);
        }

        void OnSizeSouthEast(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.SouthEast);
        }

        void OnSizeSouthWest(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnSize(sender, SizingAction.SouthWest);
        }
        #endregion

        #region Interop Helper
        const int WM_SYSCOMMAND = 0x112;
        const int SC_SIZE = 0xF000;


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        void DragSize(IntPtr handle, SizingAction sizingAction)
        {
            if (System.Windows.Input.Mouse.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                SendMessage(handle, WM_SYSCOMMAND, (IntPtr)(SC_SIZE + sizingAction), IntPtr.Zero);
                SendMessage(handle, 514, IntPtr.Zero, IntPtr.Zero);
            }
        }
        #endregion

        #region Enum
        public enum SizingAction
        {
            West = 1,
            East = 2,
            North = 3,
            NorthWest = 4,
            NorthEast = 5,
            South = 6,
            SouthWest = 7,
            SouthEast = 8
        }
        #endregion
    }
}
