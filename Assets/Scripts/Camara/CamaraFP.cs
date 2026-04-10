using UnityEngine;

public class FpsCamera : CameraCasera
{
    [Header("Ajustes de mouse")]
    public float maxPitch = 80f;
    public Vector2 pitchYawSensitivity = new(-1,1);

    [Header("Ajustes de movimiento")]
    public float speed = 1f;
    public float sprintMult = 1.2f;
    public float sneakMult = 0.4f;

    [Header("Ajustes de zoom")]
    public float normalFov = 90f;
    public float zoomedFov = 40f;
    public float zoomSwitchSpeed = 1f;

    [Header("Estado actual")]
    [SerializeField] [Tooltip("Este si está en grados :)")] private Vector2 pitchYaw = new(0,0);

    void Update()
    {
        HandleLookaround();
        HandleWalk();
        HandleZoom();
    }

    public void HandleWalk()
    {
        var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        var actualSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            actualSpeed *= sprintMult;
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl))
            actualSpeed *= sneakMult;

        // Puedo reusar la matriz de rotación para calcular la dirección de traslación
        var rotMatrix = Matrices.CreateViewMatrixFromSphericalCoords(Vector3.zero, pitchYaw);
        var localDirection = rotMatrix * new Vector4(direction.x, direction.y, direction.z, 1f);
        var localMovement = actualSpeed * Time.deltaTime * (Vector3)localDirection;

        position += localMovement;
    }

    public void HandleLookaround()
    {
        var mouseDeltaPos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        pitchYaw += pitchYawSensitivity * (Vector2)mouseDeltaPos;
        pitchYaw = new(pitchYaw.x, Mathf.Clamp(pitchYaw.y, -maxPitch, maxPitch));
    }

    public void HandleZoom()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.AltGr))
        {
            fov = Mathf.Lerp(fov, zoomedFov, Time.deltaTime * zoomSwitchSpeed);
        }
        else
        {
            fov = Mathf.Lerp(fov, normalFov, Time.deltaTime * zoomSwitchSpeed);
        }
    }

    public override Matrix4x4 GetViewMatrix()
    {
        return Matrices.CreateViewMatrixFromSphericalCoords(position, pitchYaw);
    }
}