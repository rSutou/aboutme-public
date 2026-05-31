using System;
using System.Collections.Generic;
using System.Text;
using static PropositionalFormula.PropositionalFormula;

namespace PropositionalFormula
{
    public class NotProposition : UnaryOperator
    {
        public NotProposition(PropositionalFormula formula) : base(formula)
        {
            formType = FormType.Not;
        }

        public override bool TryGetValue(Dictionary<string, bool> valueSet, out bool result)
        {
            if (_innerFormula != null && _innerFormula.TryGetValue(valueSet, out bool b))
            {
                result = !b;
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
            if (_innerFormula != null)
            {
                return StrNot + _innerFormula.ToString();
            }
            else
            {
                return StrNullRefurence;
            }
        }
    }
}
