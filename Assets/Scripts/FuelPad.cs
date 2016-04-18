using UnityEngine;
using System.Collections;

public class FuelPad : MonoBehaviour {
	
	private Animator anim;
	private bool poweredOn = false;
	private Transform player;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter2D (Collider2D spaceship) {
        if (spaceship.gameObject.tag == "Player") {
			anim.SetBool("poweredOn", true);
			poweredOn = true;
        }
    }
	
	void OnTriggerExit2D (Collider2D spaceship) {
        if (spaceship.gameObject.tag == "Player") {
			anim.SetBool("poweredOn", false);
			poweredOn = false;
        }
    }
}
