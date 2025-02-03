using System;
using UnityEngine;

public class ShopSkins : Singleton<ShopSkins>
{
    public ChangeSkin changeSkin;

    public int[] prices;

    [SerializeField]
    private ShopItem[] _shopItems;

    void Start()
    {
        Load();
        Visual();
    }

    public void Set(ShopItem item)
    {
        int id = Array.IndexOf(_shopItems, item);

        if (prices[id] == 0)
        {
            changeSkin.Set(id);
        }
        else
        {
            if (Money.Instance.Spend(prices[id]))
            {
                SaveSkin(id);
            }
            else
            {
                print("not money");
            }
        }

        Visual();
    }

    public void SaveSkin(int id)
    {
        prices[id] = 0;
        Save();
        Visual();
    }

    private void Visual()
    {
        for (int i = 0; i < _shopItems.Length; i++)
        {
            _shopItems[i].Set(prices[i], changeSkin.sprites[i], changeSkin.id == i);
        }
    }

    private void Load()
    {
        for (int i = 0; i < prices.Length; i++)
        {
            prices[i] = PlayerPrefs.GetInt(nameof(prices) + i, prices[i]);
        }
    }

    private void Save()
    {
        for (int i = 0; i < prices.Length; i++)
        {
            PlayerPrefs.SetInt(nameof(prices) + i, prices[i]);
        }
    }

}
