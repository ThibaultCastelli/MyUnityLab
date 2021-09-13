using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateTC;
using AudioTC;
using ObserverTC;

public class Test : MonoBehaviour
{
    public Easing test;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            test.PlayAnimationInOut();
    }

}
