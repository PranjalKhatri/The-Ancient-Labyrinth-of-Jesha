using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteScript : MonoBehaviour
{
    [SerializeField] Sprite[] _sprite_array;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] int _initspriteState = 0;
    [SerializeField] int _spriteState = 0;
    [SerializeField] int _prevState = 0;

    //refer child script
    public SwitchScript _switch_script;

    void Start()
    {
        
        spr = gameObject.GetComponent<SpriteRenderer>();
        spr.sprite = _sprite_array[_initspriteState]; //Debug.Log("h");
    }

    void Update()
    {
        MatchChildState();
        if (_spriteState != _prevState)
        {
            spr.sprite = _sprite_array[_spriteState];
            _prevState = _spriteState;
        }
    }

    void MatchChildState()
    {
        if (_spriteState != _switch_script._switchState)
        {
            _spriteState = _switch_script._switchState;
        }
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Gate"))
    //     {
    //         if (_switch_script._switchState ==2){
    //         GateManager gt = other.gameObject.GetComponent<GateManager>();
    //         gt._condition = true;
    //         }
    //     }
    // }


}
