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
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _shirt;
    [Header("Massie materials")]
    [SerializeField] private Material[] _greenShirt;
    [SerializeField] private Material[] _redShirt;
    [SerializeField] private Material[] _blueShirt;
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

    private bool _isPerferctMessagesShow = false;
    private bool _isLateMessageShow = false;
    private byte _perfectCount = 0;

    public byte GetPerfectCount()
    {
        return _perfectCount;
    }

    private void Start()
    {
        if (_status.PlayerStatus == StatusEnum.Paper)
            _paperButton.image.sprite = _paperButton.spriteState.selectedSprite;
        if (_status.PlayerStatus == StatusEnum.Scissors)
            _scissorsButton.image.sprite = _scissorsButton.spriteState.selectedSprite;
        if (_status.PlayerStatus == StatusEnum.Rock)
            _rockButton.image.sprite = _rockButton.spriteState.selectedSprite;
    }

    private void OnEnable()
    {
        _rockButton.onClick.AddListener(OnRockButtonClick);
        _paperButton.onClick.AddListener(OnPaperButtonClick);
        _scissorsButton.onClick.AddListener(OnScissorsButtonClick);
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
        _collision.OnEarlyMessage -= ShowEarlyMessage;
        _collision.OnPerfectMessage -= ShowPerfectMessage;
        _collision.OnLateMessage -= ShowLateMessage;
        _collision.OnFinalPush -= FinalPush;
    }

    private void ShowLateMessage()
    {
        _isPerferctMessagesShow = false;
        if (!_isLateMessageShow)
        {
            StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageLate));
        }
        else
        {
            StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageFail));
        }

        _isLateMessageShow = true;
    }

    private void ShowEarlyMessage()
    {
        _isPerferctMessagesShow = false;
        _isLateMessageShow = false;
        StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageEarly));
    }

    private void ShowPerfectMessage()
    {
        _isLateMessageShow = false;

        if (!_isPerferctMessagesShow)
            StartCoroutine(_messageManager.ShowMessage(_messageManager.MessagePerfect));
        else
            StartCoroutine(_messageManager.ShowMessage(_messageManager.MessageSuper));

        ChangePerfectCount();
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
        ChangeClothOnPlayer(_blueShirt);
    }

    private void OnPaperButtonClick()
    {
        _status.ChageStatus(StatusEnum.Paper);
        _animator.SetTrigger(Paper);
        ChangeClothOnPlayer(_greenShirt);
    }

    private void OnScissorsButtonClick()
    {
        _status.ChageStatus(StatusEnum.Scissors);
        _animator.SetTrigger(Scissors);
        ChangeClothOnPlayer(_redShirt);
    }

    private void ChangeClothOnPlayer(Material[] materials)
    {
        _shirt.GetComponent<SkinnedMeshRenderer>().materials = materials;
    }

    private void ChangePerfectCount()
    {
        byte _maxCount = 5;

        if (_perfectCount < _maxCount)
            _perfectCount++;

        return;
    }
}
