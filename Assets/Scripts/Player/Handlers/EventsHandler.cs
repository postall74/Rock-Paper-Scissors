using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsHandler : MonoBehaviour
{
    private const string Rock = "Rock";
    private const string Paper = "Paper";
    private const string Scissors = "Scissors";
    private const string Speed = "SpeedMult";
    private const string Finish = "Finish";

    [Header("Player Info")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _model;
    [SerializeField] private Animator _animator;
    [Header("Status player handler")]
    [SerializeField] private StatusHandler _status;
    [Header("Collision player handler")]
    [SerializeField] private CollisionsHandler _collision;
    [Header("UI Message Manager")]
    [SerializeField] private MessageManager _messageManager;
    [Header("UI")]
    [Header("Button")]
    [SerializeField] private Button _rockButton;
    [SerializeField] private Button _paperButton;
    [SerializeField] private Button _scissorsButton;
    [Header("Perfect Tries Slider")]
    [SerializeField] private Slider _perfectTriesSlider;

    private bool _isPerferctMessagesShow = false;
    private bool _isEarlyMessage = false;
    private bool _isPerfectMessage = false;
    private bool _isLateMessage = false;
    private bool _isFailMessage = false;
    private bool _isShow = false;

    public void ResetMessageShow()
    {
        _isPerfectMessage = false;
        _isEarlyMessage = false;
        _isLateMessage = false;
        _isFailMessage = false;
        _isShow = false;
    }

    public void ActivateMessage(bool isEarlyMessage, bool isPerfectMessage, bool isLateMessage, bool isFailMessage)
    {
        _isEarlyMessage = isEarlyMessage;
        _isPerfectMessage = isPerfectMessage;
        _isLateMessage = isLateMessage;
        _isFailMessage = isFailMessage;
    }

    public void ShowMessage()
    {
        if (_isShow) return;
        else
        {
            if (_isEarlyMessage)
            {
                ShowEarlyMessage();
            }
            else if (_isPerfectMessage)
            {
                ShowPerfectMessage();
            }
            else if (_isLateMessage)
            {
                ShowLateMessage();
            }
            else if (_isFailMessage)
            {
                ShowFailMessage();
            }
        }

        _isShow = true;
    }

    private void OnEnable()
    {
        _rockButton.onClick.AddListener(OnRockButtonClick);
        _paperButton.onClick.AddListener(OnPaperButtonClick);
        _scissorsButton.onClick.AddListener(OnScissorsButtonClick);
        _collision.OnFailMessage += ShowFailMessage;
        _collision.OnEarlyMessage += ShowEarlyMessage;
        _collision.OnPerfectMessage += ShowPerfectMessage;
        _collision.OnLateMessage += ShowLateMessage;
        _collision.OnFinalPush += FinalPush;
    }

    private void OnDisable()
    {
        _rockButton.onClick.RemoveListener(OnRockButtonClick);
        _paperButton.onClick.RemoveListener(OnPaperButtonClick);
        _scissorsButton.onClick.RemoveListener(OnScissorsButtonClick);
        _collision.OnFailMessage -= ShowFailMessage;
        _collision.OnEarlyMessage -= ShowEarlyMessage;
        _collision.OnPerfectMessage -= ShowPerfectMessage;
        _collision.OnLateMessage -= ShowLateMessage;
        _collision.OnFinalPush -= FinalPush;
    }

    private void ShowFailMessage()
    {
        _isPerferctMessagesShow = false;
        StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageFail));
    }

    private void ShowLateMessage()
    {
        _isPerferctMessagesShow = false;
        StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageLate));
    }

    private void ShowEarlyMessage()
    {
        _isPerferctMessagesShow = false;
        StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageEarly));
    }

    private void ShowPerfectMessage()
    {
        if (!_isPerferctMessagesShow)
            StartCoroutine(_messageManager.ShowMessage(_messageManager.MessagePerfect));
        else
            StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageSuper));

        _isPerferctMessagesShow = true;
    }

    private void FinalPush()
    {

        _animator.SetFloat(Speed, 0);
        _animator.SetTrigger(Finish);

    }

    private void OnRockButtonClick()
    {
        _status.ChageStatus(StatusEnum.Rock);
        _animator.SetTrigger(Rock);
    }

    private void OnPaperButtonClick()
    {
        _status.ChageStatus(StatusEnum.Paper);
        _animator.SetTrigger(Paper);
    }

    private void OnScissorsButtonClick()
    {
        _status.ChageStatus(StatusEnum.Scissors);
        _animator.SetTrigger(Scissors);
    }
}
