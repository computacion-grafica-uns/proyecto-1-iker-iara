using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraCasera orbitalCamera;
    public CameraCasera fpsCamera;
    public CameraCasera currentCamera;

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
        if (Input.GetKeyUp(KeyCode.Alpha1))
            SwitchToCamera(orbitalCamera);

        if (Input.GetKeyUp(KeyCode.Alpha2))
            SwitchToCamera(fpsCamera);
    }

    private void SwitchToCamera(CameraCasera camera)
    {
        orbitalCamera.gameObject.SetActive(false);
        fpsCamera.gameObject.SetActive(false);

        currentCamera = camera;
        camera.gameObject.SetActive(true);
    }
}
