using UnityEngine;
using System.Collections;

public class ScaleAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float _targetScale = 1.5f;
    [SerializeField] private bool _animateFromTarget = false;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);

    private Vector3 _initialScale;
    private Coroutine _animationRoutine;

    private void Awake() => CacheInitialScale();
    private void OnEnable() => RestartAnimation();

    private void CacheInitialScale() => _initialScale = transform.localScale;

    private void RestartAnimation()
    {
        if (_animationRoutine != null)
            StopCoroutine(_animationRoutine);

        _animationRoutine = StartCoroutine(AnimateScale());
    }

    private IEnumerator AnimateScale()
    {
        Vector3 startScale = _animateFromTarget ?
            Vector3.one * _targetScale : _initialScale;

        Vector3 endScale = _animateFromTarget ?
            _initialScale : Vector3.one * _targetScale;

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / _duration;
            transform.localScale = Vector3.Lerp(
                startScale,
                endScale,
                _curve.Evaluate(progress)
            );
            yield return null;
        }

        transform.localScale = endScale;
    }
}