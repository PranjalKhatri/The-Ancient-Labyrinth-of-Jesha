using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    /*
     * Also change the sprite if any to used
     */
    public string player_tag;
    public GameManager manager;
    public bool used = false;
    bool check_key = false;
    public GameObject Press_E;
    private void Update()
    {
        if (!used && check_key && Input.GetKeyDown(KeyCode.E))
        {
            Used();
        }
    }
    public void Used()
    {
        used = true;
        check_key = false;
        manager.UpdateCheckpoint(this);
        manager.SavePlayer();
        Press_E.SetActive(false);
        //GetComponent<interactable>().display.stop_display();
        //GetComponent<interactable>().is_enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Do animation
        //Debug.Log("s");
        if (collision.tag == player_tag && !used)
        {
            Debug.Log("voila");
            check_key = true;
            Press_E.SetActive(true);
            //manager.UpdateCheckpoint(transform.position);
            //used = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == player_tag)
        {
            Debug.Log("voila");
            check_key = false;
            Press_E.SetActive(false);
            //manager.UpdateCheckpoint(transform.position);
            //used = true;
        }
    }
}