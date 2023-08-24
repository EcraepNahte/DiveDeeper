using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public int SpawnWeight = 1;
    public float SpawnRoom = 0;

    public virtual float GetObjectSpawnPoint(float spawnEdgeLeft, float spawnEdgeRight)
    {
        return Random.Range(spawnEdgeLeft + SpawnRoom, spawnEdgeRight - SpawnRoom);
    }
}
