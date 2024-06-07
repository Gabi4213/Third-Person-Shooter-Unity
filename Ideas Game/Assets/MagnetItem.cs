using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public ParticleSystem magnetParticle;
    public float timer;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            StartCoroutine(Timer());
        }
       // else
      //  {
           // magnetParticle.Stop();
       // }
    }

    IEnumerator Timer()
    {
        magnetParticle.Stop();
        yield return new WaitForSeconds(timer);
        magnetParticle.Play();
    }
}
