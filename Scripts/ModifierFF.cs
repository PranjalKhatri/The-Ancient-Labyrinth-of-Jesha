using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierFF : MonoBehaviour
{
    [SerializeField] ForceFieldScriptNew ffs;
    [SerializeField] PressureTileScript _tb;
    [SerializeField] Vector2 _forcevec;
    bool _enabled = false;
    // Start is called before the first frame update

    void Start()
    {
        _tb = this.gameObject.GetComponent<PressureTileScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tb._tileBool && !_enabled)
        {
            _enabled = true;
            ffs._forcevec = _forcevec;
        }
    }
}
