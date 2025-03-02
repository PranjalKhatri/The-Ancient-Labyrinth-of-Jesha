using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
// using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public player_heavy heavy_player;
    public float heavy_mass = 10f;
    public player_light light_player;
    public float light_mass = 1f;
    public float max_range = 5f;

    public bool update_speeds;
    public float light_speed;
    public float heavy_speed;
    public float constraint_factor;
    public MenuSystem menuSystem;
    public bool under_forceField;
    public GameObject _gameOverCanvas;


    //[HideInInspector]
    public float last_checkpoint_x;
    //[HideInInspector]
    public float last_checkpoint_y;
    public int num_color = 0;

    public GameObject Heavy_obj;
    public GameObject Light_obj;
    public Vector2 SpawnOffset;

    private bool spawned = false;
    public GameObject EscapeMenu;
    public List<CheckPoint> CheckPoints;
    public int current_checkpoint;
    public float time_bw_texts = 5f;
    public DialogManager dialogmanager;
    private float c_time = 0f;
    public List<GateManager> gates;
    // Start is called before the first frame update
    private void Awake()
    {
        if (update_speeds)
        {
            heavy_player.speed = heavy_speed;
            heavy_player.constrained_factor = constraint_factor;
            light_player.constrained_factor = constraint_factor;
            light_player.speed = light_speed;
        }
    }
    void Start()
    {
        Assert.AreNotEqual(0, heavy_mass);
        Assert.AreNotEqual(0, light_mass);

    }


    void Update()
    {
        //if(!spawned) { return; }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeMenu.SetActive(!EscapeMenu.activeInHierarchy);

        }
        //update transfor accoding to manager
        //transform.position = (heavy_mass * heavy_player.transform.position + light_mass * light_player.transform.position) / (heavy_mass + light_mass);
        //at midpoint
        transform.position = (heavy_player.transform.position + light_player.transform.position) / 2;
        if (Vector2.Distance(light_player.transform.position, heavy_player.transform.position) >= max_range)
        {
            if (c_time > time_bw_texts)
            {
                //dialog_manager.Display_random_dialog();
                dialogmanager.cstart();
                c_time = 0f;
            }
            else
            {

                c_time += Time.deltaTime;

            }
            light_player.is_constrained = true;
            heavy_player.is_constrained = true;
        }
        else
        {
            c_time += Time.deltaTime;
            light_player.is_constrained = false;
            heavy_player.is_constrained = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (heavy_player != null && light_player != null)
        {
            //transform.position = (heavy_mass * heavy_player.transform.position + light_mass * light_player.transform.position) / (heavy_mass + light_mass);
            transform.position = (heavy_player.transform.position + light_player.transform.position) / 2;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, max_range / 2);
        }
    }

    public void UpdateCheckpoint(CheckPoint check_point)
    {
        for (int i = 0; i < CheckPoints.Count; i++)
        {
            if (CheckPoints[i] == check_point)
            {
                this.current_checkpoint = i;
                break;
            }
        }
    }
    public void SavePlayer()
    {
        SaveSystem.Save(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.Load();
        this.num_color = data.num_color;
        for (int i = 0; i <= data.current_checkpoint; i++)
        {
            CheckPoints[i].Used();
            SwitchScript ss = null;
            CheckPoints[i].TryGetComponent<SwitchScript>(out ss);
            if(ss != null) { ss.Disabledbs(); }
        }
        this.last_checkpoint_x = CheckPoints[current_checkpoint].transform.position.x;
        this.last_checkpoint_y = CheckPoints[current_checkpoint].transform.position.y;
        for (int i = 0; i < num_color; i++)
        {
            gates[i]._condition = true;
            gates[i].set = true;
        }
        //Instantiate player at last check point
    }
    public void SpawnPlayer(bool load = false)
    {
        if (load) { LoadPlayer(); }
        Debug.Log(last_checkpoint_x + " " + last_checkpoint_y);
        Heavy_obj.transform.position = new Vector3(last_checkpoint_x + SpawnOffset.x, last_checkpoint_y + SpawnOffset.y, 0);
        Light_obj.transform.position = new Vector3(last_checkpoint_x - SpawnOffset.x, last_checkpoint_y - SpawnOffset.y, 0);

        spawned = true;
    }

    void EndScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {

        // throw new NotImplementedException();
        _gameOverCanvas.SetActive(true);
        Invoke(nameof(EndScreen), 2.5f);
    }

}
