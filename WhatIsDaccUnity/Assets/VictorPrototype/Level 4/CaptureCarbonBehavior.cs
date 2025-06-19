using UnityEngine;

public class CaptureCarbonBehavior : MonoBehaviour
{

    Transform goalTransform;
    bool isMoving;
    float destroyTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && Input.GetMouseButton(0)) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, goalTransform.position, Time.deltaTime);
            destroyTimer += Time.deltaTime;
            if (destroyTimer > 0.5f)
            {
                Destroy(this.gameObject);
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vacuum"))
        {
            isMoving = true;
            goalTransform = other.transform;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        isMoving = false;
    }
}
