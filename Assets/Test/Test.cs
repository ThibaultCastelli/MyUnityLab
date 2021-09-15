using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateTC;
using AudioTC;
using ObserverTC;
using EasingTC;
using PoolTC;

public class Test : MonoBehaviour
{
    public Pool pool;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject prefab = pool.Request();
            prefab.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
        }
    }

}
