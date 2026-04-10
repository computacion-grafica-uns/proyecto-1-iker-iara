using UnityEngine;

public abstract class CameraCasera : MonoBehaviour
{
    public Vector3 position;
    public float aspectRatio = 16f/9f;
    public float fov = 60f;
    public float nearField = 0.1f;
    public float farField = 1000f;

    public abstract Matrix4x4 GetViewMatrix();
}