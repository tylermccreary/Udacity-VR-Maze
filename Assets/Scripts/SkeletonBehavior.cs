using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SkeletonBehavior : MonoBehaviour {

	public GameObject[] targets;
	public float zBoundary = 100f;
	public float speed = 3f;
	public AudioClip footstep;
	public AudioClip melee;

	private DeathManager deathManagerScript;
	private GameObject player;
	private Animator animator;
	private GameObject rayOrigin;
	private GameObject currentTarget;
	private NavMeshAgent nav;
	private AudioSource audioSource;

	private bool reachedBoundary;
	private bool closeToPlayer;
	private bool inSight;
	private bool attacking;

	void Start() {
		attacking = false;
		reachedBoundary = false;
		closeToPlayer = false;
		inSight = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = gameObject.GetComponentInChildren<Animator> ();
		rayOrigin = gameObject.transform.Find ("RayOrigin").gameObject;
		nav = gameObject.GetComponent<NavMeshAgent> ();
		currentTarget = targets [Random.Range (0, targets.Length)];
		audioSource = gameObject.GetComponent<AudioSource> ();
		deathManagerScript = GameObject.Find("DeathManager").GetComponent<DeathManager>();
	}

	void Update() {
		TestBoundary ();
		FindPlayer ();
		NavigateEnvironment ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			animator.SetBool ("CloseToPlayer", true);
			closeToPlayer = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			animator.SetBool ("CloseToPlayer", false);
			closeToPlayer = false;
		}
	}

	public void OnCollisionEnter(Collision collision) {
		if (collision.gameObject == player && attacking) {
			deathManagerScript.SetDeathPosition (player.transform.position);
			Debug.Log (new Vector3(player.transform.position.x, 0, player.transform.position.z));
			SceneManager.LoadScene ("A Maze");
		}
	}

	public void Attacking() {
		attacking = true;
	}

	public void NotAttacking() {
		attacking = false;
	}

	public void PlayFootStep() {
		audioSource.clip = footstep;
		audioSource.volume = 0.2f;
		audioSource.Play ();
	}

	public void PlayMelee() {
		audioSource.clip = melee;
		audioSource.volume = 1f;
		audioSource.Play ();
	}

	void TestBoundary() {
		if (gameObject.transform.position.z >= zBoundary) {
			animator.SetBool ("ReachedBoundary", true);
			reachedBoundary = true;
		} else {
			animator.SetBool ("ReachedBoundary", false);
			reachedBoundary = false;
		}
	}

	void FindPlayer() {
		Ray ray = new Ray ();
		ray.origin = rayOrigin.transform.position;
		ray.direction = (new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z)
			- ray.origin);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 50f)) {
			if (hit.transform.gameObject == player) {
				currentTarget = player;
				animator.SetBool ("InSight", true);
				inSight = true;
			} else {
				animator.SetBool ("InSight", false);
				inSight = false;
				if (currentTarget == player) {
					currentTarget = targets [Random.Range (0, targets.Length)];
				}
			}
		}
	}

	void NavigateEnvironment() {
		if (inSight && closeToPlayer) {
			Vector3 directionToTarget = transform.position - player.transform.position;
			float angel = Vector3.Angle(transform.forward, directionToTarget);
			if (Mathf.Abs (angel) < 5) {
				nav.SetDestination (gameObject.transform.position);
			} else {
				nav.SetDestination (player.transform.position);
			}
		} else if (inSight && reachedBoundary && !closeToPlayer) {
			nav.SetDestination (gameObject.transform.position);
		} else {
			nav.SetDestination (currentTarget.transform.position);
		}

		if (transform.position.x == currentTarget.transform.position.x &&
			transform.position.z == currentTarget.transform.position.z) {
			currentTarget = targets[Random.Range(0, targets.Length)];
		}
	}
}
