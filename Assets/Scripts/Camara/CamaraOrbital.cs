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
    [SerializeField] [Tooltip("OJOTA porque está en radianes")] private Vector2 pitchYaw = new(0,0);

    void Update()
    {
        HandleOrbit();
        HandleZoom();
    }

    public void HandleOrbit()
    {
        if (Input.GetMouseButton(1))
        {
            var mouseDeltaPos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            var minPitchRad = Mathf.Deg2Rad * (90f - maxPitch);
            var maxPitchRad = Mathf.Deg2Rad * (90f + maxPitch);

            pitchYaw += pitchYawSensitivity * (Vector2)mouseDeltaPos;
            pitchYaw = new(pitchYaw.x, Mathf.Clamp(pitchYaw.y, minPitchRad, maxPitchRad));

            position = distance * new Vector3(
                Mathf.Sin(pitchYaw.y) * Mathf.Cos(pitchYaw.x),
                Mathf.Cos(pitchYaw.y),
                Mathf.Sin(pitchYaw.y) * Mathf.Sin(pitchYaw.x)
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