using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorScript : Listener
{

    GameObject[,] map = new GameObject[6, 6];
    public List<GameObject> SpawnedTrees = new List<GameObject>();
    public List<GameObject> SpawnedSoldiers = new List<GameObject>();
    [SerializeField] GameObject Trees;
    [SerializeField] GameObject Soldiers;
    float timeTillSpawn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameManager.GameState.Playing && SpawnedSoldiers.Count < 5)
        {
            timeTillSpawn -= Time.deltaTime;
        }

        if (timeTillSpawn <= 0)
        {
            SpawnSoldier();
        }
    }

    public void GenerateMap()
    {
        
        SpawnNewTrees();
        SpawnNewSoldiers();

    }

    public void SpawnNewTrees()
    {
        //Destroying current trees
        foreach (GameObject tree in SpawnedTrees)
        {
            Destroy(tree);
        }

        //Spawning Trees
        int x;
        int y;

        for (int i = 0; i < Random.Range(4, 6); i++)
        {
            x = Random.Range(0, 6);
            y = Random.Range(0, 6);
            while (map[x, y] != null)
            {
                x = Random.Range(0, 6);
                y = Random.Range(0, 6);
            }
            map[x, y] = Instantiate(Trees, new Vector3(1.5f * x + Random.Range(0.3f, 0.6f) + transform.position.x, 1.5f * y + Random.Range(0.3f, 0.6f) + transform.position.y, 0), transform.rotation);
            SpawnedTrees.Add(map[x, y]);
            
        }
    }

    public void SpawnNewSoldiers()
    {
        foreach (GameObject soldier in SpawnedSoldiers)
        {
            Destroy(soldier);
        }

        SpawnSoldier();
    }

    public void SpawnSoldier()
    {
            int x = Random.Range(0, 6);
            int y = Random.Range(0, 6);
            while (map[x, y] != null)
            {
                x = Random.Range(0, 6);
                y = Random.Range(0, 6);
            }
            map[x, y] = Instantiate(Soldiers, new Vector3(1.5f * x + Random.Range(0.3f, 0.6f) + transform.position.x, 1.5f * y + Random.Range(0.3f, 0.6f) + transform.position.y, 0), transform.rotation);
            SpawnedSoldiers.Add(map[x, y]);
        map[x, y].GetComponent<SoldierController>().MapGen = this;

        timeTillSpawn = Random.Range(1.0f, 3.0f);
    }

    public override void GameReset()
    {
        GenerateMap();
    }
}
