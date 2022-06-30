using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;

namespace SHSApp_Char
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            if (e.Args.Length > 0)
            {
                if (File.Exists(e.Args[0]))
                {
                    wnd.Zeki = File.ReadAllText(e.Args[0]);
                    //if (e.Args[0] != "ABU9SHSAPPCHAR.txt") File.Delete(e.Args[0]);
                    File.Delete(e.Args[0]);
                }
                wnd.Count = wnd.Zeki.Length / 9;
            }
            wnd.Show();
        }
    }


}
