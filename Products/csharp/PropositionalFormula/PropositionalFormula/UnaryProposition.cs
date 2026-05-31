using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionalFormula
{
    public abstract class UnaryOperator : PropositionalFormula
    {
        protected PropositionalFormula _innerFormula;
        protected UnaryOperator(PropositionalFormula innerFormula)
        {
            _innerFormula = innerFormula;
        }
    }
}
