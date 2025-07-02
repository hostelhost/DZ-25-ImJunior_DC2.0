using System;
using UnityEngine;

public abstract class IAppearing : MonoBehaviour
{
    public abstract void Initialize(int lifeTime, Action<Vector3> OnDead);
}
