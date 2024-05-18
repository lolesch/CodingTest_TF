using CodingTest_TF.Utility.AttributeRefs;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodingTest_TF.Runtime.UI.Panels
{
    /// <summary>
    /// Panels provide appearence options such as fading in and out, scaling and movement.
    /// A panel should always stay enabled, only its canvasGroup alpha is set to 0.
    /// Therefore use <see cref="BeforeAppear"/> instead of <see cref="OnEnable()"/> to refresh panel data.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup), typeof(GraphicRaycaster))]
    public abstract class AbstractPanel : MonoBehaviour
    {
        #region COMPONENT REFERENCES
        [field: SerializeField, ReadOnly] protected CanvasGroup _canvasGroup = null;
        /// <summary>
        /// A cached reference of the CanvasGroup
        /// </summary>
        public CanvasGroup CanvasGroup => _canvasGroup != null ? _canvasGroup : _canvasGroup = GetComponent<CanvasGroup>();

        [field: SerializeField, ReadOnly] protected RectTransform _transform = null;

        /// <summary>
        /// A cached reference of the RectTransform
        /// </summary>
        public RectTransform Transform => _transform != null ? _transform : _transform = GetComponent<RectTransform>();
        #endregion COMPONENT REFERENCES

        public bool IsActive => CanvasGroup.alpha == 1;
        [field: SerializeField, Range(0, 1)] public float FadeDuration { get; private set; } = .2f;

        protected virtual void Awake()
        {
            if (isActiveAndEnabled)
                FadeOut(0);
        }

        protected virtual void OnDestroy() => KillTweens();

        protected virtual void OnDisable() => KillTweens();

        [ContextMenu("FadeIn")]
        public void FadeIn() => FadeIn(FadeDuration);
        public void FadeIn(float fadeInDuration)
        {
            KillTweens();

            BeforeAppear();

            if (fadeInDuration <= 0)
            {
                CanvasGroup.alpha = 1;

                OnAppear();

                return;
            }

            _ = CanvasGroup.DOFade(1, fadeInDuration).SetEase(Ease.InOutQuad).OnComplete(() => OnAppear());
        }

        public Sequence FadeInAfterDelay(float fadeInDelay = 0)
        {
            if (0 >= fadeInDelay)
            {
                FadeIn();
                return null;
            }
            else
            {
                var sequence = DOTween.Sequence();
                return sequence.InsertCallback(fadeInDelay, FadeIn);
            }
        }

        /// <summary>
        /// Called right before the CanvasGroup fades in.
        /// </summary>
        protected virtual void BeforeAppear() { }

        /// <summary>
        /// Called after the CanvasGroup completed fading in.
        /// </summary>
        protected virtual void OnAppear() => CanvasGroup.blocksRaycasts = true;

        [ContextMenu("FadeOut")]
        public void FadeOut() => FadeOut(FadeDuration);
        public void FadeOut(float fadeOutDuration)
        {
            KillTweens();

            // BeforeDisappear()

            CanvasGroup.blocksRaycasts = false;

            if (fadeOutDuration <= 0)
            {
                CanvasGroup.alpha = 0;

                // OnDisappear();

                return;
            }

            _ = CanvasGroup.DOFade(0, fadeOutDuration).SetEase(Ease.InQuad); //.OnComplete(() => OnDisappear());
        }

        [ContextMenu("Toggle Visibility")]
        public void Toggle()
        {
            if (CanvasGroup.alpha < 1)
                FadeIn();
            else
                FadeOut();
        }

        private void KillTweens()
        {
            if (CanvasGroup == null)
                return;

            if (DOTween.IsTweening(CanvasGroup))
                _ = DOTween.Kill(CanvasGroup);
        }
    }
}
