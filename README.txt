## This is a project to import large dataset from IMDB.

You can get the files from [IMDB](https://datasets.imdbws.com/)
!NB The only working file is `title.basics.tsv.gz`

## Installation
1.  Move `data.tsv` to `Import/Data/` and set property `Copy to Output Directory` to `Copy if newer`

2.  Create a migration from Persistence: 
`dotnet ef migrations add initialcreate`

3.  Update migration:
`dotnet ef database update`

