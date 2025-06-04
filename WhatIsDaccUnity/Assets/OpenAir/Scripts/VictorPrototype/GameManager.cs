using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] CameraMover cameraMover; //On Camera Manager
    [SerializeField] MoleculeManager moleculeManager; //On Level 1

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

    [Header("Text")]
    [SerializeField] TextMeshProUGUI levelTitle;
    [SerializeField] TextMeshProUGUI levelDescription;
    [SerializeField] TextMeshProUGUI interactiveTitle;
    [SerializeField] TextMeshProUGUI interactiveDescription;

    [Header("Level Descriptions")]
    [SerializeField] string airInTitle;
    [SerializeField] [TextArea] string airInDescription;

    [SerializeField] string filterTitle;
    [SerializeField][TextArea] string filterDescription;

    public enum STATE
    {
        Intro, AirIn, Filter, Water, Vacuum, AirOut
    }

    [HideInInspector] public STATE currentState = STATE.Intro;

    //Private Variables
    bool nextState = false;
    bool setUpState = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseTracker = GetComponent<MouseTracker>();

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

                    currentState = STATE.AirIn;
                    mouseTracker.enabled = true;

                    nextState = false;
                    setUpState = false;
                }
                break;
            case STATE.AirIn:

                if(!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(airInTitle, airInDescription);
                    moleculeManager.ActivateMolecules();

                    setUpState = true;
                }



                if (nextState)
                {
                    DeactivateInteractive();
                    cameraMover.UpdateCameraPosition();
                    moleculeManager.DeactivateMolecules();

                    SetLevelText(false);
                    DeactivateInteractive();

                    currentState = STATE.Filter;
                    mouseTracker.enabled = false;

                    nextState = false;
                    setUpState = false;
                }
                break;
            case STATE.Filter:

                if (!cameraMover.isMoving && !setUpState)
                {
                    SetLevelText(true);
                    SetLevelTextContent(filterTitle, filterDescription);
                    setUpState = true;

                    boxFront.enabled = false;
                    boxTop.enabled = false;
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
}
