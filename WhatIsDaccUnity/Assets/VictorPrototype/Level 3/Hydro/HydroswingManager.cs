using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class HydroswingManager : MonoBehaviour
{
    [SerializeField] GameObject[] sorbents;
    [SerializeField] Transform[] sorbentInitalTransforms;
    [SerializeField] Transform sorbentGameTransform;
    [SerializeField] GameObject carbonPrefab;
    [SerializeField] int carbonCount = 15;
    [SerializeField] GameObject waterTap;

    [Header("Box Hinge")]
    [SerializeField] GameObject hinge;
    [SerializeField] Transform openTransform;
    [SerializeField] Transform closeTransform;

    
    int currentSorbent;

    GameObject[] carbonMolecules;

    Vector3 goalPostion;
    Quaternion goalRotation;

    bool isMoveToGoal;

    public enum STATE
    {
        Idle, SetUp, Move, Fill, Clear, Return, Close, End
    }

    public STATE currentState = STATE.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterTap.SetActive(false);
        carbonMolecules = new GameObject[carbonCount];

        /*
        for(int i = 0; i < sorbents.Length; i++)
        {
            sorbentInitalTransforms[i] = sorbents[i].transform;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

        switch(currentState)
        {
            case STATE.Idle:

                break;
            case STATE.SetUp:

                hinge.transform.rotation = Quaternion.RotateTowards(hinge.transform.rotation, openTransform.rotation, 30 * Time.deltaTime);
                if (hinge.transform.rotation == openTransform.rotation)
                {
                    currentState = STATE.Move;
                }

                break;
            case STATE.Move:
                if (sorbents[currentSorbent].transform.position != sorbentGameTransform.position)
                {
                    sorbents[currentSorbent].transform.position = Vector3.MoveTowards(sorbents[currentSorbent].transform.position, sorbentGameTransform.position, Time.deltaTime / 2);
                }
                else
                {
                    if (sorbents[currentSorbent].transform.rotation != sorbentGameTransform.rotation)
                    {
                        sorbents[currentSorbent].transform.rotation = Quaternion.RotateTowards(sorbents[currentSorbent].transform.rotation, sorbentGameTransform.rotation, Time.deltaTime * 20);
                    }
                    else
                    {

                        currentState = STATE.Clear;
                        waterTap.SetActive(true);
                        sorbents[currentSorbent].GetComponent<BoxCollider>().isTrigger = false;
                    }
                }
                break;
            case STATE.Clear:

                int nullCount = 0;
                for(int i = 0; i < carbonCount; i++)
                {
                    if (carbonMolecules[i].gameObject == null) nullCount++;
                }

                if (nullCount == carbonCount) {
                    waterTap.SetActive(false);
                    currentState = STATE.Return;
                } 

                break;
            case STATE.Return:
                if (sorbents[currentSorbent].transform.rotation != sorbentInitalTransforms[currentSorbent].rotation)
                {
                    sorbents[currentSorbent].transform.rotation = Quaternion.RotateTowards(sorbents[currentSorbent].transform.rotation, sorbentInitalTransforms[currentSorbent].rotation, Time.deltaTime * 20);
                }
                else
                {
                    if (sorbents[currentSorbent].transform.position != sorbentInitalTransforms[currentSorbent].position)
                    {
                        sorbents[currentSorbent].transform.position = Vector3.MoveTowards(sorbents[currentSorbent].transform.position, sorbentInitalTransforms[currentSorbent].position, Time.deltaTime / 2);
                    }
                    else
                    {
                        if(currentSorbent < 2)
                        {
                            currentSorbent++;

                            sorbents[currentSorbent].GetComponent<SorbentBehavior>().ClearSelf();
                            FillSorbent(sorbents[currentSorbent]);

                            currentState = STATE.Move;
                        } else
                        {
                            currentState = STATE.Close;
                        }
                        
                    }
                }
                break;
            case STATE.Close:
                hinge.transform.rotation = Quaternion.RotateTowards(hinge.transform.rotation, closeTransform.rotation, 30 * Time.deltaTime);
                if (hinge.transform.rotation == closeTransform.rotation)
                {
                    currentState = STATE.End;
                }
                break;
            case STATE.End:
                //Debug.Log("ended");
                break;
        }
        
    }

    public void NextStep()
    {
        currentState = STATE.SetUp;

        sorbents[currentSorbent].GetComponent<SorbentBehavior>().ClearSelf();
        FillSorbent(sorbents[currentSorbent]);
        
    }

    void FillSorbent(GameObject emptySorbent)
    {
        float currentX = emptySorbent.transform.position.x + 0.05f;
        float currentY = emptySorbent.transform.position.y + 0.1f;
        float currentZ = emptySorbent.transform.position.z - 0.2f;

        int firstThreshold = (carbonCount / 3) - 1;
        int secondThreshold = firstThreshold + (carbonCount / 3);

        for (int i = 0; i < carbonCount; i++) {
            carbonMolecules[i] = Instantiate(carbonPrefab, new Vector3(currentX, currentY, currentZ), emptySorbent.transform.rotation);
            carbonMolecules[i].GetComponent<PhysicalCarbonBehavior>().SetUpParent(emptySorbent);

            currentZ += 0.1f;
            
            if(i == firstThreshold || i == secondThreshold)
            {
                currentZ = emptySorbent.transform.position.z - 0.2f;
                currentY -= 0.1f;
                currentX -= 0.1f;
            }

        }
    }
}
