using UnityEngine;

// G�n�rateur et gestionnaire d'une plan�te
public class Planet : MonoBehaviour
{
    // R�solution
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

    // La plan�te tourne sur elle-m�me
    private void Update() 
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }

    // Dessine le cercle et g�n�re la plan�te
    public void GeneratePlanet(float distanceToSun, float relativeDistanceToSun)
    {
        drawCircle.Draw(distanceToSun);
        this.relativeDistanceToSun = relativeDistanceToSun;

        Initialise();
        GenerateMesh();
        GenerateColors();
    }

    // Pr�pare les g�n�rateurs et les Mesh
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

    // G�n�re les Mesh
    void GenerateMesh()
    {
        for(int i = 0; i < 6; i++)
        {
            terrainFaces[i].ConstructMesh();
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    // G�n�re les couleurs
    void GenerateColors()
    {
        colorGenerator.UpdateColors();
    }
}