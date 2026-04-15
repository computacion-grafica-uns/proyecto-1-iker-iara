using UnityEngine;

[System.Serializable] // Para poder mostrar esta clase en el inspector de Unity
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