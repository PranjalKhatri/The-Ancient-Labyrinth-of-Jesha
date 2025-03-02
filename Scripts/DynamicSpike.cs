using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpikeScript : MonoBehaviour
{
    public Animator _anim;
    public bool _activeSpike = false;
    private bool _limitbool = false;
    [SerializeField] public bool _killState = false;
    [SerializeField] float _timeToMax=0.5f;
    [SerializeField] public bool _notOnCollision;
    [SerializeField] bool _staticSpike;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        AnimationKillHandler();
    }

    void AnimationKillHandler()
    {
        if (_staticSpike && !_activeSpike) 
        {
            _anim.SetInteger("_spikeState",-1);
            _killState = false;
        }

        else if (_staticSpike && _activeSpike)
        {
            _anim.SetInteger("_spikeState",1);
            _killState = true;
        }
        
        else
        {
            if (_activeSpike == true && _limitbool == false)
            {
                _anim.SetInteger("_spikeState",1);
                _limitbool = true;
                Invoke(nameof(KillState),_timeToMax);
            }
            if (_activeSpike == false && _limitbool == true)
            {
                _anim.SetInteger("_spikeState",-1);
                _limitbool = false;
                Invoke(nameof(KillState),_timeToMax);
            }
        }
    }


    void KillState()
    {
        _killState = !_killState;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (_killState)
            {
                if (other.tag == "PlayerLight" || other.tag == "PlayerHeavy")
                {
                    if (other.tag == "PlayerLight")  other.gameObject.GetComponent<player_light>().Die();
                    else if (other.tag == "PlayerHeavy")   other.gameObject.GetComponent<player_heavy>().Die();
                   
                }
            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_notOnCollision)
        {
            return;
        }
        else
        {
            if (other.gameObject.CompareTag("PlayerHeavy") || other.gameObject.CompareTag("PlayerLight"))
            {
                _activeSpike = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_notOnCollision)
        {
            return;
        }
        else
        {
           if (other.gameObject.CompareTag("PlayerHeavy") || other.gameObject.CompareTag("PlayerLight"))
            {
                _activeSpike = false;
            }
        }
    }
}
