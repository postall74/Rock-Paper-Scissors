using UnityEngine;

public enum StatusEnum
{
    Rock,
    Paper,
    Scissors
}

public class Status : MonoBehaviour
{
    [SerializeField] private StatusEnum _currentStatus;

    public StatusEnum CurrentStatus => _currentStatus;

    public void ChangeStatus(StatusEnum status)
    {
        _currentStatus = status;
    }
}