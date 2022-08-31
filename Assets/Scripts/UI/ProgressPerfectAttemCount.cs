using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPerfectAttemCount : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _progressBar;
    [Header("Player")]
    [SerializeField] private EventsHandler _playerEvents;

    private void Update()
    {
        UpdateProgressFill(_playerEvents.GetPerfectCount());
    }

    private void UpdateProgressFill(byte value)
    {
        _progressBar.value = value;
    }
}
