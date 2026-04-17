using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BulkLoader : MonoBehaviour
{
    public List<ObjectLoadOptions> objetosACargar = new List<ObjectLoadOptions>();
    public List<LoadedObjectData> objetosCargados = new List<LoadedObjectData>();

    void Start()
    {
        Loader loader = new Loader();

        foreach (var options in objetosACargar)
        {
            string path = Path.Combine(Application.dataPath, options.path);

            LoadedObjectData obj = loader.LoadObj(
                options.name,
                path,
                options.shader.name,
                options.color,
                options.position,
                options.rotation,
                options.scale,
                this.transform
            );

            objetosCargados.Add(obj);    
        }
    }

    void Update()
    {
        var curCam = GameManager.instance.currentCamera;
        var vm = curCam.GetViewMatrix();
        var pm = curCam.GetProjectionMatrix();

        foreach (var obj in objetosCargados)
        {
            var material = obj.MeshRenderer.material;
            var orig = objetosACargar.Find(o => o.name == obj.Name);
            var pos = orig.position;
            var rot = orig.rotation;
            var sca = orig.scale;

            var mm = Matrices.CreateModelMatrix(pos, rot, sca);

            // Por ahora las posiciones de los objetos no cambian,
            // por lo que no es necesario actualizar su ModelMatrix
            material.SetMatrix("_ModelMatrix", mm);
            material.SetMatrix("_ViewMatrix", vm);
            material.SetMatrix("_ProjectionMatrix", pm);
        }
    }
}