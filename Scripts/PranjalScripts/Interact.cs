using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Camera cam;
    public RectTransform rect_transform;
    public Canvas canvas;
    public Vector3 offset;

    public void display(GameObject item)
    {
        //check collision tag

            rect_transform.gameObject.SetActive(true);
            //first you need the RectTransform component of your canvas
            RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

            //then you calculate the position of the UI element
            //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

            Vector2 ViewportPosition = cam.WorldToViewportPoint(item.transform.position + offset);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            rect_transform.anchoredPosition = WorldObject_ScreenPosition;
        
    }
    public void stop_display()
    {

            rect_transform.gameObject.SetActive(false);
        
    }
}
