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

    
    int currentSorbent;

    GameObject[] carbonMolecules;

    Vector3 goalPostion;
    Quaternion goalRotation;

    bool isMoveToGoal;

    public enum STATE
    {
        Idle, Move, Fill, Clear, Return, End
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
                        FillSorbent();
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
                    if (carbonMolecules[i] == null) nullCount++;
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
                            currentState = STATE.Move;
                        } else
                        {
                            currentState = STATE.End;
                        }
                        
                    }
                }
                break;
            case STATE.End:
                //Debug.Log("ended");
                break;
        }
        
    }

    public void NextStep()
    {
        currentState = STATE.Move;
        sorbents[currentSorbent].GetComponent<SorbentBehavior>().ClearSelf();
    }

    void FillSorbent()
    {
        float currentX = sorbents[currentSorbent].transform.position.x - 0.2f;
        float currentY = sorbents[currentSorbent].transform.position.y + 0.1f;
        float currentZ = sorbents[currentSorbent].transform.position.z;

        int firstThreshold = (carbonCount / 3) - 1;
        int secondThreshold = firstThreshold + (carbonCount / 3);

        for (int i = 0; i < carbonCount; i++) {
            carbonMolecules[i] = Instantiate(carbonPrefab, new Vector3(currentX, currentY, currentZ), Quaternion.identity);

            currentX += 0.1f;
            
            if(i == firstThreshold || i == secondThreshold)
            {
                currentX = sorbents[currentSorbent].transform.position.x - 0.2f;
                currentY -= 0.1f;
                currentZ -= 0.05f;
            }

        }
    }
}
