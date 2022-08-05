using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private int _condition;

    public int Condition => _condition;

    public void ChangeStatus(int status)
    {
        _condition = status;
    }
}
