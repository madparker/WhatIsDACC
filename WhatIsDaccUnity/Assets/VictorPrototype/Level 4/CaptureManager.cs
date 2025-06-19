using UnityEngine;
using System.Collections.Generic;

public class CaptureManager : MonoBehaviour
{
    [Header("Water Pan")]
    [SerializeField] GameObject waterPan;
    [SerializeField] GameObject water;
    [SerializeField] Transform waterPanOut;
    [SerializeField] Transform waterPanIn;
    [SerializeField] Transform waterDrained;
    

    [Header("Carbon")]
    [SerializeField] GameObject carbonPrefab;
    [SerializeField] Transform waterPanBottom;
    [SerializeField] int carbonCount = 30;

    [Header("Vacuum")]
    [SerializeField] GameObject vacuum;

    Transform goalTransform;
    List<GameObject> carbonList = new List<GameObject>();
    bool isFilled;
    bool isDraining;

    public enum STATE
    {
        Idle, SetUp, Drain, Vacuum, SetDown, End
    }

    public STATE currentState = STATE.SetUp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goalTransform = waterPanIn;
        vacuum.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        switch(currentState)
        {
            case STATE.SetUp:
                waterPan.transform.position = Vector3.MoveTowards(waterPan.transform.position, goalTransform.position, Time.deltaTime);

                if (waterPan.transform.position == goalTransform.transform.position)
                {
                    FillSelf();
                    currentState = STATE.Drain; 
                }
                    break;
            case STATE.Drain:
                water.transform.position = Vector3.MoveTowards(water.transform.position, waterDrained.position, Time.deltaTime / 4);

                if (water.transform.position == waterDrained.position)
                {
                    vacuum.SetActive(true);
                    currentState = STATE.Vacuum;
                }
                break;
            case STATE.Vacuum:

                if (CheckForNull()) {
                    goalTransform = waterPanIn;
                    currentState = STATE.SetDown;
                    vacuum.SetActive(false);
                } 

                break;
            case STATE.SetDown:

                waterPan.transform.position = Vector3.MoveTowards(waterPan.transform.position, goalTransform.position, Time.deltaTime);

                if (waterPan.transform.position == goalTransform.transform.position)
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
        goalTransform = waterPanOut;
        currentState = STATE.SetUp;
    }

    bool CheckForNull()
    {
        int nullCount = 0;
        for (int i = 0; i < carbonList.Count; i++)
        {
            if (carbonList[i].gameObject == null) nullCount++;
        }
        return nullCount == carbonList.Count;
    }

    void FillSelf()
    {
        for (int i = 0; i < carbonCount; i++) {

            float deltaX = Random.Range(-0.3f, 0.3f);
            float deltaZ = Random.Range(-0.3f, 0.3f);

            Vector3 spawnPosition = new Vector3(waterPanBottom.transform.position.x + deltaX, waterPanBottom.transform.position.y + 0.05f, waterPanBottom.transform.position.z + deltaZ);
            GameObject currentCarbon = Instantiate(carbonPrefab, spawnPosition, Quaternion.identity);
            carbonList.Add(currentCarbon);
        }
    }
}
