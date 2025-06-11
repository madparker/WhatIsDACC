using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] CameraMover cameraMover; //On Camera Manager
    [SerializeField] MoleculeManager moleculeManager; //On Level 1
    [SerializeField] SorbentManager sorbentManager; //On Level 2

    [SerializeField] MeshRenderer boxTop;
    [SerializeField] MeshRenderer boxFront;

    //Internal Components
    MouseTracker mouseTracker;

    //Changable Variables


    //Public Variables


    [Header("Text Objects")]
    [SerializeField] GameObject levelTitleContainer;
    [SerializeField] GameObject levelDescriptionContainer;
    [SerializeField] GameObject interactiveDescriptionContainer;
    [SerializeField] GameObject waitButton;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI levelTitle;
    [SerializeField] TextMeshProUGUI levelDescription;
    [SerializeField] TextMeshProUGUI interactiveTitle;
    [SerializeField] TextMeshProUGUI interactiveDescription;

    [Header("Level Descriptions")]
    [SerializeField] string airInTitle;
    [SerializeField] [TextArea] string airInDescription;
    [SerializeField] string airInWaitText;

    [SerializeField] string absorbSetUpTitle;
    [SerializeField][TextArea] string absorbSetUpDescription;
    [SerializeField] string absorbSetUpWaitText;

    [SerializeField] string absorbTitle;
    [SerializeField][TextArea] string absorbDescription;

    public enum STATE
    {
        Intro, AirInSetUp, AirIn, AbsorbSetUp, Absorb, Water, Vacuum, AirOut
    }

    public STATE currentState = STATE.Intro;

    //Private Variables
    bool nextState = false;
    bool setUpState = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseTracker = GetComponent<MouseTracker>();
        
        waitButton.SetActive(false);
        SetLevelText(false);
        DeactivateInteractive();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case STATE.Intro:

                if(nextState)
                {
                    cameraMover.UpdateCameraPosition();

                    waitButton.SetActive(true);

                    currentState = STATE.AirInSetUp;
                    mouseTracker.enabled = true;

                    nextState = false;
                    setUpState = false;
                }
                break;
            case STATE.AirInSetUp:

                if(!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(airInTitle, airInDescription);
                    moleculeManager.ActivateMolecules();

                    setUpState = true;
                    waitButton.SetActive(false);
                }



                if (nextState)
                {
                    DeactivateInteractive();

                    waitButton.SetActive(true);

                    currentState = STATE.AirIn;

                    nextState = false;
                    setUpState = false;
                }
                break;
            case STATE.AirIn:
                if(!setUpState)
                {
                    moleculeManager.ToggleFan(true);
                    setUpState = true;
                }

                waitButton.SetActive(!moleculeManager.CheckIfNull()); //Checks if the array of molecules is empty


                if (nextState)
                {
                    DeactivateInteractive();
                    cameraMover.UpdateCameraPosition();
                    moleculeManager.ToggleFan(false);

                    waitButton.SetActive(true);

                    SetLevelText(false);
                    DeactivateInteractive();

                    currentState = STATE.AbsorbSetUp;
                    mouseTracker.enabled = false;

                    nextState = false;
                    setUpState = false;
                }
                break;
            case STATE.AbsorbSetUp:

                if (!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(absorbSetUpTitle, absorbSetUpDescription);
                    setUpState = true;

                    sorbentManager.SetUp();
                    
                    //boxFront.enabled = false;
                    //boxTop.enabled = false;
                }

                if(sorbentManager.isFull && waitButton.activeInHierarchy)
                {
                    waitButton.SetActive(false);
                }


                if (nextState)
                {
                    currentState = STATE.Water;
                    nextState = false;
                }
                break;
            case STATE.Water:

                if (nextState)
                {
                    cameraMover.UpdateCameraPosition();
                    currentState = STATE.Vacuum;
                    nextState = false;
                }
                break;
            case STATE.Vacuum:

                if (nextState)
                {
                    cameraMover.UpdateCameraPosition();
                    currentState = STATE.AirOut;
                    nextState = false;
                }
                break;
        }
    }

    void SetLevelText(bool textState)
    {
        levelTitleContainer.SetActive(textState);
        levelDescriptionContainer.SetActive(textState);
    }

    void SetLevelTextContent(string title, string description)
    {
        levelTitle.text = title;
        levelDescription.text = description;
    }

    public void ActivateInteractive(string title, string description)
    {
        interactiveDescriptionContainer.SetActive(true);
        interactiveTitle.text = title;
        interactiveDescription.text = description;
    }
    public void DeactivateInteractive()
    {
        interactiveDescriptionContainer.SetActive(false);
    }



    public void NextState()
    {
        nextState = true;
    }

    public void Wait()
    {
        
    }
}
