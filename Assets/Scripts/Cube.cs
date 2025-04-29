using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private int _lifeTimer;
    private bool _isCollision;

    public event Action<Cube> Died;

    public void Initialize(int lifeTime)
    {
        _lifeTimer = lifeTime;
        _isCollision = false;
        GetComponent<Renderer>().material.color = Color.white;
    }

    private IEnumerator TimerToDeath()
    {
        yield return new WaitForSeconds(_lifeTimer);

        Died?.Invoke(this);
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

    private void GetRandomColor() =>
          GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
}
