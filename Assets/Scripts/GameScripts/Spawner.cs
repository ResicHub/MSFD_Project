using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float respawnTime;
    private float timer;

    [SerializeField]
    private GameObject prefab;
    void Start()
    {
        timer = respawnTime;
    }

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            Spawn();
            timer = respawnTime;
        }
    }

    private void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation, transform);
    }
}
