using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ElectroswingManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] List<GameObject> solarPanels = new List<GameObject>();
    [SerializeField] Transform goalPivot; //Goal pivot to check rotation against
    [SerializeField] Transform debugPanel; //Solar panel to check rotation against goal pivot

    bool upCheck = false;
    bool forwardCheck = false;

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

                if (upCheck && forwardCheck) currentState = STATE.End;
                
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
    }
}
