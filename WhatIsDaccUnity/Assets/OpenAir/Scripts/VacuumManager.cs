using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumManager : MonoBehaviour
{
    public PanelCO2Manager panelCo2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateVacuum()
    {
        panelCo2.VacuumCo2();
    }
}
