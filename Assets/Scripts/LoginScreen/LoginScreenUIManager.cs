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
	void Start() {
		ChangeLanguage();
		WelcomeScreen();
	}
	
	// Update is called once per frame
	void Update() {
		
	}



	/* Button event function */
	public void SignUpButton() {
		string email;
		string password;
		string emailConfirm;

		// Constant definitions
		const int EMAIL_INDEX = 0;
		const int CONFIRMATION_INDEX = 1;
		const int PASSWORD_INDEX = 2;


		// Getting InputField strings to local variables
		InputField[] fields = FindObjectsOfType<InputField>();
		email = fields[EMAIL_INDEX].text.ToLower();
		password = fields[PASSWORD_INDEX].text.ToLower();
		emailConfirm = fields[CONFIRMATION_INDEX].text.ToLower();

		// Calling propriate function from manager
		LoginScreenManager.singleton.SignUp(email, emailConfirm, password);
	}



	/* Button event function */
	public void SignInButton() {
		string email;
		string password;

		// Constant definitions
		const int EMAIL_INDEX = 0;
		const int PASSWORD_INDEX = 1;


		// Getting InputField strings to local variables
		InputField[] fields = FindObjectsOfType<InputField>();
		email = fields[EMAIL_INDEX].text.ToLower();
		password = fields[PASSWORD_INDEX].text.ToLower();

		// Calling propriate function from manager
		LoginScreenManager.singleton.LogIn(email, password);
	}



	/* Button event function */
	public void PasswordResetButton() {
		string email;

		// Constant definitions
		const int EMAIL_INDEX = 0;


		// Getting InputField strings to local variables
		InputField[] fields = FindObjectsOfType<InputField>();
		email = fields[EMAIL_INDEX].text.ToLower();

		// Calling propriate function from manager
		LoginScreenManager.singleton.RememberPass(email);
	}



	/* Function to change language upon a change on language selection dropdown */
	public void ChangeLanguage() {
		/* Changing current language and getting all Language Label objects to change their texts */
		LanguageHandler.ChangeLanguage(languageSelectionDropdown.options[languageSelectionDropdown.value].text);
		LanguageLabel[] labels = Resources.FindObjectsOfTypeAll<LanguageLabel>();


		/* Updating each label */
		foreach (LanguageLabel label in labels) {
			label.UpdateLabel();
		}
	}



	/* Function to decide welcoming panel */
	public void WelcomeScreen() {
		/* Search for log file to see if there is any previous login attempts */
		if (File.Exists(Global.LOG_FILE_NAME)) {
			loginPanel.SetActive(true);
			registerPanel.SetActive(false);
			rememberPanel.SetActive(false);
		}
		else {
			registerPanel.SetActive(true);
			loginPanel.SetActive(false);
			rememberPanel.SetActive(false);
		}
	}
}
