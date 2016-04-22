using UnityEngine;

public class FuelPad : MonoBehaviour {
	
	private Animator anim;
	private bool poweredOn = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
    }

    void Update () {
        if (poweredOn) {
            ShipControl.fuel += 10;
        }
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
