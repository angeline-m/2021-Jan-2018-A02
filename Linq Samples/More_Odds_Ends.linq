<Query Kind="Statements">
  <Connection>
    <ID>f5df9793-87f6-42ab-90ff-59cef7c51e51</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//comparing the playlists of Roborto Almeida (AlmeidaR) and Michelle Brooks (BrooksM)
//comparing two lists to each other
//obtain a distinct list of all playlist tracks for Roberto
//the .distinct() can destroy the sort of a query syntax, so we add sort after the .distinct()

var almeida = (from x in PlaylistTracks
				where x.Playlist.UserName.Contains("AlmeidaR")
				select new
				{
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					song = x.Track.Name,
					artist = x.Track.Album.Artist.Name
				}).Distinct().OrderBy(y => y.song);
//almeida.Dump();
//110 records

var brooks = (from x in PlaylistTracks
				where x.Playlist.UserName.Contains("BrooksM")
				select new
				{
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					song = x.Track.Name,
					artist = x.Track.Album.Artist.Name
				}).Distinct().OrderBy(y => y.song);
//brooks.Dump();
//88 records

//list the tracks that both Roberto and Michelle like, compare two datasets together
//data in listA that's also in ListB

var likes = almeida
			.Where(rob => brooks.Any(mic => mic.id == rob.id))
			.OrderBy(rob => rob.song)
			.Select(rob => rob);
//likes.Dump();
//1 song in common

//list the tracks that roberto likes but michelle does not listen to
var almeidadiffs = almeida
				.Where(rob => !brooks.Any(mic => mic.id == rob.id))
				.OrderBy(rob => rob.song)
				.Select(rob => rob);
//almeidadiffs.Dump();
//109 records

var brooksdiffs = brooks
				.Where(mic => almeida.All(rob => mic.id != rob.id))
				.OrderBy(mic => mic.song)
				.Select(mic => mic);
//brooksdiffs.Dump();
//87 records

var brooksdiffssq = from mic in brooks
				where almeida.All(rob => mic.id != rob.id)
				orderby mic.song
				select mic;
//brooksdiffssq.Dump();
//87 records

//using multiple statements to solve a problem is not unusual
//you have to do some type of pre-processing to obtain some data which is used in the remaining processing

//produce a report (display) where the track is flagged as shorter than average, longer than average, or average in play length (ms)
//first find what's the avg track play time, then compare the avg play time to each track
var resultsavg = Tracks
				.Where(tr => tr.Genre.Name.Contains("Rock"))
				.Average(tr => tr.Milliseconds);
resultsavg.Dump();
//282541

//use the pre-processed value in another query
var resultsTrackAvgLength = (from x in Tracks
							where x.Genre.Name.Contains("Rock")
							select new
							{
								song = x.Name,
								milliseconds = x.Milliseconds,
								length = x.Milliseconds < resultsavg ? "Shorter" : x.Milliseconds > resultsavg ? "Longer" : "Average"
							}).OrderBy(x => x.milliseconds);
//resultsTrackAvgLength.Dump();

var resultsTrackAvgLength2 = (from x in Tracks
							where x.Genre.Name.Contains("Rock")
							select new
							{
								song = x.Name,
								milliseconds = x.Milliseconds,
								length = x.Milliseconds < Tracks
								.Where(tr => tr.Genre.Name.Contains("Rock"))
								.Average(tr => tr.Milliseconds) ? "Shorter" : x.Milliseconds > resultsavg ? "Longer" : "Average"
							}).OrderBy(x => x.milliseconds);
//resultsTrackAvgLength2.Dump();

//union: the joiing of multiple results into a single query dataset
//syntax: (query).Union.(query).Union(query)...
//number of columns must be the same
//datatype of columns must be the same
//ordering should be done as a method() on the union dataset

//List the stats of Albums on Tracks (Count, $cost, avg track length)
//Note: for cost and average, one will need an instance (track on album) to actually process the method
//to do this example you will need an album with no tracks in your db

var unionresults = from x in Albums
					select new
					{
						title = x.Title,
						totalTracks = x.Tracks.Count(),
						totalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
						AverageLength = x.Tracks.Average(tr => tr.Milliseconds) / 1000.0
					};
unionresults.Dump();

var unionresults2 = (from x in Albums
					select new
					{
						title = x.Title,
						totalTracks = x.Tracks.Count(),
						totalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
						AverageLength = x.Tracks.Average(tr => tr.Milliseconds) / 1000.0
					}).Union(Albums.Where(x => x.Tracks.Count() == 0)
				.Select(x => new
						{
							title = x.Title,
							totalTracks = x.Tracks.Count(),
							totalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
							AverageLength = x.Tracks.Average(tr => tr.Milliseconds) / 1000.0
						})).OrderBy(u => u.totalTracks);
unionresults2.Dump();

var nontrack = Albums
				.Where(x => x.Tracks.Count() == 0)
				.Select(x => new
						{
							title = x.Title,
							totalTracks = x.Tracks.Count(),
							totalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
							AverageLength = x.Tracks.Average(tr => tr.Milliseconds) / 1000.0
						});
nontrack.Dump();