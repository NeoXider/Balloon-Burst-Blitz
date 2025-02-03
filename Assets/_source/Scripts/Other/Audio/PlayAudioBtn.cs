using UnityEngine;
using UnityEngine.UI;

public class PlayAudioBtn : MonoBehaviour
{
    [SerializeField]
    private int _idClip = 0;

    [SerializeField]
    [GetComponent]
    private Button _button;

    private void OnEnable()
    {
        if (_button != null)
            _button.onClick.AddListener(AudioPlay);
    }

    private void OnDisable()
    {
        if (_button != null)
            _button.onClick.RemoveListener(AudioPlay);
    }

    public void AudioPlay()
    {
        AM.Instance.Play(_idClip);
    }
}