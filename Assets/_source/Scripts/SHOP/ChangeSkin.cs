using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public SpriteRenderer srPrefab;

    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private int _id;

    public Sprite[] sprites => _sprites;
    public int id => _id;

    public void Set(int id)
    {
        this._id = id;
        srPrefab.sprite = _sprites[id];
    }

    private void OnValidate()
    {
        Set(_id);
    }
}
