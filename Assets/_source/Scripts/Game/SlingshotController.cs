using UnityEngine;
using UnityEngine.Events;

public class SlingshotController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _maxDragDistance = 2f;
    [SerializeField] private float _launchForce = 15f;

    [Header("Container toch")]
    public Collider2D collider;
    [SerializeField] private GameObject _startTochObject;

    [Header("References")]
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private LineRenderer _lineRenderer;

    private Vector2 _direction;
    private Vector2 _startPos;
    private bool _isDragging = false;

    [SerializeField]
    private bool _ballLaunched = false;

    public Ball currentBall;
    public UnityEvent OnPush;

    private void Start()
    {
        _lineRenderer.enabled = false;
        _startTochObject.SetActive(false);
    }

    private void Update()
    {
        if (_ballLaunched) return;

        if (GameManager.Instance.CurrentState == GameManager.GameState.NotStarted
            && UI.Instance.id == 0)
        {
            HandleInput();
            UpdateLineRenderer();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            ContinueDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            EndDrag();
        }
    }

    //начало касания
    private void StartDrag(Vector3 inputPosition)
    {
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(inputPosition);

        if(collider == null)
        {
            Debug.LogWarning("null Container collider2d");
            return;
        }

        // Проверяем касание в рабочей зоне
        if (collider.OverlapPoint(currentPos))
        {
            _isDragging = true;
            _startPos = currentPos;

            _startTochObject.transform.position = _startPos;
            _startTochObject.SetActive(true);

            CreateBall(_startPos);
            _lineRenderer.enabled = true;
        }
    }

    //тянем
    private void ContinueDrag(Vector3 inputPosition)
    {
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(inputPosition);

        if (collider.OverlapPoint(currentPos))
        {
            _direction = currentPos - _startPos;
            _direction = Vector2.ClampMagnitude(_direction, _maxDragDistance);

            currentBall.transform.position = _startPos + _direction;
        }
        else
        {
            EndDrag();
        }
    }

    //отпускаем палец
    private void EndDrag()
    {
        _isDragging = false;
        _lineRenderer.enabled = false;
        _ballLaunched = true;
        _startTochObject.SetActive(false);

        Vector2 currentPos = currentBall.transform.position;
        Vector2 launchDirection = (_startPos - currentPos).normalized;

        // Рассчитываем силу пропорционально растяжению
        float stretchPercent = Vector2.Distance(_startPos, currentPos) / _maxDragDistance;
        float finalForce = Mathf.Lerp(0, _launchForce, stretchPercent);

        LaunchBall(launchDirection * finalForce);

        Debug.Log($"Направление: {launchDirection} Сила: {finalForce}");
    }

    //создаем мяч
    private void CreateBall(Vector2 position)
    {
        currentBall = Instantiate(_ballPrefab, position, Quaternion.identity);
    }

    //пуск мяча
    private void LaunchBall(Vector2 force)
    {
        print("force = " + force);
        Rigidbody2D rb = currentBall.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.AddForce(force, ForceMode2D.Impulse);
        OnPush?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if(currentBall != null && _isDragging) 
            Debug.DrawRay(currentBall.transform.position, -_direction.normalized * 5f, Color.red, 2f);
    }

    //обновление линии
    private void UpdateLineRenderer()
    {
        if (!_isDragging) return;

        _lineRenderer.SetPosition(0, _startPos);
        _lineRenderer.SetPosition(1, currentBall.transform.position);
    }

    //Разрешить запуск нового мяча
    public void ResetBall()
    {
        if (currentBall != null)
        {
            Destroy(currentBall.gameObject);
        }

        _ballLaunched = false;
    }

    public void StopBall()
    {
        if (currentBall != null)
        {
            currentBall.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}