public class LoginEventArgs : EventArgs 
{
  public bool IsSuccessful { get; set; }  
	
  public LoginEventArgs(bool msg) {
		IsSuccessful = msg;
  }		
}

public class TicketManager {
    private  List<Ticket> AllTickets = new List<Ticket>();
    private  HashSet<int> existingIDs = new HashSet<int>();
    private Random rand = new Random();
    public bool IsLoggedIn { get; set; } = false;
	public event EventHandler<LoginEventArgs> loginEvent;
	
	//It is used to raise the event when sending data to subscribers
	protected virtual void OnLogin(LoginEventArgs e) {
    // Do any necessary things in here
    loginEvent?.Invoke(this, e);
  }

    public void Login(string username, string password)
    {
		public adminUsername="adminUsername";
		public adminPassword="adminPassword";
		public bool loginSuccessful=false;
		if(username==adminUsername && password==adminPassword)
			loginSuccessful=true;
		if(loginSuccessful)
			IsLoggedIn=true;
		new LoginEventArgs { IsSuccessful = loginSuccessful 
    }
	
	

    public void DisplayMenu(bool adminperm) {
        Console.WriteLine("\nPlease select what you would like to do:");
        Console.WriteLine("1. Add a bus ticket");
        Console.WriteLine("2. Add a train ticket");
        if(adminperm){
            Console.WriteLine("3. Remove a ticket");
            Console.WriteLine("4. View all tickets");
        }
        else{
            Console.WriteLine("3. Log in as admin");
        }
    }

    public string GetUserInput() {
        string? input = Console.ReadLine();
        
        return input != null ? input : "";
    }

    private int GenerateID() {
        while(true) {
            int newId = rand.Next(100000, 1000000);
            if(!existingIDs.Contains(newId)) {
                existingIDs.Add(newId);
                return newId;
            }
        }
    }

    private DateTime GenerateRandomDate()
    {
        DateTime startDate = DateTime.Now;

        int maxDays = (int)(startDate.AddMonths(2) - startDate).TotalDays;
        int randomDays = rand.Next(0, maxDays + 1);

        DateTime randomDate = startDate.AddDays(randomDays);
 
        int hours = rand.Next(6, 24);
        int minutes = rand.Next(0, 2) * 30; 

        randomDate = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, hours, minutes, 0);

        return randomDate;
    }

    public bool QuitCheck(string input) {
        return input.ToLower() == "quit" || input.ToLower() == "exit";
    }

    public void AddNewTicket(int ticketType, string CustomerName) {
        int ticketNumber = GenerateID();
        DateTime departDate = GenerateRandomDate();

        if(ticketType == 1){
            AllTickets.Add(new BusTicket(CustomerName, ticketNumber, departDate));
            Console.WriteLine($"\nBus ticket added for {CustomerName}.");
        }
        if(ticketType == 2){
            AllTickets.Add(new TrainTicket(CustomerName, ticketNumber, departDate));
            Console.WriteLine($"\nTrain ticket added for {CustomerName}.");
        }
        
    }

    public void RemoveTicket(int ticketNumber) {
        Ticket? ticketToRemove = AllTickets.Find(ticket => ticket.TicketNumber == ticketNumber);

        if(ticketToRemove != null) {
            AllTickets.Remove(ticketToRemove);
            Console.WriteLine($"The ticket with ticket number {ticketToRemove.TicketNumber} for {ticketToRemove.CustomerName} has been removed.");
        }
        else {
            Console.WriteLine($"\nNo ticket with the ticket number {ticketNumber} was found.");
        }
    }

    public void ViewAllTickets() {
        Console.WriteLine("----------");
        AllTickets.ForEach(x => {
            x.ShowInfo();
            Console.WriteLine("\n");
        });
    }
}
