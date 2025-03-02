using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileSpriteScript : MonoBehaviour
{
    [SerializeField] Sprite[] _sprite_array;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] int _initspriteState = 0;
    [SerializeField] int _spriteState = 0;
    [SerializeField] int _prevState = 0;
    

    //refer child script
    public PressureTileScript pts;

    void Start()
    {
        
        spr = gameObject.GetComponent<SpriteRenderer>();
        spr.sprite = _sprite_array[_initspriteState]; 
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
        if (pts._tileBool)
        {
            _spriteState = 1;
        }
        else
        {
            _spriteState = 0;
        }
    }


}