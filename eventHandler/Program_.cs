namespace solution {
    class Program {
        public static void Main(string[] args)
        {
            Console.WriteLine("----------");
            Console.WriteLine("Welcome to the Transportation Ticketing System.");
            TicketManager ticketManager = new TicketManager();

         // Attach HandleLoginEvent to event
            ticketManager.LoginEvent += HandleLoginEvent;
            
            string userInput = "";

            while(!ticketManager.QuitCheck(userInput)) {
                ticketManager.DisplayMenu(ticketManager.IsLoggedIn);
                userInput = ticketManager.GetUserInput();

                switch(userInput.ToLower()) {
                    case "1":
                        Console.WriteLine("\nEnter a customer name for the bus ticket.");
                        userInput = ticketManager.GetUserInput();

                        if(ticketManager.QuitCheck(userInput)) {
                            break;
                        }

                        ticketManager.AddNewTicket(1, userInput);
                        break;
                    case "2":
                        Console.WriteLine("\nEnter a customer name for the train ticket.");
                        userInput = ticketManager.GetUserInput();

                        if(ticketManager.QuitCheck(userInput)) {
                            break;
                        }

                        ticketManager.AddNewTicket(2, userInput);
                        break;
                    
                    case "3" when !ticketManager.IsLoggedIn:
                        Console.WriteLine("\nEnter admin username:");
                        string username = ticketManager.GetUserInput();
                        Console.WriteLine("\nEnter admin password:");
                        string password = ticketManager.GetUserInput();

                        ticketManager.Login(username, password);

                        break;
                    case "3" when ticketManager.IsLoggedIn:
                        Console.WriteLine("\nPlease provide the ticket number of the ticket you wish to remove.");
                        userInput = ticketManager.GetUserInput();

                        if(ticketManager.QuitCheck(userInput)) {
                            break;
                        }

                        int ticketNumber;
                        if (int.TryParse(userInput, out ticketNumber)) {
                            ticketManager.RemoveTicket(ticketNumber);
                        }
                        else{
                            Console.WriteLine("\nA number was not provided. Please provide a ticket number to be removed.");
                        }

                        break;
                    
                    case "4" when ticketManager.IsLoggedIn:
                        ticketManager.ViewAllTickets();
                        break;
                    case "quit":
                        break;
                    case "exit":
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please give one of the specified inputs.");
                        break;
                }
            }
            Console.WriteLine("\nExiting Application.");
        }

        //Implement HandleLoginEvent to handle the event
        private static void HandleLoginEvent(object? sender, LoginEventArgs e) {
            if (e.IsSuccessful)
            {
                Console.WriteLine("\nLogin successful! User has logged in as an admin.");
            }
            else
            {
                Console.WriteLine("\nLogin unsuccessful. Incorrect credentials.");
            }
        }
    }
 
}
