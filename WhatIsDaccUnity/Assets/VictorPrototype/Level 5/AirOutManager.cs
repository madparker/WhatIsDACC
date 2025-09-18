using UnityEngine;

public class AirOutManager : MonoBehaviour
{

    [Header("Molecules")]
    [SerializeField] Transform moleculeSpawn;
    [SerializeField] GameObject oxygenPrefab;
    [SerializeField] GameObject nitrogenPrefab;

    [Header("Changeable Variables")]
    [SerializeField] float spawnDelay = 5;

    [Header("UI Elements")]
    [SerializeField] GameObject textbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        Invoke("SpawnMolecule", spawnDelay);
        textbox.SetActive(true);
    }

    void SpawnMolecule()
    {
        int rand = Random.Range(0, 2);
        float randY = Random.Range(-0.1f, 0.1f);
        float randZ = Random.Range(-0.2f, 0.2f);

        Vector3 spawnLocation = new Vector3(moleculeSpawn.position.x, moleculeSpawn.position.y + randY, moleculeSpawn.position.z + randZ);

        switch (rand)
        {
            case 0:
                Instantiate(oxygenPrefab, spawnLocation, Quaternion.identity);
                break;
            case 1:
                Instantiate(nitrogenPrefab, spawnLocation, Quaternion.identity);
                break;
        }

        Invoke("SpawnMolecule", spawnDelay);
    }
}
