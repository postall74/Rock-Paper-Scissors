using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    private LineRenderer _lineRendere;

    private void Start()
    {
        _lineRendere = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            _lineRendere.SetPosition(i, _objects[i].transform.position);
        }
    }
}
