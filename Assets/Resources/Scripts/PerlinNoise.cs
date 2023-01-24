using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoise : MonoBehaviour
{
    public bool generationComplete { get; private set; }

    public static List<GameObject> generatedEnemies = new List<GameObject>();

    public int width;
    public int height;
    public float enemySpawnRate;

    public GameObject groundTile;
    public GameObject seaTile;
    public GameObject mountainTile;

    public GameObject groundNestPrefab;
    public GameObject seaNestPrefab;
    public GameObject mountainNestPrefab;


    [SerializeField] private float seaLevel = 0.5f;
    [SerializeField] private float mountainLevel = 0.9f;
    [SerializeField] private float scale = 20f;
    [SerializeField] private int nestsAmount = 10;
 
    private float xOffset;
    private float yOffset;

    private List<List<int>> noiseGrid = new List<List<int>>();
    private List<List<GameObject>> tileGrid = new List<List<GameObject>>();

    private Dictionary<int, GameObject> tileset;
    private Dictionary<int, GameObject> tileGroups;

    private void Start()    
    {
        width = WorldData.worldWidth;
        height = WorldData.worldHeight;
        enemySpawnRate = WorldData.worldEnemyRate;

        xOffset = (float)Math.Round(Random.Range(0, 214748f));
        yOffset = (float)Math.Round(Random.Range(0, 214748f));

        Debug.Log("World data is:");
        Debug.Log(string.Format("World size: {0} x {1}", WorldData.worldWidth, WorldData.worldHeight));
        Debug.Log(string.Format("World spawn rate: {0}", WorldData.worldEnemyRate));

        CreateTileset();
        CreateTileGroups();
        MapGeneration();
        NestGeneration();

        generationComplete = true;
    }

    private void CreateTileset()
    {
        tileset = new Dictionary<int, GameObject>();

        tileset.Add(0, groundTile);
        tileset.Add(1, seaTile);
        tileset.Add(2, mountainTile);
    }

    private void CreateTileGroups()
    {
        tileGroups = new Dictionary<int, GameObject>();
        foreach(KeyValuePair<int, GameObject> kvp in tileset)
        {
            GameObject tileGroup = new GameObject(kvp.Value.name);
            tileGroup.transform.parent = gameObject.transform;
            tileGroup.transform.localPosition = new Vector3(0, 0, 0);
            tileGroups.Add(kvp.Key, tileGroup);
        }
    }

    private void MapGeneration()
    {
        for (int x = 0; x < width; x++)
        {
            noiseGrid.Add(new List<int>());
            tileGrid.Add(new List<GameObject>());
            
            for (int y = 0; y < height; y++)
            {
                int tileID = TileCalculation(x, y);
                noiseGrid[x].Add(tileID);
                CreateTile(tileID, x, y);
            }
        }
    }

    private int TileCalculation(int x, int y)
    {
        float xPerlin = (float)x / width  * scale + xOffset;
        float yPerlin = (float)y / height * scale + yOffset;
        
        float classicPerlin = Mathf.PerlinNoise(xPerlin, yPerlin);

        int tileID;

        if (classicPerlin < seaLevel)
        {
            tileID = 1;
        }
        else if (classicPerlin > mountainLevel)
        {
            tileID = 2;
        }
        else
        {
            tileID = 0;
        }

        return tileID;
    }

    private void CreateTile(int tileID, int x, int y)
    {
        GameObject tilePrefab = tileset[tileID];
        GameObject tileGroup = tileGroups[tileID];
        GameObject tile = Instantiate(tilePrefab, tileGroup.transform);
        SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);
        tileRenderer.sortingLayerName = "Background";
        
        tileGrid[x].Add(tile);
    }

    private void NestGeneration()
    {
        GameObject nestParent = new GameObject("Nests");
        GameObject.Find("Nests").transform.parent = gameObject.transform;

        for (int i = 0; i < Math.Round(nestsAmount * enemySpawnRate * (width / WorldData.worldWidth)); i++)
        {
            double xRandom = Math.Round((float)Random.Range(0, WorldData.worldWidth - 1));
            double yRandom = Math.Round((float)Random.Range(0, WorldData.worldHeight - 1));
            GameObject nestTile = GameObject.Find(string.Format("tile_x{0}_y{1}", xRandom, yRandom));
            GameObject nest;
            string typePrefix = "";
            switch (nestTile.transform.parent.name)
            {
                case "GroundTile": 
                    nest = Instantiate(groundNestPrefab);
                    typePrefix = "ground";
                    break;
                case "SeaTile": 
                    nest = Instantiate(seaNestPrefab);
                    typePrefix = "sea";
                    break;
                case "MountainTile": 
                    nest = Instantiate(mountainNestPrefab);
                    typePrefix = "mountain";
                    break;
                default: nest = new GameObject("Missing tile");   break;
            }
            nest.transform.localPosition = new Vector3((int)xRandom, (int)yRandom, 1);
            SpriteRenderer nestRenderer = nest.GetComponent<SpriteRenderer>();
            nestRenderer.sortingLayerName = "GeneratedSprites";
            nest.transform.name = string.Format("{0}_nest_x{1}_y{2}", typePrefix, xRandom, yRandom);
            generatedEnemies.Add(nest);

            GameObject.Find(string.Format("{0}_nest_x{1}_y{2}", typePrefix, xRandom, yRandom)).transform.parent = GameObject.Find("Nests").transform;
        }

    }
}
