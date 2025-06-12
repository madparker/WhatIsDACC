using UnityEngine;

public class HydroswingManager : MonoBehaviour
{
    [SerializeField] GameObject[] sorbents;
    [SerializeField] Transform sorbentGameTransform;

    Transform[] sorbentInitalTransforms;
    int currentSorbent;

    Vector3 goalPostion;
    Quaternion goalRotation;

    bool isMoveToGoal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveToGoal) {
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
            }
        }
        
    }

    public void NextStep()
    {
        isMoveToGoal = true;
    }
}
