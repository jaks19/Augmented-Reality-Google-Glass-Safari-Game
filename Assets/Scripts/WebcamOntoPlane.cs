using UnityEngine;
using System.Collections;

// Script attached to camera object serving the purpose of projecting webcam feed onto a plane in Unity

public class WebcamOntoPlane : MonoBehaviour {
	// Drag the plane to this public holder
	public GameObject planeUsedAsWebcamScreen;
	public GameObject photoZone;
	private bool enableFlash; // Flag that allows flash or not (to prevent permanent white screen)


	void Start () {
		enableFlash = true;

		// ------ Piece of code to have the head move and Unity Camera moves too along the webcam, not along the whole world
		if (Application.isMobilePlatform) {
			GameObject cameraParent = new GameObject ("camParent");
			cameraParent.transform.position = this.transform.position;
			this.transform.parent = cameraParent.transform;
			cameraParent.transform.Rotate (Vector3.right, -270);
		}
		// ----

		// Enable the device's gyroscope to work (Senses the orientaton and direction of the device)
		Input.gyro.enabled = true;

		// Webcam feed is added to a plane as a texture
		WebCamTexture webcamFeed = new WebCamTexture();
		// Project the webcamFeed onto our chosen plane
		planeUsedAsWebcamScreen.GetComponent<MeshRenderer>().material.mainTexture = webcamFeed;
		// Turn on the webcam
		webcamFeed.Play();
	}
	
	// We want to make our UNITY CAMERA move with our phone camera (Inf act, the phone camera is static 
	// 	(It is instead the gyro that moves)
	void FixedUpdate () {
		// Capture the device's OrientationChange/Rotation
		Quaternion deviceRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
		// Make the UNITY CAMERA follow same motion
		this.transform.localRotation = deviceRotation;

		// If touchpad is pressed
		if (AndroidInput.touchCountSecondary > 0 && enableFlash) {
			//if (Input.GetKeyDown("space") && enableFlash){
			// disable flash till this one disappears
			StartCoroutine(takePhoto());
		}
	}

	// Freezes time for 2 seconds and to be used whenever user has taken a photo
	IEnumerator takePhoto(){
		enableFlash = false;
		GameObject photoPlane = Instantiate (photoZone, new Vector3(0f, 0f, 0f), this.transform.rotation) as GameObject;
		GetComponent<AudioSource> ().Play ();
		Rigidbody rb = photoPlane.GetComponent<Rigidbody>();
		rb.AddForce(Camera.main.transform.forward * 800f);
		//photoPlane.transform.position = Vector3.MoveTowards(transform.position, -planeUsedAsWebcamScreen.transform.forward, 10f *Time.deltaTime);

		yield return new WaitForSeconds(1.0f);
		enableFlash = true;
	}
}