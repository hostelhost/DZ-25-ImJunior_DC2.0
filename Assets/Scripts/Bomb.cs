using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private int _lifeTime;
    private Material _material;
    private Color _color;
    private float _maxValue = 1f;
    private float _minValue = 0f;

    private float _radiusDitonate = 10f;
    private float _forceDetonate = 100f;

    public event Action<Bomb> Died;

    public void Initialize(int lifeTime)
    {
        _lifeTime = lifeTime;
        _material = GetComponent<Renderer>().material;
        _color = _material.color;
        _color.a = _maxValue;
        _material.color = _color;  // проверить как все будет работать с пулом, может стоит передавать колар напрямую пулом. или еще как то
    }

    private IEnumerator BecomesTransparent()
    {
        float timer = 0;
        float progress;

        while (timer < _lifeTime)
        {
            progress = timer / _lifeTime;
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
