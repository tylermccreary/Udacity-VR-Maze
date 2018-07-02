using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    // Create a boolean value called "locked" that can be checked in OnDoorClicked() 
	private bool locked = true;
    // Create a boolean value called "opening" that can be checked in Update() 
	private bool opening = false;
	public AudioSource lockedSource;
	public AudioSource openedSource;
	private const float MAX_DOOR_HEIGHT = 8.0f;
	private const float DOOR_SPEED = 4.0f;

    void Update() {
        // If the door is opening and it is not fully raised
            // Animate the door raising up
		if (opening && transform.position.y < MAX_DOOR_HEIGHT) {
			transform.Translate (0, DOOR_SPEED * Time.deltaTime, 0, Space.World);
		}
    }

    public void OnDoorClicked() {
        // If the door is clicked and unlocked
			// Set the "opening" boolean to true
		// (optionally) Else
			// Play a sound to indicate the door is locked
		if (!locked) {
			opening = true;
			openedSource.Play ();
		} else {
			lockedSource.Play ();
		}
    }

    public void Unlock()
    {
        // You'll need to set "locked" to false here
		locked = false;
    }
}
