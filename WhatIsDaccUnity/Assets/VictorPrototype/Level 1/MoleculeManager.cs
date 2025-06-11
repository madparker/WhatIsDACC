using UnityEngine;

public class MoleculeManager : MonoBehaviour
{
    [SerializeField] GameObject[] molecules;
    [SerializeField] GameObject fan;
    [SerializeField] Transform endGoalTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeactivateMolecules();
        ToggleFan(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMolecules()
    {
        for (int i = 0; i < molecules.Length; i++)
        {
            molecules[i].gameObject.SetActive(true);
            molecules[i].GetComponent<MoleculeBehavior>().goalPosition = endGoalTransform.position;
        }
    }

    public void DeactivateMolecules() {
        for (int i = 0; i < molecules.Length; i++)
        {
            molecules[i].gameObject.SetActive(false);
        }
    }

    public bool CheckIfNull()
    {
        int nullCount = 0;

        for (int i = 0; i < molecules.Length; i++)
        {
            if(molecules[i].gameObject == null) nullCount++;
        }

        return nullCount == molecules.Length;
    }

    public void SetUpAirIn()
    {
        for (int i = 0; i < molecules.Length; i++)
        {
            molecules[i].gameObject.SetActive(false);
        }
    }

    public void ToggleFan(bool toggle)
    {
        fan.SetActive(toggle);
    }
}
