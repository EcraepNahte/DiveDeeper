using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnableObject : SpawnableObject
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(Random.Range(0, 2) <= 1 ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
