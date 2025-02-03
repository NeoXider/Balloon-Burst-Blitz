using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int _level;

    [SerializeField]
    private int _currentLevel;

    [SerializeField]
    private Level[] _levelMaps;

    [SerializeField]
    private Transform _transformLevelParent;

    public Level levelMap;

    public int Level => _level;

    public int currentLevel => _currentLevel;

    public UnityEvent<int> OnChangeLevel;
    public UnityEvent OnWin;

    private void Awake()
    {
        Load();
    }

    public void StartLevel(int id)
    {
        if (levelMap != null)
        {
            Destroy(levelMap.gameObject);
        }

        _currentLevel = id;
        levelMap = Instantiate(_levelMaps.GetElementByModulo(_currentLevel), _transformLevelParent);
        OnChangeLevel?.Invoke(_currentLevel);
    }

    public void StartLastLevel()
    {
        StartLevel(_level);
    }

    public void WinLevel()
    {
        if (_currentLevel == _level)
        {
            _level++;
            Save();
            OnWin?.Invoke();
        }
    }

    private void Load()
    {
        _level = PlayerPrefs.GetInt(nameof(_level), 0);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(nameof(_level), _level);
    }

    public int GetCountBallon()
    {
        return levelMap.countBallon;
    }

    public float GetTime()
    {
        return levelMap.timeGame;
    }
}
