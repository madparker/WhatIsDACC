using UnityEngine;

public class HydroswingManager : MonoBehaviour
{
    [SerializeField] GameObject[] sorbents;
    [SerializeField] Transform sorbentGameTransform;
    [SerializeField] GameObject carbonPrefab;

    Transform[] sorbentInitalTransforms;
    int currentSorbent;

    Vector3 goalPostion;
    Quaternion goalRotation;



    bool isMoveToGoal;

    public enum STATE
    {
        Idle, Move, Clear, Return, End
    }

    public STATE currentState = STATE.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                    sorbents[currentSorbent].transform.position = Vector3.MoveTowards(sorbents[currentSorbent].transform.position, sorbentGameTransform.position, Time.deltaTime / 4);
                }
                else
                {
                    if (sorbents[currentSorbent].transform.rotation != sorbentGameTransform.rotation)
                    {
                        sorbents[currentSorbent].transform.rotation = Quaternion.RotateTowards(sorbents[currentSorbent].transform.rotation, sorbentGameTransform.rotation, Time.deltaTime * 10);
                    }
                    else
                    {
                        currentState = STATE.Clear;
                        FillSorbent();
                    }
                }
                break;
            case STATE.Clear:
                break;
            case STATE.Return:
                break;
            case STATE.End:
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

        for (int i = 0; i < 15; i++) {
            Instantiate(carbonPrefab, new Vector3(currentX, currentY, currentZ), Quaternion.identity);

            currentX += 0.1f;
            
            if(i == 4 || i == 9)
            {
                currentX = sorbents[currentSorbent].transform.position.x - 0.2f;
                currentY -= 0.1f;
                currentZ -= 0.05f;
            }

        }
    }
}
