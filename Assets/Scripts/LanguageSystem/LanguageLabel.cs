using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageLabel : MonoBehaviour {
	/* Public tag to set label from editor */
	public string label;



	/* Function to update the attached text of the label */
	public void UpdateLabel () {
		GetComponent<Text>().text = LanguageHandler.GetLabel(label);
	}
}
