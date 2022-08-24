using System.Collections;
using TMPro;
using UnityEngine;


public class MessageManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _fail;
    [SerializeField] private TMP_Text _late;
    [SerializeField] private TMP_Text _early;
    [SerializeField] private TMP_Text _perfect;
    [SerializeField] private TMP_Text _super;

    private bool _isActive = false;

    public TMP_Text MessageFail => _fail;
    public TMP_Text MessageLate => _late;
    public TMP_Text MessageEarly => _early;
    public TMP_Text MessagePerfect => _perfect;
    public TMP_Text MessageSuper => _super;

    private void Show(TMP_Text message)
    {
        if (_isActive)
            return;

        message.gameObject.SetActive(true);
        _isActive = true;
    }

    private void Hide(TMP_Text message)
    {
        message.gameObject.SetActive(false);
        _isActive = false;
    }

    public IEnumerator ShowMessage(TMP_Text message)
    {
        Show(message);
        yield return new WaitForSeconds(1f);
        Hide(message);
    }
}
