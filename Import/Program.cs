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
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;
            string fileName = "data.tsv";
            string pathToFilename = path + @"\Data\" + fileName;

            using (StreamReader s = new StreamReader(pathToFilename))
            {
                BulkUploadToSql myData = BulkUploadToSql.Load(s);
                myData.Flush();
            }
        }
    }
}
