using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	public GameObject chestTop;
	public ParticleSystem particle;
	public AudioSource coinSound;
	private bool opening = false;
	private const float MAX_HEIGHT = 16.0f;
	private const float CHEST_TOP_SPEED = 4.0f;

	public void Update() {
		if (opening && chestTop.transform.position.y < MAX_HEIGHT) {
			chestTop.transform.position = new Vector3 (
				chestTop.transform.position.x,
				chestTop.transform.position.y + Time.deltaTime * CHEST_TOP_SPEED,
				chestTop.transform.position.z);
		}
	}

	public void OpenChest() {
		if (!opening) {
			opening = true;
			StartCoroutine (StartParticleEffect());
			coinSound.Play ();
		}
	}

	private IEnumerator StartParticleEffect() {
		yield return new WaitForSeconds(1.0f);
		particle.Play ();
	}
}
