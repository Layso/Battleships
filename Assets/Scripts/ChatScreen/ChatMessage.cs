/* Container class for chat messages to easily convert to JSON */
public class ChatMessage
{
	// Constructor to create new message
	public ChatMessage(string nick, string message)
	{
		this.nick = nick;
		this.message = message;
	}

	public string nick;
	public string message;
}
