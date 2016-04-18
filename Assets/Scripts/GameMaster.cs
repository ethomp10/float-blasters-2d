﻿using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform[] planetPrefabs;
    public Transform playerPrefab;
    public Transform explosionEffect;
    public float respawnTime = 3;
    public float numPlanets = 8;
    public bool randomPlanetTypes = false;

    private Transform spawnPoint;
    private Transform homePlanet;

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
                Instantiate(planetPrefabs[type], pos, Quaternion.Euler(0, 0, Random.Range(0, 359)));
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
