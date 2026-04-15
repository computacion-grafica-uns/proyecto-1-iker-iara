using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraCasera orbitalCamera;
    public CameraCasera fpsCamera;
    public CameraCasera currentCamera;

    public GameObject paredes;
    public GameObject techo;

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarningFormat("Solo debe haber un '{0}' por escena. Borrando esta instancia...", nameof(GameManager));
            Destroy(this);
            return;
        }

        if (orbitalCamera == null)
        {
            Debug.LogErrorFormat("Falta asignar {0} al {1}.", nameof(orbitalCamera), nameof(GameManager));
            Destroy(this);
            return;
        }

        if (fpsCamera == null)
        {
            Debug.LogErrorFormat("Falta asignar {0} al {1}.", nameof(fpsCamera), nameof(GameManager));
            Destroy(this);
            return;
        }

        instance = this;

        SwitchToCamera(orbitalCamera);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha0) || Input.GetKeyUp(KeyCode.Keypad0))
            ToggleCursor();

        if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
            SwitchToCamera(orbitalCamera);

        if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
            SwitchToCamera(fpsCamera);

        if (Input.GetKeyUp(KeyCode.Alpha9) || Input.GetKeyUp(KeyCode.Keypad9))
            ToggleGameObject(techo);

        if (Input.GetKeyUp(KeyCode.Alpha8) || Input.GetKeyUp(KeyCode.Keypad8))
            ToggleGameObject(paredes);
    }

    private void SwitchToCamera(CameraCasera camera)
    {
        orbitalCamera.gameObject.SetActive(false);
        fpsCamera.gameObject.SetActive(false);

        currentCamera = camera;
        camera.gameObject.SetActive(true);
    }

    private void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void ToggleGameObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
