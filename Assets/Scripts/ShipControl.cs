using UnityEngine;

public class ShipControl : MonoBehaviour {

    public uint shipThrust;
    public uint rotationSpeed;

    public static float xPos;
    public static float yPos;
    public static float velocity;

    private Rigidbody2D playerShip;
    private Animator anim;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerShip = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        velocity = playerShip.velocity.magnitude;
        xPos = transform.position.x;
        yPos = transform.position.y;
        
        // Engines
        if (Input.GetKey(KeyCode.W)) {
            playerShip.AddForce(transform.up * shipThrust);
            anim.SetBool("enginesOn", true);
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
    }
}
