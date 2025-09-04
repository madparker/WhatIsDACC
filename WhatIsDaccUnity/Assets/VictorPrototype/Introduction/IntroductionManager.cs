using System.Collections.Generic;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    [Header("External GameObjects")]
    [SerializeField] GameObject daccMachine;
    [SerializeField] Transform daccReady;

    [Header("Molecules")]
    [SerializeField] Transform moleculeSpawn;
    [SerializeField] GameObject oxygenPrefab;
    [SerializeField] GameObject nitrogenPrefab;
    [SerializeField] GameObject carbonPrefab;

    [Header("UI Objects")]
    [SerializeField] GameObject introductionText;
    [SerializeField] GameObject startObjects;
    [SerializeField] GameObject introObjects;

    //private variables
    List<GameObject> molecules = new List<GameObject>();
    bool hasStarted = false;
    bool isIntro = true;
    bool spawnedMolecules = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Invoke("SpawnMolecule", 0.5f);
        startObjects.SetActive(true);
        introObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isIntro)
        {
            if (daccMachine.transform.position != daccReady.position)
            {
                daccMachine.transform.position = Vector3.MoveTowards(daccMachine.transform.position, daccReady.position, Time.deltaTime);
            }
            else
            {
                if(!spawnedMolecules)
                {
                    Invoke("SpawnMolecule", 0.5f);
                    spawnedMolecules = true;
                }
            }
        }
            
    }

    public void StartGame()
    {
        isIntro = false;

        startObjects.SetActive(false);
        introObjects.SetActive(true);
    }

    public void LearnMore()
    {
        introductionText.SetActive(false);
        for (int i = 0; i < molecules.Count; i++)
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
