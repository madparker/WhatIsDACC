using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedReact : ReactToMouseOver
{

    protected override void Setup()
    {
        base.Setup();
        CurrentState = STATE.Sorbent;
    }

    // Update is called once per frame
    void Update()
    {
        print("counter: " + PanelCO2Manager.instance.counter);
        print("PanelCO2Manager.instance.co2s.Count: " + PanelCO2Manager.instance.co2s.Count);

        if (PanelCO2Manager.instance.counter == PanelCO2Manager.instance.co2s.Count)
        {

            CurrentState = STATE.Water;
        }
    }
}
