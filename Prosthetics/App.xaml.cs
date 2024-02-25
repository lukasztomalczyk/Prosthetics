﻿using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace Prosthetics
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           // AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
 

            MainPage = new MainPage();
        }

        private void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {

            Debug.WriteLine($"***** Handling Unhandled Exception *****: {e.Exception.Message}");
        }
    }
}
