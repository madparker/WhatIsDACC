using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWaterDrop : MonoBehaviour
{
    public float minY = -0.49f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
