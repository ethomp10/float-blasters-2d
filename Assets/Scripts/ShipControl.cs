using UnityEngine;
using UnityEngine.UI;

public class ShipControl : MonoBehaviour {

    public uint shipThrust;
    public uint rotationSpeed;

    public static float xPos;
    public static float yPos;
    public static float velocity;
    public static float fuel;
    public float maxFuel;

    private Rigidbody2D playerShip;
    private Animator anim;
    private Slider fuelGauge;
    private Image fuelIcon;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerShip = GetComponent<Rigidbody2D>();
        fuelGauge = GameObject.Find("FuelGauge").GetComponent<Slider>();
        fuelGauge.maxValue = maxFuel;
        fuelIcon = fuelGauge.gameObject.transform.GetChild(3).GetComponent<Image>();;
        fuel = maxFuel;
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

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationSpeed * 0.1f);
            playerShip.angularVelocity = 0;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationSpeed * 0.1f);
            playerShip.angularVelocity = 0;
        }
        
        if (fuel > maxFuel) {
            fuel = maxFuel;
        } else if (fuel <= 0) {
            fuelIcon.color = new Color32 (255, 255, 255, 60);
        } else {
            fuelIcon.color = new Color32 (0, 178, 255, 255);
        }
        fuelGauge.value = fuel;
    }
}
