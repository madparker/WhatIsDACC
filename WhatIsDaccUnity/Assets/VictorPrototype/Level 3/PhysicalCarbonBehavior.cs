using UnityEngine;

public class PhysicalCarbonBehavior : MonoBehaviour
{
    Rigidbody rb;
    bool isStationary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            if(rb.isKinematic)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
}
