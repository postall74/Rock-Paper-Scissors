using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


[RequireComponent(typeof(Player))]
[RequireComponent(typeof(StatusHandler))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rigidbody))]
public class CollisionsHandler : MonoBehaviour
{
    public UnityAction OnEarlyMessage;
    public UnityAction OnPerfectMessage;
    public UnityAction OnLateMessage;
    public UnityAction OnFinalPush;

    [Header("Player info")]
    [SerializeField] private Player _player;
    [SerializeField] private StatusHandler _playerStatusHandler;
    [SerializeField] private Mover _mover;
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Repulse")]
    [SerializeField] private float _pushForceToPlayer = 15f;
    [SerializeField] private float _waitTime = 3f;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EarlyPlatform earlyPlatform))
        {
            if (earlyPlatform.GetComponent<Status>().CurrentStatus == _playerStatusHandler.PlayerStatus)
            {
                OnEarlyMessage?.Invoke();
                earlyPlatform.DisableAllPlatformColliders();
            }

            earlyPlatform.DisableCollider();
        }
        else if (other.TryGetComponent(out PerfectPlatform perfectPlatform))
        {
            if (perfectPlatform.GetComponent<Status>().CurrentStatus == _playerStatusHandler.PlayerStatus)
            {
                OnPerfectMessage?.Invoke();
                perfectPlatform.DisableAllPlatformColliders();
            }

            perfectPlatform.DisableCollider();
        }
        else if (other.TryGetComponent(out LatePlatform latePlatform))
        {
            OnLateMessage?.Invoke();
            latePlatform.DisableCollider();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Obstacle obstacle) && obstacle.GetComponent<Status>().CurrentStatus != _playerStatusHandler.PlayerStatus)
        {
            //_rigidbody.AddForce(Vector3.back * _pushForceToPlayer, ForceMode.VelocityChange);
            //_rigidbody.AddForce(Vector3.up * _pushForceToPlayerUp, ForceMode.VelocityChange);
            transform.DOJump(transform.position - new Vector3(0, 0, 6), _pushForceToPlayer, 0, _waitTime);
        }
        else if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            if (_playerStatusHandler.TryWin(_playerStatusHandler, enemy.GetComponent<Status>()))
            {
                OnFinalPush?.Invoke();
                _mover.SetSpeed(0);
            }
            else
            {
                //_player.GetComponent<Rigidbody>().AddForce(Vector3.back * _pushForceToPlayer, ForceMode.VelocityChange);
                //_player.GetComponent<Rigidbody>().AddForce(Vector3.up * _pushForceToPlayerUp, ForceMode.VelocityChange);
            }
        }
    }
}
