using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PropositionalFormula
{
    public abstract class PropositionalFormula
    {
        public const string StrNot = "!";
        public const string StrAnd = "&";
        public const string StrOr = "|";
        public const string StrImplication = "→";
        public const string StrBracketOpen = "(";
        public const string StrBracketClose = ")";
        public const string StrConstTrue = "true";
        public const string StrConstFalse = "false";

        public string StrNullRefurence = "\"NULL REFFERENCE ERROR\"";

        public enum FormType
        {
            Variable,
            Constant,
            Brackets,
            Not,
            And,
            Or,
            Implication,
        }

        public FormType formType;

        public static bool CheckSyntax(string str)
        {
            str = str.Trim();
            int nestedCount = 0;
            List<(FormType, int)> highestBinaryOperator = new List<(FormType, int)>();
            for (int i = 0; i < str.Length; i++)
            {
                string sub = str.Substring(i);
                if (sub.StartsWith(StrBracketOpen))
                {
                    nestedCount++;
                    i += StrBracketOpen.Length - 1;
                }
                else if (sub.StartsWith(StrBracketClose))
                {
                    nestedCount--;
                    i += StrBracketClose.Length - 1;
                }
            }

            if (nestedCount != 0)
            {
                return false;
            }



            return true;
        }

        /// <summary>
        /// 否定、括弧、変数、定数であるなど、単項式命題論理式を解析する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected static PropositionalFormula CreateFromMonomial(string str)
        {
            str = str.Trim();

            if (str.Length == 0)
            {
                return null;
            }

            if (!str.Contains(StrNot)
                && !str.Contains(StrAnd)
                && !str.Contains(StrOr)
                && !str.Contains(StrImplication)
                && !str.Contains(StrBracketOpen)
                && !str.Contains(StrBracketClose))
            {
                if (str == StrConstTrue)
                {
                    return new ConstantProposition(true);
                }
                else if (str == StrConstFalse)
                {
                    return new ConstantProposition(false);
                }
                else
                {
                    return new VariableProposition(str);
                }
            }
            else if (str.StartsWith(StrNot))
            {
                PropositionalFormula inner = CreateFromMonomial(str.Substring(StrNot.Length));
                if (inner == null)
                {
                    return null;
                }
                return new NotProposition(inner);
            }
            else if (str.StartsWith(StrBracketOpen))
            {
                int nestedCount = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    string sub = str.Substring(i);
                    if (sub.StartsWith(StrBracketOpen))
                    {
                        nestedCount++;
                        i += StrBracketOpen.Length - 1;
                    }
                    else if (sub.StartsWith(StrBracketClose))
                    {
                        nestedCount--;
                        if (nestedCount == 0)
                        {
                            if (sub.Substring(StrBracketClose.Length).Length == 0)
                            {
                                PropositionalFormula inner = Create(str.Substring(StrBracketOpen.Length, str.Length - StrBracketOpen.Length - StrBracketClose.Length));
                                if (inner == null)
                                {
                                    return new BracketsProposition(inner);
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else if (nestedCount < 0)
                        {
                            return null;
                        }
                        i += StrBracketClose.Length - 1;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 単項式になっていない命題論理式を構成する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected static PropositionalFormula CreateFromBinomial(string str)
        {
            str = str.Trim();

            if (str.Length == 0)
            {
                return null;
            }

            // 最上位にある二項演算子の全探索
            int nestedCount = 0;
            List<(FormType, int)> highestBinaryOperators = new List<(FormType, int)>();

            for (int i = 0; i < str.Length; i++)
            {
                string sub = str.Substring(i);

                if (sub.StartsWith(StrBracketOpen))
                {
                    nestedCount++;
                    i += StrBracketOpen.Length - 1;
                }
                else if (sub.StartsWith(StrBracketClose))
                {
                    nestedCount--;
                    i += StrBracketClose.Length - 1;
                }

                if (nestedCount == 0)
                {
                    if (sub.StartsWith(StrAnd))
                    {
                        highestBinaryOperators.Add((FormType.And, i));
                        i += StrAnd.Length - 1;
                    }
                    else if (sub.StartsWith(StrOr))
                    {
                        highestBinaryOperators.Add((FormType.Or, i));
                        i += StrOr.Length - 1;
                    }
                    else if (sub.StartsWith(StrImplication))
                    {
                        highestBinaryOperators.Add((FormType.Implication, i));
                        i += StrImplication.Length - 1;
                    }
                }
                else if (nestedCount < 0)
                {
                    return null;
                }
            }

            if (highestBinaryOperators.Count == 0)
            {
                return CreateFromMonomial(str);
            }
            else if (highestBinaryOperators.Exists(x => x.Item1 == FormType.Implication))   // 結合の弱いものから処理する
            {
                int operatorIndex = highestBinaryOperators.Find(x => x.Item1 == FormType.Implication).Item2;
                PropositionalFormula firstFormula = Create(str.Substring(0, operatorIndex));
                PropositionalFormula secondFormula = Create(str.Substring(operatorIndex + 1));
                if (firstFormula != null && secondFormula != null)
                {
                    return new ImplicationProposition(firstFormula, secondFormula);
                }
                else
                {
                    return null;
                }
            }
            else if (highestBinaryOperators.Exists(x => x.Item1 == FormType.Or))
            {
                int operatorIndex = highestBinaryOperators.Find(x => x.Item1 == FormType.Or).Item2;
                PropositionalFormula firstFormula = Create(str.Substring(0, operatorIndex));
                PropositionalFormula secondFormula = Create(str.Substring(operatorIndex + 1));
                if (firstFormula != null && secondFormula != null)
                {
                    return new OrProposition(firstFormula, secondFormula);
                }
                else
                {
                    return null;
                }
            }
            else if (highestBinaryOperators.Exists(x => x.Item1 == FormType.And))
            {
                int operatorIndex = highestBinaryOperators.Find(x => x.Item1 == FormType.And).Item2;
                PropositionalFormula firstFormula = Create(str.Substring(0, operatorIndex));
                PropositionalFormula secondFormula = Create(str.Substring(operatorIndex + 1));
                if (firstFormula != null && secondFormula != null)
                {
                    return new AndProposition(firstFormula, secondFormula);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public static PropositionalFormula Create(string str)
        {
            return CreateFromBinomial(str);
        }

        public abstract bool TryGetValue(Dictionary<string, bool> valueSet, out bool result);
    }
}
