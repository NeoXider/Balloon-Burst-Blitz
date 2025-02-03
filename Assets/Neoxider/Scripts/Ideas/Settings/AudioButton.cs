using Neo.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public Button button;
    public int clipId;

    private void OnEnable()
    {
        if (button != null)
        {
            OnValidate();
            button.onClick.AddListener(OnClick);
        }

    }

    public void OnClick()
    {
        AM.Instance.Play(clipId);
    }

    private void OnDisable()
    {
        if (button != null)
            button.onClick.RemoveListener(OnClick);
    }

    private void OnValidate()
    {
        button ??= GetComponent<Button>();
    }
}
