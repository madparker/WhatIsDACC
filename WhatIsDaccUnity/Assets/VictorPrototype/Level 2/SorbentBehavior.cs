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

    public enum STATE
    {
        Idle, Ready, Full, Move, Fill, Release
    }

    public STATE currentState = STATE.Idle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (previousSorbent == null) isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case STATE.Idle:
                if (previousSorbent != null && previousSorbent.GetComponent<SorbentBehavior>().currentState == STATE.Full) currentState = STATE.Ready;
                break;
            case STATE.Ready:

                break;
            case STATE.Full:

                break;
            case STATE.Release:

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentState == STATE.Ready && other.gameObject.CompareTag("CO2")) AddToSelf(other.gameObject);
    }

    void AddToSelf(GameObject molecule)
    {
        molecules[currentMolecule] = molecule;
        molecule.GetComponent<SorbentMoleculeMovement>().inPlace = true;
        molecule.transform.position = moleculePlacements[currentMolecule].transform.position;
        molecule.transform.rotation = moleculePlacements[currentMolecule].transform.rotation;
        currentMolecule++;

        if (currentMolecule == moleculePlacements.Length) currentState = STATE.Full;
    }

    public void ClearSelf()
    {
        for (int i = 0; i < molecules.Length; i++)
        {
            Destroy(molecules[i].gameObject);
        }
    }
}
