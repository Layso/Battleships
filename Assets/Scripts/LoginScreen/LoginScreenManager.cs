using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LoginScreenManager : MonoBehaviour {
	public static LoginScreenManager singleton = null;
	public PopUpPanel popUpPanel;

	// Constant definitions
	const string LANGUAGE_LABEL_INVALID_EMAIL = "invalid-email";
	const string LANGUAGE_LABEL_UNMATCHING_EMAIL = "unmatching-email";
	const string LANGUAGE_LABEL_INVALID_PASSWORD = "invalid-password";



	void Awake() {
		if (singleton == null)
			singleton = this;
		else
			Destroy(this);
	}

	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	//

	/*  */
	public void SignUp(string email, string emailConfirm, string password) {
		// Validating mail
		if (!IsMailValid(email)) {
			if (!popUpPanel.IsAnimating()) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_INVALID_EMAIL)).SetColor(Color.white).SetBackgroundColor(Color.grey).Activate();
			}
		}

		// Emails must be equal
		else if (!email.Equals(emailConfirm)) {
			if (!popUpPanel.IsAnimating()) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_UNMATCHING_EMAIL)).SetColor(Color.white).SetBackgroundColor(Color.grey).Activate();
			}
		}

		// Validating password
		else if (!IsPasswordValid(password)) {
			if (!popUpPanel.IsAnimating()) {
				popUpPanel.SetText(LanguageHandler.GetLabel(LANGUAGE_LABEL_INVALID_PASSWORD)).SetColor(Color.white).SetBackgroundColor(Color.grey).Activate();
			}
		}


		else {
			print("success");
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
		else if (email.IndexOf(AT_CHARACTER)+1 == email.IndexOf(DOT_CHARACTER)) {
			return false;
		} 

		// If email passed from all checks then return true
		else {
			return true;
		}
	}



	/* Password validator */
	private bool IsPasswordValid(string password) {
		// Constant definitions for password check
		const int PASSWORD_MIN = 8;
		const int PASSWORD_MAX = 25;


		// Password must be between 8 and 25 characters
		if (password.Length < PASSWORD_MIN || password.Length > PASSWORD_MAX) {
			return false;
		}

		// TODO: More checks for password

		else {
			return true;
		}
	}



	/* TODO */
	public void LogIn(string email, string password) {
		print("LogIn with ID:" + email + " Pass:" + password);
	}



	/* TODO */
	public void RememberPass(string email) {
		print("Sending information message to:" + email);
	}
}
