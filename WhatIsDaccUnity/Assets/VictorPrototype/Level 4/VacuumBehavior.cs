using UnityEngine;

public class VacuumBehavior : MonoBehaviour
{
    Camera cam;
    Vector3 screenPosition;
    Vector3 worldPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        TrackMouse();
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(transform.position.x);
    }

    void TrackMouse()
    {
        screenPosition = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }

        this.transform.position = new Vector3(worldPosition.x, this.transform.position.y, worldPosition.z);


    }
}
