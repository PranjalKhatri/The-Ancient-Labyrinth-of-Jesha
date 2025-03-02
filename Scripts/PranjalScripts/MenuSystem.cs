using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public string MainScene;
    public string ControlScene;
    public GameManager manager;
    public GameObject main_scene;
    public GameObject main_menu;
    public Animator main_menu_animator;
    public GameObject story_text;
    public void Exit_Game()
    {
        Debug.Log("a");
        Application.Quit();
    }
    public void Play()
    {
        story_text.SetActive(true);
        Debug.Log("a");
        main_scene.SetActive(true);
        manager.SpawnPlayer();
        main_menu.SetActive(false);
    }
    public void Load()
    {
        Debug.Log("a");
        main_scene.SetActive(true);
        manager.SpawnPlayer(true);
        main_menu.SetActive(false);
    }

    public void LoadMainMenu()
    {

        SceneManager.LoadScene(MainScene);
    }
    public void LoadControlScene()
    {
        SceneManager.LoadScene(ControlScene);
    }
    public void next()
    {
        main_menu_animator.SetBool("next", true);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
