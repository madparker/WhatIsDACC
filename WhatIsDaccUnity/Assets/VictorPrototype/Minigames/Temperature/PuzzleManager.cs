using NUnit.Framework;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [SerializeField] GameObject[] correctPieces;
    [SerializeField] GameObject[] allPieces;
    [SerializeField] GameObject outPiece;
    [SerializeField] Material outPieceMaterial;

    int filledPiece;
    public bool isDone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if (PuzzleDoor.transform.rotation != PuzzleDoorFinalRotation.rotation) PuzzleDoor.transform.rotation = Quaternion.Lerp(PuzzleDoor.transform.rotation, PuzzleDoorFinalRotation.rotation, 2 * Time.deltaTime); 


        if (Input.GetMouseButtonUp(0)) {

            int correctCount = 0;
            for (int i = 0; i < correctPieces.Length; i++)
            {
                if (correctPieces[i].GetComponent<PuzzleElementBehavior>().isInPosition) {
                    correctCount++;
                }
            }
            if (correctCount == correctPieces.Length) {
                DeactivatePieces();
                FillPieces();
            }
        }


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
        isDone = true;
        outPiece.GetComponent<MeshRenderer>().material = outPieceMaterial;
    }


}
