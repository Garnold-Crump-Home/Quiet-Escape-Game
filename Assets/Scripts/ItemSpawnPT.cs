using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPT : MonoBehaviour
{
    public GameObject CrowBar;
    public Transform[] SpawnPoints;
    void Start()
    {
        int randomIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);
        CrowBar.transform.position = SpawnPoints[randomIndex].position;
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
