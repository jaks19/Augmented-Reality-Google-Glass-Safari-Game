using UnityEngine;
using System.Collections;
using System;
using Affdex;
using System.Collections.Generic;

// Used to switch from the grey face tracking rectangle to the green one when face is found and has been showing desired emotion for nSecs seconds

public class FaceTracker: MonoBehaviour {

	private EmotionListener Listener; // The class that keeps track of emotion values

	public bool processing = true; // If true, it means this class still has to figure out when to turn the target green. Else, this has been done.
	private float secsCap = 2.0f; // The number of seconds we want the face to be seen plus making emotion before changing target lines' color
	private float secsCounter; // The number of seconds accumulated consecutively while making emotion and face being tracked

	private float valueCap = 75f; // The value between 0 and 100 above which the emotion tracked is treated as 'Seen'

	public GameObject beforeImage; // Grey photograph target
	public GameObject afterImage; // Green photograph target

	private CameraInput cameraScript;
	private Detector affdexDetector;

	private bool lossNotified = true;
	private bool foundNotified = false;

    // Use this for initialization
    void Start () {
		Listener = gameObject.GetComponent<EmotionListener> ();
		cameraScript = gameObject.GetComponent<CameraInput> ();
		affdexDetector = gameObject.GetComponent<Detector> ();
		secsCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		// Stil trying to validate that face and emotion
		if (processing) { 
			// Face ON, Emotion ON
			if (Listener.faceFound && Listener.currentEmotionValue > valueCap) {

				if (!foundNotified) {
					Communicator.SendMessage ("Face detected on Glass");
					foundNotified = true;
				}

				secsCounter += Time.deltaTime;
				lossNotified = false;
				Debug.Log (secsCounter);

			// Face or Emotion Lost
			} else {
				secsCounter = 0f;
				foundNotified = false;
				if (!lossNotified) {
					Communicator.SendMessage ("Face detected on Glass was lost");
					lossNotified = true;
				}
			}

			// if Face and Emotion ON for long enough
			if (secsCounter > secsCap) {
				// Change State Param -- Selection Mode: False --> True
				GameManager.SelectionMode = true;
				Communicator.SendMessage ("Target emotion confirmed, child is making a choice!");

				processing = false; // Will no longer track time or face
                secsCounter = 0f; // Resets timer

				// Pause the Webcam 
				cameraScript.cameraTexture.Pause (); // Requires CameraInput Class's WebcamTexture obj to be public

				// Take a screenshot!
				System.Random rnd = new System.Random(); // To get a random name for that screenshot
				int name = rnd.Next(1, 9999); // Name will be a random 9-digit number
				Application.CaptureScreenshot(name.ToString() + ".png");
				GetComponent<AudioSource> ().Play ();
			}
		}
    }
}
