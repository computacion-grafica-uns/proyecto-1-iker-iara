using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PisoDepartamento : MonoBehaviour
{
    public Vector3[] vertices;
    public int[] triangulos;

    void Start()
    {
        GenerarPiso();
    }

    void GenerarPiso()
    {
        float ancho = 5f;      // eje X
        float largo = 6f;      // eje Z
        float espesor = 0.1f;  // grosor del piso

        float yAbajo = -espesor;
        float yArriba = 0f;

        vertices = new Vector3[]
        {
            // Cara superior
            new Vector3(0f,     yArriba, 0f),     // 0
            new Vector3(ancho,  yArriba, 0f),     // 1
            new Vector3(ancho,  yArriba, largo),  // 2
            new Vector3(0f,     yArriba, largo),  // 3

            // Cara inferior
            new Vector3(0f,     yAbajo, 0f),      // 4
            new Vector3(ancho,  yAbajo, 0f),      // 5
            new Vector3(ancho,  yAbajo, largo),   // 6
            new Vector3(0f,     yAbajo, largo)    // 7
        };

        triangulos = new int[]
        {
            //superior
            0, 1, 2,
            0, 2, 3,

            //inferior
            5, 4, 7,
            5, 7, 6,

            //frente
            4, 5, 1,
            4, 1, 0,

            //atras
            3, 2, 6,
            3, 6, 7,

            //izq
            4, 0, 3,
            4, 3, 7,

            //derecha
            1, 5, 6,
            1, 6, 2
        };

        Mesh mesh = new Mesh();
        mesh.name = "PisoDepartamento";
        mesh.vertices = vertices;
        mesh.triangles = triangulos;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;

        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Lighting/SingleColor"));
        mr.material.SetColor("_MaterialColor", new Color(0.96f, 0.90f, 0.78f, 1f));
    }
}