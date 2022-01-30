using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EPlayerStatus {
	Offline,
	Online,
	Searching,
	Playing
}

public class DatabasePlayerStatusManager : MonoBehaviour {
	public event Action<EPlayerStatus> PlayerStatusChanged;
	public EPlayerStatus PlayerStatus { get; private set; }

	private string PlayerID;
	private DatabaseReference Root;
	private readonly string PATH_PLAYERS = "players";
	private readonly string PATH_STATUS = "status";


	public void UpdateStatus(EPlayerStatus NewStatus) {
		Root.Child(PATH_PLAYERS).Child(PlayerID).Child(PATH_STATUS).SetValueAsync((int)NewStatus);
	}

	void Start() {
		PlayerID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		Root = FirebaseDatabase.DefaultInstance.GetReference("");
		SetupBindings();
	}

	private void SetupBindings() {
		Root.Child(PATH_PLAYERS).Child(PlayerID).Child(PATH_STATUS).ValueChanged += this.OnPlayerStatusChanged;
	}

	private void OnPlayerStatusChanged(object Sender, ValueChangedEventArgs EventArgs) {
		if (EventArgs.Snapshot.Value != null) {
			int val = int.Parse(EventArgs.Snapshot.Value.ToString());
			PlayerStatus = (EPlayerStatus)val;
		} else {
			// Todo: implement initial status sequence
			PlayerStatus = EPlayerStatus.Offline;
		}

		PlayerStatusChanged?.Invoke(PlayerStatus);
	}

	private void OnDisable() {
		UpdateStatus(EPlayerStatus.Offline);
	}
}
