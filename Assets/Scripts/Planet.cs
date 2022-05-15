using UnityEngine;

// Générateur et gestionnaire d'une planète
public class Planet : MonoBehaviour
{
    // Résolution
    [Range(2, 256)]
    public int resolution = 256;

    // Gestionnaires de formes et de couleurs
    ShapeGenerator shapeGenerator;
    ColorGenerator colorGenerator;

    // MeshFilters et Gestionnaires de chaque face
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    // Vitesse de rotation
    public float degreesPerSecond;

    // Distance au soleil relative
    [Range(0f, 1f)]
    public float relativeDistanceToSun = .5f;

    // Gestionnaire de cercles
    public DrawCircle drawCircle;

    // La planète tourne sur elle-même
    private void Update() 
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }

    // Dessine le cercle et génère la planète
    public void GeneratePlanet(float distanceToSun, float relativeDistanceToSun)
    {
        drawCircle.Draw(distanceToSun);
        this.relativeDistanceToSun = relativeDistanceToSun;

        Initialise();
        GenerateMesh();
        GenerateColors();
    }

    // Prépare les générateurs et les Mesh
    void Initialise()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

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

    // Génère les Mesh
    void GenerateMesh()
    {
        for(int i = 0; i < 6; i++)
        {
            terrainFaces[i].ConstructMesh();
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    // Génère les couleurs
    void GenerateColors()
    {
        colorGenerator.UpdateColors();
    }
}