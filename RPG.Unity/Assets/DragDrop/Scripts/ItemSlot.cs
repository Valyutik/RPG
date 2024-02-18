using PlayForge_Team.RPG.Runtime;
using UnityEngine.EventSystems;
using UnityEngine;

namespace DragDrop.Scripts
{
    [RequireComponent(typeof(RectTransform))]
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private bool isGeneralItemSlot;
        [SerializeField] private Skills skills;
        private RectTransform _rectTransform;
    
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                var rectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
                rectTransform.SetParent(_rectTransform);
                rectTransform.anchoredPosition = Vector2.zero;
                _rectTransform.SetAsLastSibling();
                if (isGeneralItemSlot)
                {
                    skills.ChangeWeapon(eventData.pointerDrag.name);
                }
                else
                {
                    _rectTransform.parent.SetAsLastSibling();
                }
            }
        }
    }
}
