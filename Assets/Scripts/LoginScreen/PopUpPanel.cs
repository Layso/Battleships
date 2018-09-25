using UnityEngine;
using UnityEngine.UI;



public class PopUpPanel : MonoBehaviour {
	bool active;
	bool fadeIn;
	bool fadeOut;
	float timer;
	float showDuration;
	float shadeDuration;
	Text text;
	Image panel;

	// Constant definitions
	const int CHILD_TEXT_TRANSFORM_INDEX = 0;
	const int CHILD_PANEL_TRANSFORM_INDEX = 0;
	const float DEFAULT_SHOW_DURATION = 3f;
	const float DEFAULT_SHADE_DURATION = 1.5f;


	// Use this for initialization
	void Start() {
		timer = 0;
		active = false;
		fadeIn = false;
		fadeOut = false;
		showDuration = DEFAULT_SHOW_DURATION;
		shadeDuration = DEFAULT_SHADE_DURATION;
		panel = gameObject.transform.GetChild(CHILD_PANEL_TRANSFORM_INDEX).GetComponent<Image>();
		text = panel.transform.GetChild(CHILD_TEXT_TRANSFORM_INDEX).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update() {
		if (active) {
			if (fadeIn) {
				float alpha = Mathf.Lerp(0, 1, timer * shadeDuration);
				text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
				panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
				timer += Time.deltaTime;

				if (alpha == 1) {
					timer = 0;
					fadeIn = false;
					fadeOut = false;
				}
			}

			else if (!fadeIn && !fadeOut) {
				timer += Time.deltaTime;

				if (timer > showDuration) {
					fadeOut = true;
					timer = 0;
				}
			}

			else if (fadeOut) {
				float alpha = Mathf.Lerp(1, 0, timer * shadeDuration);
				text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
				panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
				timer += Time.deltaTime;

				if (alpha == 0) {
					active = false;
					fadeOut = false;
				}
			}
		}
	}


	public void Click() {
		timer = 0;
		fadeIn = false;
		fadeOut = true;
	}


	public PopUpPanel SetText(string newText) {
		text.text = newText;
		return this;
	}



	public PopUpPanel SetColor(Color newColor) {
		text.color = newColor;
		return this;
	}



	public PopUpPanel SetSize(int newSize) {
		text.resizeTextForBestFit = false;
		text.fontSize = newSize;
		return this;
	}



	public PopUpPanel Activate() {
		timer = 0;
		active = true;
		fadeIn = true;
		fadeOut = false;
		return this;
	}



	public PopUpPanel Deactivate() {
		Destroy(gameObject);
		return this;
	}

	public bool Online() {
		return active;
	}
}
