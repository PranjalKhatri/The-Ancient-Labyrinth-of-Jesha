using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    
    public bool _activateThis = false;
    public int _noOfSpikes;
    [SerializeField] PressureTileScript pts;
    // public float _timeDelay;
    public float _bulletVelocity;
    [SerializeField] int _counter = 0;
    [SerializeField] float _timebetweenShots = 0.5f;
    [SerializeField] float _timebetweenBurst = 2f;
    [SerializeField] int _burstAmount = 2;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform _spawnPoint;
    public bool Shooting=false;

    public float _timeDelay;
    void Start()
    {
        
    }

    void Update()
    {
        if (pts._tileBool && !_activateThis)
        {
            _activateThis = true;
        }
        else _activateThis = false;

        if (_activateThis && !Shooting)
        {
            Shooting = true;
            // ShootSpike();
            StartCoroutine(ShootSpike(_burstAmount,_noOfSpikes));
            _activateThis = false;
        }
    }

    IEnumerator ShootSpike(int nos, int nosBurst)
    {      
        for (int j=0; j<nosBurst;j++)
        {
            for (int i=0;i<nos; i++)
            {
                var bullet  = Instantiate(bulletPrefab,_spawnPoint.position,_spawnPoint.rotation);
                //bullet.GetComponent<Rigidbody2D>().velocity = this.transform.up * _bulletVelocity;
                yield return new WaitForSeconds(_timebetweenShots);
            }
            yield return new WaitForSeconds(_timebetweenBurst);
        }
        Shooting = false;
        // StopCoroutine("ShootSpike");
    }

    // void ShootSpike()
    // {
        
    // }


    
}
