using UnityEngine;

public class WindTurbine : MonoBehaviour
{

    [SerializeField] GameObject windTurbine;
    [SerializeField] GameObject bladePivot;
    [SerializeField] Transform windDirection;

    [SerializeField] float turbineRotationSpeed;
    [SerializeField] float bladeRotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)) {
            windTurbine.transform.Rotate(0, turbineRotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            windTurbine.transform.Rotate(0, -turbineRotationSpeed * Time.deltaTime, 0);
        }

        Debug.Log(this.transform.forward);

        float deltaX = this.transform.forward.x - windDirection.forward.x;
        float deltaZ = this.transform.forward.z - windDirection.forward.z;

        float totalDelta = new Vector3(deltaX, 0, deltaZ).magnitude;

        Debug.Log(totalDelta);

        bladePivot.transform.Rotate(0, 0, (2 - totalDelta) * bladeRotationSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.W))
        {
            bladePivot.transform.Rotate(0, 0, bladeRotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            bladePivot.transform.Rotate(0, 0, -bladeRotationSpeed * Time.deltaTime);
        }
    }
}
