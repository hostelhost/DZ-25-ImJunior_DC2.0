using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private int _lifeTimer;
    private Material _material;
    private Color _color;
    private float _maxValue = 1f;
    private float _minValue = 0f;

    private float _radiusDitonate = 10f;
    private float _forceDetonate = 1000f;

    public event Action<Bomb> Died;

    public void Initialize(int lifeTime)
    {
        _lifeTimer = lifeTime;
        _material = GetComponent<Renderer>().material;
        _color = _material.color;
        _color.a = _maxValue;
        _material.color = _color;
        StartCoroutine(BecomesTransparent());
    }

    private IEnumerator BecomesTransparent()
    {
        float timer = 0;
        float progress;

        while (timer < _lifeTimer)
        {
            progress = timer / _lifeTimer;
            _color.a = Mathf.Lerp(_maxValue, _minValue, progress);
            _material.color = _color;
            timer += Time.deltaTime;

            yield return null;
        }

        _color.a = _minValue;
        _material.color = _color;

        Detonate();
        Died?.Invoke(this);
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
