using UnityEngine;
using UnityEngine.Events;

public class StatusHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Status _status;

    private bool _isChangeStatus = false;

    public bool IsChangeStatus => _isChangeStatus;
    public StatusEnum PlayerStatus => _status.CurrentStatus;

    public void ResetChangeStatus()
    {
        _isChangeStatus = false;
    }

    public void ChageStatus(StatusEnum status)
    {
        if (_status.CurrentStatus != status)
        {
            _status.ChangeStatus(status);
            _isChangeStatus = true;
        }
        else
            _isChangeStatus = false;
    }

    public bool TryWin(StatusHandler player, Status enemy)
    {
        if (ScissorsWin(player, enemy))
            return true;
        if (PaperWin(player, enemy))
            return true;
        if (RockWin(player, enemy))
            return true;

        return false;
    }

    private bool ScissorsWin(StatusHandler player, Status enemy)
    {
        if (player.PlayerStatus == StatusEnum.Scissors && enemy.CurrentStatus == StatusEnum.Paper)
            return true;

        return false;
    }

    private bool PaperWin(StatusHandler player, Status enemy)
    {
        if (player.PlayerStatus == StatusEnum.Paper && enemy.CurrentStatus == StatusEnum.Rock)
            return true;

        return false;
    }

    private bool RockWin(StatusHandler player, Status enemy)
    {
        if (player.PlayerStatus == StatusEnum.Rock && enemy.CurrentStatus == StatusEnum.Scissors)
            return true;

        return false;
    }
}
