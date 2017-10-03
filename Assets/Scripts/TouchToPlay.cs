using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TouchToPlay : MonoBehaviour {
	public Text touchToPlay;
	private bool textFull;
	// Use this for initialization
	void Start () {
		textFull = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		StartCoroutine (Flash ());

		//if (Input.GetKeyDown("space")){
		// If touchpad is pressed Start Game (== load next level)
		if (AndroidInput.touchCountSecondary > 0) {
			SceneManager.LoadScene (1);
		}
	}

	IEnumerator Flash(){
		if (textFull) {
			touchToPlay.text = " ";
			textFull = false;
			yield return new WaitForSeconds (1f);
		} else {
			touchToPlay.text = "Touch D-pad to play!";
			textFull = true;
			yield return new WaitForSeconds (1f);
		}
	}
}