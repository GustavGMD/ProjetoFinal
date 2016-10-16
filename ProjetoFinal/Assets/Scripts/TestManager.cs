using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

    public GeneticAlgorithm geneticAlgorithm;
    public GameObject tile;
    public GameObject[][][] tileMap;
    public int[][][] map;

    public int generations = 10;

    public GameObject SingleTileObject;
    public GameObject MultiTileObject;

    public int size;

    public Vector2[][] PointsToBeIncluded;

    public float stopAt;

    private Renderer SingleTileObjectRenderer;
    private int[][] SingleTileObjectMap;

    private Renderer MultiTileObjectRenderer;
    //private int[][][] MultiTileObjectMap;

    // Use this for initialization
    void Start () {

        PointsToBeIncluded = new Vector2[9][];

        /**
        PointsToBeIncluded[0] = new Vector2[4];
        PointsToBeIncluded[0][0] = new Vector2(24, 49);
        PointsToBeIncluded[0][1] = new Vector2(25, 49);
        PointsToBeIncluded[0][2] = new Vector2(49, 24);
        PointsToBeIncluded[0][3] = new Vector2(49, 25);

        PointsToBeIncluded[1] = new Vector2[6];
        PointsToBeIncluded[1][0] = new Vector2(24, 49);
        PointsToBeIncluded[1][1] = new Vector2(25, 49);
        PointsToBeIncluded[1][2] = new Vector2(49, 24);
        PointsToBeIncluded[1][3] = new Vector2(49, 25);
        PointsToBeIncluded[1][4] = new Vector2(0, 24);
        PointsToBeIncluded[1][5] = new Vector2(0, 25);

        PointsToBeIncluded[2] = new Vector2[4];
        PointsToBeIncluded[2][0] = new Vector2(24, 49);
        PointsToBeIncluded[2][1] = new Vector2(25, 49);
        PointsToBeIncluded[2][2] = new Vector2(0, 24);
        PointsToBeIncluded[2][3] = new Vector2(0, 25);

        PointsToBeIncluded[3] = new Vector2[6];
        PointsToBeIncluded[3][0] = new Vector2(24, 49);
        PointsToBeIncluded[3][1] = new Vector2(25, 49);
        PointsToBeIncluded[3][2] = new Vector2(49, 24);
        PointsToBeIncluded[3][3] = new Vector2(49, 25);
        PointsToBeIncluded[3][4] = new Vector2(24, 0);
        PointsToBeIncluded[3][5] = new Vector2(25, 0);

        PointsToBeIncluded[4] = new Vector2[8];
        PointsToBeIncluded[4][0] = new Vector2(24, 49);
        PointsToBeIncluded[4][1] = new Vector2(25, 49);
        PointsToBeIncluded[4][2] = new Vector2(0, 24);
        PointsToBeIncluded[4][3] = new Vector2(0, 25);
        PointsToBeIncluded[4][4] = new Vector2(24, 0);
        PointsToBeIncluded[4][5] = new Vector2(25, 0);
        PointsToBeIncluded[4][6] = new Vector2(49, 24);
        PointsToBeIncluded[4][7] = new Vector2(49, 25);

        PointsToBeIncluded[5] = new Vector2[6];
        PointsToBeIncluded[5][0] = new Vector2(24, 49);
        PointsToBeIncluded[5][1] = new Vector2(25, 49);
        PointsToBeIncluded[5][2] = new Vector2(0, 24);
        PointsToBeIncluded[5][3] = new Vector2(0, 25);
        PointsToBeIncluded[5][4] = new Vector2(24, 0);
        PointsToBeIncluded[5][5] = new Vector2(25, 0);

        PointsToBeIncluded[6] = new Vector2[4];
        PointsToBeIncluded[6][0] = new Vector2(24, 0);
        PointsToBeIncluded[6][1] = new Vector2(25, 0);
        PointsToBeIncluded[6][2] = new Vector2(49, 24);
        PointsToBeIncluded[6][3] = new Vector2(49, 25);

        PointsToBeIncluded[7] = new Vector2[6];
        PointsToBeIncluded[7][0] = new Vector2(24, 0);
        PointsToBeIncluded[7][1] = new Vector2(25, 0);
        PointsToBeIncluded[7][2] = new Vector2(49, 24);
        PointsToBeIncluded[7][3] = new Vector2(49, 25);
        PointsToBeIncluded[7][4] = new Vector2(0, 24);
        PointsToBeIncluded[7][5] = new Vector2(0, 25);

        PointsToBeIncluded[8] = new Vector2[4];
        PointsToBeIncluded[8][0] = new Vector2(24, 0);
        PointsToBeIncluded[8][1] = new Vector2(25, 0);
        PointsToBeIncluded[8][2] = new Vector2(0, 24);
        PointsToBeIncluded[8][3] = new Vector2(0, 25);
        /**/

        PointsToBeIncluded[0] = new Vector2[4];
        PointsToBeIncluded[0][0] = new Vector2(24, 49);
        PointsToBeIncluded[0][1] = new Vector2(25, 49);
        PointsToBeIncluded[0][2] = new Vector2(49, 24);
        PointsToBeIncluded[0][3] = new Vector2(49, 25);

        PointsToBeIncluded[1] = new Vector2[6];
        PointsToBeIncluded[1][0] = new Vector2(24, 49);
        PointsToBeIncluded[1][1] = new Vector2(25, 49);
        PointsToBeIncluded[1][2] = new Vector2(49, 24);
        PointsToBeIncluded[1][3] = new Vector2(49, 25);
        PointsToBeIncluded[1][4] = new Vector2(0, 24);
        PointsToBeIncluded[1][5] = new Vector2(0, 25);

        PointsToBeIncluded[2] = new Vector2[2];
        PointsToBeIncluded[2][0] = new Vector2(24, 49);
        PointsToBeIncluded[2][1] = new Vector2(25, 49);

        PointsToBeIncluded[3] = new Vector2[2];
        PointsToBeIncluded[3][0] = new Vector2(49, 24);
        PointsToBeIncluded[3][1] = new Vector2(49, 25);

        PointsToBeIncluded[4] = new Vector2[4];
        PointsToBeIncluded[4][0] = new Vector2(24, 49);
        PointsToBeIncluded[4][1] = new Vector2(25, 49);
        PointsToBeIncluded[4][2] = new Vector2(0, 24);
        PointsToBeIncluded[4][3] = new Vector2(0, 25);

        PointsToBeIncluded[5] = new Vector2[2];
        PointsToBeIncluded[5][0] = new Vector2(24, 49);
        PointsToBeIncluded[5][1] = new Vector2(25, 49);

        PointsToBeIncluded[6] = new Vector2[2];
        PointsToBeIncluded[6][0] = new Vector2(49, 24);
        PointsToBeIncluded[6][1] = new Vector2(49, 25);

        PointsToBeIncluded[7] = new Vector2[6];
        PointsToBeIncluded[7][0] = new Vector2(24, 0);
        PointsToBeIncluded[7][1] = new Vector2(25, 0);
        PointsToBeIncluded[7][2] = new Vector2(49, 24);
        PointsToBeIncluded[7][3] = new Vector2(49, 25);
        PointsToBeIncluded[7][4] = new Vector2(0, 24);
        PointsToBeIncluded[7][5] = new Vector2(0, 25);

        PointsToBeIncluded[8] = new Vector2[4];
        PointsToBeIncluded[8][0] = new Vector2(24, 0);
        PointsToBeIncluded[8][1] = new Vector2(25, 0);
        PointsToBeIncluded[8][2] = new Vector2(0, 24);
        PointsToBeIncluded[8][3] = new Vector2(0, 25);

        /**/
        geneticAlgorithm.Initialize();
        size = geneticAlgorithm.mapSize;
        stopAt = (geneticAlgorithm.fitnessBaseValue * geneticAlgorithm.fitnessPointsWeight) * geneticAlgorithm.fitnessPoints.Length;

        /**
        tileMap = new GameObject[geneticAlgorithm.populationSize][][];
        for (int i = 0; i < tileMap.Length; i++)
        {
            tileMap[i] = new GameObject[size][];
        }
        for (int i = 0; i < tileMap.Length; i++)
        {
            for (int j = 0; j < tileMap[i].Length; j++)
            {
                tileMap[i][j] = new GameObject[size];
            }
        }

        for (int i = 0; i < tileMap.Length; i++)
        {
            for (int j = 0; j < tileMap[i].Length; j++)
            {
                for (int k = 0; k < tileMap[i][j].Length; k++)
                {
                    GameObject __temp = (GameObject)Instantiate(tile, new Vector3(k + (i * size), (size - j), 0), Quaternion.identity);
                    tileMap[i][j][k] = __temp;
                }
            }
        }
        /**/

        SingleTileObjectRenderer = SingleTileObject.GetComponent<Renderer>();
        //Texture2D __tempTex = new Texture2D(size, size);
        //SingleTileObjectRenderer.material.SetTexture("_MainTex", __tempTex); 
          
        MultiTileObjectRenderer = MultiTileObject.GetComponent<Renderer>();

        UpdateMapSingleTile();
        UpdateTilesSingleTile();
        /**/
    }
	
	// Update is called once per frame
	void Update () {
        /**/
        if (Input.GetKeyDown(KeyCode.A))
        {
            geneticAlgorithm.Simulate();

            Debug.Log("Generation: " + geneticAlgorithm.generation);
            Debug.Log(" Best Fitness: " + geneticAlgorithm.population[0].fitness);

            UpdateMapSingleTile();
            UpdateTilesSingleTile();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            //Debug.Log(Time.timeSinceLevelLoad);
            Texture2D __tempTex = new Texture2D(size * 3, size * 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int[][] __map;
                    geneticAlgorithm.Initialize();
                    geneticAlgorithm.fitnessPoints = PointsToBeIncluded[j + (i * 3)];
                    for (int k = 0; k < generations; k++)
                    {
                        geneticAlgorithm.Simulate();
                    }

                    //Debug.Log("Finished Simulation");

                    __map = CellularAutomata.FillExclaves(CellularAutomata.SimulateMap(geneticAlgorithm.population[0].GetMap(), 5));

                    for (int l = 0; l < __map.Length; l++)
                    {
                        for (int m = 0; m < __map[i].Length; m++)
                        {
                            __tempTex.SetPixel(m + (size*j), l + (size * i), __map[l][m] == 0 ? Color.black : Color.white);
                        }
                    }
                }

                //Debug.Log(Time.timeSinceLevelLoad);
                __tempTex.Apply();
                MultiTileObjectRenderer.material.SetTexture("_MainTex", __tempTex);
            }
            
        }
        /**/
    }

    public void UpdateMapSingleTile()
    {
        /**
        map = new int[geneticAlgorithm.populationSize][][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = CellularAutomata.FillExclaves(CellularAutomata.SimulateMap(geneticAlgorithm.population[i].GetMap(), 5));
            //map[i] = geneticAlgorithm.population[i].GetMap();
            //map[i] = CellularAutomata.SimulateMap(geneticAlgorithm.population[i].GetMap(), 5);
        }
        /**/

        SingleTileObjectMap = CellularAutomata.FillExclaves(CellularAutomata.SimulateMap(geneticAlgorithm.population[0].GetMap(), 5));
    }

    public void UpdateTilesSingleTile()
    {
        /**
        for (int i = 0; i < tileMap.Length; i++)
        {
            for (int j = 0; j < tileMap[i].Length; j++)
            {
                for (int k = 0; k < tileMap[i][j].Length; k++)
                {
                    tileMap[i][j][k].GetComponent<Renderer>().material.color = map[i][j][k] == 0 ? Color.black : Color.white;
                }
            }
        }
        /**/
        Texture2D __tempTex = new Texture2D(size, size);

        
        for (int i = 0; i < SingleTileObjectMap.Length; i++)
        {
            for (int j = 0; j < SingleTileObjectMap[i].Length; j++)
            {
                __tempTex.SetPixel(j, i, SingleTileObjectMap[i][j] == 0 ? Color.black : Color.white);
            }
        }
        
        __tempTex.Apply();

        SingleTileObjectRenderer.material.SetTexture("_MainTex", __tempTex);
    }

    public void GenerateNewRandomMap()
    {
        /**
        map = CellularAutomata.GetRandomBinaryMap(size - 2, size - 2, 0.48f);

        for (int i = 0; i < 5; i++)
        {
            CellularAutomata.SimulateMap(map);
        }  
        /**/      
    }
}
