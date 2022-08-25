using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstaclePaper : Obstacle
{
    [Header("Repulse")]
    [SerializeField] private float _pushForceToObstacle = 650f;
    [SerializeField] private float _waitTime;
    [Header("Object information")]
    [SerializeField] private Status _status;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            if (player.TryGetComponent(out StatusHandler playerCurrentStatus))
            {
                if (_status.CurrentStatus == playerCurrentStatus.PlayerStatus)
                    StartCoroutine(DoorBroke());
            }
        }
    }

    private IEnumerator DoorBroke()
    {
        _rigidbody.isKinematic = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DetachChildren();
        }

        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
        transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * _pushForceToObstacle, ForceMode.Acceleration);

        yield return new WaitForSeconds(.05f);
        _boxCollider.enabled = false;

        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }
}
