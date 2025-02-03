
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Public Properties
    /// <summary>
    /// Possible states of the game.
    /// </summary>
    public enum GameState
    {
        NotStarted,
        Playing,
        Win,
        Lose,
    }
    public GameState CurrentState => _state;
    public bool IsPlaying => _state == GameState.Playing;

    #endregion
    [SerializeField]
    private GameState _state;

    #region Initialization Methods

    protected override void OnInstanceCreated()
    {
        base.OnInstanceCreated();
    }

    #endregion

    #region Core Game Methods

    public void StartGame()
    {
        EventManager.GameStart();
        _state = GameState.Playing;
    }

    public void StopGame()
    {
        if (_state == GameState.NotStarted) return;

        _state = GameState.NotStarted;
        EventManager.StopGame();
    }

    public void Lose()
    {
        if (_state != GameState.Playing) return;

        _state = GameState.Lose;
        EventManager.StopGame();
        EventManager.GameOver();
    }

    public void Win()
    {
        if (_state != GameState.Playing) return;

        _state = GameState.Win;
        EventManager.StopGame();
        EventManager.Win();
    }
    #endregion
}
