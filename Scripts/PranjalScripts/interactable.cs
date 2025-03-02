using UnityEngine;

public class interactable : MonoBehaviour
{
    public string playerTag;
    // public Interact display;
    public GameObject press_E;
    private SwitchScript _sws;
    // public bool is_enabled = true;

    private void Start()
    {
        _sws = gameObject.GetComponent<SwitchScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == playerTag && _sws._switchState==1)
        {
            press_E.SetActive(true);
            //display.display(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            press_E.SetActive(false);
            //display.stop_display();
        }
    }
}