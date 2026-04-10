using UnityEngine;

public class CameraOrbital : CameraCasera
{
    [Header("Ajustes de orbitado")]
    public float distance = 10f;
    public Vector3 target = Vector3.zero;
    public float maxPitch = 60f;
    public Vector2 pitchYawSensitivity = new(-1,1);

    [Header("Ajustes de zoom")]
    public float minFov = 10f;
    public float maxFov = 160f;
    public float scrollSensitivity = 1f;

    [Header("Estado actual")]
    [SerializeField] [Tooltip("OJOTA porque está en radianes")] private Vector2 yawPitch = new(0,0);

    void Start()
    {
        // Lo llamo una vez para que se posicione bien al inicio, sino hasta que el usuario mueva el mouse no se va a actualizar la posición de la cámara
        HandleOrbit(true);
    }

    void Update()
    {
        HandleOrbit();
        HandleZoom();
    }

    public void HandleOrbit(bool forceUpdate = false)
    {
        if (Input.GetMouseButton(1) || forceUpdate)
        {
            var mouseDeltaPos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            var minPitchRad = Mathf.Deg2Rad * (90f - maxPitch);
            var maxPitchRad = Mathf.Deg2Rad * (90f + maxPitch);

            yawPitch += pitchYawSensitivity * (Vector2)mouseDeltaPos;
            yawPitch = new(yawPitch.x, Mathf.Clamp(yawPitch.y, minPitchRad, maxPitchRad));

            position = distance * new Vector3(
                Mathf.Sin(yawPitch.y) * Mathf.Cos(yawPitch.x),
                Mathf.Cos(yawPitch.y),
                Mathf.Sin(yawPitch.y) * Mathf.Sin(yawPitch.x)
            );
        }
    }

    public void HandleZoom()
    {
        var scroll = Input.mouseScrollDelta.y;

        fov = Mathf.Clamp(fov + scrollSensitivity * scroll, minFov, maxFov);
    }

    public override Matrix4x4 GetViewMatrix()
    {
        return Matrices.CreateViewMatrixFromTargetPoint(position, target, Vector3.up);
    }
}