using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
/*    public GameObject ButtonAirIn;
    public GameObject RestartButton;
    public GameObject UIBox1;
    public GameObject UIBox2;
    public GameObject ButtonSorbent;
    public GameObject ButtonVacuum;
    public GameObject ButtonWater;
    public GameObject ButtonAirOut;*/
    
    //camera and lerping
    public Camera Cam;
    public float CamStartSize;
    public float CamEndSize;
    public float LerpDuration;
    private float _timeElapsed;

    private bool _start;

    //things that appear
    public GameObject[] UIElements;
    public GameObject DAC;

    //things that disappear 
    public GameObject StartButton;
    public Text StartButtonText;
    public Text IntroParagraph;


    void Start()
    {
        //ButtonAirIn.GetComponent<CanvasRenderer>().SetAlpha(0f);
        //RestartButton.GetComponent<CanvasRenderer>().SetAlpha(0f);
        DAC.GetComponent<ReactToMouseOver>().enabled = false;

        foreach (GameObject i in UIElements) {
            i.SetActive(false);
        }

        CamEndSize = 1.1f;
        Cam.orthographicSize = CamStartSize;

        _start = false;
    }

    void Update()
    {
        //DAC.GetComponent<ReactToMouseOver>().enabled = true;

        
        if (_start && _timeElapsed < LerpDuration) {
            //zoom in on graphic
            Cam.orthographicSize = Mathf.Lerp(CamStartSize, CamEndSize, _timeElapsed / LerpDuration);

            //make button and text disappear
            StartButton.GetComponent<Image>().color = new Color(255,255,255, Mathf.Lerp(1, 0, _timeElapsed / LerpDuration));
            StartButtonText.color = new Color(50, 50, 50, Mathf.Lerp(1, 0, _timeElapsed / LerpDuration));
            IntroParagraph.color = new Color(255, 255, 255, Mathf.Lerp(1, 0, _timeElapsed / LerpDuration));


            _timeElapsed += Time.deltaTime;
        }
        else if (LerpDuration < _timeElapsed) {
            //finished fading and lerping

            //reactivate DAC component
            DAC.GetComponent<ReactToMouseOver>().enabled = true;

            //reactivate restart button
            UIElements[7].SetActive(true);

            //disable irrelevant things
            StartButton.SetActive(false);
        }
    }

    public void OnPressStart() {
        _start = true;

        StartButton.GetComponent<Image>().CrossFadeAlpha(0f, LerpDuration, false);

    }
}
