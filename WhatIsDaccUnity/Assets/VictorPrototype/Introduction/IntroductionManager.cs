using System.Collections.Generic;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    [Header("Molecules")]
    [SerializeField] Transform moleculeSpawn;
    [SerializeField] GameObject oxygenPrefab;
    [SerializeField] GameObject nitrogenPrefab;
    [SerializeField] GameObject carbonPrefab;

    [Header("UI Objects")]
    [SerializeField] GameObject introductionText;

    //private variables
    List<GameObject> molecules = new List<GameObject>();
    bool hasStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("SpawnMolecule", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        introductionText.SetActive(false);
        for(int i = 0; i < molecules.Count; i++)
        {
            Destroy(molecules[i]);
        }
        hasStarted = true;
    }

    void SpawnMolecule()
    {
        int rand = Random.Range(0, 3);
        float randY = Random.Range(-0.1f, 0.1f);
        float randZ = Random.Range(-0.2f, 0.2f);

        Vector3 spawnLocation = new Vector3(moleculeSpawn.position.x, moleculeSpawn.position.y + randY, moleculeSpawn.position.z + randZ);

        GameObject newMolecule = null;

        switch (rand)
        {
            case 0:
                if(!hasStarted) newMolecule = Instantiate(carbonPrefab, spawnLocation, Quaternion.identity);
                break;
            case 1:
                if (!hasStarted) newMolecule = Instantiate(oxygenPrefab, spawnLocation, Quaternion.identity);
                break;
            case 2:
                if (!hasStarted) newMolecule = Instantiate(nitrogenPrefab, spawnLocation, Quaternion.identity);
                break;
        }

        if(newMolecule != null) molecules.Add(newMolecule);

        if(!hasStarted) Invoke("SpawnMolecule", 0.5f);
    }
}
