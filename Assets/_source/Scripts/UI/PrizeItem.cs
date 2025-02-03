using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrizeItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textMoney;

    [SerializeField]
    private GameObject[] _views;

    [SerializeField]
    private Image _iamge;

    public int idSkin;
    public int prizeMoney;

    public void Set(int price, Sprite sprite, bool choose, int idSkin = -1)
    {
        prizeMoney = price;
        this.idSkin = idSkin;
        _views.SetActiveAll(false).SetActiveId(choose.ToInt()+1, true);
        _views.SetActiveId(0, price != 0);

        _iamge.sprite = sprite;
        _iamge.SetNativeSize();

        _textMoney.text = price.ToString();
    }

    public void OnClick()
    {
        Prize.Instance.Set(this);
    }

    public void SetChoose(bool choose)
    {
        _views.SetActiveAll(false).SetActiveId(choose.ToInt() + 1, true);
        _views.SetActiveId(0, prizeMoney != 0);
    }
}
