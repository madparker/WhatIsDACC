using JetBrains.Annotations;
using UnityEngine;

public class PuzzleElementBehavior : MonoBehaviour
{

    [SerializeField] GameObject mouseTracker;
    [SerializeField] Material filledMaterial;
    [SerializeField] bool isStartEnd;

    public int correctPosition;
    public int currentPosition;
    public int startPosition;

    bool canTurn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!isStartEnd) canTurn = true;
        currentPosition = startPosition;
        this.transform.Rotate(0, 0, -90 * startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canTurn && mouseTracker.transform.position.x < this.transform.position.x + 0.5f && mouseTracker.transform.position.x > this.transform.position.x - 0.5f
            && mouseTracker.transform.position.y < this.transform.position.y + 0.5f && mouseTracker.transform.position.y > this.transform.position.y - 0.5f)
            {
                this.transform.Rotate(0, 0, -90);
                currentPosition += 1;
                if (currentPosition > 3) currentPosition = 0;
                Debug.Log("turned");
            }
        }
    }

    public void FillSelf()
    {
        this.GetComponent<Renderer>().material = filledMaterial;
    }

    public void DeactivateSelf()
    {
        canTurn = false;
    }
}
