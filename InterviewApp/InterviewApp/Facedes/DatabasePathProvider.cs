using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using DamatMobile.Core.Abstractions;
using SQLitePCL;
using Xamarin.Forms;

namespace InterviewApp
{
    [ExcludeFromCodeCoverage]
    public class DatabasePathProvider : IDatabasePathProvider
    {
        private const string DataBaseFileName = "database.db";

        public string GetDatabasePath()
        {
            string directoryPath;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Batteries_V2.Init();
                    directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..",
                        "Library");
                    break;
                case Device.Android:
                    directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    break;

                default:
                    directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    break;
            }

            var dataBasePath = Path.Combine(directoryPath, DataBaseFileName);
            return dataBasePath;
        }
    }
}