using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Tween.Scripts
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByComposition : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;

        private const float MinValue = 300;
        private const float MaxValue = 600;
        [SerializeField, Range(MinValue, MaxValue)] private float _width;

        private Coroutine _coroutine;

        [ContextMenu(nameof(Play))]
        public void Play()
        {
            Stop();
            _coroutine = StartCoroutine(nameof(ActivateAnimation));
        }

        [ContextMenu(nameof(Stop))]
        public void Stop()
        {
            if (_coroutine == null)
                return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            InitButton();
            InitRectTransform();
        }

        private void InitButton() =>
            _button ??= GetComponent<Button>();

        private void InitRectTransform() =>
            _rectTransform ??= GetComponent<RectTransform>();

        private void OnButtonClick() =>
            ActivateAnimation();

        private void ActivateAnimation()
        {
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase);
                    break;
                case AnimationButtonType.ChangePosition:
                    _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
                    break;
            }
        }
    }
}
