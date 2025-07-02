using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : IAppearing
{
    private int _lifeTimer;
    private Material _material;
    private Color _color;
    private float _maxValue = 1f;
    private float _minValue = 0f;
    private Action<Vector3> _onDead;

    private float _radiusDitonate = 10f;
    private float _forceDetonate = 1000f;

    private void Awake() =>   
        _material = GetComponent<Renderer>().material;

    public override void Initialize(int lifeTime, Action<Vector3> onDead)
    {
        _lifeTimer = lifeTime;
        _onDead = onDead;
        ChangeColorADiopozon(_maxValue);
        StartCoroutine(BecomesTransparent());
    }

    private IEnumerator BecomesTransparent()
    {
        float timer = 0;
        float progress;

        while (timer < _lifeTimer)
        {
            progress = timer / _lifeTimer;
            ChangeColorADiopozon(Mathf.Lerp(_maxValue, _minValue, progress));
            timer += Time.deltaTime;

            yield return null;
        }

        ChangeColorADiopozon(_minValue);
        Detonate();
        _onDead?.Invoke(transform.position);
    }

    private void ChangeColorADiopozon(float endResult)
    {
        _color.a = endResult;
        _material.color = _color;
    }

    private void Detonate()
    {
        foreach (Rigidbody rigidbody in GetRigidbody(_radiusDitonate))
            rigidbody.AddExplosionForce(_forceDetonate, transform.position, _radiusDitonate);       
    }

    private List<Rigidbody> GetRigidbody(float radius)
    {
        List<Rigidbody> rigidbodies = new ();
        Rigidbody rigidbody;

        foreach (Collider collider in Physics.OverlapSphere(transform.position, radius))
        {
            rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
                rigidbodies.Add(rigidbody);
        }

        return rigidbodies;
    }
}
