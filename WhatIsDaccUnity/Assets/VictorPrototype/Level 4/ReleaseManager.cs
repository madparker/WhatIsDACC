using System.Threading;
using UnityEngine;

public class ReleaseManager : MonoBehaviour
{

    [Header("Sorbents")]
    [SerializeField] GameObject sorbent;
    [SerializeField] Transform sorbentInitialTransform;
    [SerializeField] Transform sorbentFinalTransform;
    [SerializeField] GameObject carbonPrefab;
    [SerializeField] int carbonCount = 15;
    GameObject[] carbonMolecules;

    [Header("Box Hinge")]
    [SerializeField] GameObject hinge;
    [SerializeField] Transform openTransform;
    [SerializeField] Transform closeTransform;

    [Header("Other")]
    [SerializeField] GameObject uiElements;
    [SerializeField] GameObject waitButton;


    bool isWaiting;
    float waitTimer;


    public enum STATE
    {
        Idle, Setup, Active, Return, End
    }

    public STATE currentState = STATE.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carbonMolecules = new GameObject[carbonCount];
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case STATE.Idle:
                break;
            case STATE.Setup:

                hinge.transform.rotation = Quaternion.RotateTowards(hinge.transform.rotation, openTransform.rotation, 30 * Time.deltaTime);
                if (hinge.transform.rotation == openTransform.rotation)
                {
                    if (sorbent.transform.position != sorbentFinalTransform.transform.position)
                    {
                        sorbent.transform.position = Vector3.MoveTowards(sorbent.transform.position, sorbentFinalTransform.position, Time.deltaTime / 2);
                    }
                    else
                    {
                        currentState = STATE.Active;

                        waitButton.SetActive(false);

                        sorbent.GetComponent<BoxCollider>().isTrigger = false;
                    }
                }

                break;
            case STATE.Active:

                if(isWaiting)
                {
                    waitTimer += Time.deltaTime;

                    if(waitTimer > 5)
                    {
                        currentState = STATE.Return;

                        sorbent.GetComponent<BoxCollider>().isTrigger = true;
                    }
                }
                

                break;

            case STATE.Return:

                if (sorbent.transform.position != sorbentInitialTransform.transform.position)
                {
                    sorbent.transform.position = Vector3.MoveTowards(sorbent.transform.position, sorbentInitialTransform.position, Time.deltaTime / 2);
                }
                else
                {
                    if (hinge.transform.rotation != closeTransform.rotation)
                    {
                        hinge.transform.rotation = Quaternion.RotateTowards(hinge.transform.rotation, closeTransform.rotation, 30 * Time.deltaTime);
                    } else
                    {
                        currentState = STATE.End;
                    }
                }


                break;

            case STATE.End:
                break;

        }
    }

    public void SetUp()
    {
        currentState = STATE.Setup;

        sorbent.GetComponent<SorbentBehavior>().ClearSelf();
        FillSorbent(sorbent);

        uiElements.SetActive(true);
        waitButton.SetActive(true);
    }

    void FillSorbent(GameObject emptySorbent)
    {
        float currentX = emptySorbent.transform.position.x + 0.05f;
        float currentY = emptySorbent.transform.position.y + 0.1f;
        float currentZ = emptySorbent.transform.position.z - 0.2f;

        int firstThreshold = (carbonCount / 3) - 1;
        int secondThreshold = firstThreshold + (carbonCount / 3);

        for (int i = 0; i < carbonCount; i++)
        {
            carbonMolecules[i] = Instantiate(carbonPrefab, new Vector3(currentX, currentY, currentZ), emptySorbent.transform.rotation);
            carbonMolecules[i].GetComponent<PhysicalCarbonBehavior>().SetUpParent(emptySorbent);

            currentZ += 0.1f;

            if (i == firstThreshold || i == secondThreshold)
            {
                currentZ = emptySorbent.transform.position.z - 0.2f;
                currentY -= 0.1f;
                currentX -= 0.1f;
            }

        }
    }


    public void ActivateElectricity()
    {
        for (int i = 0; i < carbonMolecules.Length; i++) {
            carbonMolecules[i].GetComponent<Rigidbody>().isKinematic = false;
            carbonMolecules[i].GetComponent<Rigidbody>().useGravity = true;
            carbonMolecules[i].GetComponent<PhysicalCarbonBehavior>().isStationary = false;
        }

        uiElements.SetActive(false);

        isWaiting = true;

    }

}
