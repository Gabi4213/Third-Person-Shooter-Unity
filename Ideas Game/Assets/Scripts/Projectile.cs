using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime;
    public ParticleSystem destroyParticle;
    public MeshRenderer mesh;


    private void Start()
    {
        StartCoroutine(DestroyParticle());
    }

    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(lifetime);
        mesh.enabled = false;
        destroyParticle.Play();
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}