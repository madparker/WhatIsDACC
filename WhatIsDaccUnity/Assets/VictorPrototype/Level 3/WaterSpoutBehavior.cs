using UnityEngine;

public class WaterSpoutBehavior : MonoBehaviour
{

    [SerializeField] GameObject waterPrefab;

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
        if (Input.GetMouseButtonDown(0)) 
        {
            SpawnWater();
        }

    }

    void SpawnWater()
    {
        Instantiate(waterPrefab, new Vector3(this.transform.position.x, this.transform.position.y - 0.1f, this.transform.position.z - 0.2f), Quaternion.identity);
    }

    void TrackMouse()
    {
        screenPosition = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }

        this.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z + 0.15f);
    }
}
