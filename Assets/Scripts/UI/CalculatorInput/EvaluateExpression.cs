using System;

namespace UI.CalculatorInput
{
    public class EvaluateExpression : CalculatorInput
    {
        public static event Action EventOnEvaluatePress;

        protected override void OnButtonPress()
        {
            EventOnEvaluatePress?.Invoke();
        }
    }
}