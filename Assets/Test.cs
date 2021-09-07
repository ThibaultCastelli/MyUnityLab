using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioThibaultCastelli;

public class Test : MonoBehaviour
{
    public FloatReference floatReference;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AudioLocator.GetServiceProvider().Play("test");

        if (Input.GetMouseButtonDown(0))
            floatReference.Value--;
    }
}
