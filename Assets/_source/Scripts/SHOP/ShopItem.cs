using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textMoney;

    [SerializeField]
    private GameObject[] _views;

    [SerializeField]
    private Image _iamge;

    public void Set(int price, Sprite sprite, bool current)
    {
        int id = current ? 2
            : (price == 0 ? 1 : 0);

        _views.SetActiveAll(false).SetActiveId(id, true);
        _iamge.sprite = sprite;
        _iamge.SetNativeSize();

        _textMoney.text = price.ToString();
    }

    public void OnClick()
    {
        ShopSkins.Instance.Set(this);
    }
}
