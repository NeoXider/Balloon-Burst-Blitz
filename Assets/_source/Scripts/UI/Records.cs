using System.Collections.Generic;
using UnityEngine;

public class Records : Singleton<Records>
{
    [SerializeField]
    private LevelManager _levelManager;

    [SerializeField]
    private List<RecordItem> _recordItems;

    [SerializeField]
    private RecordItem _prefabRecord;

    [SerializeField]
    private Transform _transformContainer;

    void Start()
    {
        _levelManager.OnWin.AddListener(NewRecord);
        Visual();
    }

    private void OnDestroy()
    {
        _levelManager.OnWin.RemoveListener(NewRecord);
    }

    private void NewRecord()
    {
        print("newRecord");
        int countJump = GameController.Instance.slingshotController.currentBall.countCollisionEnter;
        float timerGame = GameController.Instance.winTimer;
        PlayerPrefs.SetInt("countJump" + _levelManager.currentLevel, countJump);
        PlayerPrefs.SetFloat("timerGame" + _levelManager.currentLevel, timerGame);

        Visual();
    }

    private void Visual()
    {
        print("Records");
        SpawnRecords();

        for (int i = 0; i < _levelManager.Level; i++)
        {
            int countJump = PlayerPrefs.GetInt("countJump" + i, 0);
            float timerGame = PlayerPrefs.GetFloat("timerGame" + i, 0);
            _recordItems[i].Set(i, countJump, timerGame);
        }
    }

    private void SpawnRecords()
    {
        int count = _levelManager.Level + 1 - _recordItems.Count;

        for (int i = 0; i < count; i++)
        {
            var item = Instantiate(_prefabRecord, _transformContainer);
            NewItem(item);
            _recordItems.Add(item);
        }
    }

    private void NewItem(RecordItem item)
    {
        item.levelText.text = (_levelManager.Level + 1).ToString();
        item.textJump.text = "???";
        item.timeToText.text.text = "???";
    }
}
