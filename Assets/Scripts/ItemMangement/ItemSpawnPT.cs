using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPT : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Transform[] SpawnPoints;
    void Start()
    {
        foreach (GameObject x in gameObjects)
        {
            int randomIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);
        x.transform.position = SpawnPoints[randomIndex].position; }
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
