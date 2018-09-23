using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoginScreenUIManager : MonoBehaviour {
	/* Public UI items to make it possible to attach from editor */
	public Dropdown languageSelectionDropdown;
	public GameObject registerPanel;
	public GameObject loginPanel;
	public GameObject rememberPanel;


	// Use this for initialization
	void Start () {
		ChangeLanguage();
		WelcomeScreen();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	/*  */
	public void SignUpButton() {
		string email;
		string password;
		string emailConfirm;

		// Constant definitions
		const int EMAIL_INDEX = 0;
		const int CONFIRMATION_INDEX = 1;
		const int PASSWORD_INDEX = 2;
		const int PASSWORD_MIN = 8;
		const int PASSWORD_MAX = 25;
		const string REQUIRED_CHAR_1 = "@";
		const string REQUIRED_CHAR_2 = ".";


		// Getting InputField strings to local variables
		InputField[] fields = FindObjectsOfType<InputField>();
		email = fields[EMAIL_INDEX].text.ToLower();
		password = fields[PASSWORD_INDEX].text.ToLower();
		emailConfirm = fields[CONFIRMATION_INDEX].text.ToLower();

		// Validating mail and password format
		// Emails must be equal
		if (!email.Equals(emailConfirm)) {
			print("fail");
		}

		// There must be only one '@' character for a valid mail address
		else if (!email.Contains(REQUIRED_CHAR_1) || email.IndexOf(REQUIRED_CHAR_1) != email.LastIndexOf(REQUIRED_CHAR_1)) {
			print("fail");
		}

		// There must be only one '.' character for a valid mail address
		else if (!email.Contains(REQUIRED_CHAR_2) || email.IndexOf(REQUIRED_CHAR_2) != email.LastIndexOf(REQUIRED_CHAR_2)) {
			print("fail");
		}

		// '.' character can not be behind the '@' character in a mail address
		else if (email.LastIndexOf(REQUIRED_CHAR_2) < email.IndexOf(REQUIRED_CHAR_1)) {
			print("fail");
		}

		// '.' character can not be at the end of address
		else if (email.EndsWith(REQUIRED_CHAR_2)) {
			print("fail");
		}

		// Password must be between 8 and 25 characters
		else if (password.Length < PASSWORD_MIN || password.Length > PASSWORD_MAX) {
			print("fail");
		}

		// TODO: More checks for password


		else {
			print("success");
		}
	}



	/*  */
	public void SignInButton()
	{
		print("Signing up");
	}



	/*  */
	public void PasswordResetButton()
	{
		print("Rememberence");
	}



	/* Function to change language upon a change on language selection dropdown */
	public void ChangeLanguage() {
		/* Changing current language and getting all Language Label objects to change their texts */
		LanguageHandler.ChangeLanguage(languageSelectionDropdown.options[languageSelectionDropdown.value].text);
		LanguageLabel[] labels = Resources.FindObjectsOfTypeAll<LanguageLabel>();


		/* Updating each label */
		foreach(LanguageLabel label in labels) {
			label.UpdateLabel();
		}
	}

	/* Function to decide welcoming panel */
	public void WelcomeScreen() {
		/* Search for log file to see if there is any previous login attempts */
		if (File.Exists (Global.LOG_FILE_NAME)) {
			loginPanel.SetActive (true);
			registerPanel.SetActive (false);
			rememberPanel.SetActive (false);
		}

		else {
			registerPanel.SetActive (true);
			loginPanel.SetActive (false);
			rememberPanel.SetActive (false);
		}
	}
}
