using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] Transform[] cameraPositions;
    [SerializeField] GameObject mainCamera;
    [SerializeField] float cameraMoveSpeed;
    [SerializeField] float cameraRotateSpeed;

    int currentCameraPosition = 0;
    public bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mainCamera.transform.rotation != cameraPositions[currentCameraPosition].rotation
        if (isMoving && mainCamera.transform.position != cameraPositions[currentCameraPosition].position
            || mainCamera.transform.rotation != cameraPositions[currentCameraPosition].rotation) {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraPositions[currentCameraPosition].position, cameraMoveSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, cameraPositions[currentCameraPosition].rotation, cameraRotateSpeed * Time.deltaTime);

            if((mainCamera.transform.position == cameraPositions[currentCameraPosition].position) && mainCamera.transform.rotation == cameraPositions[currentCameraPosition].rotation)
            {
                isMoving = false;
                mainCamera.transform.position = cameraPositions[currentCameraPosition].position;
                mainCamera.transform.rotation = cameraPositions[currentCameraPosition].rotation;
            }
        }
    }

    public void UpdateCameraPosition()
    {
        currentCameraPosition++;

        if (currentCameraPosition < cameraPositions.Length) {
            isMoving = true;
        }
    }

    public void DebugCamera(int camNum)
    {
        currentCameraPosition = camNum;
    }
}
