using System;

namespace UI.CalculatorInput
{
    public class ClearExpression : CalculatorInput
    {
        public static event Action EventOnClearExpressionClick;
        protected override void OnButtonPress()
        {
            EventOnClearExpressionClick?.Invoke();
        }
    }
}