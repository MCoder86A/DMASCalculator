using System;
using UnityEngine;

namespace UI.CalculatorInput
{
    public class TermInput : CalculatorInput
    {
        [SerializeField] private string _term;
        public static event Action<string> EventOnInput;

        protected override void OnButtonPress()
        {
            EventOnInput?.Invoke(_term);
        }
    }
}
