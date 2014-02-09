using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace WpfApplication4
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            //log.Fatal("An unexpected application exception occurred", args.Exception);

            MessageBox.Show("An unexpected exception has occurred. Shutting down the application. Please check the log file for more details.");

            // Prevent default unhandled exception processing
            args.Handled = true;

            Environment.Exit(0);
        }
    }

}
