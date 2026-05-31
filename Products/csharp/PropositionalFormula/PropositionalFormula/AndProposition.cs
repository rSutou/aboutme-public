using System;
using System.Collections.Generic;
using System.Text;
using static PropositionalFormula.PropositionalFormula;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PropositionalFormula
{
    public class AndProposition : BinaryOperator
    {
        public AndProposition(PropositionalFormula first, PropositionalFormula second) : base(first, second)
        {
            formType = FormType.And;
        }

        public override bool TryGetValue(Dictionary<string, bool> valueSet, out bool result)
        {
            if (_firstFormula != null && _secondFormula != null && _firstFormula.TryGetValue(valueSet, out bool b1) && _secondFormula.TryGetValue(valueSet, out bool b2))
            {
                result = b1 && b2;
                return true;
            }
            else
            {
                result = false;
                return false;
            }
        }

        public override string ToString()
        {
            if (_firstFormula != null && _secondFormula != null)
            {
                return _firstFormula.ToString() + StrAnd + _secondFormula.ToString();
            }
            else
            {
                return StrNullRefurence;
            }
        }
    }
}
