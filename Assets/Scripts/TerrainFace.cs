using UnityEngine;

// Gestionnaire d'une face du cube formant la plan�te
public class TerrainFace
{
    // Gestionnaire du relief
    ShapeGenerator shapeGenerator;

    // Mesh de la face
    Mesh mesh;

    // R�solution demand�e
    int resolution;

    // Vecteurs de la face pour la g�n�ration des triangles
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        // Pour chaque point de la face 
        for(int y = 0; y < resolution; y++)
        {
            for(int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;

                // R�cup�ration du point sur le plan
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;

                // Normalisation -> R�cup�re les coordonn�es sur une sph�re
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;

                // R�cup�ration de l'�l�vation � ce point
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                // Si le triangle associ� n'est pas en dehors de la face, on le cr�e
                if(x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    triIndex += 6;
                }
            }
        }

        // On applique les nouvelles vertices et triangles au mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
