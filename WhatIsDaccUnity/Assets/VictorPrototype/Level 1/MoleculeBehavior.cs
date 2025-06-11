using UnityEngine;

public class MoleculeBehavior : MonoBehaviour
{
    public string Name;
    [TextArea]public string Description;

    public Vector3 goalPosition;

    bool isActive = false;
    bool isBlown = false;
    Outline outline;

    Vector3 startPosition;
    Vector3 endPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        startPosition = this.transform.position;
        StartMove();
    }

    // Update is called once per frame
    void Update()
    {

        if(isBlown && Input.GetMouseButton(0))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, goalPosition, Time.deltaTime / 4);
            Debug.Log("isBlowing!");

            if(this.transform.position == goalPosition) Destroy(this.gameObject); 
        } else
        {
            if (!isActive && this.transform.position != endPosition)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, endPosition, Time.deltaTime / 4);
            }
            else
            {
                StartMove();
            }
        }
    }

    public void ActivateSelf()
    {
        isActive = true;
        outline.enabled = true;
    }

    public void DeactivateSelf() { 
        outline.enabled = false;
        isActive = false;
    }

    void StartMove()
    {
        float randX = Random.Range(startPosition.x - 0.05f, startPosition.x + 0.05f);
        float randY = Random.Range(startPosition.y - 0.05f, startPosition.y + 0.05f);

        endPosition = new Vector3(randX, randY, this.transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Fan"))
        {
            isBlown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fan"))
        {
            isBlown = false;
        }
    }
}
