using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionalFormula
{
    public class ConstantProposition : PropositionalFormula
    {
        private bool _value;

        public ConstantProposition(bool value)
        {
            formType = FormType.Constant;
            _value = value;
        }

        public override bool TryGetValue(Dictionary<string, bool> valueSet, out bool result)
        {
            result = _value;
            return true;
        }

        public override string ToString()
        {
            return _value ? StrConstTrue : StrConstFalse;
        }
    }
}
