using UnityEngine;

public class Nyan : MonoBehaviour {
    
    public Transform star;
    public Transform ship;
    
    public float orbitSpeed;
    public float atmosphere;
    
    private float distance;
    private float zoomFactor;
    
    void Update () {
        if (ship != null) {
            distance = (transform.position - ship.position).magnitude - 1;
            Debug.DrawLine(transform.position, ship.position, Color.blue);
        } else
            FindPlayer();
    }
    
	void FixedUpdate () {
        // Distance to player
        if (ship != null) {
            
            // Calculate camera size based on player distance to surface
            if ((distance) < 100)
                zoomFactor = distance + 10;
            else
                zoomFactor = 100;
                
            if ((distance) <= atmosphere) {
                // Camera zoom
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomFactor, Time.fixedDeltaTime);
            }
                
            // Orbit around sun
            if (star != null)
                transform.RotateAround(star.position, Vector3.back, orbitSpeed / 100 * Time.fixedDeltaTime);
        }
    }
    
    void FindPlayer () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            ship = player.transform;
        }
    }
}
