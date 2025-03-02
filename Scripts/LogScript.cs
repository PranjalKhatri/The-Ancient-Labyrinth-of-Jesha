
using UnityEngine;

public class LogScript : MonoBehaviour
{
    [SerializeField] SwitchScript _switch_script;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float _velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       LogStart(); 
       if (this.transform.position.x < -65f)
       {
            Destroy(this.gameObject);
       }
    }

    void LogStart()
    {
        if (_switch_script._switchState == 2)
        {
            rb.velocity = new Vector3 (-1*_velocity,0,0);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerLight") || other.CompareTag("PlayerHeavy"))
        {
             if (other.tag == "PlayerLight")  other.gameObject.GetComponent<player_light>().Die();
            else if (other.tag == "PlayerHeavy")   other.gameObject.GetComponent<player_heavy>().Die();
        }
    }

}
