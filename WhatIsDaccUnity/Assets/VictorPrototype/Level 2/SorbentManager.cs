using UnityEngine;
using UnityEngine.UI;

public class SorbentManager : MonoBehaviour
{
    [Header("Molecules")]
    [SerializeField] Transform moleculeSpawn;
    [SerializeField] GameObject carbonPrefab;
    [SerializeField] GameObject oxygenPrefab;
    [SerializeField] GameObject nitrogenPrefab;


    [Header("Sorbents")]
    [SerializeField] GameObject firstSorbent;
    [SerializeField] GameObject lastSorbent;
    [SerializeField] float spawnDelay = 5;

    [Header("UI Elements")]
    [SerializeField] GameObject uiElements;
    [SerializeField] Slider sorbentFillMeter;

    public bool isFull;
    public float fillCount;
    int carbonCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFull) {
            //isFull = lastSorbent.GetComponent<SorbentBehavior>().currentState == SorbentBehavior.STATE.Full;
            sorbentFillMeter.value = fillCount / 9;

            if(fillCount == 9)
            {
                isFull = true;
            }
        }
    }

    public void SetUp()
    {
        Invoke("SpawnMolecule", spawnDelay);
        uiElements.SetActive(true);
        firstSorbent.GetComponent<SorbentBehavior>().currentState = SorbentBehavior.STATE.Ready;
    }

    public void DeactivateUI()
    {
        uiElements.SetActive(false);
    }

    void SpawnMolecule()
    {
        int rand = Random.Range(0, 5);
        float randY = Random.Range(-0.1f, 0.1f);
        float randZ = Random.Range(-0.2f, 0.2f);

        Vector3 spawnLocation = new Vector3(moleculeSpawn.position.x, moleculeSpawn.position.y + randY, moleculeSpawn.position.z + randZ);

        switch(rand)
        {
            case 0:
                carbonCount++;
                Instantiate(carbonPrefab, spawnLocation, Quaternion.identity);
                break;
            case 1:
                Instantiate(oxygenPrefab, spawnLocation, Quaternion.identity);
                break;
            case 2:
                Instantiate(nitrogenPrefab, spawnLocation, Quaternion.identity);
                break;
            default:
                carbonCount++;
                Instantiate(carbonPrefab, spawnLocation, Quaternion.identity);
                break;
        }

        if(!isFull && carbonCount < 9) Invoke("SpawnMolecule", spawnDelay);
    }
} 
