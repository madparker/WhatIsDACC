using Unity.Cinemachine;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] Transform[] cameraPositions;
    [SerializeField] CinemachineCamera[] cameras;
    [SerializeField] GameObject mainCamera;
    [SerializeField] float cameraMoveSpeed;
    [SerializeField] float cameraRotateSpeed;

    CinemachineCamera previousCamera;
    CinemachineCamera currentCamera;

    int currentCameraPosition = 0;
    public bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCamera = cameras[currentCameraPosition];
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = mainCamera.GetComponent<CinemachineBrain>().IsBlending;
    }

    public void UpdateCameraPosition()
    {
        previousCamera = currentCamera;
        previousCamera.Priority = 0;

        currentCameraPosition++;

        currentCamera = cameras[currentCameraPosition];
        currentCamera.Priority = 1;

    }

    public void PrecisionUpdateCameraPosition(int camNum)
    {
        previousCamera = currentCamera;
        previousCamera.Priority = 0;

        currentCameraPosition = camNum;

        currentCamera = cameras[currentCameraPosition];
        currentCamera.Priority = 1;
    }

    public void DebugCamera(int camNum)
    {

        previousCamera = currentCamera;
        previousCamera.Priority = 0;

        currentCameraPosition = camNum;

        currentCamera = cameras[currentCameraPosition];
        currentCamera.Priority = 1;
    }
}
