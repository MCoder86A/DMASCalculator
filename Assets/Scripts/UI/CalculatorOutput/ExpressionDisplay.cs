using TMPro;
using UnityEngine;
using UI.CalculatorInput;
using System.Text;
using Utility;

namespace UI.CalculatorOutput
{
    public class ExpressionDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _displayText;

        private readonly StringBuilder _displayString = new();
        private string _ans = "";

        private void Awake()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            TermInput.EventOnInput += TermInput_OnInput;
            EvaluateExpression.EventOnEvaluatePress += EvaluateExpression_OnEvaluatePress;
            ClearExpression.EventOnClearExpressionClick += ClearExpression_OnClearExpressionClick;
            ClearCurrentExpression.EventOnClearCurrentExpressionClick += ClearCurrentExpression_OnClearCurrentExpressionClick;
        }

        private void ClearCurrentExpression_OnClearCurrentExpressionClick()
        {
            if(_displayString.Length > 0)
            {
                _displayString.Remove(_displayString.Length - 1, 1);
                RenderToDisplay(_displayString.ToString());
            }
        }

        private void ClearExpression_OnClearExpressionClick()
        {
            _displayString.Clear();
            _ans = null;
            RenderToDisplay("");
        }

        private void RenderToDisplay(string p_text)
        {
            _displayText.SetText(p_text);
        }

        private void EvaluateExpression_OnEvaluatePress()
        {
            _ans = DMASEvaluator.Evaluate(_displayString.ToString()).ToString();
            _displayString.Clear();
            RenderToDisplay(_ans);
        }

        private void TermInput_OnInput(string p_term)
        {
            if (!string.IsNullOrEmpty(_ans))
            {
                //ADD OPERATOR ON LAST ANSWER
                if ("+-/*".Contains(p_term))
                {
                    _displayString.Append(_ans);
                }
                _ans = null;
            }

            //ADD 0 IF INSERTED OPERATOR IN EMPTY EXPRESSION
            if (_displayString.Length == 0 && "+-/*.".Contains(p_term)) _displayString.Append('0');

            //RESTRICT TWO CONSECUTIVE OPERATOR
            if (_displayString.Length > 0
                && "+-/*".Contains(p_term)
                && "+-/*".Contains(_displayString[^1])) _displayString.Remove(_displayString.Length - 1, 1);

            //RESTRICT TWO CONSECUTIVE .

            if (_displayString.Length > 0
                && p_term == "."
                && _displayString[^1] == '.') _displayString.Remove(_displayString.Length - 1, 1);

            _displayString.Append(p_term);
            RenderToDisplay(_displayString.ToString());
        }

        private void OnDestroy()
        {
            UnRegisterEvents();
        }

        private void UnRegisterEvents()
        {
            TermInput.EventOnInput -= TermInput_OnInput;
            EvaluateExpression.EventOnEvaluatePress -= EvaluateExpression_OnEvaluatePress;
            ClearExpression.EventOnClearExpressionClick -= ClearExpression_OnClearExpressionClick;
            ClearCurrentExpression.EventOnClearCurrentExpressionClick += ClearCurrentExpression_OnClearCurrentExpressionClick;
        }
    }
}