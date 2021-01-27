<Query Kind="Expression">
  <Connection>
    <ID>f5df9793-87f6-42ab-90ff-59cef7c51e51</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
  <Output>DataGrids</Output>
</Query>

//commenting: CTRL + KC
//uncommenting: CTRL + KU
//or to comment and uncomment CTRL + /

//Method Syntax
//or use .Select(AnyRowAtAnyPointInTime => AnyRowAtAnyPointInTime)
Albums
	.Select(x => x),
	
//Query Syntax
from x in Albums
select x

//filtering
//where clause in query syntax
//,Where() method in method syntax

//Find all albums released in 1990
//query syntax
from x in Albums
where x.ReleaseYear == 1990
select x

//method syntax
Albums
	.Where(x => x.ReleaseYear == 1990)
	.Select(x => x)

//Find all albums released in 1970's
//query syntax
from x in Albums
where x.ReleaseYear >= 1970 && x.ReleaseYear <= 1979
select x

//method syntax
Albums
	.Where(x => x.ReleaseYear >= 1970 && x.ReleaseYear <= 1979)
	.Select(x => x)
	
//ordering
//List all albums by ascending year of release

//query syntax
from x in Albums
orderby x.ReleaseYear
select x

//method syntax
Albums
	.OrderBy(x => x.ReleaseYear)
	.Select(x => x)

//List all albums by ascending year of release and in alphabetical order
//query syntax
from x in Albums
orderby x.ReleaseYear, x.Title
select x

//method syntax
Albums
	.OrderBy(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Select(x => x)
	
	//List all albums by descending year of release and in alphabetical order
//query syntax
from x in Albums
orderby x.ReleaseYear descending, x.Title
select x

//method syntax
Albums
	.OrderByDescending(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Select(x => x)
	
//What about only certain fields (partial entity records or fields from anothere table)
//List all records from 1970's showing the title, artist name, and year (and order by title and year)

//query syntax
from x in Albums
where x.ReleaseYear >= 1970 && x.ReleaseYear <= 1979
orderby x.ReleaseYear, x.Title
select new
{
	Title = x.Title,
	Artist = x.Artist.Name,
	Year = x.ReleaseYear
}

//method syntax
Albums
	.Where(x => x.ReleaseYear >= 1970 && x.ReleaseYear <= 1979)
	.OrderBy(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Select(x => new
				{
					Title = x.Title,
					Artist = x.Artist.Name,
					Year = x.ReleaseYear
				})
