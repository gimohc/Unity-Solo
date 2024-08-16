using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction swing; 
    bool isSwinging = false;

    void Start()
    {
        swing.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(swing.ReadValue<float>() > 0 && ! isSwinging) {
            StartCoroutine(hitCoroutine());
        }
    }
    private IEnumerator hitCoroutine() {
        isSwinging = true;
        yield return new WaitForSeconds(1f); //placeholder replace with weapon speed
        isSwinging = false;
    }
}
