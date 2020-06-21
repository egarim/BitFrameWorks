using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;


namespace Brevitas.AppFramework.DataAccess.EF
{
    public class EFContext : DbContext
    {
        private const string databaseName = "database.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databasePath = "";
            //switch (Device.RuntimePlatform)
            //{
            //    case Device.iOS:
            //        Batteries_V2.Init();
            //        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..",
            //            "Library", databaseName);
            //        ;
            //        break;

            //    case Device.Android:
            //        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            //            databaseName);
            //        break;

            //    default:
            //        throw new NotImplementedException("Platform not supported");
            //}

            // Specify that we will use sqlite and the path of the database here
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}