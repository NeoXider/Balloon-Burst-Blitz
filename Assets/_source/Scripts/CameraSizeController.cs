using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    [SerializeField] private float _referenceScreenHeight = 1920f; 
    [SerializeField] private float _defaultCameraSize = 5.2f;
    [SerializeField] private float _scaleStrength = 1f;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        UpdateCameraSize();
    }

    private void UpdateCameraSize()
    {
        float scaleFactor = Screen.height / _referenceScreenHeight;

        if (scaleFactor > 1)
        {
            // ��������� ���� ���������
            scaleFactor = Mathf.Pow(scaleFactor, _scaleStrength);

            // ������������� ����� ������ ������
            _camera.orthographicSize = _defaultCameraSize * scaleFactor;
        }
    }
}