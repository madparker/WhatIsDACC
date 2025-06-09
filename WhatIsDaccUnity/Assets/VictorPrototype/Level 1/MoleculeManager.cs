using UnityEngine;

public class MoleculeManager : MonoBehaviour
{
    [SerializeField] GameObject[] molecules;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeactivateMolecules();
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
        }
    }

    public void DeactivateMolecules() {
        for (int i = 0; i < molecules.Length; i++)
        {
            molecules[i].gameObject.SetActive(false);
        }
    }
}
