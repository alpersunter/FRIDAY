using System;
using System.Windows;
using System.Windows.Forms;
using NonInvasiveKeyboardHookLibrary;

namespace FRIDAY
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        KeyboardHookManager keyboardHookManager;
        bool minimized = false;
        bool tekAtis = false;
        public MainWindow()
        {
            // System.Windows.Forms.Screen 
            var screenWidth = Screen.PrimaryScreen.Bounds.Width;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height;
            //Width = screenWidth / 3;
            //Height = screenHeight / 1.73;
            
            InitializeComponent();
            //mainGrid.Width = Width;
            //mainGrid.Height = Height;
            Left = (screenWidth - Width) / 2;
            Top = (screenHeight - Height) / 3;

            // http://blog.walterlv.com/post/create-blur-background-window-en.html
            Walterlv.Demo.Interop.WindowBlur.SetIsEnabled(this, true);

            // suggestqueries.google.com/complete/search?client=firefox&q=facbook


            // https://stackoverflow.com/questions/1556182/finding-the-handle-to-a-wpf-window

            // https://github.com/kfirprods/NonInvasiveKeyboardHook/
            // https://www.codeproject.com/Articles/1273010/Global-Hotkeys-within-Desktop-Applications

            keyboardHookManager = new KeyboardHookManager();
            keyboardHookManager.Start();
            keyboardHookManager.RegisterHotkey(NonInvasiveKeyboardHookLibrary.ModifierKeys.Alt, 0x31, () =>
            {
                //System.Windows.MessageBox.Show("Alt+1 detected");

                Dispatcher.Invoke(() => {
                    if (minimized)
                    {
                        minimized = false;
                        this.WindowState = WindowState.Normal;
                        queryBox.Focus();
                        tekAtis = false;
                    }
                    else
                    {
                        minimized = true;
                        this.WindowState = WindowState.Minimized;
                    }
                });
                
            });
            

        }

        private void fridayWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            keyboardHookManager.UnregisterAll();
        }



        private void fridayWindow_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            Dispatcher.Invoke(() => {
                queryBox.Text = "";
                this.WindowState = WindowState.Minimized;
                minimized = true;
            });
        }

        private void queryBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
           
                if (e.Key == System.Windows.Input.Key.Enter && tekAtis==false)
                {
                    // System.Windows.MessageBox.Show("Yow");
                    System.Diagnostics.Process.Start("https://tureng.com/tr/turkce-ingilizce/" + queryBox.Text);
                    tekAtis = true;
                }
            
            
        }
    }
}
