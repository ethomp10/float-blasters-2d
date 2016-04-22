using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {

    public float orbitSpeed;

    private Rigidbody2D moonRB;
    private Transform planet;
    private float planetRadius;
    private float planetGravity;
    private float distanceToPlanet;

	// Use this for initialization
	void Start () {
        moonRB = GetComponent<Rigidbody2D>();
        moonRB.AddForce(Vector3.right * 50000);
        planet = transform.parent;
        planetRadius = planet.GetComponent<CircleCollider2D>().radius;
        planetGravity = planetRadius * 30;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        distanceToPlanet = (transform.position - planet.position).magnitude;
        moonRB.AddForce((planet.position - transform.position).normalized * 10 * planetGravity / distanceToPlanet);
    }
}
