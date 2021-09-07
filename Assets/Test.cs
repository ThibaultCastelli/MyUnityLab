using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioThibaultCastelli;
using CustomVariablesTC;

public class Test : MonoBehaviour
{
    public FloatReference floatReference;
    public IntReference intRef;
    public BoolReference boolref;
    public StringReference stringref;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AudioLocator.GetServiceProvider().Play("test");

        if (Input.GetMouseButtonDown(0))
            stringref.Value += "a";
    }
}
