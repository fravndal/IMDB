using Domain;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Import.Core
{
    public class BulkUploadToSql
    {
        private List<Movie> internalMovieList;
        public static List<Genre> internalGenreList;
        public static List<MovieGenre> internalMovieGenreList;
        protected string movieTable;
        protected string genreTable;
        protected string movieGenreTable;
        protected DataTable dataTableMovie = new DataTable();
        protected DataTable dataTableGenre = new DataTable();
        protected DataTable dataTableMovieGenre = new DataTable();
        protected int recordCount;
        protected int commitBatchSize;

        private BulkUploadToSql(string movieTable, string genreTable, string movieGenreTable, int commitBatchSize)
        {
            internalMovieList = new List<Movie>();
            internalGenreList = new List<Genre>();
            internalMovieGenreList = new List<MovieGenre>();
            this.movieTable = movieTable;
            this.genreTable = genreTable;
            this.movieGenreTable = movieGenreTable;
            this.dataTableMovie = new DataTable(movieTable);
            this.dataTableGenre = new DataTable(genreTable);
            this.dataTableMovieGenre = new DataTable(movieGenreTable);
            this.recordCount = 0;
            this.commitBatchSize = commitBatchSize;
            // add columns to this data table
            InitializeStructures();
        }

        // Set how many rows to insert at a time
        private BulkUploadToSql() : this("Movies", "Genres", "MovieGenres", 50000)
        { }
        

        private void InitializeStructures()
        {
            this.dataTableMovie.MovieStructure();
            this.dataTableGenre.GenreStructure();
            this.dataTableMovieGenre.MovieGenreStructure();
        }

        

        public static BulkUploadToSql Load(StreamReader dataSource)
        {
            // create a new object to return
            BulkUploadToSql o = new BulkUploadToSql();

            for (int i = 0; !dataSource.EndOfStream; i++)
            {
                var line = dataSource.ReadLine();
                if (i < 1) continue;

                var movie = line.ParseMovie(i);
                o.internalMovieList.Add(movie);

                var genreIds = line.GenreExistInInternalList(internalGenreList);
                foreach(var genreId in genreIds)
                {
                    internalMovieGenreList.Add(i.ParseMovieGenre(genreId));
                }
            }
            return o;
        }

        


        private void PopulateMoviesTable(Movie record)
        {
            DataRow row;
            row = this.dataTableMovie.NewRow();
            row[1] = record.ExternalId;
            row[2] = record.Type;
            row[3] = record.PrimaryTitle;
            row[4] = record.OriginalTitle;
            row[5] = record.IsAdult;
            row[6] = record.StartYear;
            row[7] = record.EndYear;
            row[8] = record.RuntimeMinutes;
            this.dataTableMovie.Rows.Add(row);
            this.recordCount++;
        }

        private void PopulateGenresTable(Genre record)
        {
            DataRow row;
            row = this.dataTableGenre.NewRow();
            row[1] = record.Name;
            this.dataTableGenre.Rows.Add(row);
            this.recordCount++;
        }

        private void PopulateMovieGenresTable(MovieGenre record)
        {
            DataRow row;
            row = this.dataTableMovieGenre.NewRow();
            row[0] = record.MovieId;
            row[1] = record.GenreId;
            this.dataTableMovieGenre.Rows.Add(row);
            this.recordCount++;
        }

        public void Flush()
        {
            // transfer data to the datatable
            foreach (Movie rec in this.internalMovieList)
            {
                this.PopulateMoviesTable(rec);
                if (this.recordCount >= this.commitBatchSize)
                    this.WriteToDatabase();
            }
            foreach (Genre rec in internalGenreList)
            {
                this.PopulateGenresTable(rec);
                if (this.recordCount >= this.commitBatchSize)
                    this.WriteToDatabase();
            }
            foreach (MovieGenre rec in internalMovieGenreList)
            {
                this.PopulateMovieGenresTable(rec);
                if (this.recordCount >= this.commitBatchSize)
                    this.WriteToDatabase();
            }
            // write remaining records to the DB
            if (this.recordCount > 0)
                this.WriteToDatabase();
        }

        private void WriteToDatabase()
        {
            string connString = GetDbString();

            
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                SqlBulkCopy bulkCopy2 = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                SqlBulkCopy bulkCopy3 = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);

                bulkCopy.DestinationTableName = this.movieTable;
                bulkCopy2.DestinationTableName = this.genreTable;
                bulkCopy3.DestinationTableName = this.movieGenreTable;

                connection.Open();
                bulkCopy.WriteToServer(dataTableMovie);
                bulkCopy2.WriteToServer(dataTableGenre);
                bulkCopy3.WriteToServer(dataTableMovieGenre);
                connection.Close();
            }
            // reset
            this.dataTableMovie.Clear();
            this.dataTableGenre.Clear();
            this.dataTableMovieGenre.Clear();

            this.recordCount = 0;
        }





        private string GetDbString()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddUserSecrets<Program>()
               .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var mySettingsConfig = new MySettingsConfig();
            configuration.GetSection("MySettings").Bind(mySettingsConfig);

            return configuration.GetConnectionString("IMDBDb");
        }
    }
}
