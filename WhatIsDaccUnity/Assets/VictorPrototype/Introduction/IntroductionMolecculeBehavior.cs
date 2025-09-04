using UnityEngine;

public class IntroductionMolecculeBehavior : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    [SerializeField] bool isCarbon;

    bool slowingDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(slowingDown)
        {
            movementSpeed -= Time.deltaTime * 2;

            if(movementSpeed <= 0 ) Destroy(this.gameObject);
        }
        
        this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);

        if (this.transform.position.x > 11) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCarbon && other.gameObject.CompareTag("Sorbent")) slowingDown = true;
    }
}
