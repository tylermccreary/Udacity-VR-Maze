using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    //Create a reference to the KeyPoofPrefab and Door
	public GameObject keyPoof;
	public GameObject door;
	private const float HEIGHT = 2.0f;

	void Update()
	{
		//Not required, but for fun why not try adding a Key Floating Animation here :)
		transform.position = new Vector3(transform.position.x, HEIGHT + Mathf.Sin (Mathf.PI * Time.fixedTime), transform.position.z);
	}

	public void OnKeyClicked()
	{
		// Instatiate the KeyPoof Prefab where this key is located
		// Make sure the poof animates vertically
		Object.Instantiate(keyPoof, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
        // Call the Unlock() method on the Door
		Door doorScript = door.GetComponent<Door>();
		doorScript.Unlock ();
        // Set the Key Collected Variable to true
        // Destroy the key. Check the Unity documentation on how to use Destroy
		Object.Destroy(gameObject);
    }

}
