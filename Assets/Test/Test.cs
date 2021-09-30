using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateTC;
using SFXTC;
using ObserverTC;
using EasingTC;
using PoolTC;
using MusicTC;
using UnityEngine.SceneManagement;
using PathFindingTC;

public class Test : MonoBehaviour
{
    public MusicEvent musicEventA;
    public MusicEvent musicEventB;
    public MusicEvent musicEventC;

    public SFXEvent SFXEventA;
    public SFXEvent SFXEventB;

    public GameObject testObj;
    float elapsedTime = 0;

    GridMap grid;
    public HeatMapVisual heatMapVisual;

    private void Awake()
    {
        grid = new GridMap(4, 2, 10f, new Vector3(-10, 0), 0, 200, this.transform);
        heatMapVisual.grid = grid;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            grid.AddValue(mousePos, 20);

        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            grid.SetValue(mousePos, 0);
        }
        //testObj.transform.position = new Vector3(EasingFunctions.EaseInOutElastic(-4, 4, elapsedTime, 3), 0, 0);
        elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.G))
            elapsedTime = 0;

        if (Input.GetKeyDown(KeyCode.H))
            SceneManager.LoadScene(1);

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
            MusicManager.Instance.IncreaseLayer(musicEventB);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            MusicManager.Instance.DecreaseLayer(musicEventB);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MusicManager.Instance.IncreaseLayer(musicEventB, 5);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MusicManager.Instance.DecreaseLayer(musicEventB, 5);

        if (Input.GetKeyDown(KeyCode.W))
            musicEventB.Stop();

        if (Input.GetKeyDown(KeyCode.X))
            musicEventB.Stop(5);

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
