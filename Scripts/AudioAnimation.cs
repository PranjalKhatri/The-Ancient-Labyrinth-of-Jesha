using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnimation : MonoBehaviour
{
    public void play()
    {
        GetComponent<AudioSource>().Play();
    }
}
