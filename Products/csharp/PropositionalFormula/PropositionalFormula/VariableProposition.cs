using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionalFormula
{
    public class VariableProposition : PropositionalFormula
    {
        private string _representation;

        public VariableProposition(string representation)
        {
            formType = FormType.Variable;
            _representation = representation;
        }

        public override bool TryGetValue(Dictionary<string, bool> valueSet, out bool result)
        {
            if (valueSet != null && valueSet.TryGetValue(_representation, out bool b))
            {
                result = b;
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
            return _representation;
        }
    }
}
