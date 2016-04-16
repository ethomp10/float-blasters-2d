using UnityEngine;

public class Sun : MonoBehaviour {
    
    public Transform ship;
    public Transform explosionEffect;
    public Rigidbody2D shipRB;
    public float atmosphere;

    private float distance;
    private float radius;
    private float gravity;
    private float zoomFactor;
	
    void Start () {
        radius = GetComponent<CircleCollider2D>().radius;
        gravity = radius * 100;
    }
    
	void FixedUpdate () {
        // Distance to player
        if (ship != null) {
            distance = (transform.position - ship.position).magnitude;
            // Gravity effect on player
            shipRB.AddForce((transform.position - ship.position).normalized * gravity / distance);
            
            // Calculate camera size based on player distance to surface
            if ((distance - radius) < 100)
                zoomFactor = distance - radius + 10;
            else
                zoomFactor = 100;
            
            // Atmosphere
            if ((distance - radius) <= atmosphere) {                
                // Camera zoom
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomFactor, Time.fixedDeltaTime);
            }
        } else {
            FindPlayer();
        }
    }
    
    void OnTriggerEnter2D (Collider2D spaceship) {
        if (spaceship.gameObject.tag == "Player") {
            GameMaster.KillPlayer(spaceship.gameObject);
        }
    }
    
    void FindPlayer () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            ship = player.transform;
            shipRB = player.GetComponent<Rigidbody2D>();
        }
    }
}
