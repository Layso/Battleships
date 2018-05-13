using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreenManager : MonoBehaviour {
	/* Public UI items to make it possible to attach from editor */
	public Dropdown languageSelectionDropdown;



	// Use this for initialization
	void Start () {
		ChangeLanguage();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeLanguage() {
		LanguageHandler.ChangeLanguage(languageSelectionDropdown.options[languageSelectionDropdown.value].text);
		LanguageLabel[] labels = FindObjectsOfType<LanguageLabel>();
		foreach(LanguageLabel label in labels) {
			label.UpdateLabel();
		}
	}
}
