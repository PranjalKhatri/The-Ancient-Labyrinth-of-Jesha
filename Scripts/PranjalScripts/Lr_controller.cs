using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Lr_controller : MonoBehaviour
{
    public Transform[] players = new Transform[2];
    public GameManager gameManager;
    [Range(0f, 0.5f)]
    public float minWidth = 5f;
    [Range(0.1f, 1f)]
    public float maxWidth = 5f;
    private float max_range;
    private LineRenderer line_renderer;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsTrue(maxWidth > minWidth);
        line_renderer = GetComponent<LineRenderer>();
        line_renderer.positionCount = 2;
        max_range = gameManager.max_range;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(players[0].position, players[1].position);
        line_renderer.startWidth = Mathf.Lerp(minWidth,maxWidth,(1-dist/max_range));
        line_renderer.endWidth = line_renderer.startWidth;
        for (int i = 0; i < players.Length;i++)
        {
            line_renderer.SetPosition(i, players[i].position);
        }
    }
}
