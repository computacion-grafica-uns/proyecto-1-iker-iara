using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Paredes : MonoBehaviour
{
    public Vector3[] vertices;
    public int[] caras;

    void Start()
    {
        GenerarParedes();
    }

    void GenerarParedes()
    {
        // Medidas
        float ancho = 5f;      // eje X
        float alto = 2.5f;     // eje Y
        float largo = 6f;      // eje Z
        float espesor = 0.2f;  // grosor de las paredes

        List<Vector3> listaVertices = new List<Vector3>();
        List<int> listaCaras = new List<int>();

        // Pared frontal
        AgregarCaja(
            listaVertices,
            listaCaras,
            new Vector3(0f, 0f, 0f),
            new Vector3(ancho, alto, espesor)
        );

        // Pared trasera
        AgregarCaja(
            listaVertices,
            listaCaras,
            new Vector3(0f, 0f, largo - espesor),
            new Vector3(ancho, alto, espesor)
        );

        // Pared izquierda
        AgregarCaja(
            listaVertices,
            listaCaras,
            new Vector3(0f, 0f, espesor),
            new Vector3(espesor, alto, largo - 2f * espesor)
        );

        // Pared derecha
        AgregarCaja(
            listaVertices,
            listaCaras,
            new Vector3(ancho - espesor, 0f, espesor),
            new Vector3(espesor, alto, largo - 2f * espesor)
        );

        vertices = listaVertices.ToArray();
        caras = listaCaras.ToArray();

        Mesh mesh = new Mesh();
        mesh.name = "ParedesDepartamento";

        mesh.vertices = vertices;
        mesh.triangles = caras;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;

        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("ShaderBasico"));
    }

    void AgregarCaja(List<Vector3> listaVertices, List<int> listaCaras, Vector3 origen, Vector3 tam)
    {
        Vector3 min = origen;
        Vector3 max = origen + tam;

        Vector3 v0 = new Vector3(min.x, min.y, min.z);
        Vector3 v1 = new Vector3(max.x, min.y, min.z);
        Vector3 v2 = new Vector3(max.x, max.y, min.z);
        Vector3 v3 = new Vector3(min.x, max.y, min.z);

        Vector3 v4 = new Vector3(min.x, min.y, max.z);
        Vector3 v5 = new Vector3(max.x, min.y, max.z);
        Vector3 v6 = new Vector3(max.x, max.y, max.z);
        Vector3 v7 = new Vector3(min.x, max.y, max.z);

        AgregarQuad(listaVertices, listaCaras, v0, v3, v2, v1);
        AgregarQuad(listaVertices, listaCaras, v5, v6, v7, v4);
        AgregarQuad(listaVertices, listaCaras, v4, v7, v3, v0);
        AgregarQuad(listaVertices, listaCaras, v1, v2, v6, v5);
        AgregarQuad(listaVertices, listaCaras, v3, v7, v6, v2);
        AgregarQuad(listaVertices, listaCaras, v4, v0, v1, v5);
    }

    void AgregarQuad(List<Vector3> listaVertices, List<int> listaCaras, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        int i = listaVertices.Count;

        listaVertices.Add(a);
        listaVertices.Add(b);
        listaVertices.Add(c);
        listaVertices.Add(d);

        listaCaras.Add(i + 0);
        listaCaras.Add(i + 1);
        listaCaras.Add(i + 2);

        listaCaras.Add(i + 0);
        listaCaras.Add(i + 2);
        listaCaras.Add(i + 3);
    }
}