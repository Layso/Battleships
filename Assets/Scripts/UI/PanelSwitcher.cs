using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour {
	public int Count { get { return ChildPanels.Count; } }

	private int CurrentIndex;
	private List<GameObject> ChildPanels;



	public void SwitchPanel(int Index) {
		if (Index >= 0 && Index < ChildPanels.Count && Index != CurrentIndex) {
			DisableCurrent();
			CurrentIndex = Index;
			ChildPanels[Index].SetActive(true);
		}
	}

	public void SwitchPanel(GameObject Panel) {
		SwitchPanel(ChildPanels.IndexOf(Panel));
	}

	public void SwitchPanel(Transform Panel) {
		SwitchPanel(Panel.gameObject);
	}

	public void AddPanel(GameObject Panel) {
		if (!ChildPanels.Contains(Panel)) {
			ChildPanels.Add(Panel);
		}
	}

	public void AddPanel(Transform Panel) {
		AddPanel(Panel.gameObject);
	}

	private void Awake() {
		CurrentIndex = -1;
		ChildPanels = new List<GameObject>();
		for (int i=0; i<transform.childCount; ++i) {
			ChildPanels.Add(transform.GetChild(i).gameObject);
			ChildPanels[i].SetActive(false);
		}

		SwitchPanel(0);
	}

	private void DisableCurrent() {
		if (CurrentIndex > 0) {
			ChildPanels[CurrentIndex].SetActive(false);
		}
	}
}
