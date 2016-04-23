using UnityEngine;
using UnityEngine.UI;

public class ShipControl : MonoBehaviour {

    public uint shipThrust;
    public uint rotationSpeed;
    public float maxFuel;
    public LayerMask hitLayer;
    public Transform laserBeam;

    public static float xPos;
    public static float yPos;
    public static float velocity;
    public static float fuel;

    private Rigidbody2D playerShip;
    private Animator anim;
    private Slider fuelGauge;
    private Image fuelIcon;
    private uint numGuns = 2;
    private Transform[] firePoints;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerShip = GetComponent<Rigidbody2D>();
        fuelGauge = GameObject.Find("FuelGauge").GetComponent<Slider>();
        fuelGauge.maxValue = maxFuel;
        fuelIcon = fuelGauge.gameObject.transform.GetChild(3).GetComponent<Image>();;
        fuel = maxFuel;
        
        string fpName;
        firePoints = new Transform[numGuns];
        for (int i = 0; i < numGuns; i++) {
            fpName = string.Format("FirePoint{0}", i);
            firePoints[i] = transform.FindChild(fpName);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        velocity = playerShip.velocity.magnitude;
        xPos = transform.position.x;
        yPos = transform.position.y;

        // Engines
        if (Input.GetKey(KeyCode.W)) {
            if (fuel > 0) {
                playerShip.AddForce(transform.up * shipThrust);
                fuel--;
                anim.SetBool("enginesOn", true);
            } else {
                anim.SetBool("enginesOn", false);
            }
        } else
            anim.SetBool("enginesOn", false);

        // Rotation
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationSpeed * 0.1f);
            playerShip.angularVelocity = 0;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationSpeed * 0.1f);
            playerShip.angularVelocity = 0;
        }
        
        // Fuel
        if (fuel > maxFuel)
            fuel = maxFuel;
        else if (fuel <= 0)
            fuelIcon.color = new Color32 (255, 255, 255, 60);
        else
            fuelIcon.color = new Color32 (0, 178, 255, 255);

        fuelGauge.value = fuel;
    }
    
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot ();
        }
    }

    void Shoot () {
        for (int i = 0; i < numGuns; i++) {
            
            Debug.DrawRay (firePoints[i].position, transform.up * 1000, Color.cyan);
            RaycastHit2D hit = Physics2D.Raycast (firePoints[i].position, transform.up, 1000, hitLayer);
            
            if (hit.collider != null) {
                Debug.DrawRay (firePoints[i].position, hit.point - (new Vector2(firePoints[i].position.x, firePoints[i].position.y)), Color.red);
                Debug.Log(hit.collider.name);
            }
            
            Vector3 hitPos;
            if (hit.collider == null)
                hitPos = firePoints[i].up * 1000 + firePoints[i].position;
            else
                hitPos = hit.point;
            
            Effect (hitPos, firePoints[i]);
        }
    }

    void Effect (Vector3 hitPos, Transform firePoint) {
        Transform trail = Instantiate (laserBeam, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        lr.sortingLayerName = "Background";
        
        if (lr != null) {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.02f);
    }
}
