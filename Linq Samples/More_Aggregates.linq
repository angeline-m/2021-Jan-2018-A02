<Query Kind="Statements">
  <Connection>
    <ID>f5df9793-87f6-42ab-90ff-59cef7c51e51</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//List of all the albums showing their title, artist name, and the number of tracks for that album. Show only the albums of the 60's, order by the number of tracks from most to least

//how to analyze the question
//title -> Albums
//artist name -> Album.Artist (single value child to parent)
//count() -> Album.Tracks (collection parent -> child)
//where ReleaseYear of Albums

//select title, artist name, count(trackid)
//where releaseyear >= 1960 && releaseyear <= 1969
//order by count(trackid) desc

var ex = from x in Albums
			where x.ReleaseYear > 1959 && x.ReleaseYear < 1970
			orderby x.Tracks.Count descending
			select new
			{
				Title = x.Title,
				Artist = x.Artist.Name,
				Year = x.ReleaseYear,
				NumberOfTracks = x.Tracks.Count()
			};
ex.Dump();

var exm = Albums
			.Where(x => x.ReleaseYear > 1959 && x.ReleaseYear < 1970)
			.OrderByDescending(x => x.Tracks.Count)
			.Select(x => new
			{
				Title = x.Title,
				Artist = x.Artist.Name,
				Year = x.ReleaseYear,
				NumberOfTracks = x.Tracks.Count()
			});
exm.Dump();

var ex2 = from x in Albums
			where x.ReleaseYear > 1959 && x.ReleaseYear < 1970
			orderby x.Tracks.Count descending
			select new
			{
				Title = x.Title,
				Artist = x.Artist.Name,
				Year = x.ReleaseYear,
				NumberOfTracks = (from y in x.Tracks select y).Count()
			};
ex2.Dump();

var ex3 = from x in Albums
			where x.ReleaseYear > 1959 && x.ReleaseYear < 1970
			orderby x.Tracks.Count descending
			select new
			{
				Title = x.Title,
				Artist = x.Artist.Name,
				Year = x.ReleaseYear,
				NumberOfTracks = (from y in Tracks
									where y.AlbumId == x.AlbumId
									select y).Count()
			};
ex3.Dump();

//Produce a list of 60's albums which have tracks showing their title, artist, number of tracks on album, total price of all tracks on album, the longest album track, the shortest album track, and the average track length

//title -> Albums
//artist -> x.Artist....
//numberoftracks -> x.Tracks.Count
//totalprice -> x.Tracks.Sum(tr.unitprice)
//longtrack -> x.Tracks.Max(tr.milliseconds)
//shorttrack -> x.Tracks.Min(tr.milliseconds)
//averagetrack -> x.Tracks.Average(tr.milliseconds)
//conditions -> which have tracks

var ex2m = Albums
			.Where(x =>( x.ReleaseYear > 1959 && x.ReleaseYear < 1970) && (x.Tracks.Count() > 0))
			.OrderByDescending(x => x.Tracks.Count)
			.Select(x => new
			{
				Title = x.Title,
				Artist = x.Artist.Name,
				Year = x.ReleaseYear,
				NumberOfTracks = x.Tracks.Count(),
				TotalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
				LongestM = x.Tracks.Max(tr => tr.Milliseconds),
				LongestQ = (from y in x.Tracks
							select y.Milliseconds).Max(),
				LongestTrackName = (from y in x.Tracks
									where y.Milliseconds == x.Tracks.Max(tr => tr.Milliseconds)
									select y.Name).FirstOrDefault(),
				ShortestQ = (from y in x.Tracks
							select y.Milliseconds/1000.0).Min(),
				AverageLength = x.Tracks.Average(tr => tr.Milliseconds)
			});
ex2m.Dump();

//MY ANSWERS -- WHICH ARE WRONG
//var exq2 = from x in Albums
//			where x.ReleaseYear > 1959 && x.ReleaseYear < 1970
//			select new
//			{
//				Title = x.Title,
//				Artist = x.Artist.Name,
//				NumberOfTracks = x.Tracks.Count(),
//				TotalPrice = (from y in x.Tracks select y.UnitPrice).Sum(),
//				LongestTrack = (from y in x.Tracks select y.Milliseconds).Max(),
//				ShortestTrack = (from y in x.Tracks select y.Milliseconds).Min(),
//				AverageTrack = (from y in x.Tracks select y.Milliseconds).Average()	
//			};
//exq2.Dump();
//
//var exm2 = Albums
//	   .Where (x => ((x.ReleaseYear > 1959) && (x.ReleaseYear < 1970)))
//	   .Select (
//	      x => 
//	         new  
//	         {
//	            Title = x.Title, 
//	            Artist = x.Artist.Name, 
//	            NumberOfTracks = x.Tracks.Count (), 
//	            TotalPrice = x.Tracks.Select (y => y.UnitPrice).Sum (), 
//	            LongestTrack = x.Tracks.Select (y => y.Milliseconds).Max (), 
//	            ShortestTrack = x.Tracks.Select (y => y.Milliseconds).Min (), 
//	            AverageTrack = x.Tracks.Select (y => y.Milliseconds).Average ()
//	         }
//	   )
//exm2.Dump();