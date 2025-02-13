using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SimpleTubeMesh : MonoBehaviour
{
    public float radius = 0.1f;
    public float height = 1f;
    public int radialSegments = 8;

    void Start()
    {
        CreateTubeMesh();
    }

    void CreateTubeMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(radialSegments + 1) * 2];
        int[] triangles = new int[radialSegments * 6];

        for (int i = 0; i <= radialSegments; i++)
        {
            float angle = i * 2 * Mathf.PI / radialSegments;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            vertices[i] = new Vector3(x, 0, z);
            vertices[i + radialSegments + 1] = new Vector3(x, height, z);
            
            if (i < radialSegments)
            {
                int start = i * 6;
                triangles[start] = i;
                triangles[start + 1] = i + radialSegments + 1;
                triangles[start + 2] = (i + 1) % (radialSegments + 1);
                triangles[start + 3] = (i + 1) % (radialSegments + 1);
                triangles[start + 4] = i + radialSegments + 1;
                triangles[start + 5] = (i + 1) % (radialSegments + 1) + radialSegments + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}