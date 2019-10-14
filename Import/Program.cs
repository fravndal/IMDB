using Import.Core;
using System.IO;
using System;

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
