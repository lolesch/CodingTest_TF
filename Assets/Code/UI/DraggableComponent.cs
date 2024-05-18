using UnityEngine;
using UnityEngine.EventSystems;

namespace CodingTest_TF.UI
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class DraggableComponent : MonoBehaviour, IDragHandler
    {
        /// <summary>
        /// A cached reference to the RectTransform
        /// </summary>
        public RectTransform RectTransform
        {
            get
            {
                if (m_rectTransform == null)
                    m_rectTransform = GetComponent<RectTransform>();
                return m_rectTransform;
            }
        }
        private RectTransform m_rectTransform;

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform.anchoredPosition += eventData.delta / RectTransform.lossyScale;
            RectTransform.anchoredPosition = ClampToParentRect();
        }

        private Vector2 ClampToParentRect()
        {
            var anchoredPosition = RectTransform.anchoredPosition;

            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f, (RectTransform.parent as RectTransform).rect.width - RectTransform.rect.width);
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0f, (RectTransform.parent as RectTransform).rect.height - RectTransform.rect.height);

            return anchoredPosition;
        }
    }
}
