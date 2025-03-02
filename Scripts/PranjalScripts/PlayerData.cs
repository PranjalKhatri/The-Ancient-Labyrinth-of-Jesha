using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    public int current_checkpoint;
    //public List<Color> color_keys;
    public int num_color;
    public PlayerData(GameManager manager) 
    {
        current_checkpoint = manager.current_checkpoint;
        num_color = manager.num_color;
      //  color_keys = manager.color_keys;
    }
}
