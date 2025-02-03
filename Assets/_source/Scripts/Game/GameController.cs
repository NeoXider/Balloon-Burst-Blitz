using UnityEngine;

public class GameController : Singleton<GameController>
{
    public SlingshotController slingshotController;


    [SerializeField]
    private TimerObject _timer;

    [SerializeField]
    private LevelManager _levelManager;

    [SerializeField]
    private Prize _prize;

    [Space]
    [Header("GameSett")]
    [SerializeField] private float _time = 15;

    private int _countPop;
    private int _countBallon;

    public float winTimer;

    protected override void Init()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePage(int page)
    {
        Time.timeScale = page == 0 ? 1 : 0;
    }

    //устанавливает нужный уровень
    public void SetGame()
    {
        GameManager.Instance.StopGame();
        UI.Instance.SetPage(0);
        _levelManager.StartLastLevel();
        SetSettings();
        Reset();
    }

    //запуск игры
    public void StartGame()
    {
        _timer.StartTimer(_time);
    }

    private void Reset()
    {
        slingshotController.ResetBall();
        _countPop = 0;
        _timer.Stop();
    }

    //настройки уровня и сам уровень
    private void SetSettings()
    {
        _countBallon = _levelManager.GetCountBallon();
        _time = _levelManager.GetTime();
        slingshotController.collider = _levelManager.levelMap.container;
    }

    public void Win()
    {
        winTimer = _timer.timer;
        print("win");
        _levelManager.WinLevel();

        if (_levelManager.Level % 4 == 0 && _levelManager.Level > 0)
        {
            Prize();
        }
        else
        {
            UI.Instance.SetPage(1);
        }
    }

    private void Prize()
    {
        _prize.SetPrizes();
    }

    public void Lose()
    {
        print("lose");
        SetGame();
    }

    public void PopBallon()
    {
        AM.Instance.Play(1, 0.6f);

        if (++_countPop >= _countBallon)
        {
            GameManager.Instance.Win();
        }
    }
}
