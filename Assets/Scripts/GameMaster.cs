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

    // Use this for initialization
    void Start () {
        if (gm == null) {
            gm = GameObject.Find("GM").GetComponent<GameMaster>();
        }
        SpawnPlanets();
    }
    
    public void SpawnPlanets() {
        int type;        
        Vector3 pos;
        
        for (int i = 0; i < numPlanets; i++) {
            type = Random.Range(0, planetPrefabs.Length);
            pos = new Vector3 (Random.Range(-20000, 20000), Random.Range(-20000, 20000), 0);
            Instantiate(planetPrefabs[type], pos, Quaternion.identity);
        }
    }
    
	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (respawnTime);
        
        float spawnOffset = homePlanet.GetComponent<CircleCollider2D>().radius;
		Instantiate (playerPrefab, homePlanet.position + new Vector3(0, spawnOffset + 1, 0), Quaternion.identity);
	}
    
    public static void KillPlayer (GameObject player) {
        Transform explosion = Instantiate(gm.explosionEffect, player.transform.position, player.transform.rotation) as Transform;
        explosion.GetComponent<Renderer>().sortingLayerName = "Foreground";
		Destroy (player);
        Destroy (explosion.gameObject, 5f);
		gm.StartCoroutine(gm.RespawnPlayer ());
    }
}
