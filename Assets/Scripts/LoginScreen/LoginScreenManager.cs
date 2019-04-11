using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoginScreenManager : MonoBehaviour {
	// One instance to rule them all
	public static LoginScreenManager instance = null;

	// 
	public PopUpPanel popUpPanel;

	// Constant definitions
	const string LANGUAGE_LABEL_INVALID_EMAIL = "invalid-email";
	const string LANGUAGE_LABEL_UNMATCHING_EMAIL = "unmatching-email";
	const string LANGUAGE_LABEL_INVALID_PASSWORD = "invalid-password";
	const string LANGUAGE_LABEL_WRONG_CREDENTIALS = "wrong-credentials";
	const string LANGUAGE_LABEL_EXISTING_CREDENTIALS = "existing-credentials";
	const string LANGUAGE_LABEL_CONNECTION_ERROR = "connection-error";
	const string LANGUAGE_LABEL_NON_EXISTING_EMAIL = "non-existing-email";
	const string PASSWORD_SPECIAL_CHARACTERS = "@%+/!#$?*.-_";
	const int PASSWORD_MIN = 5;
	const int PASSWORD_MAX = 25;



	/* Assigning singleton at Awake() to be ready at Start() */
	void Awake() {
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update() {

	}



	/* Function to validate emails and password to continue with registering new account */
	public void SignUp(string email, string emailConfirm, string password) {
		// Validating mail
		if (!IsMailValid(email)) {
			popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_INVALID_EMAIL)).SetColor(Color.white).SetBackgroundColor(Color.grey).SetAlignment(TextAnchor.MiddleCenter).Activate();
		}
		
		// Emails must be equal
		else if (!email.Equals(emailConfirm)) {
			popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_UNMATCHING_EMAIL)).SetColor(Color.white).SetBackgroundColor(Color.grey).SetAlignment(TextAnchor.MiddleCenter).Activate();
		}
		
		// Validating password
		else if (!IsPasswordValid(password)) {
			popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_INVALID_PASSWORD)).SetColor(Color.white).SetBackgroundColor(Color.grey).SetAlignment(TextAnchor.MiddleLeft).Activate();
		} 
		
		// If no errors, try to register new account to database
		else {
			DatabaseManager.LoginScreenStatus status = DatabaseManager.instance.Register(email, password);
			print("Annen");
			
			// If registeration is succesfull, automatically login to account
			if (status == DatabaseManager.LoginScreenStatus.Succesfull) {
				LogIn(email, password);
			}

			// If failed, show error message according to error reason
			else if (status == DatabaseManager.LoginScreenStatus.CredentialError) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_EXISTING_CREDENTIALS)).SetColor(Color.white).SetBackgroundColor(Color.red).Activate();
			} else if (status == DatabaseManager.LoginScreenStatus.ConnectionError) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_CONNECTION_ERROR)).SetColor(Color.white).SetBackgroundColor(Color.red).Activate();
			}
			
		}
	}



	/* Function to validate mail and password to try connecting with the database */
	public void LogIn(string email, string password) {
		// Checking mail and password locally to be sure they are valid before connecting to internet
		if (!IsMailValid(email) || !IsPasswordValid(password)) {
			popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_WRONG_CREDENTIALS)).SetColor(Color.white).SetBackgroundColor(Color.grey).SetAlignment(TextAnchor.MiddleCenter).Activate();
		}

		// If credentials are valid then check with database entries
		else {
			DatabaseManager.LoginScreenStatus status = DatabaseManager.instance.LogIn(email, password);


			// If login is succesfull, load next scene
			if (status == DatabaseManager.LoginScreenStatus.Succesfull) {
				SceneManager.LoadScene("Chat Scene");
			}
			
			// If failed, show error message accordin to error reason
			else if (status == DatabaseManager.LoginScreenStatus.CredentialError) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_WRONG_CREDENTIALS)).SetColor(Color.white).SetBackgroundColor(Color.red).Activate();
			} else if (status == DatabaseManager.LoginScreenStatus.ConnectionError) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_CONNECTION_ERROR)).SetColor(Color.white).SetBackgroundColor(Color.red).Activate();

				// TODO
				// Open a new panel asking to continue offline or retry connection
			}
		}
	}



	/* Function to validate mail to reset password and sent to user */
	public void ResetPassword(string email) {
		// Checking mail locally to be sure they are valid before connecting to internet
		if (!IsMailValid(email)) {
			popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_NON_EXISTING_EMAIL)).SetColor(Color.white).SetBackgroundColor(Color.grey).SetAlignment(TextAnchor.MiddleCenter).Activate();
		}

		// If credentials are valid then check with database entries
		else {
			DatabaseManager.LoginScreenStatus status = DatabaseManager.instance.ResetPassword(email);


			if (status == DatabaseManager.LoginScreenStatus.Succesfull) {
				// TODO
				// Show an informative message
				// Direct to login screen
			}

			// If failed, show error message according to error reason
			else if (status == DatabaseManager.LoginScreenStatus.CredentialError) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_NON_EXISTING_EMAIL)).SetColor(Color.white).SetBackgroundColor(Color.red).Activate();
			} else if (status == DatabaseManager.LoginScreenStatus.ConnectionError) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_CONNECTION_ERROR)).SetColor(Color.white).SetBackgroundColor(Color.red).Activate();
			}
		}
	}



	/* Email validator */
	private bool IsMailValid(string email) {
		// Constant definitions for email address check
		const string AT_CHARACTER = "@";
		const string DOT_CHARACTER = ".";


		// There must be only one "@" character that is not at the beginning
		if (!email.Contains(AT_CHARACTER) || email.IndexOf(AT_CHARACTER) != email.LastIndexOf(AT_CHARACTER) || email.StartsWith(AT_CHARACTER)) {
			return false;
		}

		// There must be only one '.' character that is not at the end
		else if (!email.Contains(DOT_CHARACTER) || email.IndexOf(DOT_CHARACTER) != email.LastIndexOf(DOT_CHARACTER) || email.EndsWith(DOT_CHARACTER)) {
			return false;
		}

		// '.' character can not be behind the '@' character in a mail address
		else if (email.LastIndexOf(DOT_CHARACTER) < email.IndexOf(AT_CHARACTER)) {
			return false;
		}

		// There must be a domain name between "@" and "." characters
		else if (email.IndexOf(AT_CHARACTER) + 1 == email.IndexOf(DOT_CHARACTER)) {
			return false;
		}

		// If email passed from all checks then return true
		else {
			return true;
		}
	}



	/* Password validator */
	private bool IsPasswordValid(string password) {
		// Password must be between 5 and 25 characters
		if (password.Length < PASSWORD_MIN || password.Length > PASSWORD_MAX) {
			return false;
		}

		// Password must include one upper and one lower case character
		else if (password.ToLower().Equals(password) || password.ToUpper().Equals(password)) {
			return false;
		}

		// Password should only include alphanumeric characters and valid special characters
		else {
			// Return false if an unknown character has found
			foreach (char ch in password) {
				if (!char.IsLetterOrDigit(ch) && !PASSWORD_SPECIAL_CHARACTERS.Contains(ch.ToString())) {
					return false;
				}
			}

			// Return true if all checks are passed
			return true;
		}
	}
}
