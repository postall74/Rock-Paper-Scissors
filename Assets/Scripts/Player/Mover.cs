using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public float Speed => _speed;

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * _speed * Vector3.forward, Space.World);
    }
}
