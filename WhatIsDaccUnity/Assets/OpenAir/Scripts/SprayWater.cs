using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWater : MonoBehaviour
{
    public GameObject waterPrefab;
    public float spraySpeed = 0.1f;

    bool toggle = false;

    public PanelCO2Manager panelCo2Manager;

    public MoveFoward airFlow; 
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpray()
    {
        if (!toggle)
        {
            toggle = true;
            InvokeRepeating("TriggerWater", spraySpeed, spraySpeed);

            panelCo2Manager.ReleaseCo2();

            airFlow.pause = true;
        }
    }

    public void StopSpray()
    {
        toggle = false;
        CancelInvoke();

        if (!PanelCO2Manager.instance.HasCo2())
        {
            airFlow.pause = false;
        }
    }

    public void TriggerWater(){
        GameObject water = Instantiate<GameObject>(waterPrefab);
        water.transform.position = transform.position;
        water.transform.parent = transform;

        Vector3 velocity = new Vector3(Random.Range(-0.25f, -1.5f), 
                                       Random.Range(0.25f, .75f), 
                                       Random.Range(-1.5f, .5f));

        water.GetComponent<Rigidbody>().velocity = velocity;
    }
}
