using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPT : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Transform[] spawnPoints;

    void Start()
    {
        // Convert to list so we can shuffle
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        // Shuffle the spawn points
        for (int i = 0; i < availablePoints.Count; i++)
        {
            Transform temp = availablePoints[i];
            int randomIndex = Random.Range(i, availablePoints.Count);
            availablePoints[i] = availablePoints[randomIndex];
            availablePoints[randomIndex] = temp;
        }

        // Assign each object to a unique spawn point
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (i >= availablePoints.Count)
            {
                Debug.LogWarning("Not enough spawn points!");
                break;
            }

            gameObjects[i].transform.position = availablePoints[i].position;
        }
    }
}