using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using UnityEditor.Experimental.RestService;

public class UIPlayerData : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private AnimalMatchPlayerData _data;
    private void OnEnable()
    {

        _text.text= _data.Points.ToString();
    }
}
