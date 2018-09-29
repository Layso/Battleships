using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour {
	// One instance to rule them all
	public static DatabaseManager singleton = null;

	// Enum type to specify the result of register, login or password reset
	public enum LoginScreenStatus {
		Succesfull,
		ConnectionError,
		CredentialError
	}



	/* Assigning singleton at Awake() to be ready at Start() */
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



	/* Function to try registering new account to database, returns status as enum type*/
	public LoginScreenStatus Register(string email, string password) {
		return LoginScreenStatus.ConnectionError;
	}



	/* Function to try logging in with given account credentials */
	public LoginScreenStatus LogIn(string email, string password) {
		return LoginScreenStatus.ConnectionError;
	}
}
