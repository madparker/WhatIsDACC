using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] CameraMover cameraMover; //On Camera Manager
    [SerializeField] MoleculeManager moleculeManager; //On Level 1
    [SerializeField] SorbentManager sorbentManager; //On Level 2
    [SerializeField] HydroswingManager hydroswingManager; //On Level 3

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
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject releaseOptions;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI levelTitle;
    [SerializeField] TextMeshProUGUI levelDescription;
    [SerializeField] TextMeshProUGUI interactiveTitle;
    [SerializeField] TextMeshProUGUI interactiveDescription;

    [Header("Level Descriptions")]
    [SerializeField] string airInSetUpTitle;
    [SerializeField][TextArea] string airInSetUpDescription;
    [SerializeField] string airInTitle;
    [SerializeField] [TextArea] string airInDescription;
    [SerializeField] string airInWaitText;

    [SerializeField] string absorbSetUpTitle;
    [SerializeField][TextArea] string absorbSetUpDescription;
    [SerializeField] string absorbSetUpWaitText;
    [SerializeField] string[] absorbTitles;
    [SerializeField][TextArea] string[] absorbDescriptions;

    //private variables
    int releaseChoice = 0;

    public enum STATE
    {
        Intro, AirInSetUp, AirIn, Absorb, Release, Water, Vacuum, AirOut
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
                    SetLevelTextContent(airInSetUpTitle, airInSetUpDescription);
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
                    SetLevelTextContent(airInTitle, airInDescription);

                    setUpState = true;
                }

                bool airIsEmpty = moleculeManager.CheckIfNull();

                waitButton.SetActive(!airIsEmpty); //Checks if the array of molecules is empty
                moleculeManager.ToggleFan(!airIsEmpty); //Turns off the fan

                if (nextState)
                {
                    DeactivateInteractive();
                    cameraMover.UpdateCameraPosition();
                    moleculeManager.ToggleFan(false);

                    //waitButton.SetActive(true);
                    nextButton.SetActive(false);

                    SetLevelText(false);
                    DeactivateInteractive();

                    currentState = STATE.Absorb;
                    mouseTracker.enabled = false;

                    nextState = false;
                    setUpState = false;
                }
                break;
            case STATE.Absorb:

                if (!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(absorbSetUpTitle, absorbSetUpDescription);
                    setUpState = true;

                    sorbentManager.SetUp();
                    
                    boxFront.enabled = false;
                    //boxTop.enabled = false;
                }

                if(sorbentManager.isFull && !releaseOptions.activeInHierarchy)
                {
                    releaseOptions.SetActive(true);
                    SetDescription(false);
                    ActivateInteractive(absorbTitles[releaseChoice], absorbDescriptions[releaseChoice]);
                }


                if (nextState)
                {
                    cameraMover.UpdateCameraPosition();
                    currentState = STATE.Release;

                    nextState = false;
                    setUpState = false;

                    releaseOptions.SetActive(false);

                    SetLevelText(false);
                    DeactivateInteractive();
                }
                break;
            case STATE.Release:
                if (!cameraMover.isMoving && !setUpState)
                {
                    //SetLevelText(true);
                    //SetLevelTextContent(absorbSetUpTitle, absorbSetUpDescription);
                    
                    hydroswingManager.NextStep();
                    setUpState = true;

                    //boxFront.enabled = false;
                    //boxTop.enabled = false;
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

    void SetDescription(bool textState)
    {
        levelDescriptionContainer.SetActive(textState);
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

    public void ReleaseChoice(int choice)
    {
        releaseChoice = choice;
        ActivateInteractive(absorbTitles[releaseChoice], absorbDescriptions[releaseChoice]);
    }
}
