using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;



public class DatabaseManager : MonoBehaviour {
	// One instance to rule them all
	public static DatabaseManager instance = null;

	public delegate void ReceiveMessageHandler(ChatMessage message);

	FirebaseAuth auth;
	FirebaseUser user;
	DatabaseReference reference;
	ReceiveMessageHandler handler;

	// Enum type to specify the result of register, login or password reset
	public enum LoginScreenStatus {
		Succesfull,
		ConnectionError,
		CredentialError
	}



	/* Assigning singleton at Awake() to be ready at Start() */
	void Awake() {
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}


	// Use this for initialization
	void Start () {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(Secrets.DATABASE_URL);
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		reference.Child("messages").ChildAdded += MessageStateChanged;
	}



	/* Function to try registering new account to database, returns status as enum type*/
	public LoginScreenStatus Register(string email, string password) {
		LoginScreenStatus status = LoginScreenStatus.Succesfull;


		auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			if (task.IsCanceled)
				status = LoginScreenStatus.ConnectionError;

			else if (task.IsFaulted)
				status = LoginScreenStatus.CredentialError;
		});
		
		return status;
	}



	/* Function to try logging in with given account credentials, returns status as enum type */
	public LoginScreenStatus LogIn(string email, string password) {
		LoginScreenStatus status = LoginScreenStatus.Succesfull;


		auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			if (task.IsCanceled)
				status = LoginScreenStatus.ConnectionError;

			else if (task.IsFaulted)
				status = LoginScreenStatus.CredentialError;

			else
				user = auth.CurrentUser;

		});
		
		return status;
	}



	/* Function to reset the password related to the mail address and send informative mail, returns status as enum type */
	public LoginScreenStatus ResetPassword(string email) {
		//TODO
		// Check if email exists in database

		// If it doesn't exist
			// Return credential error
		
		// If it exists
			// Generate a new password according to rules
			// Set new password as the password of the mail in database
			// Send a mail to user to inform it about new mail
			// Return succesfull status
		
		// If database connection can't be established
		return LoginScreenStatus.ConnectionError;
	}


	/* EventHandler method for  */
	void MessageStateChanged(object sender, ChildChangedEventArgs args)
	{
		if (handler != null)
		{
			string nick = args.Snapshot.Child("nick").Value.ToString();
			string message = args.Snapshot.Child("message").Value.ToString();
			ChatMessage chatMessage = new ChatMessage(nick, message);
			handler(chatMessage);
		}
	}


	/* Method to save the message that sent to the database */
	public void SendChatMessage(ChatMessage message)
	{
		string json = JsonUtility.ToJson(message);
		reference.Child("messages").Push().SetRawJsonValueAsync(json);
	}


	/* Setter for ChildChange event handler */
	public void SetChatMessageReceiveHandler(ReceiveMessageHandler handler)
	{
		this.handler = handler;
	}
}
