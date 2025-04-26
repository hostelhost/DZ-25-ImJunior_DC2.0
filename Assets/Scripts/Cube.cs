using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private int _lifeTime;
    private bool _isCollision;

    public event Action<Cube> Died;

    public void Initialize(int lifeTime)
    {
        _lifeTime = lifeTime;
        _isCollision = false;
        GetComponent<Renderer>().material.color = Color.white;
    }

    private IEnumerator TimerToDeath()
    {
        yield return new WaitForSeconds(_lifeTime);

        Died?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollision == false)
        {
            if (collision.collider.GetComponent<TriggerPlatform>())
            {
                _isCollision = true;
                StartCoroutine(TimerToDeath());
            }
        }
    }

    private void GetRandomColor() =>
          GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
}
