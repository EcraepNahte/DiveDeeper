using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public MeshGen LeftWall;
    public MeshGen RightWall;
    
    private GameManager gameManager;

    private float screenEdgeLeft;
    private float screenEdgeRight;

    private void Start()
    {
        gameManager = GameManager.Instance;

        screenEdgeLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenEdgeRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    public void SpawnObject()
    {
        List<GameObject> spawnPool = gameManager.GetSpawnPool();
        List<SpawnableObject> spawnObjPool = new List<SpawnableObject>();

        // Get a random object based on spawn weights
        int totalWeight = 0;
        foreach (GameObject obj in spawnPool)
        {
            SpawnableObject spawnObj = obj.GetComponent<SpawnableObject>();
            
            if (spawnObj == null)
            {
                continue;
            }

            totalWeight += spawnObj.SpawnWeight;
            spawnObjPool.Add(spawnObj);
        }

        int weightedIndex = Random.Range(0, totalWeight);
        int actualIndex = 0;
        foreach (SpawnableObject spawnObj in spawnObjPool)
        {
            if (weightedIndex < spawnObj.SpawnWeight)
            {
                actualIndex = spawnObjPool.IndexOf(spawnObj);
                break;
            }

            // Remove the current object's weight for the next comparison
            weightedIndex -= spawnObj.SpawnWeight;
        }

        SpawnableObject objectToSpawn = spawnObjPool[actualIndex];

        float spawnZoneLeft = screenEdgeLeft + LeftWall.GetHeight(transform.position.y); 
        float spawnZoneRight = screenEdgeRight - RightWall.GetHeight(transform.position.y);

        float spawnPositionX = spawnObjPool[actualIndex].GetObjectSpawnPoint(spawnZoneLeft, spawnZoneRight);

        Instantiate(spawnPool[actualIndex].gameObject, new Vector3(spawnPositionX, transform.position.y, 0), gameObject.transform.rotation);
    }
}
