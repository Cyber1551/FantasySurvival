using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Items
{

    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI = null;
        private CanvasGroup canvasGroup = null;
        private Transform originalParent = null;
        private bool isHovering = false;


        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private void Start() => canvasGroup = GetComponent<CanvasGroup>();

        private void OnDisable()
        {
            if (isHovering)
            {
                isHovering = false;
            }
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {

                //Raise event
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                canvasGroup.blocksRaycasts = false;
            }
        }
        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParent);
                transform.localPosition = Vector3.zero;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
        }
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
        }
    }
}