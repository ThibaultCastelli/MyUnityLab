using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateTC;
using SFXTC;
using ObserverTC;
using EasingTC;
using PoolTC;
using MusicTC;

public class Test : MonoBehaviour
{
    public MusicEvent musicEventA;
    public MusicEvent musicEventB;
    public MusicEvent musicEventC;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            musicEventA.PlayFade();

        if (Input.GetKeyDown(KeyCode.B))
            musicEventB.PlayFade();

        if (Input.GetKeyDown(KeyCode.C))
            musicEventC.PlayFade();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MusicManager.Instance.IncreaseLayer();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            MusicManager.Instance.DecreaseLayer();

        if (Input.GetKeyDown(KeyCode.Space))
            MusicManager.Instance.StopFade();
    }

}
