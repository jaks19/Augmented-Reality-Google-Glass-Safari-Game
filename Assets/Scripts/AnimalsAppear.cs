using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class AnimalsAppear : MonoBehaviour {
	public GameObject[] animals;
	private GameObject currentAnimal;
	private int reachedIndex;

	//public Transform[] transforms;
	private int initialNumber = 1;
	private float maxX = 7f;
	private float minX = -7f;
	private float maxY = 4f;
	private float minY = -4f;
	private float maxZ = 7f;
	private float minZ = -7f;

	// We want 'initialNumber' of animals to be present at first
	void Start () {
		reachedIndex = 0;
		currentAnimal = drawAnimal (animals [reachedIndex], generatePos());
		reachedIndex++;
	}
		
	// We want to generate new animal GO's when "..."
	void Update () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	GameObject drawAnimal(GameObject whichAnimal, Vector3 desiredPosition){
		GameObject newAnimal = Instantiate (whichAnimal, desiredPosition, Quaternion.identity) as GameObject;
		// I have specified correct scales of these animals before turning them to prefabs
		// But some of them have scripts attached to them that re-inititalize their size so I make my defined size active again
		newAnimal.transform.localScale = new Vector3 (newAnimal.transform.localScale.x, newAnimal.transform.localScale.y, newAnimal.transform.localScale.z); //if need to scale
		newAnimal.layer = 5; // make appearance in 'UI' layer
		return newAnimal;
	}

	Vector3 generatePos(){
		return new Vector3 (Random.Range (minX, maxX), Random.Range (minY, maxY), Random.Range (minZ, maxZ));
	}

	public void Next(){
		Destroy (currentAnimal, 0f);
		currentAnimal = drawAnimal (animals [reachedIndex], generatePos ());
		reachedIndex++;
	}
}