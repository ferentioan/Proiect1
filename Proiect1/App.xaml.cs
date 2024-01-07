using System;
using Proiect1.Data;
using System.IO;

namespace Proiect1
{
    public partial class App : Application
    {
        static ProduseListDatabase? database;
        public static ProduseListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   ProduseListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "Produse.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
