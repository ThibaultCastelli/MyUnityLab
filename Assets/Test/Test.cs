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

public class GridObject
{
    public int maxValue;
    public int minValue;

    public bool value;

    public GridMap<GridObject> grid;
    public int x;
    public int y;

    public GridObject(GridMap<GridObject> grid, int x, int y)
    {

        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void Add(bool value)
    {
        this.value = value;
        grid.OnGridValueChanged(x, y);
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
public class Test : MonoBehaviour
{
    public MusicEvent musicEventA;
    public MusicEvent musicEventB;
    public MusicEvent musicEventC;

    public SFXEvent SFXEventA;
    public SFXEvent SFXEventB;

    public GameObject testObj;
    float elapsedTime = 0;

    PathFinding pathFinding;
    public HeatMapVisualInt heatMapVisual;

    private void Awake()
    {
        pathFinding = new PathFinding(new Vector3(-10, -10), 12, 8, 5);
        //heatMapVisual.grid = grid;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            int x, y;
            pathFinding.Grid.GetCoordonates(mousePos, out x, out y);
            List<PathNode> path = pathFinding.FindPath(0, 0, x, y);
            if (path == null)
                return;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].X, path[i].Y) * 5 + pathFinding.Grid.Origin + Vector3.one * 2.5f, new Vector3(path[i + 1].X, path[i + 1].Y) * 5 + pathFinding.Grid.Origin + Vector3.one * 2.5f, Color.green, 100f);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            
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
