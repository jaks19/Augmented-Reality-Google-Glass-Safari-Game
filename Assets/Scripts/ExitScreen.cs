using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitScreen : MonoBehaviour {
	public Text textboxScore;
	public Text textboxHigh;

	// Use this for initialization
	void Start () {
		textboxScore.text = "Your Score: " + PlayerPrefs.GetInt ("last-score");
		textboxHigh.text = "All-time Highscore: " + PlayerPrefs.GetInt ("high-score");
	}
}