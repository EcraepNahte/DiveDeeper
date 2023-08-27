using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockUrchinSpawner : MonoBehaviour
{
    public GameObject Urchin;
    public List<Vector3> SpawnPoints;

    private void Start()
    {
        foreach(Vector3 spawnPoint in SpawnPoints)
        {
            if (((int)Random.Range(0, 5)) == 1)
            {
                Instantiate(Urchin, transform.position + spawnPoint, Quaternion.identity);
            }
        }
    }
}
