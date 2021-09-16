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

    public SFXEvent SFXEventA;
    public SFXEvent SFXEventB;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            musicEventA.Play();

        if (Input.GetKeyDown(KeyCode.Z))
            musicEventB.Play();

        if (Input.GetKeyDown(KeyCode.E))
            musicEventC.Play();

        if (Input.GetKeyDown(KeyCode.Q))
            musicEventA.Play(5);

        if (Input.GetKeyDown(KeyCode.S))
            musicEventB.Play(5);

        if (Input.GetKeyDown(KeyCode.D))
            musicEventC.Play(5);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MusicManager.Instance.IncreaseLayer();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            MusicManager.Instance.DecreaseLayer();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MusicManager.Instance.IncreaseLayer(5);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MusicManager.Instance.DecreaseLayer(5);

        if (Input.GetKeyDown(KeyCode.W))
            MusicManager.Instance.Stop();

        if (Input.GetKeyDown(KeyCode.X))
            MusicManager.Instance.Stop(5);

        if (Input.GetKeyDown(KeyCode.O))
            SFXEventA.Play();

        if (Input.GetKeyDown(KeyCode.P))
            SFXEventB.Play();

        if (Input.GetKeyDown(KeyCode.L))
            SFXEventA.Stop();

        if (Input.GetKeyDown(KeyCode.M))
            SFXEventB.Stop();
    }

}
