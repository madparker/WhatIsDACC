using UnityEngine;

public class TemperatureswingManager : MonoBehaviour
{
    [Header("External GameObjects")]
    [SerializeField] GameObject mouseTracker;

    [Header("Smoke System")]
    [SerializeField] ParticleSystem smokeParticles;
    [SerializeField] GameObject firstPiece;

    [Header("Puzzle Pieces")]
    [SerializeField] GameObject[] correctPieces;
    [SerializeField] GameObject[] allPieces;
    [SerializeField] GameObject outPiece;
    [SerializeField] Material outPieceMaterial;

    int filledPiece;

    public enum STATE
    {
        Idle, PipeTurn, End
    }

    public STATE currentState = STATE.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseTracker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case STATE.Idle:
                break;
            case STATE.PipeTurn:

                if (Input.GetMouseButtonUp(0))
                {
                    int correctCount = 0;
                    for (int i = 0; i < correctPieces.Length; i++)
                    {
                        if (correctPieces[i].GetComponent<PuzzleElementBehavior>().isInPosition)
                        {
                            correctCount++;
                        }
                    }
                    if (correctCount == correctPieces.Length)
                    {
                        DeactivatePieces();
                        FillPieces();

                        currentState = STATE.End;
                    }

                    if (firstPiece.GetComponent<PuzzleElementBehavior>().isInPosition)
                    {
                        if (smokeParticles.isPlaying) smokeParticles.Stop();
                    }
                    else
                    {
                        if (smokeParticles.isStopped) smokeParticles.Play();
                    }

                }

                

                break;
            case STATE.End:
                break;
        }
    }

    public void StartMinigame()
    {
        currentState = STATE.PipeTurn;

        mouseTracker.SetActive(true);
    }

    void FillPieces()
    {
        correctPieces[filledPiece].GetComponent<PuzzleElementBehavior>().FillSelf();
        if (filledPiece < correctPieces.Length - 1)
        {
            filledPiece++;
            Invoke("FillPieces", 0.5f);
        }
        else
        {
            Invoke("EndPuzzle", 0.5f);
        }
    }

    void DeactivatePieces()
    {
        for (int i = 0; i < allPieces.Length; i++)
        {
            allPieces[i].GetComponent<PuzzleElementBehavior>().DeactivateSelf();
        }
    }
}
