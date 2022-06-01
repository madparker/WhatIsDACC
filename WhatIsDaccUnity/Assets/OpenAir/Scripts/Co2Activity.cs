using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Co2Activity : MonoBehaviour
{
    Vector3 orginalPos;
    Quaternion orginalRot;  
    Vector3 orginalScale;

    float upLerp = 0;

    public float maxHeight = 0.8f;
    public GameObject vacuum;
    
    public enum State
    {
        inactive, floatUp, floating, grabbed
    }

    private State currentState = State.inactive;

    public State CurrentState
    {
        get { return currentState; }
        set
        {
            print("State: " + value.ToString());
            currentState = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        orginalPos = transform.position;
        orginalRot = transform.rotation;
        orginalScale = transform.localScale;

        vacuum = GameObject.Find("Vacuum");
    }

    // Update is called once per frame
    void Update()
    {   
        print("CurrentState: " + CurrentState.ToString());
        
        switch (CurrentState)
        {
            case State.inactive:
                break;
            case State.floatUp:
                FloatUp();
                break;
            case State.floating:
                Floating();
                break;
            case State.grabbed:
                Grab();
                break;
            default:
                break;
        }
    }
    
    void FloatUp()
    {
        Vector3 position = transform.position;

        upLerp += Time.deltaTime;

        position.y = Mathf.SmoothStep(orginalPos.y, maxHeight, upLerp);

        transform.position = position;

        if (maxHeight - transform.position.y < 0.0001f)
        {
            CurrentState = State.floating;
            upLerp = 0;
        }
    }

    void Floating()
    {
        float noise = Mathf.PerlinNoise(transform.position.z, transform.position.x + Time.frameCount / 100f) / 10f;
        
        Vector3 position = transform.position;
        position.y = maxHeight + noise;
        transform.position = position;
        
        noise -= 0.05f;

        transform.Rotate(Vector3.up, 3 * noise);
        transform.Rotate(Vector3.forward, 3 * noise);


//        upLerp = 0;
//        CurrentState = State.grabbed;
    }

    void Grab()
    {
        upLerp += Time.deltaTime/2f;
        
        Vector3 endPos = vacuum.transform.position;

        transform.position = Vector3.Lerp(transform.position, endPos, upLerp);
        
        transform.localScale = Vector3.Lerp(orginalScale, Vector3.zero, upLerp * 3f);
        
//        print("transform.localScale: " + transform.localScale.x + " : " + upLerp );

        if (Vector3.Distance(transform.position, endPos) < 0.01f)
        {   
            PanelCO2Manager.instance.Reset();
        }
    }

    public void Reset()
    {
        CurrentState = State.inactive;

        transform.position = orginalPos;
        transform.rotation = orginalRot;
        transform.localScale = orginalScale;

        if (gameObject.activeInHierarchy)
        {
            GetComponent<FadeToTransparent>().Reset();
            gameObject.SetActive(false);
        }
    }
}
