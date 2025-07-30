using NUnit.Framework;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [SerializeField] GameObject[] correctPieces;
    [SerializeField] GameObject[] allPieces;
    [SerializeField] GameObject outPiece;
    [SerializeField] Material outPieceMaterial;
    [SerializeField] GameObject pieceHighlight;
    [SerializeField] GameObject player;
    [SerializeField] GameObject PuzzleStarter;
    [SerializeField] GameObject PuzzleDoor;
    [SerializeField] Transform PuzzleDoorFinalRotation;
    [SerializeField] GameObject gatePuzzle;

    int filledPiece;
    int pieceHighlightX;
    int pieceHighlightY;
    GameObject[,] gridPieces = new GameObject[3,3];
    bool doorIsOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player.SetActive(false);

        /*
        int columnNum = 0;
        for(int i = 0; i < allPieces.Length; i++)
        {
            gridPieces[columnNum, i % 3] = allPieces[i];
            if (i == 2 || i == 4) columnNum++;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

        if (PuzzleDoor.transform.rotation != PuzzleDoorFinalRotation.rotation) PuzzleDoor.transform.rotation = Quaternion.Lerp(PuzzleDoor.transform.rotation, PuzzleDoorFinalRotation.rotation, 2 * Time.deltaTime); 


        if (Input.GetMouseButtonUp(0)) {

            int correctCount = 0;
            for (int i = 0; i < correctPieces.Length; i++)
            {
                if (correctPieces[i].GetComponent<PuzzleElementBehavior>().currentPosition == correctPieces[i].GetComponent<PuzzleElementBehavior>().correctPosition) {
                    correctCount++;
                }
            }
            if (correctCount == correctPieces.Length) {
                DeactivatePieces();
                FillPieces();
            }
        }

        /*
        if(Input.GetKeyDown(KeyCode.S))
        {
            pieceHighlightY += 1;
            if(pieceHighlightY > 2) pieceHighlightY = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            pieceHighlightY -= 1;
            if (pieceHighlightY < 0) pieceHighlightY = 2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            pieceHighlightX += 1;
            if (pieceHighlightX > 2) pieceHighlightX = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pieceHighlightX -= 1;
            if (pieceHighlightX < 0) pieceHighlightX = 2;
        }

        pieceHighlight.transform.position = gridPieces[pieceHighlightX, pieceHighlightY].transform.position;
        */

    }

    void FillPieces()
    {
        correctPieces[filledPiece].GetComponent<PuzzleElementBehavior>().FillSelf();
        if (filledPiece < correctPieces.Length - 1)
        {
            filledPiece++;
            Invoke("FillPieces", 0.5f);
        } else
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

    void EndPuzzle()
    {
        Debug.Log("ended");
    }


}
