using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gaem.UI
{
    public class UIElement : MonoBehaviour
    {
        [SerializeField] private UIElementType type;

        public UIElementType Type => type;
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
    public enum UIElementType {STATIC,DYNAMIC };
}

