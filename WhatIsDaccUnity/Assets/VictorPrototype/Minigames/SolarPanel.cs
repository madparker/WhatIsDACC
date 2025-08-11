using UnityEngine;

public class SolarPanel : MonoBehaviour
{

    [SerializeField] GameObject solarPanelParent;
    [SerializeField] GameObject panelPivot;
    [SerializeField] Transform goalPivot;

    [SerializeField] float parentRotationSpeed;
    [SerializeField] float pivotRotationSpeed;

    [SerializeField] bool isDebug;

    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft || Input.GetKey(KeyCode.A))
        {
            solarPanelParent.transform.Rotate(0, parentRotationSpeed * Time.deltaTime, 0);
        }
        if (moveRight || Input.GetKey(KeyCode.D))
        {
            solarPanelParent.transform.Rotate(0, -parentRotationSpeed * Time.deltaTime, 0);
        }

        if (moveUp || Input.GetKey(KeyCode.W))
        {
            panelPivot.transform.Rotate(0, 0, pivotRotationSpeed * Time.deltaTime);
        }
        if (moveDown || Input.GetKey(KeyCode.S))
        {
            panelPivot.transform.Rotate(0, 0, -pivotRotationSpeed * Time.deltaTime);
        }

        if (isDebug) {

            //Debug.Log((panelPivot.transform.up - goalPivot.up).magnitude + " " + (panelPivot.transform.forward - goalPivot.forward).magnitude);  || ((panelPivot.transform.forward - goalPivot.forward).magnitude) > 1.9f;

            bool upCheck = (panelPivot.transform.up - goalPivot.up).magnitude < 0.5f;
            bool forwardCheck = ((panelPivot.transform.forward - goalPivot.forward).magnitude) < 0.5f;
            Debug.Log(upCheck + " " +  forwardCheck);

        } 
    }
}
