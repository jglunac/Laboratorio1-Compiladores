using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_Compis_1310019
{
    class Parser
    {
        //Lab2 - 3-09
        Scanner _scanner;
        Token _token;
        private double E()
        {
            double res;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.LParen:
                case TokenType.Num:
                    double result = T();
                    res = EP(result);
                    break;
                default:
                    throw new Exception("Sintaxis Error");
            }
            return res;
        }
        private double EP(double prev)
        {
            double res;
            switch (_token.Tag)
            {
                case TokenType.Plus:
                    Match(TokenType.Plus);

                    double result = T();
                    res = prev + result;
                    res = EP(res);
                    break;
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    double result2 = T();
                    res = prev - result2;
                    res = EP(res);
                    break;
                case TokenType.EOF:
                case TokenType.RParen:
                    res = prev;
                    break;
                default:
                    throw new Exception("Sintaxis Error");
            }
            return res;
        }
        private double F()
        {
            double res;
            switch (_token.Tag)
            {
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    res = E();
                    Match(TokenType.RParen);
                    break;
                case TokenType.Num:
                    res = Convert.ToDouble(_token.Value);
                    Match(TokenType.Num);
                    break;
                default:
                    throw new Exception("Sintaxis Error");
            }
            return res;
        }
        private double T()
        {
            double res;
            switch (_token.Tag)
            {
               
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    res = F()*-1;
                    res = TP(res);
                    break;
                case TokenType.LParen:
                    res = F ();
                    res = TP (res);
                    break;

                case TokenType.Num:
                    res = F();
                    res = TP(res);
                    break;
                default:
                    throw new Exception("Sintaxis Error");
            }
            return res;
        }
        private double TP(double prev)
        {
            double res;
            switch (_token.Tag)
            {
                case TokenType.Star:
                    Match(TokenType.Star);
                    double result = F();
                    res = result * prev;
                    res = TP(res);
                    break;
                case TokenType.Divi:
                    Match(TokenType.Divi);
                    double result2 = F();
                    res = prev / result2;
                    res = TP(res);
                    break;

                case TokenType.Minus:
                case TokenType.RParen:
                case TokenType.Plus:
                case TokenType.EOF:
                    res = prev;
                    break;
                default:
                    throw new Exception("Sintaxis Error");
            }
            return res;
        }
        private void Match(TokenType tag)
        {
            if (_token.Tag == tag)//Esta evaluación parece obvia, pero sirve para reglas q tienen terminales no solo al inicio, Ej: E->(-F) y para matchear el EOF
            {
                _token = _scanner.GetToken();
            }
            else
            {
                throw new Exception("Error de sintaxis");
            }
        }
        public double Parse(string regexp)
        {
            double res;
            _scanner = new Scanner(regexp+(char)TokenType.EOF);
            _token = _scanner.GetToken();
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.LParen:
                case TokenType.Num:
                    res = E ();
                    break;
                default:
                    res = 0;
                    break;
            }
            Match(TokenType.EOF);
            return res;
        }
    }
}
