using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAnimator : MonoBehaviour {
	// Customizable elements from editor
	public Transform[] turretHeads;
	public Transform[] turretGuns;
	[Space]
	[Space]
	public int animationDelayTime;
	[Space]
	[Space]
	public int minVerticalRotation;
	public int maxVerticalRotation;
	public int minHorizontalRotation;
	public int maxHorizontalRotation;

	int i;
	bool active;
	float timer;



	void Start() {
		// Setting default values
		timer = 0;
		active = false;
		Activate();
	}

	void Update() {
		if (active) {
			timer+= Time.deltaTime;
			if (timer > animationDelayTime) {
				timer = 0;
				
				// TODO
				// Do some kind of animation here
			}
		}
	}



	/* Function to activate the animation */
	public void Activate() {
		active = true;
	}



	/* Function to deactivate the animation */
	public void Deactivate() {
		active = false;
	}
}
