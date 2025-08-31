using JetBrains.Annotations;
using UnityEngine;

public class PuzzleElementBehavior : MonoBehaviour
{

    [SerializeField] GameObject mouseTracker;
    [SerializeField] Material filledMaterial;
    [SerializeField] bool isStartEnd;
    [SerializeField] bool isStraight;

    public int correctPosition;
    public int currentPosition;
    public int startPosition;

    public bool isInPosition;

    int altCorrectPosition;

    bool canTurn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!isStartEnd) canTurn = true;
        currentPosition = startPosition;
        this.transform.Rotate(0, 0, -90 * startPosition);

        if (isStraight) {
            altCorrectPosition = correctPosition + 2;
            if (altCorrectPosition > 3) altCorrectPosition = altCorrectPosition - 4;
        } else
        {
            altCorrectPosition = correctPosition;
        }
        
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

                if (currentPosition == correctPosition || currentPosition == altCorrectPosition) isInPosition = true; else isInPosition = false;

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
