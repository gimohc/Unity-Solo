using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pouch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pouch;
    public GameObject GetPouch() {
        return pouch;
    }
    // whenever one spawns under this gameobject start coroutine to make it disappear within 60 seconds 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
