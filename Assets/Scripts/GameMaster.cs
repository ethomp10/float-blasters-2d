using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform[] planetPrefabs;
    public Transform playerPrefab;
    public Transform homePlanet;
    public Transform explosionEffect;
    public float respawnTime = 3;
    public float numPlanets = 8;
    public bool randomPlanetTypes = false;

    // Use this for initialization
    void Start () {
        if (gm == null) {
            gm = GameObject.Find("GM").GetComponent<GameMaster>();
        }
        SpawnPlanets();
        SpawnPlayer();
    }
    
    public void SpawnPlanets() {
        int type = planetPrefabs.Length;
        int planetSpacing = 5000;
        Vector3 pos;
        
        for (int i = 0; i < numPlanets; i++) {
            if (randomPlanetTypes) {
                type = Random.Range(0, planetPrefabs.Length);                
            } else {
                type++;
                if (type >= planetPrefabs.Length) {
                    type = 0;
                }
            }
            do {
                pos = new Vector3 (Random.Range(-planetSpacing * (i+1), planetSpacing * (i+1)),
                    Random.Range(-planetSpacing * (i+1), planetSpacing * (i+1)), 0);
            } while (pos.magnitude < planetSpacing * (i+1));
            
            if (i == 1)
                homePlanet = Instantiate(planetPrefabs[type], pos, Quaternion.identity) as Transform; 
            else
                Instantiate(planetPrefabs[type], pos, Quaternion.identity);
        }
    }
    
    public void SpawnPlayer () {
        float spawnOffset = homePlanet.GetComponent<CircleCollider2D>().radius;
        Debug.Log("Spawn");
		Instantiate (playerPrefab, homePlanet.position + new Vector3(0, spawnOffset + 5, 0), Quaternion.identity);
	}
    
	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (respawnTime);
        
        float spawnOffset = homePlanet.GetComponent<CircleCollider2D>().radius;
		Instantiate (playerPrefab, homePlanet.position + new Vector3(0, spawnOffset + 5, 0), Quaternion.identity);
	}
    
    public static void KillPlayer (GameObject player) {
        Transform explosion = Instantiate(gm.explosionEffect, player.transform.position, player.transform.rotation) as Transform;
        explosion.GetComponent<Renderer>().sortingLayerName = "Foreground";
		Destroy (player);
        Destroy (explosion.gameObject, 5f);
		gm.StartCoroutine(gm.RespawnPlayer ());
    }
}
