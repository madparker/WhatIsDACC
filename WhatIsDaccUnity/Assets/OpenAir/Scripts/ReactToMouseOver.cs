using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ReactToMouseOver : MonoBehaviour {
	private Camera cam;
	private int maxDepth = 20;

	[Header("Box")]
	public MeshRenderer boxRender;
	public Material opaqueBox;
	public Material transparentBox;

	[Header("Sound")]
	public AudioSource voiceOver;
	public AudioClip[] sounds;

	[Header("UI Elements")]
	public GameObject UIBox1;
	public GameObject UIBox2;
	public Text UIBox1Text;
	public Text UIBox2Text;

	[Header("Animation References")]
	public WaterPan water;
	public SprayWater spray;
	public VacuumManager vacuum;

	[Space(10)]
	public GameObject[] buttons;

	[Header("Text")]
	[FormerlySerializedAs("InAirText")]
	[TextArea]
	public string inAirText;
	[TextArea]
	public string sorbentText;
	[TextArea]
	public string outAirText;
	[TextArea]
	public string waterText;
	[TextArea]
	public string vaccumText;
    
	[HideInInspector] public bool overBox;
	[HideInInspector] public bool overAirIn;
	[HideInInspector] public bool overAirOut;
	[HideInInspector] public bool overSorbent;
	[HideInInspector] public bool overVaccum;
	[HideInInspector] public bool overWater;


	public enum STATE {
		None, AirIn, Sorbent, Water, CO2, AirOut
	}

	[HideInInspector] public STATE currentState = STATE.None;

	public STATE CurrentState {
		get { return currentState; }
		set {
			//buttons[currentState.GetHashCode()].transform.localScale = Vector3.one;
			currentState = value;
		}
	}

	// Use this for initialization
	void Start() {
		cam = Camera.main;

		Setup();

		overBox = false;
		overAirIn = false;
		overAirOut = false;
		overSorbent = false;
		overVaccum = false;
		overWater = false;
	}

	protected virtual void Setup() {
		CurrentState = STATE.None;

		for (int i = 1; i < buttons.Length; i++) {
			buttons[i].SetActive(false);
		}
	}

	// Update is called once per frame
	void Update() {
		//print(inAirText);
		//print(sorbentText);
		//print(outAirText);
		//print(waterText);
		//print(vaccumText);

		//Reset on R
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(0);
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray);

		overBox = false;
/*        overBox = false;
        overAirIn = false;
        overAirOut = false;
        overSorbent = false;
        overVaccum = false;
        overWater = false;*/
		
        //check to see what the mouse is over
        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];

            if (hit.collider.name == "Box") {
				overBox = CurrentState >= STATE.AirIn;
			}

			//old code that used raycasting instead of OnPointerEnter: 
            /*switch (hit.collider.name) {
                case "AirInTrigger":
                    overAirIn = CurrentState >= STATE.None;
                    break;
                case "Box":
                    overBox = CurrentState >= STATE.AirIn;
                    break;
                case "SorbentTrigger":
                    overSorbent = CurrentState >= STATE.AirIn;
                    break;
                case "WaterTrigger":
                    overWater = CurrentState >= STATE.Sorbent;
                    break;
                case "VaccumTrigger":
                    overVaccum = CurrentState >= STATE.Water;
                    break;
                case "AirOutTrigger":
                    overAirOut = CurrentState >= STATE.CO2;
                    break;
            }*/
        }

		//check the state, advance to the next state when the next state is mousedOver
		switch (CurrentState) {
			case STATE.None:
				if (overAirIn) {
					CurrentState = STATE.AirIn;
				}
				break;
			case STATE.AirIn:
				if (overSorbent) {
					CurrentState = STATE.Sorbent;
				}
				break;
			case STATE.Sorbent:
				if (overWater) {
					CurrentState = STATE.Water;
				}
				break;
			case STATE.Water:
				if (overVaccum) {
					CurrentState = STATE.CO2;
				}
				break;
			case STATE.CO2:
				if (overAirOut) {
					CurrentState = STATE.AirOut;
				}
				break;
		}

		//Show all UI Boxes
		UIBox1.SetActive(overAirIn || overSorbent || overVaccum);
		UIBox2.SetActive(overAirOut || overWater);

		water.waterUp = false;

		//pulse the next UI element to attract interaction (unless they are all unlocked
		if (CurrentState < STATE.AirOut) {
			Dance(buttons[CurrentState.GetHashCode()]);
            if (CurrentState.GetHashCode() != 0) {
				buttons[CurrentState.GetHashCode() - 1].transform.localScale = Vector3.one * 1.4f;
			}
		} else if (CurrentState == STATE.AirOut) {
			buttons[CurrentState.GetHashCode() - 1].transform.localScale = Vector3.one * 1.4f;
		}

		boxRender.material = overBox ? transparentBox : opaqueBox;

		if (overAirIn) {
			UIBox1Text.text = inAirText;
		}

		if (overVaccum) {
			UIBox1Text.text = vaccumText;
			vacuum.ActivateVacuum();
		}
		if (overWater) {
			UIBox2Text.text = waterText;
			water.waterUp = true;
			spray.StartSpray();
		} else {
			spray.StopSpray();
		}

		if (overSorbent) {
			UIBox1Text.text = sorbentText;
		}

		if (overAirOut) {
			UIBox2Text.text = outAirText;
		}

	}

	public void Dance(GameObject button) {
		Vector3 newScale = Vector3.one;
		newScale.x = Mathf.Sin(Time.unscaledTime * 7.5f) / 5f + 1.5f;
		newScale.y = newScale.x;

		button.transform.localScale = newScale;
		button.SetActive(true);
	}
}