using Import.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.IO;
using System;
using StartImport = Import.Core;
using System.Linq;
using Domain;
using Microsoft.Extensions.Configuration;

namespace Import
{
    class Program
    {
        //private static IMDBDbContext _context;

        static void Main(string[] args)
        {
            string fileName = @"C:\Users\%USERPROFILE%\Dropbox\projects\CSHARP\IMDB\Import\Data\data.tsv";

            using (StreamReader s = new StreamReader(fileName))
            {
                BulkUploadToSql myData = BulkUploadToSql.Load(s);
                myData.Flush();
            }

        }




        
    }
}
