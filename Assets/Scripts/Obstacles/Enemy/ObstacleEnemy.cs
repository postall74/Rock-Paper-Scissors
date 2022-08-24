using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleEnemy : Obstacle
{
    [Header("Repulse")]
    [SerializeField] private float _pushForceToObstacle = 650f;
    [SerializeField] private float _pushForceToPlayer = 15f;
    [SerializeField] private float _pushForceToPlayerUp = 3f;
    [Header("Object information")]
    [SerializeField] private Status _status;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [Header("")]
    [SerializeField] private Player _player;
    [SerializeField] private Mover _mover;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && player.TryGetComponent(out StatusHandler playerCurrentStatus))
        {
            if (playerCurrentStatus.TryWin(playerCurrentStatus, _status))
            {
                StartCoroutine(FinalPush());
            }
        }
    }

    public IEnumerator FinalPush()
    {
        _mover.SetSpeed(0);
        _player.GetComponent<Rigidbody>().AddForce(Vector3.forward * _pushForceToPlayer, ForceMode.VelocityChange);
        _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _pushForceToPlayerUp, ForceMode.VelocityChange);

        yield return StartCoroutine(EnemyPush());
    }

    private IEnumerator EnemyPush()
    {
        _rigidbody.isKinematic = false;
        _boxCollider.isTrigger = false;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.forward * _pushForceToObstacle, ForceMode.Acceleration);

        yield return new WaitForSeconds(2.5f);
    }
}
