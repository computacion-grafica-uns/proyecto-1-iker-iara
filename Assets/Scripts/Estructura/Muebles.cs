using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Muebles : MonoBehaviour
{
    public List<LoadedObjectData> objetosCargados = new List<LoadedObjectData>();

    private Color marron = new Color(0.6f, 0.3f, 0.1f);

    void Start()
    {
        Loader loader1 = new Loader();

        string path = Path.Combine(Application.dataPath, "ModelosOBJ/chairs/chair3/chair3.obj");
        LoadedObjectData silla = loader1.LoadObj(
            path,
            "ShaderMVP",
            marron,
            new Vector3(2f, 0f, 5f),      // posición
            Vector3.zero,                 // rotación
            new Vector3(0.77f, 0.77f, 0.77f),                  // escala
            new Vector3(0f, 2f, -10f),    // cámara
            new Vector3(0f, 0f, 5f),      // target
            Vector3.up                    // up
        );
        objetosCargados.Add(silla);

        string path2 = Path.Combine(Application.dataPath, "ModelosOBJ/tables/table/table.obj");
        LoadedObjectData mesa = loader1.LoadObj(
            path2,
            "ShaderMVP",
            marron,
            new Vector3(3f, 0f, 5f),      // posición
            Vector3.zero,                 // rotación
            new Vector3(0.55f, 0.55f, 0.55f), // escala
            new Vector3(0f, 2f, -10f),    // cámara
            new Vector3(3f, 0f, 5f),      // target
            Vector3.up                    // up
        );
        objetosCargados.Add(mesa);
    }

    void Update()
    {
        var curCam = GameManager.instance.currentCamera;
        var vm = curCam.GetViewMatrix();
        var pm = curCam.GetProjectionMatrix();

        foreach (var obj in objetosCargados)
        {
            var material = obj.MeshRenderer.material;
            // Si las posiciones de los objetos no cambian, no es necesario actualizar su ModelMatrix
            material.SetMatrix("_ViewMatrix", vm);
            material.SetMatrix("_ProjectionMatrix", pm);
        }
    }
}