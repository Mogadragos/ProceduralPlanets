using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;

    public float planetRadius = 1;

    ShapeGenerator shapeGenerator;
    ColorGenerator colorGenerator;

    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    float degreesPerSecond;

    private void Awake()
    {
        GeneratePlanet();
    }

    private void Update() 
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }

    public void GeneratePlanet()
    {
        Initialise();
        GenerateMesh();
        GenerateColors();
    }

    void Initialise()
    {
        transform.parent.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        degreesPerSecond = Random.Range(5, 50);

        shapeGenerator = new ShapeGenerator(planetRadius);

        colorGenerator = new ColorGenerator();

        meshFilters = new MeshFilter[6];

        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;
            meshObj.transform.localPosition = Vector3.zero;
            meshObj.transform.localRotation = Quaternion.identity;
            meshObj.transform.localScale = Vector3.one;

            meshObj.AddComponent<MeshRenderer>();
            meshFilters[i] = meshObj.AddComponent<MeshFilter>();
            meshFilters[i].sharedMesh = new Mesh();

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorGenerator.material;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        for(int i = 0; i < 6; i++)
        {
            terrainFaces[i].ConstructMesh();
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors()
    {
        colorGenerator.UpdateColors();
    }
}