<Query Kind="Program">
  <Connection>
    <ID>f5df9793-87f6-42ab-90ff-59cef7c51e51</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//list all customers in alphabetic order by last name then first name who live in the US and have an email of yahoo. List their full name, email, city, and state
	
	//query
	//the code inside the new { ... } is called the initializer list
	string country = "USA";
	string email = "yahoo";
	var resultsA = from x in Customers
	where x.Country.Contains(country) && x.Email.Contains(email)
	orderby x.LastName, x.FirstName
	select new
	{
		Name = x.LastName + ", " + x.FirstName,
		Email = x.Email,
		City = x.City,
		State = x.State,
		Country = x.Country
	};
	resultsA.Dump();
	//within LinqPad to see the contents of a variable, you will use the LinqPad method .Dump()
	
	//method
	var resultsB = Customers
	   .Where (x => (x.Country.Contains (country) && x.Email.Contains (email)))
	   .OrderBy (x => x.LastName)
	   .ThenBy (x => x.FirstName)
	   .Select (
	      x => 
	         new  CustomersOfCountryEmail
	         {
	            Name = ((x.LastName + ", ") + x.FirstName), 
	            Email = x.Email, 
	            City = x.City, 
	            State = x.State, 
	            Country = x.Country
	         }
	   );
	resultsB.Dump();
	
	//Create an alphabetical list of Albums by ReleaseLabel. Show the Title and ReleaseLabel. Missing album labels will be listed as "Unknown"
	
	//query
	from x in Albums
	orderby x.ReleaseLabel
	select new
	{
		Title = x.Title,
		Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel
	}
	
	//method
	Albums
	   .OrderBy (x => x.ReleaseLabel)
	   .Select (
	      x => 
	         new  
	         {
	            Title = x.Title, 
	            Label = (x.ReleaseLabel == null) ? "Unknown" : x.ReleaseLabel
	         }
	   )
	   
	//Create an alphabetical list of Albums stating the album decade for the 70's, 80's, and 90's. List the album Title, Year, and its decade.
	
	from x in Albums
	where x.ReleaseYear >= 1970 && x.ReleaseYear < 2000
	select new
	{
		Title = x.Title,
		Year = x.ReleaseYear,
		Decade = x.ReleaseYear >= 1970 && x.ReleaseYear <= 1979 ? "70's" : 
		x.ReleaseYear >= 1980 && x.ReleaseYear <= 1989 ? "80's" : "90's"
	}
}

// Define other methods and classes here

//classes are strongly specified developer-defined datatypes
public class CustomersOfCountryEmail
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Country { get; set; }
	
}
