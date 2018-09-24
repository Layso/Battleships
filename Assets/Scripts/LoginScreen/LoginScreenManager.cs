using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreenManager : MonoBehaviour {
	public static LoginScreenManager singleton = null;


	void Awake() {
		if (singleton == null)
			singleton = this;
		else
			Destroy(this);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	/*  */
	public void SignUp(string email, string emailConfirm, string password) {
		const int PASSWORD_MIN = 8;
		const int PASSWORD_MAX = 25;
		const string REQUIRED_CHAR_1 = "@";
		const string REQUIRED_CHAR_2 = ".";


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



	/* TODO */
	public void LogIn(string email, string password) {
		print("LogIn with ID:" + email + " Pass:" + password);
	}



	/* TODO */
	public void RememberPass(string email) {
		print("Sending information message to:" + email);
	}
}
