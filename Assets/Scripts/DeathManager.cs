using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

	public static DeathManager instance = null;
	public GameObject deathParticleContainer;

	private Vector3 deathPosition;
	private bool deathBool;
	private bool winner;
	private GameObject cameraSee;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject); 
		}
		DontDestroyOnLoad (gameObject);
		instance.PlayDeath ();
		instance.ShowEnemies ();
	}

	void PlayDeath() {
		if (deathBool) {
			Instantiate (deathParticleContainer, deathPosition, Quaternion.identity);
		}
	}

	void ShowEnemies() {
		if (!winner) {
			cameraSee = GameObject.Find ("CameraSeeThrough");
			cameraSee.SetActive (false);
		}
	}

	public void SetDeathPosition(Vector3 position) {
		deathPosition = position;
		deathBool = true;
		winner = false;
	}

	public void ClearDeathPosition() {
		deathBool = false;
		winner = true;
	}
}
