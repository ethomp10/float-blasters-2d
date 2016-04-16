using UnityEngine;

public class ShipControl : MonoBehaviour {

    public Rigidbody2D playerShip;
    public uint shipThrust;
    public uint rotationSpeed;

    public static float xPos;
    public static float yPos;
    public static float velocity;

    private Vector3 lastPosition;

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        velocity = GetComponent<Rigidbody2D>().velocity.magnitude;
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
