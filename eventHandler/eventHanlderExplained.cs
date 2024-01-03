// Custom event class for usage in the generic EventHandler<TEventArgs>
public class ExampleEventArgs : EventArgs {
  public string data { get; set; }  
	
  public ExampleEventArgs(string msg) {
		data = msg;
  }		
}

class EventSender 
{
	// Publisher class
  // Declare example events
  public event EventHandler simpleEvent;
  public event EventHandler<ExampleEventArgs> genericEvent;
	
  // This method is ONLY for genericEvent. It is used to raise the event when sending data to subscribers
  // Best practice to be set as protected virtual to allow derived classes the ability to override or extend the behavior of raising the event
  // Another best practice is the name the event raising method OnEventName
  protected virtual void OnExampleEvent(ExampleEventArgs e) {
    // Do any necessary things in here
    genericEvent?.Invoke(this, e);
  }
	
  // Place your event raisers in here. When called, the subscribers will be notified
  public void ExampleMethod() {
    // Code that does stuff
    // Used when using the non-generic. Always use EventArgs.Empty since you are not providing extra information
    simpleEvent?.Invoke(this, EventArgs.Empty);
		
    OnExampleEvent(new ExampleEventArgs("generic event occurred!"));
    }
}
	
class EventReceiver { // Handles raised event
  public void HandleSimpleEvent(object? sender, EventArgs e) { 
	
    Console.WriteLine("Simple event handled!");
  }

  public void HandleGenericEvent(object? sender, ExampleEventArgs e) {
	  
    Console.WriteLine($"{e.data}");
  }
}
	
// Main
EventSender sender = new EventSender();

EventReceiver handler = new EventReceiver();

sender.simpleEvent += handler.HandleSimpleEvent; //Attach handler to event, aka subscribe to publisher

sender.genericEvent += handler.HandleGenericEvent; //Attach generic handler to event

sender.ExampleMethod(); //Will trigger the event in the publisher

//Since event is triggered, subscriber will perform HandleSimpleEvent and HandleGenericEvent
	
}