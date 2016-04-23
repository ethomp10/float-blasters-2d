using UnityEngine;

public class FuelPad : MonoBehaviour {
	
    private int refuelRate = 5;
	private Animator anim;
	private bool poweredOn = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
    }

    void FixedUpdate () {
        if (poweredOn) {
            ShipControl.fuel += refuelRate;
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
