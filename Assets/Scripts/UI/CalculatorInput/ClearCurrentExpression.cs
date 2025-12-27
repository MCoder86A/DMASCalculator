using System;

namespace UI.CalculatorInput
{
    public class ClearCurrentExpression : CalculatorInput
    {
        public static event Action EventOnClearCurrentExpressionClick;
        protected override void OnButtonPress()
        {
            EventOnClearCurrentExpressionClick?.Invoke();
        }
    }
}