using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreenUIManager : MonoBehaviour {
	/* Public UI items to make it possible to attach from editor */
	public Dropdown languageSelectionDropdown;



	// Use this for initialization
	void Start () {
		ChangeLanguage();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	/*  */
	public void SignUpButton() {
		InputField[] fields = FindObjectsOfType<InputField>();

		foreach (InputField field in fields)
			print(field.text);
	}



	/*  */
	public void SignInButton()
	{

	}



	/*  */
	public void PasswordResetButton()
	{

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
}
