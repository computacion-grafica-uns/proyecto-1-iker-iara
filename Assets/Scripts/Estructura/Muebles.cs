using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Muebles : MonoBehaviour
{
    public List<LoadedObjectData> objetosCargados = new List<LoadedObjectData>();

    void Start()
    {
        Loader loader1 = new Loader();

        string path = Path.Combine(Application.dataPath, "ModelosOBJ/chairs/chair3/chair3.obj");
        LoadedObjectData silla = loader1.LoadObj(
            "Silla",
            path,
            "ShaderMVP",
            new Color(0.5f, 0.25f, 0f),   // marrón
            new Vector3(1f, 0f, -0.8f),   // posición
            new Vector3(0f, 90f, 0f),     // rotación
            new Vector3(0.77f, 0.77f, 0.77f) // escala
        );
        objetosCargados.Add(silla);

        string path2 = Path.Combine(Application.dataPath, "ModelosOBJ/tables/table/table.obj");
        LoadedObjectData mesa = loader1.LoadObj(
            "Mesa",
            path2,
            "ShaderMVP",
            new Color(0.25f, 0.12f, 0f),  // marrón oscuro
            new Vector3(1f, 0f, -1.4f),   // posición
            Vector3.zero,                 // rotación
            new Vector3(0.55f, 0.55f, 0.55f) // escala
        );
        objetosCargados.Add(mesa);

        //
        // ESTRUCTURA:
        // De momento lo dejo acá, pero despues sería mejor moverlo a un script aparte, para poder desactivar
        // las paredes, el techo, el piso, etc.
        //
        LoadedObjectData paredes = loader1.LoadObj(
            "Paredes",
            Path.Combine(Application.dataPath, "ModelosOBJ/Estructura/Paredes.obj"),
            "ShaderMVP",
            new Color(0.5f, 0.5f, 0.5f), // gris
            new Vector3(0f, 0f, 0f),     // posición
            Vector3.zero,                // rotación
            new Vector3(1f, 1f, 1f)      // escala
        );
        objetosCargados.Add(paredes);

        LoadedObjectData techo = loader1.LoadObj(
            "Techo",
            Path.Combine(Application.dataPath, "ModelosOBJ/Estructura/Techo.obj"),
            "ShaderMVP",
            new Color(0.1f, 0.1f, 0.1f), // gris oscuro
            new Vector3(0f, 2.4f, 0f),   // posición
            Vector3.zero,                // rotación
            new Vector3(1f, 1f, 1f)      // escala
        );
        objetosCargados.Add(techo);

        LoadedObjectData piso = loader1.LoadObj(
            "Piso",
            Path.Combine(Application.dataPath, "ModelosOBJ/Estructura/Piso.obj"),
            "ShaderMVP",
            new Color(0.9f, 0.9f, 0.4f), // amarillo claro
            new Vector3(0f, 0f, 0f),     // posición
            Vector3.zero,                // rotación
            new Vector3(1f, 1f, 1f)      // escala
        );
        objetosCargados.Add(piso);
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