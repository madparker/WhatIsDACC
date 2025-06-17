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

        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(transform.position.x);
    }

    void SpawnWater()
    {
        Instantiate(waterPrefab, new Vector3(this.transform.position.x - 0.1f, this.transform.position.y - 0.1f, this.transform.position.z - 0.13f), Quaternion.identity);
    }

    void TrackMouse()
    {
        screenPosition = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }

        //Keeps tap from going ON sorbent
        if (worldPosition.y >= 1.08)
        {
            this.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z + 0.15f);
        }
        else {
            this.transform.position = new Vector3(-0.04f, 1.08f, worldPosition.z + 0.15f);
        }

    }
}
