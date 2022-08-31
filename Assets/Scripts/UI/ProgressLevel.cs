using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLevel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _progressBar;

    [Header("Player & Level")]
    [SerializeField] private Transform _endLevelTransform;
    [SerializeField] private Transform _playerTransform;

    private Vector3 _endLevelPosition;
    private float _fullDistance;

    private void Start()
    {
        _endLevelPosition = _endLevelTransform.position;
        _fullDistance = GetDistance();
    }

    private void Update()
    {
        float newDistance = GetDistance();
        float progressValue = Mathf.InverseLerp(_fullDistance, 0f, newDistance);

        UpdateProgressFill(progressValue);
    }

    private float GetDistance()
    {
        return Vector3.Distance(_playerTransform.position, _endLevelPosition);
    }

    private void UpdateProgressFill(float value)
    {
        _progressBar.value = value;
    }
}
