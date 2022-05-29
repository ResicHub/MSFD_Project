using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform myTransform;
    [SerializeField]
    private float respawnTime;
    private float timer;

    [SerializeField]
    private GameObject prefab;
    void Start()
    {
        myTransform = GetComponent<Transform>();
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
        Instantiate(prefab, myTransform.position, myTransform.rotation);
    }
}
