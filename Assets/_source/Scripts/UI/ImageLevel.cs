using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageLevel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] _levelTexts;

    [SerializeField]
    private GameObject[] _viewVariant;

    [SerializeField]
    private Image[] prizes;

    public int level;
    public bool prize;
    public int idViev;

    public void Set(int level, int idVariant, bool prize = false)
    {
        this.level = level;
        this.prize = prize;
        this.idViev = idVariant;

        foreach (var item in _levelTexts)
        {
            item.text = (level + 1).ToString();
        }

        prizes.SetActiveAll(prize);
        _levelTexts.SetActiveAll(!prize);

        _viewVariant.SetActiveAll(false).SetActiveId(idViev, true);
    }

    private void OnValidate()
    {
        Set(level, idViev, prize);
    }
}
