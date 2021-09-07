using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioTC;
using CustomVariablesTC;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [MinMaxRange(0, 2)]
    public RangedFloat test;
    public RangedFloat test2;
    [MinMaxRange(-1, 4)]
    public RangedFloat test3;
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioLocator.GetAudioPlayer().Play("test");
            SceneManager.LoadScene(1);
        }

    }
}
