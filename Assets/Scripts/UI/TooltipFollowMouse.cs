using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TooltipFollowMouse : MonoBehaviour
    {
        [SerializeField] RectTransform rectTransform;
        private bool flipHor;
        private bool flipVer;

        private void Update()
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
            transform.localPosition = new Vector2(localPoint.x + (rectTransform.rect.width / 2) * ((flipHor) ? -1 : 1), localPoint.y + (rectTransform.rect.height / 2) * ((flipVer) ? -1 : 1));
            flipHor = (localPoint.x > (Screen.width - (rectTransform.rect.width * 2)));
            flipVer = (localPoint.y > (Screen.height - (rectTransform.rect.height * 2)));
        }
    }
}