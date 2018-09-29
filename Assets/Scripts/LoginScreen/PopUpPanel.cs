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
	const int HALF = 2;
	const int DOUBLE = 2;
	const int VISIBLE = 1;
	const int INVISIBLE = 0;
	const int CHILD_TEXT_TRANSFORM_INDEX = 0;
	const int CHILD_PANEL_TRANSFORM_INDEX = 0;
	const float DEFAULT_SHOW_DURATION = 3f;
	const float DEFAULT_SHADE_DURATION = 1.5f;



	// Use this for initialization
	void Start() {
		showDuration = DEFAULT_SHOW_DURATION;
		shadeDuration = DEFAULT_SHADE_DURATION;
		panel = gameObject.transform.GetChild(CHILD_PANEL_TRANSFORM_INDEX).GetComponent<Image>();
		text = panel.transform.GetChild(CHILD_TEXT_TRANSFORM_INDEX).GetComponent<Text>();
		Deactivate();
	}
	
	// Update is called once per frame
	void Update() {
		if (active) {
			if (fadeIn) {
				float alpha = Mathf.Lerp(INVISIBLE, VISIBLE, timer * shadeDuration);
				text.color = new Color(text.color.r, text.color.g, text.color.b, alpha*DOUBLE);
				panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
				timer += Time.deltaTime;

				if (alpha == VISIBLE) {
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
				float alpha = Mathf.Lerp(VISIBLE, INVISIBLE, timer * shadeDuration);
				text.color = new Color(text.color.r, text.color.g, text.color.b, alpha/HALF);
				panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
				timer += Time.deltaTime;

				if (alpha == INVISIBLE) {
					Deactivate();
				}
			}
		}
	}



	/* Function to perform fade out on click */
	public void Click() {
		if (!fadeOut) {
			timer = 0;
			fadeIn = false;
			fadeOut = true;
		}
	}



	/* Functions to control or check the pop-up behaviour */
	public PopUpPanel Activate() {
		if (!active) {
			timer = 0;
			active = true;
			fadeIn = true;
			fadeOut = false;
			gameObject.SetActive(true);
		}

		return this;
	}



	public PopUpPanel Deactivate() {
		timer = 0;
		active = false;
		fadeIn = false;
		fadeOut = false;
		gameObject.SetActive(false);

		return this;
	}



	public bool IsAnimating() {
		return active;
	}



	/* Functions to customize the pop-up panel */
	public PopUpPanel SetAlignment(TextAnchor newAlignment) {
		text.alignment = newAlignment;

		return this;
	}



	public PopUpPanel SetText(string newText) {
		text.text = newText;

		return this;
	}



	public PopUpPanel SetColor(Color newColor) {
		text.color = new Color(newColor.r, newColor.g, newColor.b, text.color.a);

		return this;
	}



	public PopUpPanel SetBackgroundColor(Color newColor) {
		panel.color = new Color(newColor.r, newColor.g, newColor.b, panel.color.a);

		return this;
	}



	public PopUpPanel SetSize(int newSize) {
		text.resizeTextForBestFit = false;
		text.fontSize = newSize;

		return this;
	}
}
