using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float speed = 5.0f;

	void Update () {
		gameObject.transform.Rotate (new Vector3 (0, speed * Time.deltaTime, 0));
	}
}
