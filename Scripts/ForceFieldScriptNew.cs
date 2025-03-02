using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldScriptNew : MonoBehaviour
{
    public Vector2 _forcevec = new Vector2(100, 0);

    private bool in_triggre = false;
    private player_light light_player = null;
    private BoxCollider2D boxCollider = null;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       /* if (in_triggre)
        {
            light_player.forcefield_vec = _forcevec;
            //in_triggre=false;
            //light_player._isunderFF = false;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLight"))
        {
            light_player = other.gameObject.GetComponent<player_light>();
            in_triggre = true;
            // other.gameObject._isunderFF = true;
            // other.gameObject.forcefield_vec = _forcevec;
            player_light pl = other.gameObject.GetComponent<player_light>();
            pl._isunderFF = true;
            pl.forcefield_vec = _forcevec;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("PlayerLight"))
        {
            in_triggre = false;
            player_light pl = other.gameObject.GetComponent<player_light>();
            pl._isunderFF = false;
            // pl.forcefield_vec = _forcevec;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLight"))
        {
            Debug.Log("inside stay");
            player_light pl = other.gameObject.GetComponent<player_light>();
            pl._isunderFF = true;
            in_triggre = true;
            pl.forcefield_vec = _forcevec;
        }
    }
}
