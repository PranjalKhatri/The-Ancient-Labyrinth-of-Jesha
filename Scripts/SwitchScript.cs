using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    // public Camera cam;
    // public RectTransform interactable;
    // public Canvas canvas;
    public int _switchState=0; // disabled:0 enabledoff:1 enabledon:2
    [SerializeField] bool _isInRange = false;
    public bool _aBool=false;
    public GameObject E_state;
    //take reference of all pts required
    public List<DynamicSpikeScript> dbs;
    [SerializeField] bool _noActivationRequired=false;
    public bool E_canbe = true;
    public List<PressureTileScript> pts;
    public bool endswitch = false;
    //take reference of door/action object

    
    void Start()
    {
        if (_noActivationRequired == true)
        {
            for(int i =0;i < pts.Count;i++)
            {
                pts[i]._tileBool = true;
                // if (pts[i]._tileBool == true)
                // {
                //     _aBool = true;
                // }
                
            }
        }
    }

    void Update()
    {
        for(int i =0;i < pts.Count;i++)
            {
                // pts[i]._tileBool = true;
                if (pts[i]._tileBool == true)
                {
                    _aBool = true;
                }
                else 
                {
                    _aBool = false;
                    break;
                }
                
            }
        
        //check for state, and corresponding action
        if (_aBool == true && _switchState == 0)
        {
            _switchState = 1;
        }
        else if (_aBool == false && _switchState != 0)
        {
            _switchState = 0;
        }

        if (_switchState!=0 && _isInRange==true)
        {
            //Debug.Log("Hello");
            if (_switchState == 1 && Input.GetKeyDown(KeyCode.E)) 
            {
                ChangeSwitchState(2);
                E_state.SetActive(false);
                E_canbe = false;
               /* foreach(var j in dbs)
                {
                    j._activeSpike = false;
                    j._killState = false;
                    j._notOnCollision = true;
                }*/
               Disabledbs();
                if (endswitch)
                {
                    GameObject.FindAnyObjectByType<DialogManager>().End();
                }
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.E) && _switchState==0 && _isInRange==true) {} // play sound?
        
    }
    public void Disabledbs()
    {
        foreach (var j in dbs)
        {
            j._activeSpike = false;
            j._killState = false;
            j._notOnCollision = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLight")){
            _isInRange = true;
            if (E_canbe)
            {
                Debug.Log("ActivatedE");
                E_state.SetActive(true);
            }
            // interactable.gameObject.SetActive(true);
            // RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
            // Vector2 ViewportPosition = cam.WorldToViewportPoint(transform.position);

            // //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
            
            // Vector2 WorldObject_ScreenPosition = new Vector2(
            // ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            // ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            // //now you can set the position of the ui element

            // interactable.anchoredPosition = WorldObject_ScreenPosition;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLight")){
            _isInRange = false;
            E_state.SetActive(false);
            // interactable.gameObject.SetActive(false);

        }
    }

    void ChangeSwitchState(int targetstate)
    {
        _switchState = targetstate;
    }

}
