using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform playerPrefab;
    public Transform homePlanet;
    public Transform explosionEffect;
    public float respawnTime;

    // Use this for initialization
    void Start () {
        if (gm == null) {
            gm = GameObject.Find("GM").GetComponent<GameMaster>();
        }
    }
    
	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (respawnTime);
        
        float spawnOffset = homePlanet.GetComponent<CircleCollider2D>().radius;
		Instantiate (playerPrefab, homePlanet.position + new Vector3(0, spawnOffset + 2, 0), Quaternion.identity);
	}
    
    public static void KillPlayer (GameObject player) {
        Transform explosion = Instantiate(gm.explosionEffect, player.transform.position, player.transform.rotation) as Transform;
        explosion.GetComponent<Renderer>().sortingLayerName = "Foreground";
		Destroy (player);
        Destroy (explosion.gameObject, 5f);
		gm.StartCoroutine(gm.RespawnPlayer ());
    }
}
