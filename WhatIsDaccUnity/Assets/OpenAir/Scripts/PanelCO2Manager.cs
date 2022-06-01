using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCO2Manager : MonoBehaviour
{
    public MoveFoward moveFoward;
    public ReactToMouseOver react;
    public List<GameObject> co2s;
    public int counter = 0;

    public static PanelCO2Manager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        GameObject[] allCo2s = GameObject.FindGameObjectsWithTag("CO2");

        co2s = new List<GameObject>();

        foreach (var co2 in allCo2s)
        {
            if (co2.name.Contains("Panel"))
            {
                co2s.Add(co2);

                co2.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateCo2()
    {
        print("ActivateCo2: " + counter);

        if (counter < co2s.Count && react.CurrentState >= ReactToMouseOver.STATE.Sorbent)
        {
            co2s[counter].SetActive(true);
            counter++;
        }
    }

    public void ReleaseCo2()
    {
        foreach (var co2 in co2s)
        {
            Co2Activity ca = co2.GetComponent<Co2Activity>();
            ca.CurrentState = Co2Activity.State.floatUp;
        }
    }

    public void VacuumCo2()
    {

        foreach (var co2 in co2s)
        {
            Co2Activity ca = co2.GetComponent<Co2Activity>();
            ca.CurrentState = Co2Activity.State.grabbed;
        }
    }

    public void Reset()
    {
        print("RESET");

        moveFoward.pause = false;
        counter = 0;

        foreach (var co2 in co2s)
        {
            Co2Activity ca = co2.GetComponent<Co2Activity>();
            ca.Reset();
        }
    }

    public bool HasCo2()
    {
        return counter > 0;
    }

}
