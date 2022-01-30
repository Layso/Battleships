using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneController : MonoBehaviour {
	void Start() {
		print(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
		FindObjectOfType<DatabasePlayerStatusManager>().PlayerStatusChanged += this.OnPlayerStatusChanged;
	}

	private void OnPlayerStatusChanged(EPlayerStatus NewStatus) {
		print("Player status: " + NewStatus);
	}

	void Update() {

	}

	public void Test() {
		DatabasePlayerStatusManager manager = FindObjectOfType<DatabasePlayerStatusManager>();
		if (manager) {
			manager.UpdateStatus(manager.PlayerStatus == EPlayerStatus.Online ? EPlayerStatus.Searching : EPlayerStatus.Online);
		}
	}
}
