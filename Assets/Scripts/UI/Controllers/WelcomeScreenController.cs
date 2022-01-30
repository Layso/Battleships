using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using Firebase.Extensions;

public class WelcomeScreenController : MonoBehaviour {
	[SerializeField] private PanelSwitcher MenuSwitcher;
	[SerializeField] private InputField Input_LoginEmail;
	[SerializeField] private InputField Input_LoginPassword;
	[SerializeField] private InputField Input_RegisterEmail;
	[SerializeField] private InputField Input_RegisterPassword;
	[SerializeField] private InputField Input_RegisterPasswordConfirm;
	[SerializeField] private InputField Input_ResetEmail;

	//private Firebase.Auth.FirebaseAuth Auth;


	public void OnLoginUsingEmail() {
		/*
		Auth.SignInWithEmailAndPasswordAsync(Input_LoginEmail.text, Input_LoginPassword.text).ContinueWithOnMainThread(task => {
			if (!task.IsCanceled && !task.IsFaulted) {
				LoadNextScene();
			} else {
				print(task.Exception);
			}
		});
		 */
	}

	public void OnRegisterUsingEmail() {
		if ((Input_RegisterEmail.text != string.Empty) && (Input_RegisterPassword.text != string.Empty) && (Input_RegisterPassword.text == Input_RegisterPasswordConfirm.text)) {
			/*
			Auth.CreateUserWithEmailAndPasswordAsync(Input_RegisterEmail.text, Input_RegisterPassword.text).ContinueWithOnMainThread(task => {
				if (!task.IsCanceled && !task.IsFaulted) {
					LoadNextScene();
				} else {
					print(task.Exception);
				}
			});
			 */

		}
	}

	public void OnResetUsingEmail() {
		//Auth.SendPasswordResetEmailAsync(Input_ResetEmail.text);
	}


	private void Start() {
		//Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		MenuSwitcher.PanelSwitched += this.OnPanelSwitched;
	}

	private void OnPanelSwitched(int OldIndex, int NewIndex) {
		if (OldIndex != NewIndex) {
			switch (NewIndex) {
				case 0: ClearLoginPage(); break;
				case 1: ClearRegisterPage(); break;
				case 2: ClearResetPage(); break;
			}
		}
	}

	private void LoadNextScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void ClearLoginPage() {
		Input_LoginEmail.text = string.Empty;
		Input_LoginPassword.text = string.Empty;
	}

	private void ClearRegisterPage() {
		Input_RegisterEmail.text = string.Empty;
		Input_RegisterPassword.text = string.Empty;
		Input_RegisterPasswordConfirm.text = string.Empty;
	}

	private void ClearResetPage() {
		Input_ResetEmail.text = string.Empty;
	}
}
