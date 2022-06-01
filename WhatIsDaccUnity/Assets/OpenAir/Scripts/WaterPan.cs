using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPan : MonoBehaviour
{
    public GameObject water;

    public float waterMax = 0.15f;
    public bool waterUp = false;

    Vector3 startPos;
    Vector3 endPos;

    float currentStep = 0;

    public float CurrentStep
    {
        get
        {
            return currentStep;
        }
        set
        {
            currentStep = value;


            if (currentStep > 1)
            {
                currentStep = 1;
            } else if (currentStep < 0)
            {
                currentStep = 0;
            }
        }
    }

    public float speedMod;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = water.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + waterMax, startPos.z);

        currentStep = 0;
    }

    // Update is called once per frame
    void Update()
    {

        water.transform.position = startPos;
        
        if (waterUp && transform.position.y < endPos.y)
        {
            CurrentStep += Time.deltaTime/speedMod;
            water.transform.position = Vector3.Lerp(startPos, endPos, CurrentStep);
        }
        else
        {
            CurrentStep -= Time.deltaTime/speedMod;
            water.transform.position = Vector3.Lerp(startPos, endPos, CurrentStep);
        }
    }
}
