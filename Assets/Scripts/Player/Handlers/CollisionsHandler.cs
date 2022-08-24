using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Player))]
[RequireComponent(typeof(StatusHandler))]
[RequireComponent(typeof(EventsHandler))]
public class CollisionsHandler : MonoBehaviour
{
    public UnityAction OnEarlyMessage;
    public UnityAction OnPerfectMessage;
    public UnityAction OnLateMessage;
    public UnityAction OnFailMessage;
    public UnityAction OnFinalPush;

    [Header("Player info")]
    [SerializeField] private Player _player;
    [SerializeField] private StatusHandler _playerStatusHandler;
    [SerializeField] private EventsHandler _eventsHandler;
    [SerializeField] private Mover _mover;
    [Header("Repulse")]
    [SerializeField] private float _pushForceToPlayer = 15f;
    [SerializeField] private float _pushForceToPlayerUp = 3f;




    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EarlyPlatform earlyPlatform))
        {
            if (earlyPlatform.GetComponent<Status>().CurrentStatus == _playerStatusHandler.PlayerStatus)
            {
                OnEarlyMessage?.Invoke();
                _eventsHandler.ActivateMessage(true, false, false, false);
                earlyPlatform.DisableAllPlatformColliders();
            }

            earlyPlatform.DisableCollider();
        }
        else if (other.TryGetComponent(out PerfectPlatform perfectPlatform))
        {
            if (perfectPlatform.GetComponent<Status>().CurrentStatus == _playerStatusHandler.PlayerStatus)
            {
                OnPerfectMessage?.Invoke();
                _eventsHandler.ActivateMessage(false, true, false, false);
                perfectPlatform.DisableAllPlatformColliders();
            }

            perfectPlatform.DisableCollider();
        }
        else if (other.TryGetComponent(out LatePlatform latePlatform))
        {
            if (latePlatform.GetComponent<Status>().CurrentStatus == _playerStatusHandler.PlayerStatus)
            {
                OnLateMessage?.Invoke();
                _eventsHandler.ActivateMessage(false, false, true, false);
                latePlatform.DisableCollider();
            }
            else
            {
                OnFailMessage?.Invoke();
                _eventsHandler.ActivateMessage(false, false, true, true);
                latePlatform.DisableCollider();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            if (_playerStatusHandler.TryWin(_playerStatusHandler, enemy.GetComponent<Status>()))
            {
                OnFinalPush?.Invoke();
            }
            else
            {
                _player.GetComponent<Rigidbody>().AddForce(Vector3.back * _pushForceToPlayer, ForceMode.VelocityChange);
                _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _pushForceToPlayerUp, ForceMode.VelocityChange);
            }
        }
    }
}
