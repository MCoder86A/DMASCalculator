using UnityEngine;
using UnityEngine.UI;

namespace UI.CalculatorInput
{
    [RequireComponent(typeof(Button))]
    public abstract class CalculatorInput : MonoBehaviour
    {
        private Button _button;

        protected void Awake()
        {
            _button = GetComponent<Button>();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _button.onClick.AddListener(OnButtonPress);
        }

        abstract protected void OnButtonPress();

        protected void OnDestroy()
        {
            UnRegisterEvents();
        }

        private void UnRegisterEvents()
        {
            _button.onClick.RemoveListener(OnButtonPress);
        }
    }
}