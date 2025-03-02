using System.Collections.Generic;
using UnityEngine;
// using Unity.

public class player_light : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    public float speed = 10f;
    public bool isfacingright = false;
    // public bool isfacingup = false;
    [Tooltip("speed when constrained")]
    public player_heavy heavy_player;
    public GameManager manager;
    // public SpringJoint2D spring_joint;
    //[HideInInspector]
    public bool iscoliding = false;
    private Animator animL;
    public Vector2 forcefield_vec;
    [HideInInspector]
    public bool is_constrained = false;
    public Vector2 collision_dir;
    private Vector2 movement_vec = new Vector2(0, 0);
    public float constrained_factor;
    [HideInInspector]
    public Vector2 heavy_velocity;
    public bool _isunderFF = false;
    [SerializeField] float vel_vector_dot;

    private Vector2 m_prev_vel = Vector2.zero;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animL = GetComponent<Animator>();
    }
    void Update()
    {
        movement_vec.x = 0;
        movement_vec.y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            //rb.velocity = Vector2.up*Time.fixedDeltaTime*speed;
            //transform.position += Vector3.up * Time.fixedDeltaTime * speed;
            movement_vec.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement_vec.y = -1;
            //transform.position += Vector3.down * Time.fixedDeltaTime * speed;
            //rb.velocity = Vector2.down*Time.fixedDeltaTime*speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement_vec.x = 1;
            //transform.position += Vector3.right * Time.fixedDeltaTime * speed;
            //rb.velocity = Vector2.right*Time.fixedDeltaTime*speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement_vec.x = -1;
            //transform.position += Vector3.left * Time.fixedDeltaTime * speed;
            //rb.velocity = Vector2.left*Time.fixedDeltaTime*speed;
        }

    }
    private void FixedUpdate()
    {
        Move(movement_vec);
        flip();
    }
    public void Move(Vector2 move_vec)
    {
        if (_isunderFF)
        {
            if ((this.transform.position - heavy_player.transform.position).magnitude < manager.max_range - 0.1f)
            {
                this.rb.AddForce(forcefield_vec);
            }

            else
            {
                if (heavy_velocity != Vector2.zero)
                {
                    rb.velocity = heavy_velocity.normalized * Time.fixedDeltaTime * constrained_factor * speed;
                }
                Vector2 vector = new Vector2(this.transform.position.x - heavy_player.transform.position.x, this.transform.position.y - heavy_player.transform.position.y);
                if (Vector2.Dot(forcefield_vec.normalized, vector.normalized) > 0)
                {
                    vector = Quaternion.Euler(0, 0, 90) * vector;
                    vel_vector_dot = Vector2.Dot(forcefield_vec.normalized, vector.normalized);

                    if (vel_vector_dot != 0)
                    {
                        this.rb.AddForce(vector.normalized * forcefield_vec.magnitude * vel_vector_dot);
                    }
                }
                else
                {
                    this.rb.AddForce(forcefield_vec);
                }
            }
        }

        //if (move_vec == Vector2.zero) { return; }
        //heavy_velocity = Vector2.zero;
        else if (is_constrained)
        {
            Vector2 inwardsvec = transform.position - heavy_player.transform.position;
            float theta = Vector2.Dot(move_vec.normalized, inwardsvec.normalized);
            if (theta > 0 && heavy_velocity == Vector2.zero)
            {
                Vector2 perp = Quaternion.Euler(0, 0, 90) * inwardsvec;
                //Debug.Log(perp);
                //add heavy velocity
                theta = Vector2.Dot(perp.normalized, move_vec.normalized);
                rb.velocity = (perp.normalized * speed * Time.fixedDeltaTime) * theta * constrained_factor;
            }
            else if (heavy_velocity != Vector2.zero)
            {
                rb.velocity = heavy_velocity.normalized * Time.fixedDeltaTime * constrained_factor * speed;
            }

            /*if (Vector2.Distance(transform.position + move_vec.normalized * Time.fixedDeltaTime * speed, heavy_player.transform.position) < manager.max_range)
            {
                rb.velocity = move_vec.normalized * speed * Time.fixedDeltaTime;

            }*/
            else
            {
               // Debug.Log("in else:(");
                //rb.velocity = new Vector2(0, 0) + heavy_velocity.normalized * constrained_factor * Time.fixedDeltaTime * speed;
                rb.velocity = move_vec.normalized * speed * Time.fixedDeltaTime;
            }
        }
        else
        {
            rb.velocity = move_vec.normalized * speed * Time.fixedDeltaTime;
        }




    }

    public void heavy_move(Vector2 dir)
    {
        /* Vector2 vel = dir.normalized * Time.fixedDeltaTime*speed*constrained_factor;
         Debug.Log(vel);
         rb.velocity += dir.normalized * speed * constrained_factor * Time.fixedDeltaTime;*/
        heavy_velocity = dir;
        //flip();
    }
    public void flip()
    {
        int upsate = -2;
        /*
         play right animation if rb.velocity.x > 0
        0->stop down
        1->stop up
        2->stop left
        3->move left
        4->
        5->
        6->
        7->
        8->
        9->
        10->
        11->
         */
        int cstate = 0;
        if ((isfacingright && rb.velocity.x < 0) || (!isfacingright && rb.velocity.x > 0))
        {
            isfacingright = !isfacingright;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
        if (rb.velocity.x == 0)
        {
            if (rb.velocity.y > 0) { upsate = 1; }
            else if (rb.velocity.y < 0) { upsate = 0; }

        }

        float vx = rb.velocity.x;
        float vy = rb.velocity.y;
        if(rb.velocity == Vector2.zero)
        {
            if(m_prev_vel.x != 0) {}
            else if(m_prev_vel.y != 0) { upsate = animL.GetInteger("AnimState"); }

        }
        else
        {
            m_prev_vel = rb.velocity;
        }
        /*if(vx > 0)
        {
            cstate = 4;
        }else if (vx < 0)
        {
            cstate = 3;
        }
        else
        {
            cstate = 2;
        }
        if(vx == 0 && vy > 0) { 
            cstate = 1;
        } else if(vx==0 && vy ==0)
        {
            cstate = 0;
        }*/
        if (upsate >= 0) { cstate = upsate; }
        else
        {
            //Debug.Log("x vel" + Mathf.Abs(rb.velocity.x));
            cstate = (Mathf.Abs(rb.velocity.x) > 0) ? 3 : 2;
        
        }
        /*if(isfacingright && movement_vec != Vector2.zero ) { cstate =  2; }
        else if()*/
       
        animL.SetInteger("AnimState", cstate);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        iscoliding = true;
        //spring_joint.enabled = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        iscoliding = false;
        //spring_joint.enabled = false;
    }

    /* void FieldEffect()
     {
         if ((this.transform.position - heavy_player.transform.position).magnitude < manager.max_range - 0.1f)
         {
             this.rb.AddForce(forcefield_vec);
         }
         else
         {
             Vector2 vector = new Vector2(this.transform.position.x - heavy_player.transform.position.x, this.transform.position.y - heavy_player.transform.position.y);
             if (Vector2.Dot(forcefield_vec.normalized, vector.normalized) > 0)
             {
                 vector = Quaternion.Euler(0, 0, 90) * vector;
                 vel_vector_dot = Vector2.Dot(forcefield_vec.normalized, vector.normalized);

                 if (vel_vector_dot != 0)
                 {
                     this.rb.AddForce(vector.normalized * forcefield_vec.magnitude * vel_vector_dot);
                 }
             }
             else
             {
                 this.rb.AddForce(forcefield_vec);
             }
         }
     }*/
    public void Die()
    {
        /*
            Play animation
         */
        animL.SetBool("Dead",true);
        manager.GameOver();
        

    }
}
