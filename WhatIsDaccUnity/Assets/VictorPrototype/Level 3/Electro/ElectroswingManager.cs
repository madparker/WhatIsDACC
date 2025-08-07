using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ElectroswingManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] List<GameObject> solarPanels = new List<GameObject>();
    [SerializeField] Transform goalPivot; //Goal pivot to check rotation against
    [SerializeField] Transform debugPanel; //Solar panel to check rotation against goal pivot
    [SerializeField] Slider slider;

    bool upCheck = false;
    bool forwardCheck = false;
    bool rightCheck = false;

    float electricityContained;
    float upMagnitude;

    public enum STATE
    {
        Idle, Release, End
    }

    public STATE currentState = STATE.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < solarPanels.Count; i++)
        {
            solarPanels[i].GetComponent<SolarPanel>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) { 
            case STATE.Idle:
                break;
            case STATE.Release:
               
                upCheck = (debugPanel.up - goalPivot.up).magnitude < 0.5f;
                forwardCheck = ((debugPanel.forward - goalPivot.forward).magnitude) < 0.5f;
                rightCheck = (debugPanel.right - goalPivot.right).magnitude < 0.5f;

                upMagnitude = (debugPanel.up - goalPivot.up).magnitude;

                if(upMagnitude < 0.2)
                {
                    electricityContained += Time.deltaTime;
                } else if(upMagnitude < 0.5)
                {
                    electricityContained += Time.deltaTime / 2;
                }

                    slider.value = 1 - (debugPanel.up - goalPivot.up).magnitude;
                    //slider.value = electricityContained;


                //Debug.Log("up: " + (debugPanel.up - goalPivot.up).magnitude + " forward: " + (debugPanel.forward - goalPivot.forward).magnitude + " right: " + (debugPanel.right - goalPivot.right).magnitude);
                //Debug.Log(upCheck.ToString() + forwardCheck.ToString() + rightCheck.ToString());
                Debug.Log((debugPanel.up - goalPivot.up).magnitude);
                //if (upCheck && forwardCheck) currentState = STATE.End;

                break;
            case STATE.End:
                break;
        }
    }

    public void SetUp()
    {
        for(int i = 0; i < solarPanels.Count; i++)
        {
            solarPanels[i].GetComponent<SolarPanel>().enabled = true;
        }

        currentState = STATE.Release;
    }
}
