using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioThibaultCastelli;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AudioLocator.GetServiceProvider().Play("test");
    }
}
