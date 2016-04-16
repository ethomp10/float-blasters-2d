using UnityEngine;
using UnityEngine.UI;

public class Nyan : MonoBehaviour {
    
    public Transform star;
    public Transform ship;
    public Text distanceMeter;
    public RectTransform compas;
    
    public float orbitSpeed;
    public float atmosphere;
    
    private float distance;
    private float zoomFactor;
    
    void Update () {
        if (ship != null) {
            distance = (transform.position - ship.position).magnitude - 1;
            Debug.DrawLine(transform.position, ship.position, Color.blue);
            
            Vector2 direction = (ship.transform.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            
            compas.Rotate(new Vector3(0, 0, angle));
            compas.rotation = Quaternion.Euler(0, 0, angle);
            
            distanceMeter.text = (gameObject.transform.name + ": " + Mathf.Round(distance) + " space bits");
        }
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
                transform.RotateAround(star.position, Vector3.forward, orbitSpeed / 100 * Time.fixedDeltaTime);
        }
    }
}
