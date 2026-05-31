using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionalFormula
{
    public class OrProposition : BinaryOperator
    {
        public OrProposition(PropositionalFormula first, PropositionalFormula second) : base(first, second)
        {
            formType = FormType.Or;
        }

        public override bool TryGetValue(Dictionary<string, bool> valueSet, out bool result)
        {
            if (_firstFormula != null && _secondFormula != null && _firstFormula.TryGetValue(valueSet, out bool b1) && _secondFormula.TryGetValue(valueSet, out bool b2))
            {
                result = b1 || b2;
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
                return _firstFormula.ToString() + StrOr + _secondFormula.ToString();
            }
            else
            {
                return StrNullRefurence;
            }
        }
    }
}
