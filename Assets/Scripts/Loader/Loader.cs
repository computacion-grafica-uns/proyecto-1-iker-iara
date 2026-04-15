using System.IO;
using UnityEngine;

public class Loader
{
    public LoadedObjectData LoadObj(
        string objName,
        string objPath,
        string shaderName,
        Color color,
        Vector3 position,
        Vector3 rotation,
        Vector3 scale
    )
    {
        FileReader reader = new FileReader();
        ObjData objData = reader.Read(objPath);

        if (objData == null)
        {
            return null;
        }

        GameObject obj = new GameObject(objName);

        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.name = objName + "_Mesh";
        mesh.vertices = objData.Vertices;
        mesh.triangles = objData.Triangles;

        Color[] colors = new Color[objData.Vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }

        mesh.colors = colors;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;

        Shader shader = Shader.Find(shaderName);
        if (shader == null)
        {
            Debug.LogWarning("No se encontró el shader: " + shaderName + ". Se usará Standard.");
            shader = Shader.Find("Standard");
        }

        Material material = new Material(shader);
        Matrix4x4 mm = Matrices.CreateModelMatrix(position, rotation, scale);

        material.color = color;
        material.SetMatrix("_ModelMatrix", mm);
        meshRenderer.material = material;

        LoadedObjectData loadedData = new LoadedObjectData
        {
            Name = objName,
            Path = objPath,
            GameObject = obj,
            Mesh = mesh,
            MeshFilter = meshFilter,
            MeshRenderer = meshRenderer,
            Material = material,
            Vertices = objData.Vertices,
            Triangles = objData.Triangles,
            Colors = colors
        };

        return loadedData;
    }
}