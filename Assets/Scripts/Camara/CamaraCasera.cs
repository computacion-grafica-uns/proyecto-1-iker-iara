using UnityEngine;

public abstract class CameraCasera : MonoBehaviour
{
    public Vector3 position;
    public float aspectRatio = 16f/9f;
    public bool calculateAspectOnLoad = true;
    public float fov = 60f;
    public float nearClip = 0.1f;
    public float farClip = 1000f;
    public bool recalculateCulling = false;

    public Camera realCamera;

    void Awake()
    {
        if (calculateAspectOnLoad)
            aspectRatio = Screen.width / Screen.height;
    }

    // Esto está acá pura y exclusivamente para resolver el
    // problema de que a Unity le encanta calcular su propia
    // culling matrix para hacer Frustum Culling
    void LateUpdate()
    {
        if (Camera.current != null && recalculateCulling)
            Camera.current.cullingMatrix = GetCullingMatrix();
    }

    public abstract Matrix4x4 GetCullingMatrix();

    public abstract Matrix4x4 GetViewMatrix();

    public virtual Matrix4x4 GetProjectionMatrix() =>
        Matrices.CreateProjectionMatrix(fov, aspectRatio, nearClip, farClip);
}