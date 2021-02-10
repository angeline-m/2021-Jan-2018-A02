<Query Kind="Statements">
  <Connection>
    <ID>f5df9793-87f6-42ab-90ff-59cef7c51e51</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Grouping
//a) by column				groupname.Key
//b) by multiple columns	groupname.Key.attribute
//c) by an entity			groupname.Key.attribute

//groups have 2 components
//a) key component (group by); reference this component groupname.Key[.attribute]
//b) data (instances in the group)

//process
//start with a pile of data
//specify the grouping attribute(s)
//result is smaller piles of data determined by the attributes which can be reported upon

//display albums by ReleaseYear
//order by
var resultsorderby = from x in Albums
						orderby x.ReleaseYear
						select x;
resultsorderby.Dump();

//group by ReleaseYear
var resultsgroupby = from x in Albums
						group x by x.ReleaseYear;
resultsgroupby.Dump();

//group by Artist name and album ReleaseYear
var resultsgroupbycolumns = from x in Albums
							group x by new {x.Artist.Name, x.ReleaseYear};
resultsgroupbycolumns.Dump();

//group tracks by their album
var resultsgroupbyentity = from x in Tracks
							group x by x.Album;
resultsgroupbyentity.Dump();

//IMPORTANT: if you wish to "report" on groups (AFTER the group by) you must save the grouping in a temporary dataset, then you must use the temporary dataset to report from

//for query syntax, your temporary dataset name is created by using  -> into gName

//for method syntax, your temporary dataset name is the placeholder of your Select -> .Select(gName => ...

//the temporary datasets are created in memory and once the query is completed, the temporary datasets no longer exist

//group by ReleaseYear
var resultsgroupbyReport = from x in Albums
							group x by x.ReleaseYear into gAlbumYear
							select new
							{
								KeyValue = gAlbumYear.Key,
								numberofAlbums = gAlbumYear.Count(),
								albumandartist = from y in gAlbumYear
												select new
												{
													Title = y.Title,
													Artist = y.Artist.Name
												}
							};
resultsgroupbyReport.Dump();

//group by an entity
var groupAlbumsbyArtist = from x in Albums
							//you can do an orderby here with x.Artist.Name
							group x by x.Artist into gArtistAlbums
							// you can also do an orderby here like: orderby y.ReleaseYear or by gArtistAlbums.Key.Name (for the artist, we have to use the group name because the orderby is after the group statement
							select new
							{
								KeyValue = gArtistAlbums.Key.Name,
								numberofAlbums = gArtistAlbums.Count(),
								albumandartist = from y in gArtistAlbums
												orderby y.ReleaseYear
												select new
												{
													Title = y.Title,
													Artist = y.Artist.Name
												}
							};
groupAlbumsbyArtist.Dump();

//Create a query which will report the employee and their customer base
//List employee fullname (phone), number of customer in their base
//List the fullname, city, and state for the customer base

//how to attack this question, tips:
//What is the detailf of the query? What is reported on most? Customer base
//Is the report one complete report or is it smaller components? Order by vs group by?
//Can I subdivide (group) my details into specific piles? If so, on what? Employee (smaller piles of data on xxxx)
//Is there an association between Customers and Employees? nav property SupportRep

var groupCustomersOfEmployees = from x in Customers
								group x by x.SupportRepIdEmployee into gTemp
								select new
								{
									Employee = gTemp.Key.LastName + ", " + gTemp.Key.FirstName + "(" + gTemp.Key.Phone + ")",
									BaseCount = gTemp.Count(),
									CustomerList = from y in gTemp
													select new
													{
														CustName = y.LastName + ", " + y.FirstName,
														City = y.City,
														State = y.State
													}
								};
groupCustomersOfEmployees.Dump();