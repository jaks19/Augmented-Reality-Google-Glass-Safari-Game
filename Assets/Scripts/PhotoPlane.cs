using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PhotoPlane : MonoBehaviour {
	private bool gotMain; // Has this photo plane got its main character in the photo?
	private TimeAndScore tsScript;
	private AnimalsAppear AnimalsScript;

	// Use this for initialization
	void Start () {
		tsScript = GameObject.Find ("Main Camera").GetComponent<TimeAndScore> ();
		AnimalsScript = GameObject.Find ("Main Camera").GetComponent<AnimalsAppear> ();
		gotMain = false;
		StartCoroutine (Destruction ()); 
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other){
		if (!gotMain) {
			if (!(other.transform.tag == "Untagged")) {
				gotMain = true;
				tsScript.IncrementScore ();
				tsScript.AnnounceCatch (other.transform.tag);
				AnimalsScript.Next ();
			} else {
				tsScript.AnnounceMiss ();
			}
		}
	}

	IEnumerator Destruction(){
		yield return new WaitForSeconds (1f);
		if (!gotMain) {
			tsScript.AnnounceMiss ();
		}
		Destroy (gameObject, 1f);
	}
}
