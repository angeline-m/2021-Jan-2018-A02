<Query Kind="Statements">
  <Connection>
    <ID>f5df9793-87f6-42ab-90ff-59cef7c51e51</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Aggregates
//.Count(), .Sum(), .Min(), .Max(), .Average()

//Aggregates operate on collections
var ex1m = Albums.Count();
ex1m.Dump();

var ex1q = (from x in Albums
			select x).Count();
ex1q.Dump();

var ex1qLabels = (from x in Albums
				where x.ReleaseLabel != null
				select x).Count();
ex1qLabels.Dump();

//.Sum(), .Min(), .Max(), .Average()
//you need to specify a field to aggregate

//How much room does the music collection on the database take for albums of the 1990's
//basically asking for the sum of the tracks from those albums

//Tracks table has a numeric field called Bytes -> this field holds the size of the track for storage
//summing the field gets total track storage

var ex2q = (from x in Tracks
			where x.Album.ReleaseYear == 1990
			select x.Bytes).Sum();
ex2q.Dump();

var ex2m = Tracks
			.Where(x => x.Album.ReleaseYear == 1990)
			.Sum(x => x.Bytes);
ex2m.Dump();

//What is the shortest playtime of a track released in 1990

var ex2qs = (from x in Tracks
			where x.Album.ReleaseYear == 1990
			select x.Milliseconds).Min();
ex2qs.Dump();

var ex2ms = Tracks
			.Where(x => x.Album.ReleaseYear == 1990)
			.Min(x => x.Milliseconds);
ex2ms.Dump();

//What is the longest playtime of a track released in 1990

var ex2ql = (from x in Tracks
			where x.Album.ReleaseYear == 1990
			select x.Milliseconds).Max();
ex2ql.Dump();

var ex2ml = Tracks
			.Where(x => x.Album.ReleaseYear == 1990)
			.Max(x => x.Milliseconds);
ex2ml.Dump();

//What is the average playtime of a track released in 1990

var ex2qa = (from x in Tracks
			where x.Album.ReleaseYear == 1990
			select x.Milliseconds).Average();
ex2qa.Dump();

var ex2ma = Tracks
			.Where(x => x.Album.ReleaseYear == 1990)
			.Average(x => x.Milliseconds);
ex2ma.Dump();