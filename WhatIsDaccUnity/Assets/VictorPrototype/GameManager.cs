using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] CameraMover cameraMover; //On Camera Manager
    [SerializeField] IntroductionManager introductionManager; //On Introduction
    [SerializeField] MoleculeManager moleculeManager; //On Level 1
    [SerializeField] SorbentManager sorbentManager; //On Level 2
    [SerializeField] HydroswingManager hydroswingManager; //On Level 3-A
    [SerializeField] TemperatureswingManager temperatureswingManager; //On Level 3-B
    [SerializeField] ElectroswingManager electroswingManager; //On Level 3-C
    [SerializeField] CaptureManager captureManager; //On Level 4
    [SerializeField] AirOutManager airOutManager; //On Level 5

    [SerializeField] MeshRenderer boxTop;
    [SerializeField] MeshRenderer boxFront;

    //Internal Components
    MouseTracker mouseTracker;

    //Changable Variables


    //Public Variables
    [Header("UI Objects")]
    [SerializeField] GameObject airInUi;


    [Header("Text Objects")]
    [SerializeField] GameObject levelTitleContainer;
    [SerializeField] GameObject levelDescriptionContainer;
    [SerializeField] GameObject interactiveDescriptionContainer;
    [SerializeField] GameObject waitButton;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject releaseOptions;
    [SerializeField] GameObject toggleInstructions;

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

    [SerializeField] string releaseTitle;
    [SerializeField][TextArea] string releaseDescription;

    [SerializeField] string tempTitle;
    [SerializeField][TextArea] string tempDescription;

    [SerializeField] string captureTitle;
    [SerializeField][TextArea] string captureDescription;

    [SerializeField] string airOutTitle;
    [SerializeField][TextArea] string airOutDescription;

    //private variables
    int releaseChoice = 0;

    public enum STATE
    {
        Start, Intro, AirInSetUp, AirIn, Absorb, Hydro, Electro, Temp, Capture, AirOut
    }

    public STATE currentState = STATE.Start;

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
            case STATE.Start:
                if (nextState)
                {
                    cameraMover.UpdateCameraPosition();

                    introductionManager.StartGame();

                    currentState = STATE.Intro;

                    nextState = false;
                    setUpState = false;
                }

                break;

            case STATE.Intro:

                if(nextState)
                {
                    cameraMover.UpdateCameraPosition();

                    introductionManager.LearnMore();

                    nextButton.SetActive(true);
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

                    //airInUi.SetActive(true);

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

                if (Input.GetMouseButtonDown(0)) SetDescription(false);
                if (Input.GetMouseButtonDown(1)) SetDescription(!levelDescriptionContainer.activeInHierarchy);


                if (nextState)
                {
                    DeactivateInteractive();
                    cameraMover.UpdateCameraPosition();
                    moleculeManager.ToggleFan(false);

                    //waitButton.SetActive(true);
                    nextButton.SetActive(false);
                    toggleInstructions.SetActive(false);

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
                    
                }

                if(sorbentManager.isFull && !releaseOptions.activeInHierarchy)
                {
                    releaseOptions.SetActive(true);
                    SetDescription(false);
                    ActivateInteractive(absorbTitles[releaseChoice], absorbDescriptions[releaseChoice]);
                    toggleInstructions.SetActive(false);
                }


                if (nextState)
                {
                    //cameraMover.UpdateCameraPosition();

                    switch(releaseChoice)
                    {
                        case 0:
                            currentState = STATE.Hydro;
                            cameraMover.UpdateCameraPosition();
                            break;
                        case 1:
                            currentState = STATE.Temp;
                            cameraMover.PrecisionUpdateCameraPosition(4);
                            break;
                        case 2:
                            currentState = STATE.Electro;
                            cameraMover.PrecisionUpdateCameraPosition(5);
                            break;

                    }

                    nextState = false;
                    setUpState = false;

                    releaseOptions.SetActive(false);

                    SetLevelText(false);
                    DeactivateInteractive();
                    toggleInstructions.SetActive(false);
                }
                break;
            case STATE.Hydro:
                if (!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(releaseTitle, releaseDescription);

                    hydroswingManager.NextStep();
                    setUpState = true;
                }

                if (Input.GetMouseButtonDown(0)) SetDescription(false);
                if (Input.GetMouseButtonDown(1)) SetDescription(!levelDescriptionContainer.activeInHierarchy);

                if (hydroswingManager.currentState == HydroswingManager.STATE.End)
                {
                    cameraMover.PrecisionUpdateCameraPosition(6);
                    currentState = STATE.AirOut;
                    SetLevelText(false);
                    toggleInstructions.SetActive(false);

                    setUpState = false;

                    boxFront.enabled = true;
                }
                break;
            case STATE.Temp:
                if (!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(tempTitle, tempDescription);

                    temperatureswingManager.StartMinigame();
                    setUpState = true;
                }


                if (Input.GetMouseButtonDown(0)) SetDescription(false);
                if (Input.GetMouseButtonDown(1)) SetDescription(!levelDescriptionContainer.activeInHierarchy);

                if (temperatureswingManager.currentState == TemperatureswingManager.STATE.End)
                {
                    cameraMover.PrecisionUpdateCameraPosition(6);
                    currentState = STATE.AirOut;
                    SetLevelText(false);
                    toggleInstructions.SetActive(false);

                    setUpState = false;
                }
                break;

            case STATE.Electro:
                if (!cameraMover.isMoving && !setUpState)
                {
                    //SetLevelText(true);
                    //SetLevelTextContent(releaseTitle, releaseDescription);

                    electroswingManager.SetUp();
                    setUpState = true;
                }

                if(electroswingManager.currentState == ElectroswingManager.STATE.End)
                {
                    cameraMover.PrecisionUpdateCameraPosition(6);
                    currentState = STATE.AirOut;
                    SetLevelText(false);
                    toggleInstructions.SetActive(false);

                    setUpState = false;
                }
                break;
            case STATE.Capture:
                if (!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(captureTitle, captureDescription);

                    captureManager.SetUp();
                    setUpState = true;
                }

                if (Input.GetMouseButtonDown(0)) SetDescription(false);
                if (Input.GetMouseButtonDown(1)) SetDescription(!levelDescriptionContainer.activeInHierarchy);

                if (captureManager.currentState == CaptureManager.STATE.End)
                {
                    cameraMover.UpdateCameraPosition();
                    currentState = STATE.AirOut;
                    SetLevelText(false);
                    toggleInstructions.SetActive(false);

                    setUpState = false;
                }


                break;
            case STATE.AirOut:
                if (!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(airOutTitle, airOutDescription);
                    
                    restartButton.SetActive(true);

                    airOutManager.SetUp();

                    setUpState = true;
                }
                break;
        }

        //DEBUG
        if(Input.GetKeyDown(KeyCode.Alpha1)) //HYDRO
        {
            currentState = STATE.Hydro;

            cameraMover.DebugCamera(3);

            introductionManager.StartGame();

            nextState = false;
            setUpState = false;

            releaseOptions.SetActive(false);

            SetLevelText(false);
            DeactivateInteractive();

            nextState = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) //Electro
        {
            currentState = STATE.Electro;
            cameraMover.PrecisionUpdateCameraPosition(5);
            introductionManager.StartGame();

            nextState = false;
            setUpState = false;

            releaseOptions.SetActive(false);

            SetLevelText(false);
            DeactivateInteractive();

            nextState = true;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) //CAPTURE
        {
            currentState = STATE.Capture;
            cameraMover.DebugCamera(4);
            introductionManager.StartGame();

            nextState = false;
            setUpState = false;

            releaseOptions.SetActive(false);

            SetLevelText(false);
            DeactivateInteractive();

            nextState = true;

        }

    }

    public void NextState()
    {
        nextState = true;
    }

    void SetUpNextState(STATE next)
    {
        switch(next)
        {
            case STATE.AirInSetUp:
                break;
            case STATE.AirIn:
                break;
            case STATE.Absorb:
                break;
            case STATE.Hydro:
                break;
            case STATE.Electro:
                break;
            case STATE.Temp:
                break;
            case STATE.Capture:
                break;
            case STATE.AirOut:
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
        toggleInstructions.SetActive(!textState);
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



    


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReleaseChoice(int choice)
    {
        releaseChoice = choice;
        ActivateInteractive(absorbTitles[releaseChoice], absorbDescriptions[releaseChoice]);
    }
}
