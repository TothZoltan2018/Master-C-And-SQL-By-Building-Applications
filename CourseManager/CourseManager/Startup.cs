using Caliburn.Micro;
using CourseManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManager
{
    //A Caliburn-nek kell ez az allomany
    //Nuget telepitesek: Dapper, Caliburn. 
    //A Caliburn-nek mappakat hoztunk letre: View, ViewModels, Models
    //Letoroljuk a MainWindow.xaml-t.
    class Startup : BootstrapperBase
    {
        public Startup()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //Majd ide tesszuk a ViewModel-unket. Itt indul a programunk.
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
