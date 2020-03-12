using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;
    [SerializeField]
    TextMeshProUGUI killCounterTMP;
    [HideInInspector]
    public int killCount;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateKillCounterUI()
    {
        killCounterTMP.text = killCount.ToString();
    }
}
