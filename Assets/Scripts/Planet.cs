using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    public float atmosphere;

    private Transform star;
    private Transform ship;
    private Rigidbody2D shipRB;
    private RectTransform compass;

    private float radius;
    private float gravity;
    private float distanceToPlayer;
    private float orbitSpeed;
    private float zoomFactor;
    private float relativeVelocity;
    private string planetName;

    void Start () {
        radius = GetComponent<CircleCollider2D>().radius;
        gravity = radius * 30;
        star = GameObject.FindGameObjectWithTag("Star").transform;
        orbitSpeed = 100000 / transform.position.magnitude;
    }

    void Update () {
        if (ship != null) {
            relativeVelocity = ShipControl.velocity - GetComponent<Rigidbody2D>().velocity.magnitude;
            distanceToPlayer = (transform.position - ship.position).magnitude - radius - 1;
            Debug.DrawLine(transform.position, ship.position, Color.blue);
            
            Vector2 direction = (ship.transform.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            
            // Compas stuff
            if (compass != null) {
                compass.GetChild(0).rotation = Quaternion.Euler(0, 0, angle);
                compass.GetComponent<Text>().text = (planetName + ": " + Mathf.Round(distanceToPlayer) + " space bits");
            }
        }
    }
    
	void FixedUpdate () {
        if (ship != null) {
            distanceToPlayer = (transform.position - ship.position).magnitude;
            // Gravity effect on player
            shipRB.AddForce((transform.position - ship.position).normalized * gravity / distanceToPlayer);
            
            // Calculate camera size based on player distance to surface
            if ((distanceToPlayer - radius) < 100)
                zoomFactor = distanceToPlayer - radius + 10;
            else
                zoomFactor = 100;
            
            // Atmosphere
            if ((distanceToPlayer - radius) <= atmosphere) {
                // Follow planet's orbit around sun
                if (star != null && ship != null)
                    ship.RotateAround(star.position, Vector3.back, orbitSpeed / 100 * Time.fixedDeltaTime);
                
                // Camera zoom
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomFactor, Time.fixedDeltaTime);
            }
        } else
            FindPlayer();
        
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

    public void GetCompass(int planetNum)
    {
        string gpsName = string.Format("PlanetGPS{0}", planetNum);
        compass = GameObject.Find(gpsName).GetComponent<RectTransform>();
    }

    public void NamePlanet(string name)
    {
        planetName = name;
    }
}
