using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyUI : MonoBehaviour
{
    public IntSO money;
    public Text moneyText;

    private void Awake()
    {
        money.value = 0;
    }
    private void Update()
    {
        moneyText.text =  "$ " + money.value.ToString();
    }
}
