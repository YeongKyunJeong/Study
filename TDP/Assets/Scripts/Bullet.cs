using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Vector3 dir;
    private float speed = 70f;
    private float distanceThisFrame;
    [SerializeField] private GameObject impactEffect;
    private GameObject impactEffectGO;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        dir = target.position - transform.position;
        distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) // hit
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        impactEffectGO = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impactEffectGO, 2f);

        target.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
