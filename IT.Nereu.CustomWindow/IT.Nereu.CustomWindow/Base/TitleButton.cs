using System.Windows;

namespace IT.Nereu.CustomWindow.Base
{
    public class TitleButton : System.Windows.Controls.Button
    {
        static TitleButton()
        {
            DefaultStyleKeyProperty
                .OverrideMetadata(typeof(TitleButton), new FrameworkPropertyMetadata(typeof(TitleButton)));
        }


        #region Properties
        public System.Windows.Controls.Canvas Icon
        {
            get { return (System.Windows.Controls.Canvas)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string MouseOverBackground
        {
            get { return (string)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public string PressedBackground
        {
            get { return (string)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty IconProperty =
           DependencyProperty.Register(
               "Icon", typeof(System.Windows.Controls.Canvas), typeof(TitleButton));

        public static readonly DependencyProperty MouseOverBackgroundProperty =
        DependencyProperty.Register(
            "MouseOverBackground", typeof(string), typeof(TitleButton));

        public static readonly DependencyProperty PressedBackgroundProperty =
        DependencyProperty.Register(
            "PressedBackground", typeof(string), typeof(TitleButton));
        #endregion
    }
}
