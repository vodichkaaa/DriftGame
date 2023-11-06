using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public static int Money;

    private void Start()
    {
        Money = PlayerPrefs.GetInt("MoneyAmount");
        moneyText.text = Money.ToString();
    }

    public static void UpdateMoney()
    {
        PlayerPrefs.SetInt("MoneyAmount", Money);
        Money = PlayerPrefs.GetInt("MoneyAmount");
        PlayerPrefs.Save();
    }
}
