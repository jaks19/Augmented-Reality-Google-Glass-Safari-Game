using UnityEngine;
using System.Collections;

public class AnimalMovement : MonoBehaviour {
	private float speed = 3f; // Speed we want animal to move
	private Vector3 desiredNewPosition; // Always the next position we want to be at

	private float maxX = 7f;
	private float minX = -7f;
	private float maxY = 4f;
	private float minY = -4f;
	private float maxZ = 7f;
	private float minZ = -7f;

	// Animal has been generated within the bounds given
	// Need to generate a new position for it to move to
	// Before moving it there, we need to rotate the go towards that direction till it faces it (movement more realistic)
	// We move it to this desired positon and WHEN it reached there, we generate a new positon (within the bounds and again move it...)

	void Start () {
		desiredNewPosition = generatePos (transform.position); // Generate the first destination position
	}

	void Update () {
		// Keep updating the object's rotation till il faces the destination point
		// 	(angle never zero, always within 5 degrees though)
		if (Vector3.Angle (transform.forward, desiredNewPosition - transform.position) > 5.0f) {
			ChangeAngle ();
		} 
		// Wait until finished rotating, then move
		else {
			transform.position = Vector3.MoveTowards(transform.position, desiredNewPosition, speed * Time.deltaTime);
		}

		// If we reached our designated destination, keep picking the next 
		if (transform.position == desiredNewPosition) {
			desiredNewPosition = generatePos (desiredNewPosition); // Generate a new destination within our game bounds
		}
	}

	// Generates a new Vector3 'position' for the GamObject to move to within the visible bounds
	Vector3 generatePos(Vector3 oldPos){
		return new Vector3 (Random.Range (minX, maxX), oldPos.y, Random.Range (minZ, maxZ));
	}

	// Rotates the animal towards the target position to make movement only "head-first" type
	void ChangeAngle() {
		Vector3 targetDir = desiredNewPosition - transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
	}
}