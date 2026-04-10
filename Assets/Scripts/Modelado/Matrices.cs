using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class Matrices
{
    private Matrix4x4 CreateModelMatrix(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
    {
        Matrix4x4 positionMatrix = new Matrix4x4(
            new Vector4(1f, 0f, 0f, newPosition.x), //Primera columna
            new Vector4(0f, 1f, 0f, newPosition.y),
            new Vector4(0f, 0f, 1f, newPosition.z),
            new Vector4(0f, 0f, 0f, 1f)
        );

        positionMatrix = positionMatrix.transpose;

        Matrix4x4 rotationMatrixX = new Matrix4x4(
            new Vector4(1f, 0f, 0f, 0f),
            new Vector4(0f, Mathf.Cos(newRotation.x), -Mathf.Sin(newRotation.x), 0f),
            new Vector4(0f, Mathf.Sin(newRotation.x), Mathf.Cos(newRotation.x), 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );

        Matrix4x4 rotationMatrixY = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.y), 0f, Mathf.Sin(newRotation.y), 0f),
            new Vector4(0f, 1f, 0f, 0f),
            new Vector4(-Mathf.Sin(newRotation.y), 0f, Mathf.Cos(newRotation.y), 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );

        Matrix4x4 rotationMatrixZ = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.z), -Mathf.Sin(newRotation.z), 0f, 0f),
            new Vector4(Mathf.Sin(newRotation.z), Mathf.Cos(newRotation.z), 0f, 0f),
            new Vector4(0f, 0f, 1f, 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );

        Matrix4x4 rotationMatrix = rotationMatrixZ * rotationMatrixY * rotationMatrixX;
        rotationMatrix = rotationMatrix.transpose;

        Matrix4x4 scaleMatrix = new Matrix4x4(
            new Vector4(newScale.x, 0f, 0f, 0f),
            new Vector4(0f, newScale.y, 0f, 0f),
            new Vector4(0f, 0f, newScale.z, 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );

        scaleMatrix = scaleMatrix.transpose;

        Matrix4x4 finalMatrix = positionMatrix;
        finalMatrix *= rotationMatrix;
        finalMatrix *= scaleMatrix;
        return finalMatrix;
    }

    private Matrix4x4 CreateViewMatrix(Vector3 pos, Vector3 target, Vector3 up)
    {
        // Target apunta al objeto a ver y Pos apunta a la cámara
        // Target-pos apunta al objeto desde la cámara
        Vector3 forward = (target-pos).normalized;

        // Eje de right es perpendicular al forward y al up
        Vector3 right = Vector3.Cross(forward, up).normalized;
        Vector3 newUp = Vector3.Cross(forward, right);

        Matrix4x4 vista = new Matrix4x4
        (
            new Vector4(right.x, newUp.x, -forward.x, 0f),
            new Vector4(right.y, newUp.y, -forward.y, 0f),
            new Vector4(right.z, newUp.z, -forward.z, 0f),
            new Vector4(-Vector3.Dot(right, pos), -Vector3.Dot(newUp, pos), Vector3.Dot(forward, pos), 1f)
        );
        return vista;
    }

    private Matrix4x4 CreateProjectionMatrix(float fov, float aspect, float nearClip, float farClip)
    {
        var invTan = 1f / Mathf.Tan(Mathf.Deg2Rad * fov / 2);
        var diffNearFar = nearClip - farClip;

        return new Matrix4x4(
            new Vector4(invTan / aspect,                  0,                                    0,                                  0),
            new Vector4(              0,             invTan,                                    0,                                  0),
            new Vector4(              0,                  0,   (nearClip + farClip) / diffNearFar,   2*nearClip*farClip / diffNearFar),
            new Vector4(              0,                  0,                                   -1,                                  0)
        );
    }

    public void RecalcularMatrices(Vector3 newPosition, Vector3 newRotation, Vector3 newScale, Vector3 cameraPos, Vector3 target, Vector3 up, GameObject gameObject)
    {
        Matrix4x4 modelMatrix, viewMatrix, projMatrix;

        modelMatrix = CreateModelMatrix(newPosition, newRotation, newScale);
        gameObject.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix);
        viewMatrix = CreateViewMatrix(cameraPos, target, up);
        gameObject.GetComponent<Renderer>().material.SetMatrix("_ViewMatrix", viewMatrix);
        projMatrix = CreateProjectionMatrix(60, 16f/9f, 0.01f, 100f);
        gameObject.GetComponent<Renderer>().material.SetMatrix("_ProjectionMatrix", GL.GetGPUProjectionMatrix(projMatrix.transpose, true));
    }
}
