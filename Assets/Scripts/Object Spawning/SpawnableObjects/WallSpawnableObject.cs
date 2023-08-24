using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnableObject : SpawnableObject
{
    public override float GetObjectSpawnPoint(float spawnZoneLeft, float spawnZoneRight)
    {
        return ((int)Random.Range(0, 2)) < 1 ? spawnZoneLeft : spawnZoneRight;
    }
}
