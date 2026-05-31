using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionalFormula
{
    public abstract class BinaryOperator : PropositionalFormula
    {
        protected PropositionalFormula _firstFormula;
        protected PropositionalFormula _secondFormula;
        public BinaryOperator(PropositionalFormula first, PropositionalFormula second)
        {
            _firstFormula = first;
            _secondFormula = second;
        }
    }
}
