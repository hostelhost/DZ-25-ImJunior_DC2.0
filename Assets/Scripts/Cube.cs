using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour, IAppearing
{
    private WaitForSeconds _lifeTimer;
    private bool _isCollision;
    private Rigidbody _rigidbody;
    private Action<Vector3> _onDead;
    private Material _material;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollision == false)
        {
            if (collision.collider.GetComponent<TriggerPlatform>())
            {
                _isCollision = true;
                GetRandomColor();
                StartCoroutine(TimerToDeath());
            }
        }
    }

    public void Initialize(int lifeTime, Action<Vector3> onDead)
    {
        _lifeTimer = new WaitForSeconds(lifeTime);
        _onDead = onDead;
        Reseting();
    }

    private IEnumerator TimerToDeath()
    {
        yield return _lifeTimer;

        _onDead?.Invoke(transform.position);
    }

    private void Reseting()
    {
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _isCollision = false;
        _material.color = Color.white;
    }

    private void GetRandomColor() =>
          _material.color = UnityEngine.Random.ColorHSV();
}
