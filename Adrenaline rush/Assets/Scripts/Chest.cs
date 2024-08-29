using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool isOpened;
    void Start()
    {
        isOpened = false;

    }
    public void Open()
    {
        if (isOpened) return;
        isOpened = true;
    }

}
