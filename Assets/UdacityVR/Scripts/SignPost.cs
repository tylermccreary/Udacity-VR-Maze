using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SignPost : MonoBehaviour
{	
	private DeathManager deathManagerScript;

	void Start() {
		deathManagerScript = GameObject.Find("DeathManager").GetComponent<DeathManager> ();
	}

	public void ResetScene() 
	{
        // Reset the scene when the user clicks the sign post
		deathManagerScript.ClearDeathPosition();
		SceneManager.LoadScene("A Maze");
	}
}