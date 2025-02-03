using System;
using System.Linq;
using UnityEngine;

public class Prize : Singleton<Prize>
{
    [SerializeField]
    private PrizeItem[] _prizeItems;

    [SerializeField]
    private int _prizeMoney = 100;

    [SerializeField]
    private Sprite _moneySprite;

    private bool[] _takes;

    public void SetPrizes()
    {
        UI.Instance.SetPage(2);

        _takes = new bool[_prizeItems.Length];
        SetPrize();
    }

    private void SetPrize()
    {
        int moneyId = 0.RandomToValue(_prizeItems.Length);

        for (int i = 0; i < _prizeItems.Length; i++)
        {
            if (moneyId == i)
            {
                _prizeItems[i].Set(_prizeMoney,
                    _moneySprite,
                    _takes[i]);
            }
            else
            {
                int id = GetSkin(i - (moneyId < i ? 1 : 0));
                Sprite skin = ShopSkins.Instance.changeSkin.sprites[id];

                _prizeItems[i].Set(0,
                    skin,
                    _takes[i],
                    id);
            }
        }
    }

    private void Visual()
    {
        for (int i = 0; i < _prizeItems.Length; i++)
        {
            _prizeItems[i].SetChoose(_takes[i]);
        }
    }

    private int GetSkin(int id)
    {
        var indexedItems = ShopSkins.Instance.prices
            .Select((item, index) => (Index: index, Item: item))
            .Where(e => e.Item >0)
            .ToList();

        if (indexedItems.Count > 1 + id)
        {
            return indexedItems[id].Index;
        }
        else
        {
            return 0;
        }
    }

    internal void Set(PrizeItem prizeItem)
    {
        int id = Array.IndexOf(_prizeItems, prizeItem);

        if (!_takes[id])
        {
            _takes[id] = true;

            if (prizeItem.prizeMoney != 0)
            {
                Money.Instance.Add(prizeItem.prizeMoney);
            }
            else
            {
                ShopSkins.Instance.SaveSkin(prizeItem.idSkin);
            }
        }

        Visual();

        if (_takes.Count(x => x) == 2)
        {
            GameController.Instance.SetGame();
        }
    }
}
