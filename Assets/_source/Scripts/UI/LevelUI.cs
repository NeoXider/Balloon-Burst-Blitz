using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField]
    private ImageLevel[] _imageLevels;

    [SerializeField]
    private LevelManager _levelManager;

    public void Visual()
    {
        int level = _levelManager.currentLevel;

        int count = level / (_imageLevels.Length - 1);
        int srartLevel = count * 4;

        for (int i = 0; i < _imageLevels.Length; i++)
        {
            int curLevel = srartLevel + i;
            int idView = curLevel > level ? 0 :
                (curLevel == level ? 1 : 2);
            _imageLevels[i].Set(curLevel, idView, i == _imageLevels.Length - 1);
        }
    }
}
