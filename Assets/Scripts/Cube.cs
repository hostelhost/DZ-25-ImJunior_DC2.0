using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour, IAppearing
{
    private WaitForSeconds _lifeTimer;
    private bool _isCollision;
    private Rigidbody _rigidbody;
    private Action<Vector3> _onDead;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(int lifeTime, Action<Vector3> onDead)
    {
        _lifeTimer = new WaitForSeconds(lifeTime);
        _onDead = onDead;
        ResetSpins();

        _isCollision = false;
        GetComponent<Renderer>().material.color = Color.white;
    }

    private IEnumerator TimerToDeath()
    {
        yield return _lifeTimer;

        _onDead?.Invoke(transform.position);
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

    private void ResetSpins()
    {
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void GetRandomColor() =>
          GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
}
