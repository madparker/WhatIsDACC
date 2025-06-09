using UnityEngine;

public class SorbentBehavior : MonoBehaviour
{
    [SerializeField] Transform[] moleculePlacements;
    [SerializeField] GameObject previousSorbent;
    [SerializeField] bool isLastSorbet;

    public bool isFull;
    public bool isActive;

    GameObject[] molecules = new GameObject[3];
    int currentMolecule = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (previousSorbent == null) isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive && previousSorbent != null && previousSorbent.GetComponent<SorbentBehavior>().isFull) isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActive && other.gameObject.CompareTag("CO2") && !isFull) AddToSelf(other.gameObject);
    }

    void AddToSelf(GameObject molecule)
    {
        molecules[currentMolecule] = molecule;
        molecule.GetComponent<SorbentMoleculeMovement>().inPlace = true;
        molecule.transform.position = moleculePlacements[currentMolecule].transform.position;
        molecule.transform.rotation = moleculePlacements[currentMolecule].transform.rotation;
        currentMolecule++;

        if(currentMolecule == moleculePlacements.Length) isFull = true;
    }
}
