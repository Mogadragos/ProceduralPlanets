using UnityEngine;

// Dessin du cercle (inspiré de https://www.codinblack.com/how-to-draw-lines-circles-or-anything-else-using-linerenderer/)
public class DrawCircle : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int vertexNumber = 50;

    public void Draw(float radius)
    {
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = lineRenderer.startWidth;
        lineRenderer.loop = true;

        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), 0, Mathf.Sin(angle * i), 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), 0, Mathf.Cos(angle * i), 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }
}
