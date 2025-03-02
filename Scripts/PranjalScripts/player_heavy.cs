using UnityEngine;


public class player_heavy : MonoBehaviour
{
    public float speed = 10f;
    [Tooltip("speed when constrained")]
    public float constrained_factor = 0.2f;
    public player_light light_player;
    public GameManager manager;
    private Animator animH;
    public bool _isdead = false;

    public bool isfacingright = false;
    [HideInInspector]
    public bool iscoliding = false;
    [HideInInspector]
    public bool is_constrained = false;
    private Vector2 m_prev_vel = Vector2.zero;

    // public Animator animH;

    public Vector2 collision_dir;

    private Vector2 movement_vec = new Vector2(0, 0);
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animH = GetComponent<Animator>();

        // animH = GetComponent<Animator>();

        // animH.SetInteger("MoveState",0);
    }

    void Update()
    {
        if (_isdead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        movement_vec.x = 0;
        movement_vec.y = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //rb.velocity = Vector2.up*Time.fixedDeltaTime*speed;
            //transform.position += Vector3.up * Time.fixedDeltaTime * speed;
            movement_vec.y = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement_vec.y = -1;
            //transform.position += Vector3.down * Time.fixedDeltaTime * speed;
            //rb.velocity = Vector2.down*Time.fixedDeltaTime*speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement_vec.x = 1;
            //transform.position += Vector3.right * Time.fixedDeltaTime * speed;
            //rb.velocity = Vector2.right*Time.fixedDeltaTime*speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
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
    private void Move(Vector3 move_vec)
    {
        if (move_vec == Vector3.zero)
        {
            light_player.heavy_velocity = Vector2.zero;
            rb.velocity = Vector2.zero;
            return;
        }
        /* if (Vector2.Distance(transform.position, light_player.transform.position) >= manager.max_range)
         {
             float angle = Vector2.Angle(move_vec, transform.position - light_player.transform.position);

             Debug.Log(angle + " degrees");
             if (!(angle > 133 && angle < 182))
             {
                 Debug.Log("constrained colliding but going inside");
                 rb.velocity = Vector2.zero;

                 return;
             }
         }*/
        if (is_constrained)
        {
            //light_player.heavy_velocity = Vector2.zero;
            //if light player is not colliding
            if (light_player.iscoliding == false)
            {
                //check if our movement keeps us inside the circle if so then move
                if (Vector2.Distance(transform.position + move_vec.normalized * Time.fixedDeltaTime * speed, light_player.transform.position) <= manager.max_range)
                {
                    light_player.heavy_velocity = Vector2.zero;
                    Debug.Log("constrained but not coliding go within limits");
                    rb.velocity = (move_vec.normalized * Time.fixedDeltaTime * speed);

                }
                else//move light player with us in the direction towards us
                {
                    Debug.Log("ififelse");
                    rb.velocity = (move_vec.normalized * Time.fixedDeltaTime * speed * constrained_factor);
                    //transform.position += move_vec.normalized * Time.fixedDeltaTime * speed * constrained_factor;
                    ////light_player.rb.velocity = ((transform.position - light_player.transform.position).normalized * Time.fixedDeltaTime * speed * constrained_factor);
                    light_player.heavy_move((transform.position - light_player.transform.position));
                    //light_player.heavy_velocity = transform.position-light_player.transform.position;

                }
            }
            else//if light player is colliding :(
            {
                //if we are inside the circle : Bindass
                //Debug.Log(transform.position + move_vec*speed*Time.fixedDeltaTime);
                //Debug.Log("D" + Vector2.Angle(transform.position - light_player.transform.position, move_vec));
                float angle = Vector2.Angle(move_vec, transform.position - light_player.transform.position);
                Debug.Log(angle + " degrees");
                if ((angle > 133 && angle < 182))
                {
                    Debug.Log("constrained colliding but going inside");
                    rb.velocity = (move_vec.normalized * Time.fixedDeltaTime * speed);

                }
                else
                {
                    //move light player towards us
                    light_player.heavy_move((transform.position - light_player.transform.position));
                    //light_player.heavy_velocity = transform.position-light_player.transform.position;
                    //if now we are in circle of influence then move ourselves
                    /* if (Vector2.Distance(transform.position + move_vec.normalized * Time.fixedDeltaTime * speed * constrained_factor, light_player.transform.position) <= manager.max_range)
                     {
                         Debug.Log("constrained colliding and going out");
                         //rb.velocity = light_player.rb.velocity * (move_vec.normalized * Time.fixedDeltaTime * speed * constrained_factor);

                     }*/
                    //rb.velocity = light_player.rb.velocity*move_vec;

                    /*Changes*//////////////////////
                    Vector2 inwardsvec = transform.position - light_player.transform.position;
                    float theta = Vector2.Dot(move_vec.normalized, inwardsvec.normalized);
                    if (theta > 0)
                    {
                        Vector2 perp = Quaternion.Euler(0, 0, 90) * inwardsvec;
                        Debug.Log(perp);
                        //add heavy velocity
                        theta = Vector2.Dot(perp.normalized, move_vec.normalized);
                        rb.velocity = (perp.normalized * speed * Time.fixedDeltaTime) * theta * constrained_factor;
                    }
                }
            }
        }
        else
        {
            Debug.Log("else");
            rb.velocity = move_vec.normalized * speed * Time.fixedDeltaTime;
            //transform.position += move_vec.normalized * Time.fixedDeltaTime * speed;
        }
        //rb.velocity = move_vec.normalized*speed*Time.fixedDeltaTime;
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
        if (rb.velocity == Vector2.zero)
        {
            if (m_prev_vel.x != 0) { }
            else if (m_prev_vel.y != 0) { upsate = animH.GetInteger("AnimState"); }

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

        animH.SetInteger("AnimState", cstate);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        iscoliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        iscoliding = false;
    }

    public void Die()
    {

        /*
            Play animation
         */
        animH.SetBool("Dead", true);
        manager.GameOver();
        // Invoke(nameof(manager.menuSystem.LoadMainMenu))
    }
    // void OnCollisionEnter2D()

    //Interacting stuff

}
