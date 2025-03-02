using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldScript : MonoBehaviour
{
    public GameManager manager;
    public player_light light_player;
    public player_heavy heavy_player;
    [SerializeField] Vector2 forcefield_vec;
    [SerializeField] float vel_vector_dot;
    public int forcefield_int;  // access it from the tiles (tiles will have property to switch forcefield direction and switch will have prop to turn it off)
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (forcefield_int != -1)
        {
            FieldEffect();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLight"))
        {
            manager.under_forceField = true;
            forcefield_int = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLight"))
        {
            manager.under_forceField = false;
            forcefield_int = -1;
        }
        
    }
    void FieldEffect()
    {
        if ((light_player.transform.position - heavy_player.transform.position).magnitude < manager.max_range - 0.1f )
        {
            light_player.rb.AddForce(forcefield_vec);
        }
        else
        {
            Vector2 vector = new Vector2(light_player.transform.position.x - heavy_player.transform.position.x,light_player.transform.position.y - heavy_player.transform.position.y);
            if (Vector2.Dot(forcefield_vec.normalized, vector.normalized) > 0)
            {
                vector = Quaternion.Euler(0, 0, 90) * vector;
                vel_vector_dot = Vector2.Dot(forcefield_vec.normalized, vector.normalized);

                if (vel_vector_dot != 0)
                {
                    light_player.rb.AddForce(vector.normalized * forcefield_vec.magnitude * vel_vector_dot);
                }
            }
            else
            {
                light_player.rb.AddForce(forcefield_vec);
            }
        }
    }

}
