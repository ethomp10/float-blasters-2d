using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
    
    public static GameMaster gm;
    public Transform[] planetPrefabs;
    public Transform playerPrefab;
    public Transform explosionEffect;
    public float respawnTime = 3;
    public float numPlanets = 8;
    public bool randomPlanetTypes = false;

    private string[] planetNames = { "Kornax", "Home", "Nebrol", "Hath" };

    private Transform spawnPoint;
    private Transform tempPlanet;
    private Transform homePlanet;
    private Planet pScript;

    // Use this for initialization
    void Start () {
        if (gm == null) {
            gm = GameObject.Find("GM").GetComponent<GameMaster>();
        }
        SpawnPlanets();
        SpawnPlayer();
        
        Cursor.visible = false;
    }
    
    public void SpawnPlanets() {
        int type = planetPrefabs.Length;
        int planetSpacing = 5000;
        Vector3 pos;

        for (int i = 0; i < numPlanets; i++) {
            if (randomPlanetTypes) {
                type = Random.Range(0, planetPrefabs.Length);
            }
            else {
                type++;
                if (type >= planetPrefabs.Length) {
                    type = 0;
                }
            }
            // Search for a spot that isn't too close to other planets
            do {
                pos = new Vector3(Random.Range(-planetSpacing * (i + 1), planetSpacing * (i + 1)),
                    Random.Range(-planetSpacing * (i + 1), planetSpacing * (i + 1)), 0);
            } while (pos.magnitude < planetSpacing * (i + 1));

            if (i == 1) {
                // Make second planet from the sun home planet
                homePlanet = Instantiate(planetPrefabs[type], pos, Quaternion.identity) as Transform;
                pScript = homePlanet.GetComponent<Planet>();
                pScript.NamePlanet(planetNames[i]);
                pScript.GetCompass(i);
            } else {
                tempPlanet = Instantiate(planetPrefabs[type], pos, Quaternion.Euler(0, 0, Random.Range(0, 359))) as Transform;
                pScript = tempPlanet.GetComponent<Planet>();
                pScript.NamePlanet(planetNames[i]);
                pScript.GetCompass(i);
            }
        }

        spawnPoint = homePlanet.GetChild(3).transform;
    }

    public void SpawnPlayer () {
		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
	}
    
	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (respawnTime);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void KillPlayer (GameObject player) {
        Transform explosion = Instantiate(gm.explosionEffect, player.transform.position, player.transform.rotation) as Transform;
        explosion.GetComponent<Renderer>().sortingLayerName = "Foreground";
		Destroy (player);
        Destroy (explosion.gameObject, 5f);
		gm.StartCoroutine(gm.RespawnPlayer ());
    }
}
