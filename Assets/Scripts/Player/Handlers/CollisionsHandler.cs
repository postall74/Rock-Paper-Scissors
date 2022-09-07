using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(StatusHandler))]
[RequireComponent(typeof(Mover))]
public class CollisionsHandler : MonoBehaviour
{
    public UnityAction OnEarlyMessage;
    public UnityAction OnPerfectMessage;
    public UnityAction OnLateMessage;
    public UnityAction OnFinalPush;

    [Header("Player info")]
    [SerializeField] private StatusHandler _playerStatusHandler;
    [SerializeField] private Mover _mover;
    [Header("Repulse")]
    [SerializeField] private float _jumpPower = 3;
    [SerializeField] private float _duration = 0.65f;
    [SerializeField] private Vector3 _jumpDirection;

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
            transform.DOJump(NewPositionAfterJump(transform.position, _jumpDirection), _jumpPower, 0, _duration);
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
                transform.DOJump(NewPositionAfterJump(transform.position, _jumpDirection), _jumpPower, 0, _duration);
            }
        }
    }

    private Vector3 NewPositionAfterJump(Vector3 currentPosition, Vector3 jumpdirection)
    {
        return currentPosition - jumpdirection;
    }
}
