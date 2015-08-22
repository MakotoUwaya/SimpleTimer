using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SimpleTimer
{
    public partial class SimpleBalloon : UserControl
    {
        private bool isClosing = false;

        public static readonly DependencyProperty BalloonTextProperty =
                DependencyProperty.Register("BalloonText",
                typeof(string),
                typeof(SimpleBalloon),
                new FrameworkPropertyMetadata(""));

        public static readonly DependencyProperty BalloonBodyProperty =
                DependencyProperty.Register("BalloonBody",
                typeof(string),
                typeof(SimpleBalloon),
                new FrameworkPropertyMetadata(""));

        public string BalloonText
        {
            get { return (string)GetValue(BalloonTextProperty); }
            set { SetValue(BalloonTextProperty, value); }
        }

        public string BalloonBody
        {
            get { return (string)GetValue(BalloonBodyProperty); }
            set { SetValue(BalloonBodyProperty, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SimpleBalloon()
        {
            InitializeComponent();
            TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
        }

        private void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            isClosing = true;
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }

        private void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isClosing) return;
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.ResetBalloonCloseTimer();
        }

        private void OnFadeOutCompleted(object sender, EventArgs e)
        {
            Popup pp = (Popup)Parent;
            pp.IsOpen = false;
        }
    }
}
