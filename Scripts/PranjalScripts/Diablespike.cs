using System.Collections.Generic;
using UnityEngine;

public class Disablespike : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerLight" || collision.tag == "PlayerHeavy")
        {
            foreach(GameObject go in gameObjects)
            {
                go.SetActive(false);
            }
        }
    }
}