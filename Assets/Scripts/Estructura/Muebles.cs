using System.IO;
using UnityEngine;

public class Muebles : MonoBehaviour
{
    void Start()
    {
        //Creamos loader
        Loader loader1 = new Loader();
        //Cargamos silla
        string path = Path.Combine(Application.dataPath, "ModelosOBJ/chairs/chair3/chair3.obj");
        LoadedObjectData modelo = loader1.LoadObj(path, "Standard", Color.gray);

        //Cargamos mesa
        string path2 = Path.Combine(Application.dataPath, "ModelosOBJ/tables/table/table.obj");
        LoadedObjectData modelo2 = loader1.LoadObj(path2, "Standard", Color.gray);

    }
}