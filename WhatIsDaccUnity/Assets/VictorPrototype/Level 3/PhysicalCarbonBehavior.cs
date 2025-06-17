using UnityEngine;

public class PhysicalCarbonBehavior : MonoBehaviour
{
    //Components
    GameObject parentSorbent; 
    Rigidbody rb;

    //PrivateVariables
    public bool isStationary;
    Vector3 parentDisplacement;

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
        if (transform.position.y < 0.4f && !isStationary) {
            Destroy(this.gameObject);
            Debug.Log("Destroyed");
        } 
        if(isStationary)
        {
            this.transform.position = parentSorbent.transform.position - parentDisplacement;
        }
    }

    public void SetUpParent(GameObject parent)
    {
        parentSorbent = parent;
        parentDisplacement = parent.transform.position - this.transform.position;

        isStationary = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            if(rb.isKinematic && isStationary)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                isStationary = false;
            }
        }
    }
}
