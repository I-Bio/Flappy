using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _windowGroup;
        [SerializeField] private Button _actionButton;
        
        private void OnEnable()
        {
            _actionButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _actionButton.onClick.RemoveListener(OnButtonClick);
        }

        public void Close()
        {
            _windowGroup.alpha = 0f;
            _windowGroup.blocksRaycasts = false;
            _actionButton.interactable = false;
        }

        public void Open()
        {
            _windowGroup.alpha = 1f;
            _windowGroup.blocksRaycasts = true;
            _actionButton.interactable = true;
        }

        protected abstract void OnButtonClick();
    }
}