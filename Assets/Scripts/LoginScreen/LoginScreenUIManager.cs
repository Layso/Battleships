using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class LoginScreenUIManager : MonoBehaviour {
	/* Public UI items to make it possible to attach from editor */
	[Space][Space]
	public Dropdown languageSelectionDropdown;
	[Space][Space]
	public GameObject registerPanel;
	public GameObject loginPanel;
	public GameObject rememberPanel;
	[Space][Space]
	public InputField registerMail;
	public InputField registerMailConfirm;
	public InputField registerPassword;
	[Space][Space]
	public InputField loginMail;
	public InputField loginPassword;
	[Space][Space]
	public InputField resetMail;



	// Use this for initialization
	void Start() {
		ChangeLanguage();
		WelcomeScreen();
	}



	/* Button event function */
	public void SignUpButton() {
		string email;
		string password;
		string emailConfirm;


		// Getting InputField strings to local variables
		password = registerPassword.text;
		email = registerMail.text.ToLower();
		emailConfirm = registerMailConfirm.text.ToLower();

		// Calling appropriate function from scene manager
		LoginScreenManager.instance.SignUp(email, emailConfirm, password);
	}



	/* Button event function */
	public void SignInButton() {
		string email;
		string password;


		// Getting InputField strings to local variables
		email = loginMail.text.ToLower();
		password = loginPassword.text;

		// Calling propriate function from manager
		LoginScreenManager.instance.LogIn(email, password);
	}



	/* Button event function */
	public void PasswordResetButton() {
		string email;


		// Getting InputField strings to local variables
		email = resetMail.text.ToLower();

		// Calling propriate function from manager
		LoginScreenManager.instance.ResetPassword(email);
	}



	/* Function to change language upon a change on language selection dropdown */
	public void ChangeLanguage() {
		// Changing current language and getting all Language Label objects to change their texts
		LanguageHandler.ChangeLanguage(languageSelectionDropdown.options[languageSelectionDropdown.value].text);
		LanguageLabel[] labels = Resources.FindObjectsOfTypeAll<LanguageLabel>();


		// Updating each label
		foreach (LanguageLabel label in labels) {
			label.UpdateLabel();
		}
	}



	/* Function to clear input field texts */
	public void ClearInputFields() {
		registerMail.text = string.Empty;
		registerMailConfirm.text = string.Empty;
		registerPassword.text = string.Empty;
		loginMail.text = string.Empty;
		loginPassword.text = string.Empty;
		resetMail.text = string.Empty;
	}



	/* Function to decide welcoming panel */
	public void WelcomeScreen() {
		// Search for log file to see if there is any previous login attempts
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
