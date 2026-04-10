using System.IO;
using UnityEngine;
public class LoadedObjectData
{
    public string Name;
    public string Path;
    public GameObject GameObject;
    public Mesh Mesh;
    public MeshFilter MeshFilter;
    public MeshRenderer MeshRenderer;
    public Material Material;
    public Vector3[] Vertices;
    public int[] Triangles;
    public Color[] Colors;
}
public class Loader
{
    public string objPath;
    public LoadedObjectData LoadObj(string objPath, string shaderName, Color color)
    {
        FileReader reader = new FileReader();
        ObjData objData = reader.Read(objPath);

        if (objData == null)
        {
            return null;
        }

        string objectName = Path.GetFileNameWithoutExtension(objPath);

        GameObject obj = new GameObject(objectName);

        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.name = objectName + "_Mesh";
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
        material.color = color;
        //Aca deberiamos ver el tema de la MVPmatrix
        meshRenderer.material = material;

        LoadedObjectData loadedData = new LoadedObjectData
        {
            Name = objectName,
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