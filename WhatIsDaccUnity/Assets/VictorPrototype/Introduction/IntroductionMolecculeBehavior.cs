using UnityEngine;

public class IntroductionMolecculeBehavior : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    [SerializeField] bool isCarbon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);

        if (this.transform.position.x > 10) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCarbon && other.gameObject.CompareTag("Sorbent")) Destroy(this.gameObject);
    }
}
