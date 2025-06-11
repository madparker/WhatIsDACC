using UnityEngine;

public class FanBehavior : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] GameObject mouseTracker;

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
        //this.transform.position = new Vector3(mouseTracker.transform.position.x, mouseTracker.transform.position.y, this.transform.position.z);

        Vector3 targetDirection = targetTransform.position - this.transform.position;
        this.transform.forward = targetDirection.normalized;
        
    }

    void TrackMouse()
    {
        screenPosition = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }

        this.transform.position = new Vector3(worldPosition.x, worldPosition.y, this.transform.position.z);
    }
}
