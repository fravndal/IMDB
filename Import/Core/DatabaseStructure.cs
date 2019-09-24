using System.Data;

namespace Import.Core
{
    public static class DatabaseStructure
    {
        public static void MovieStructure(this DataTable dataTable)
        {
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("ExternalId", typeof(string));
            dataTable.Columns.Add("Type", typeof(string));
            dataTable.Columns.Add("PrimaryTitle", typeof(string));
            dataTable.Columns.Add("OriginalTitle", typeof(string));
            dataTable.Columns.Add("IsAdult", typeof(string));
            dataTable.Columns.Add("StartYear", typeof(int));
            dataTable.Columns.Add("EndYear", typeof(int));
            dataTable.Columns.Add("RuntimeMinutes", typeof(int));
        }

        public static void GenreStructure(this DataTable dataTable)
        {
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
        }

        public static void MovieGenreStructure(this DataTable dataTable)
        {
            dataTable.Columns.Add("MovieId", typeof(int));
            dataTable.Columns.Add("GenreId", typeof(int));
        }
    }
}
