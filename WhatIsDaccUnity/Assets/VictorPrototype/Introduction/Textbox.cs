using UnityEngine;

public class Textbox : MonoBehaviour
{
    [SerializeField] GameObject textbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDescription()
    {
        textbox.SetActive(!textbox.activeInHierarchy);
    }
}
