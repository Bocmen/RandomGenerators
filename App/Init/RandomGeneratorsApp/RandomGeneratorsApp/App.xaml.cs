﻿using System.Reflection;
using WorkInvoker.Pages;
using Xamarin.Forms;

namespace RandomGeneratorsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            WorkInvoker.WorksLoader.AppendWorks(Assembly.GetAssembly(typeof(WorksRandomGenerator.Const)));
            SettingPage.ApplayThemes();
            MainPage = WorkInvoker.Pages.MainPage.CreateRootPage(new DefaultViewWorks()
            {
                Title = DefaultViewWorks.DefaultTitlePage
            }, new SettingPage()
            {
                Title = SettingPage.DefaultTitlePage
            });
        }
    }
}
