using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class FileReader
{
    public ObjData Read(string objPath)
    {
        if (!File.Exists(objPath))
        {
            Debug.LogError("No se encontró el archivo: " + objPath);
            return null;
        }

        string fileData = File.ReadAllText(objPath);
        return ParseObj(fileData);
    }

    private ObjData ParseObj(string fileData)
    {
        List<Vector3> verticesList = new List<Vector3>();
        List<int> trianglesList = new List<int>();

        float minX = 0f, maxX = 0f;
        float minY = 0f, maxY = 0f;
        float minZ = 0f, maxZ = 0f;
        bool firstVertex = true;

        string[] lines = fileData.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (line.StartsWith("v "))
            {
                string[] coord = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                float x = float.Parse(coord[1], CultureInfo.InvariantCulture);
                float y = float.Parse(coord[2], CultureInfo.InvariantCulture);
                float z = float.Parse(coord[3], CultureInfo.InvariantCulture);

                if (firstVertex)
                {
                    firstVertex = false;
                    minX = maxX = x;
                    minY = maxY = y;
                    minZ = maxZ = z;
                }
                else
                {
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                    if (z < minZ) minZ = z;
                    if (z > maxZ) maxZ = z;
                }

                verticesList.Add(new Vector3(x, y, z));
            }
            else if (line.StartsWith("f "))
            {
                string[] cara = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (cara.Length == 5) // quad
                {
                    int[] quadIndices = new int[4];

                    for (int j = 0; j < 4; j++)
                    {
                        string[] partes = cara[j + 1].Split('/');
                        quadIndices[j] = int.Parse(partes[0]) - 1;
                    }

                    trianglesList.Add(quadIndices[0]);
                    trianglesList.Add(quadIndices[1]);
                    trianglesList.Add(quadIndices[2]);

                    trianglesList.Add(quadIndices[0]);
                    trianglesList.Add(quadIndices[2]);
                    trianglesList.Add(quadIndices[3]);
                }
                else if (cara.Length == 4) // triángulo
                {
                    for (int j = 0; j < 3; j++)
                    {
                        string[] partes = cara[j + 1].Split('/');
                        trianglesList.Add(int.Parse(partes[0]) - 1);
                    }
                }
            }
        }

        // Centrar en X y Z, como hacías vos
        float reposx = (minX + maxX) / 2f;
        float reposz = (minZ + maxZ) / 2f;

        for (int i = 0; i < verticesList.Count; i++)
        {
            Vector3 v = verticesList[i];
            v.x -= reposx;
            v.z -= reposz;
            verticesList[i] = v;
        }

        return new ObjData
        {
            Vertices = verticesList.ToArray(),
            Triangles = trianglesList.ToArray()
        };
    }
}

public class ObjData
{
    public Vector3[] Vertices;
    public int[] Triangles;
}