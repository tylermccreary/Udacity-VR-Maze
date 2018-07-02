using UnityEngine;
using System.Collections;

public class DieAfterSeconds : MonoBehaviour {

	public float seconds;

    IEnumerator Start () {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
	}
}
