using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// This class will be attached to the player to manage the Timer for each level
// 	Also, the current max level reached will be saved in the backend using userprefs
public class TimeAndScore : MonoBehaviour {
	// Init
	public float initialTime; // Placeholder in Unity to specify the total time for a level
	private float leftTime;
	private string timeAnnounced; // We hold the time string here to pass to the textbox
	public Text textboxTime;
	public int score;
	public Text textboxUpdates;
	public Text textboxScore;
	private string scoreAnnounced;
	private string messageAnnounced;

	private int customMessageCount;
	private List<string> customMessages;


	void Start(){
		leftTime = initialTime;
		customMessageCount = 0;

		customMessages = new List<string>{
			"Nice picture of a",
			"Candid",
			"Awesome snap of a", 
			"Delightful pose, Mr.",
			"Wow! Such a nice",
			"Nice ",
			"Surely a poser, this",
			"Majestic",
			"Beautiful picture of a"
		};
	}

	void Update(){
		if (leftTime > 0f) {
			timeAnnounced = "Time: " + string.Format ("{0:0.0}", leftTime);
			leftTime -= Time.deltaTime;
			// If below 5.0s make text red
			if (leftTime <= 10.0f) {
				textboxTime.color = Color.red;
			}
		} else {
			leftTime = 0f;
			timeAnnounced = "Time: " + string.Format ("{0:0.0}", leftTime);
			// Time up so reload level (in future, make a menu appear with continue etc..)
			PlayerPrefs.SetInt ("last-score", score);
			if (score > PlayerPrefs.GetInt ("high-score")) {
				PlayerPrefs.SetInt ("high-score", score);
			}

			SceneManager.LoadScene (2);
		}
		// We have already dragged the textbox to this script now puch the value to it
		textboxTime.text = timeAnnounced;
	}

	public void IncrementScore(){
		score++;
		scoreAnnounced = "Score: " + score;
		textboxScore.text = scoreAnnounced;
	}

	public void AnnounceCatch(string animalTag){
		textboxUpdates.color = Color.green;
		messageAnnounced = customMessages[customMessageCount] + " " + animalTag + "!";
		textboxUpdates.text = messageAnnounced;
		customMessageCount++;
		StartCoroutine (ClearUpdatesIn (2.0f));
	}

	public void AnnounceMiss(){
		textboxUpdates.color = Color.red;
		textboxUpdates.text = "Try Again! AWEFUL PICTURE!";
		StartCoroutine (ClearUpdatesIn (1.0f));
	}

	IEnumerator ClearUpdatesIn(float time){
		yield return new WaitForSeconds (time);
		textboxUpdates.text = " ";
	}
}