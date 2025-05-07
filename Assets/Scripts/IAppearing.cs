using System;
using UnityEngine;

public interface IAppearing
{
    void Initialize(int lifeTime, Action<Vector3> OnDead );
}
