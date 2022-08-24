using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private const string Rock = "Rock";
    private const string Paper = "Paper";
    private const string Scissors = "Scissors";

    [Header("Enemy Info")]
    [SerializeField] private GameObject _model;
    [SerializeField] private Status _status;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        if (_status.CurrentStatus == StatusEnum.Rock)
        {
            RockStatusAnimation();
        }
        else if (_status.CurrentStatus == StatusEnum.Paper)
        {
            PapaerStatusAnimation();
        }
        else if (_status.CurrentStatus == StatusEnum.Scissors)
        {
            ScissorsStatusAnimation();
        }
    }

    private void RockStatusAnimation()
    {
        _animator.SetTrigger(Rock);
    }

    private void PapaerStatusAnimation()
    {
        _animator.SetTrigger(Paper);
    }

    private void ScissorsStatusAnimation()
    {
        _animator.SetTrigger(Scissors);
    }
}
