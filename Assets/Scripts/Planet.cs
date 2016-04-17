using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {
    
    public Text distanceMeter;
    public RectTransform compas;
    
    public float atmosphere;
    public float orbitSpeed;
    
    private Transform star;
    private Transform ship;
    private Rigidbody2D shipRB;
    private float radius;
    private float gravity;
    private float distance;
    private float zoomFactor;
    
    private float relativeVelocity;
	
    void Start () {
        radius = GetComponent<CircleCollider2D>().radius;
        gravity = radius * 30;
        
        star = GameObject.FindGameObjectWithTag("Star").transform;
        // ship = GameObject.FindGameObjectWithTag("Player").transform;
        // shipRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        
        // Debug.Log(gameObject.transform.name + " | Radius: " + radius + " | Gravity: " + gravity);
    }
    
    void Update () {
        if (ship != null) {
            relativeVelocity = ShipControl.velocity - GetComponent<Rigidbody2D>().velocity.magnitude;
            distance = (transform.position - ship.position).magnitude - radius - 1;
            Debug.DrawLine(transform.position, ship.position, Color.blue);
            
            Vector2 direction = (ship.transform.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            
            // Compas stuff
            if (compas != null) {
                compas.Rotate(new Vector3(0, 0, angle));
                compas.rotation = Quaternion.Euler(0, 0, angle);
            }
            if (distanceMeter != null)
                distanceMeter.text = (gameObject.transform.name + ": " + Mathf.Round(distance) + " space bits");
        }
    }
    
	void FixedUpdate () {
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
                // Follow planet's orbit around sun
                if (star != null && ship != null)
                    ship.RotateAround(star.position, Vector3.back, orbitSpeed / 100 * Time.fixedDeltaTime);
                
                // Camera zoom
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomFactor, Time.fixedDeltaTime);
            }
        } else {
            FindPlayer();
        }
        
        // Orbit around sun
        if (star != null)
            transform.RotateAround(star.position, Vector3.back, orbitSpeed / 100 * Time.fixedDeltaTime);
    }
    
    void OnCollisionEnter2D (Collision2D spaceship) {
        if (spaceship.gameObject.tag == "Player") {
            // Debug.Log(relativeVelocity);
            if (relativeVelocity > 20) {
                GameMaster.KillPlayer(spaceship.gameObject);
            }
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
