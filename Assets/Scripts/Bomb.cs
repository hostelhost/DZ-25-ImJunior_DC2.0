using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Material _material;
    private WaitForSeconds _lifeTime;

    public event Action<Bomb> Died;

    public void Initialize(int lifeTime)
    {
        _lifeTime = new WaitForSeconds(lifeTime);
        _material = GetComponent<Renderer>().material;
        //_material.color.a; тут нужно возвращать прозрачность к исходному значению
    }

    //private IEnumerator BecomesTransparent()
    //{
    //    //while ()
    //    //{

    //    //    yield return _WFSlifeTime;
    //    //}

    //    private IEnumerator StartVampirise()
    //    {
    //        int ticksCount = _workTimeAbility / _pauseTimeBetweenTicks;
    //        int damageDone;

    //        for (int i = 0; i < ticksCount; i++)
    //        {
    //            if (_detector.TryIdentifyNearestTarget(out Enemy enemy))
    //            {
    //                damageDone = enemy.TakeDamage(_powerAbility);
    //                _player.AddLifeForceIfValid(damageDone);
    //            }

    //            yield return _waitForSecondsPauseTimeBetweenTicks;
    //        }

    //        _detectorZoneDisplay.enabled = false;

    //        yield return _waitForSecondsReloadTimeAbility;

    //        _isBusy = false;
    //    }
    //}

    private void Detonate()
    {

    }
}
