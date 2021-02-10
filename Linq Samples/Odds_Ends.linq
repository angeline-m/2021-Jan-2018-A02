<Query Kind="Statements">
  <Connection>
    <ID>f5df9793-87f6-42ab-90ff-59cef7c51e51</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

var distinctmq = Customers
				.Select(x => x.Country)
				.Distinct();
distinctmq.Dump();

var distinctsq = (from x in Customers
				select x.Country).Distinct();
distinctsq.Dump();

//Any() and All()

//number of genres
var GenreCount = Genres.Count();
GenreCount.Dump();

//show genres that have tracks which are not on any playlist
var genreTrackAny = from g in Genres
					where g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0)
					select g;
genreTrackAny.Dump();

//show genres that have all their tracks appearing at least once on a playlist
//show the genre name and list of genre tracks and number of playlists
var genreTrackAll = from g in Genres
					where g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0)
					select new
					{
						name = g.Name,
						thetracks = from y in g.Tracks
									where y.PlaylistTracks.Count() > 0
									select new
									{
										song = y.Name,
										count = y.PlaylistTracks.Count()
									}
					};
genreTrackAll.Dump();