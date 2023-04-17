using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringSO : ScriptableObject
{
    [SerializeField] private string _pick;

    public string Pick
    {
        get { return _pick; }
        set { _pick = value; }
    }
}
