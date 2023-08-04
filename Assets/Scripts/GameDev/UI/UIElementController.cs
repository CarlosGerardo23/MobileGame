using Gaem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Rendering.FilterWindow;

namespace GameDev.UI
{
    public class UIElementController : MonoBehaviour
    {
        [SerializeField] private List<UIElement> _uiElementsList = new List<UIElement>();

        private void Start()
        {
            for (int i = 0; i < _uiElementsList.Count; i++)
            {
                if (_uiElementsList[i].Type == UIElementType.STATIC)
                    _uiElementsList[i].Show();
                else
                    _uiElementsList[i].Hide();
            }
        }

        public void ShowUniqueElement(UIElement element)
        {
            for (int i = 0; i < _uiElementsList.Count; i++)
            {
                if (_uiElementsList[i] == element && element.Type != UIElementType.STATIC)
                    _uiElementsList[i].Show();
                else if(element.Type != UIElementType.STATIC)
                    _uiElementsList[i].Hide();
            }
        }

        public void ShowAdditiveElement(UIElement element)
        {
            for (int i = 0; i < _uiElementsList.Count; i++)
            {
                if (_uiElementsList[i] == element && element.Type != UIElementType.STATIC)
                {

                    _uiElementsList[i].Show();
                    return;
                }
            }
        }

        public void HideElement(UIElement element)
        {
            for (int i = 0; i < _uiElementsList.Count; i++)
            {
                if (_uiElementsList[i] == element && element.Type != UIElementType.STATIC)
                {
                    _uiElementsList[i].Hide();
                    return;
                }
            }
        }
        public void HideAllElement()
        {
            for (int i = 0; i < _uiElementsList.Count; i++)
            {

                if (_uiElementsList[i].Type != UIElementType.STATIC)
                    _uiElementsList[i].Hide();
            }
        }
    }
}
