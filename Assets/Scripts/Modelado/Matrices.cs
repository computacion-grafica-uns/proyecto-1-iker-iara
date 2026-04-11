using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class Matrices
{
    public static Matrix4x4 GetTranslationMatrix(Vector3 position)
    {
        return new Matrix4x4(
            new Vector4(1,0,0,position.x),
            new Vector4(0,1,0,position.y),
            new Vector4(0,0,1,position.z),
            new Vector4(0,0,0,         1)
        );
    }

    public static Matrix4x4 GetScaleMatrix(Vector3 scale)
    {
        return new Matrix4x4(
            new Vector4(scale.x,      0,      0,0),
            new Vector4(      0,scale.y,      0,0),
            new Vector4(      0,      0,scale.z,0),
            new Vector4(      0,      0,      0,1)
        );
    }

    public static Matrix4x4 GetRotationMatrixFromEuler(Vector3 rotation)
    {
        var r = Mathf.Deg2Rad * rotation;

        var matrices = new Matrix4x4[] {
            new Matrix4x4(  // Rx
                new Vector4(1,             0,              0,0),
                new Vector4(0,Mathf.Cos(r.x),-Mathf.Sin(r.x),0),
                new Vector4(0,Mathf.Sin(r.x), Mathf.Cos(r.x),0),
                new Vector4(0,             0,              0,1)
            ),
            new Matrix4x4(  // Ry
                new Vector4( Mathf.Cos(r.y),0,Mathf.Sin(r.y),0),
                new Vector4(              0,1,             0,0),
                new Vector4(-Mathf.Sin(r.y),0,Mathf.Cos(r.y),0),
                new Vector4(              0,0,             0,1)
            ),
            new Matrix4x4(  // Rz
                new Vector4(Mathf.Cos(r.z),-Mathf.Sin(r.z),0,0),
                new Vector4(Mathf.Sin(r.z), Mathf.Cos(r.z),0,0),
                new Vector4(             0,              0,1,0),
                new Vector4(             0,              0,0,1)
            )
        };

        return matrices[2] * matrices[1] * matrices[0];
    }

    public static Matrix4x4 CreateModelMatrix(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
    {
        var translationMatrix = GetTranslationMatrix(newPosition);
        var rotationMatrix = GetRotationMatrixFromEuler(newRotation);
        var scaleMatrix = GetScaleMatrix(newScale);

        return translationMatrix.transpose * rotationMatrix.transpose * scaleMatrix.transpose;
    }

    public static Matrix4x4 CreateViewMatrixFromTargetPoint(Vector3 pos, Vector3 target, Vector3 up)
    {
        // Target apunta al objeto a ver y Pos apunta a la cámara
        // Target-pos apunta al objeto desde la cámara
        Vector3 forward = (target - pos).normalized;

        // Eje de right es perpendicular al forward y al up
        Vector3 right = Vector3.Cross(up, forward).normalized;
        Vector3 newUp = Vector3.Cross(forward, right).normalized;

        Matrix4x4 vista = new Matrix4x4
        (
            new Vector4(                 right.x,                  newUp.x,                 forward.x, 0f),
            new Vector4(                 right.y,                  newUp.y,                 forward.y, 0f),
            new Vector4(                 right.z,                  newUp.z,                 forward.z, 0f),
            new Vector4(-Vector3.Dot(right, pos), -Vector3.Dot(newUp, pos), Vector3.Dot(forward, pos), 1f)
        );

        Debug.DrawLine(pos,      newUp+pos, Color.green);
        Debug.DrawLine(pos, 3f*forward+pos, Color.blue);
        Debug.DrawLine(pos,      right+pos, Color.red);

        return vista;
    }

    public static Matrix4x4 CreateViewMatrixFromSphericalCoords(Vector3 pos, Vector2 yawPitch)
    {
        var yaw = yawPitch.x;
        var pitch = yawPitch.y;
        var cosP = Mathf.Cos(pitch);
        var sinP = Mathf.Sin(pitch);
        var cosY = Mathf.Cos(yaw);
        var sinY = Mathf.Sin(yaw);

        Matrix4x4 pitchRot = new(
            new Vector4(1f,   0f,    0f, 0f),
            new Vector4(0f, cosP, -sinP, 0f),
            new Vector4(0f, sinP,  cosP, 0f),
            new Vector4(0f,   0f,    0f, 1f)
        );

        Matrix4x4 yawRot = new(
            new Vector4( cosY, 0f, sinY, 0f),
            new Vector4(   0f, 1f,   0f, 0f),
            new Vector4(-sinY, 0f, cosY, 0f),
            new Vector4(   0f, 0f,   0f, 1f)
        );

        Matrix4x4 rotationMatrix = yawRot * pitchRot;

        Matrix4x4 positionMatrix = new(
            new Vector4(1f, 0f, 0f, -pos.x),
            new Vector4(0f, 1f, 0f, -pos.y),
            new Vector4(0f, 0f, 1f, -pos.z),
            new Vector4(0f, 0f, 0f,     1f)
        );

        return rotationMatrix.transpose * positionMatrix.transpose;
    }

    public static Matrix4x4 CreateProjectionMatrix(float fov, float aspect, float nearClip, float farClip)
    {
        var invTan = 1f / Mathf.Tan(Mathf.Deg2Rad * fov / 2);
        var diffNearFar = nearClip - farClip;

        return GL.GetGPUProjectionMatrix(new Matrix4x4(
            new Vector4(invTan / aspect,                  0,                                    0,                                  0),
            new Vector4(              0,             invTan,                                    0,                                  0),
            new Vector4(              0,                  0,   (nearClip + farClip) / diffNearFar,   2*nearClip*farClip / diffNearFar),
            new Vector4(              0,                  0,                                   -1,                                  0)
        ).transpose, true);
    }

    public void RecalcularMatrices(Vector3 newPosition, Vector3 newRotation, Vector3 newScale, Vector3 cameraPos, Vector3 target, Vector3 up, GameObject gameObject)
    {
        Matrix4x4 modelMatrix, viewMatrix, projMatrix;
        Material material = gameObject.GetComponent<Renderer>().material;

        modelMatrix = CreateModelMatrix(newPosition, newRotation, newScale);
        viewMatrix = CreateViewMatrixFromTargetPoint(cameraPos, target, up);
        projMatrix = CreateProjectionMatrix(60, 16f/9f, 0.01f, 100f);

        material.SetMatrix("_ModelMatrix", modelMatrix);
        material.SetMatrix("_ViewMatrix", viewMatrix);
        material.SetMatrix("_ProjectionMatrix", projMatrix);
    }
}
