using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordItem : MonoBehaviour
{
    public TMP_Text levelText, textJump;

    public TimeToText timeToText;

    [SerializeField]
    private Image _imageBg;

    [SerializeField]
    private Sprite _spriteBgRecord;

    internal void Set(int i, int countJump, float timerGame)
    {
        levelText.text = (i + 1).ToString();
        textJump.text = (countJump).ToString();
        timeToText.SetText(timerGame);
        _imageBg.sprite = _spriteBgRecord;
    }
}
