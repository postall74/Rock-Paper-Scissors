using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public float Speed => _speed;

    private void Update()
    {
        transform.Translate(Time.deltaTime * _speed * Vector3.forward, Space.World);
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.GetComponent<Player>())
        {
            collision.transform.SetParent(transform, false);
        }
    }*/
}
