using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Justin_Math
{
    public static float VolumeOfMesh(Mesh mesh)
    {
        // calculate volume of tetrahedron
        // 1/6 (v1 cross v2) dot v3
        float volume = 0;

        int[] indices = mesh.GetIndices(0);
        Vector3[] vertices = mesh.vertices;
        mesh.GetTriangles(0);

        // for every triangle in mesh
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            // get vertices of triangle
            Vector3 v1 = mesh.vertices[mesh.triangles[i + 0]];
            Vector3 v2 = mesh.vertices[mesh.triangles[i + 1]];
            Vector3 v3 = mesh.vertices[mesh.triangles[i + 2]];

            // cross v1 v2
            Vector3 cross = Vector3.Cross(v1, v2);
            // dot result with v3
            volume += Vector3.Dot(cross, v3);
        }

        // total final volume /6
        volume = Mathf.Abs(volume / 6.0f);
        return volume;
    }

    public static float VolumeOfObject(GameObject go)
    {
        Mesh mesh = go.GetComponent<MeshFilter>().mesh;
        if (!(go.GetComponent<MeshFilter>())) return -1;
        // calculate volume of tetrahedron
        // 1/6 (v1 cross v2) dot v3
        float volume = 0;

        int[] indices = mesh.GetIndices(0);
        Vector3[] vertices = mesh.vertices;
        mesh.GetTriangles(0);

        // for every triangle in mesh
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            // get vertices of triangle
            Vector3 v1 = mesh.vertices[mesh.triangles[i + 0]];
            Vector3 v2 = mesh.vertices[mesh.triangles[i + 1]];
            Vector3 v3 = mesh.vertices[mesh.triangles[i + 2]];

            //adjust for go scale
            v1.x *= go.transform.localScale.x;
            v2.x *= go.transform.localScale.x;
            v3.x *= go.transform.localScale.x;

            v1.y *= go.transform.localScale.y;
            v2.y *= go.transform.localScale.y;
            v3.y *= go.transform.localScale.y;

            v1.z *= go.transform.localScale.z;
            v2.z *= go.transform.localScale.z;
            v3.z *= go.transform.localScale.z;

            // cross v1 v2
            Vector3 cross = Vector3.Cross(v1, v2);
            // dot result with v3
            volume += Vector3.Dot(cross, v3);
        }

        // total final volume /6
        volume = Mathf.Abs(volume / 6.0f);
        return volume;
    }
}
