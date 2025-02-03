using Neo.Audio;
using TMPro;
using UnityEngine;

namespace Neo
{
public class SwipeTextAudio : MonoBehaviour, ISwipeSubscriber
{
        public TextMeshProUGUI text;

        public void SubscribeToSwipe(SwipeData swipeData)
        {
            AudioManager.PlaySound(ClipType.click);
            text.text = swipeData.Direction.ToString();
        }

        private void OnValidate()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
    }
}
