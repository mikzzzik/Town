using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;

    [SerializeField] private int _deep = 20;
    [SerializeField] private int _width = 256;
    [SerializeField] private int _height = 256;

    [SerializeField] private float _scale = 20f;

    public void Generate()
    {
        _terrain.terrainData = GenerationTerrain(_terrain.terrainData);
    }

    private TerrainData GenerationTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = _width + 1;

        terrainData.size = new Vector3(_width, _deep, _height);

        terrainData.SetHeights(0, 0, GenerateHeight());
        
        return terrainData;
    }

    private float[,] GenerateHeight()
    {
        float[,] heights = new float[_width, _height];

        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {
                heights[x, y] = CalculateWeight(x, y);

            }
        }

        return heights;
    }

    private float CalculateWeight(int x, int y)
    {
        float xCoord = (float)x / _width * _scale;
        float yCoord = (float)y / _height * _scale;
        
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
