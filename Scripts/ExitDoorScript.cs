using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorScript : MonoBehaviour
{
    public bool _doorState = false;

    //refer its trigger
    [SerializeField] SwitchScript _switch;


    void Update()
    {
        if (_switch._switchState == 2 && _doorState==false)
        {
            _doorState = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PlayerLight") || other.gameObject.CompareTag("PlayerHeavy"))
        {
            return; // camera change
        }
        else return;
    }

    
}
