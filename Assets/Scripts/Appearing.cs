using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public abstract class Appearing : MonoBehaviour
{
    public abstract void Initialize(int lifeTime, Action<Vector3> OnDead);
}