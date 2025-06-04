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
        if (isMoving && mainCamera.transform.position != cameraPositions[currentCameraPosition].position) {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPositions[currentCameraPosition].position, cameraMoveSpeed * Time.deltaTime);
            //mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, cameraPositions[currentCameraPosition].rotation, cameraRotateSpeed * Time.deltaTime);

            if((mainCamera.transform.position == cameraPositions[currentCameraPosition].position))
            {
                isMoving = false;
                mainCamera.transform.position = cameraPositions[currentCameraPosition].position;
                //mainCamera.transform.rotation = cameraPositions[currentCameraPosition].rotation;
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
}
