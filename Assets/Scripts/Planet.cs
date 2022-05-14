using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 256;

    ShapeGenerator shapeGenerator;
    ColorGenerator colorGenerator;

    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    public float degreesPerSecond;

    public bool generateOnAwake = true;

    const float MAX_DISTANCE_TO_SUN = 300f;
    float relativeDistanceToSun;

    private void Awake()
    {
        if (generateOnAwake) GeneratePlanet();
    }

    private void Update() 
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }

    public void GeneratePlanet()
    {
        relativeDistanceToSun = 150f / MAX_DISTANCE_TO_SUN; // TODO: calculer

        Initialise();
        GenerateMesh();
        GenerateColors();
    }

    void Initialise()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        transform.parent.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        degreesPerSecond = Random.Range(1f, 15f);

        shapeGenerator = new ShapeGenerator(relativeDistanceToSun);

        colorGenerator = new ColorGenerator(relativeDistanceToSun);

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