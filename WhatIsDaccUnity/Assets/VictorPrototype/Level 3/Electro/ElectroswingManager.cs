using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElectroswingManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] List<GameObject> solarPanels = new List<GameObject>();
    [SerializeField] Transform goalPivot; //Goal pivot to check rotation against
    [SerializeField] Transform debugPanel; //Solar panel to check rotation against goal pivot
    [SerializeField] Slider slider;
    [SerializeField] Slider batteryLevel;
    [SerializeField] GameObject uiElements;

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
        uiElements.SetActive(false);
        
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

                Debug.Log(slider.value);

                if (slider.value > 0.965) {

                    batteryLevel.value += Time.deltaTime / 4;
                }

                if (batteryLevel.value == 1)
                {
                    currentState = STATE.End;
                }

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

        Vector3 goalUp = Vector3.zero;

        float randX = Random.Range(-0.3f, 0.3f);
        float randY = Random.Range(0.8f, 1);
        float randZ = Random.Range(-0.3f, 0.3f);

        goalUp = new Vector3(randX, randY, randZ).normalized;

        Debug.Log(goalUp);

        goalPivot.transform.up = goalUp;

        uiElements.SetActive(true);

        currentState = STATE.Release;
    }


    public void StartMove(int direction)
    {
        if (currentState == STATE.Release) {
            switch (direction)
            {
                case 0:

                    for (int i = 0; i < solarPanels.Count; i++)
                    {
                        solarPanels[i].GetComponent<SolarPanel>().moveUp = true;
                    }

                    break;
                case 1:

                    for (int i = 0; i < solarPanels.Count; i++)
                    {
                        solarPanels[i].GetComponent<SolarPanel>().moveDown = true;
                    }

                    break;
                case 2:

                    for (int i = 0; i < solarPanels.Count; i++)
                    {
                        solarPanels[i].GetComponent<SolarPanel>().moveLeft = true;
                    }

                    break;
                case 3:

                    for (int i = 0; i < solarPanels.Count; i++)
                    {
                        solarPanels[i].GetComponent<SolarPanel>().moveRight = true;
                    }

                    break;
            }
        }
        
    }

    public void StopMove(int direction)
    {
        switch (direction)
        {
            case 0:

                for (int i = 0; i < solarPanels.Count; i++)
                {
                    solarPanels[i].GetComponent<SolarPanel>().moveUp = false;
                }

                break;
            case 1:

                for (int i = 0; i < solarPanels.Count; i++)
                {
                    solarPanels[i].GetComponent<SolarPanel>().moveDown = false;
                }

                break;
            case 2:

                for (int i = 0; i < solarPanels.Count; i++)
                {
                    solarPanels[i].GetComponent<SolarPanel>().moveLeft = false;
                }

                break;
            case 3:

                for (int i = 0; i < solarPanels.Count; i++)
                {
                    solarPanels[i].GetComponent<SolarPanel>().moveRight = false;
                }

                break;
        }
    }


}
