using UnityEngine;

[System.Serializable] // Para poder mostrar esta clase en el inspector de Unity
public class ObjectLoadOptions
{
    public string name;
    public string path;

    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 scale = new Vector3(1,1,1);

    public Shader shader;

    public Color color;
}