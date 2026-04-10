using System.IO;
using UnityEngine;

public class Muebles : MonoBehaviour
{
    private Color marron = new Color(0.6f, 0.3f, 0.1f);
    void Start()
    {
        Loader loader1 = new Loader();

        string path = Path.Combine(Application.dataPath, "ModelosOBJ/chairs/chair3/chair3.obj");
        LoadedObjectData silla = loader1.LoadObj(
            path,
            "ShaderBasico",
            marron,
            new Vector3(2f, 0f, 5f),      // posición
            Vector3.zero,                 // rotación
            new Vector3(0.77f, 0.77f, 0.77f),                  // escala
            new Vector3(0f, 2f, -10f),    // cámara
            new Vector3(0f, 0f, 5f),      // target
            -Vector3.up                    // up
        );

        string path2 = Path.Combine(Application.dataPath, "ModelosOBJ/tables/table/table.obj");
        LoadedObjectData mesa = loader1.LoadObj(
            path2,
            "ShaderBasico",
            marron,
            new Vector3(3f, 0f, 5f),      // posición
            Vector3.zero,                 // rotación
            new Vector3(0.55f, 0.55f, 0.55f), // escala
            new Vector3(0f, 2f, -10f),    // cámara
            new Vector3(3f, 0f, 5f),      // target
            -Vector3.up                    // up
        );
    }
}