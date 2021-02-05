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
	//Nested Queries - aka subqueries, a query within a query
	
	//List all sales support employees showing their full name (lastname, firstname), their title, and the number of customers each supports. Order by fullname.
	//In addition, show a list of the customers for each employee. List the customer full name, phone, city, and state.
	
	//There will be 2 separate lists within the same final dataset collection, one for employees, and one for customers of an employee
	
	//C# POV in a class definition
	//classname
	//	property (field)
	//	property (field)
	//	...
	//	collection<T> (set of records)
	
	//to accomplish the list of customers, we will use a nested query
	//the data source for the list of customers will be the x.collection<Customers>
	//x is the employee record
	//x.NavCollectionName
	//.NavCollectionName is the navigational property to x's "children"
	
	
	var resultsq = from x in Employees
					where x.Title.Contains("Sales Support")
					orderby x.LastName, x.FirstName
					select new EmployeeCustomerList
					{
						EmployeeName = x.LastName + ", " + x.FirstName,
						Title = x.Title,
						CustomerSupportCount = x.SupportRepIdCustomers.Count(),
						CustomerList = (from y in x.SupportRepIdCustomers
										select new CustomerSupportItem
										{
											CustomerName = y.LastName + ", " + y.FirstName,
											Phone = y.Phone,
											City = y.City,
											State = y.State
										}).ToList()
					};
	resultsq.Dump();
	
	var resultsm = Employees
	   .Where (x => x.Title.Contains ("Sales Support"))
	   .OrderBy (x => x.LastName)
	   .ThenBy (x => x.FirstName)
	   .Select (
	      x => 
	         new EmployeeCustomerList
	         {
	            EmployeeName = ((x.LastName + ", ") + x.FirstName), 
	            Title = x.Title, 
	            CustomerSupportCount = x.SupportRepIdCustomers.Count (), 
	            CustomerList = x.SupportRepIdCustomers
	               .Select (
	                  y => 
	                     new CustomerSupportItem
	                     {
	                        CustomerName = ((y.LastName + ", ") + y.FirstName), 
	                        Phone = y.Phone, 
	                        City = y.City, 
	                        State = y.State
	                     }
	               ).ToList()
	         }
	   );
	resultsm.Dump();
	
	
	
	
	//Create a list of albums showing its title and artist.
	//Show albums with 25 or more tracks only.
	//Show the songs on the album listing the name and song length
	
	var results2q = from x in Albums
					where x.Tracks.Count() >= 25
					select new
					{
						Title = x.Title,
						Artist = x.Artist.Name,
						TracksOfAlbum = from y in x.Tracks
										select new 
										{
											Song = y.Name,
											LengthOfSong = y.Milliseconds / 1000.0
										}
					};
	results2q.Dump();
}

// Define other methods and classes here


public class CustomerSupportItem
{
	public string CustomerName { get; set; }
	public string Phone { get; set; }
	public string City { get; set; }
	public string State { get; set; }
}

public class EmployeeCustomerList
{
	public string EmployeeName { get; set; }
	public string Title { get; set; }
	public int CustomerSupportCount { get; set; }
	public List<CustomerSupportItem> CustomerList { get; set; }
}


