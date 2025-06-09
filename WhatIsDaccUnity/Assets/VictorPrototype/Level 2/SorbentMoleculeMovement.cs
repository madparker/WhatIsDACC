using UnityEditor.Build;
using UnityEngine;

public class SorbentMoleculeMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    public bool inPlace = false;

    Collider col;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPlace) this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        else if(col.enabled && inPlace) col.enabled = false;

        if(this.transform.position.x > 1) Destroy(this.gameObject);
    }
}
