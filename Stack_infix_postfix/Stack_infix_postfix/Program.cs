using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_infix_postfix
{
    public class Node
    {
        public object data;
        public Node next;
    }
    public class MyStack
    {
        Node top;
        public int count = 0;
        public bool IsEmpty()
        {
            return top == null;
        }
        public void Push(object data)
        {
            Node newNode = new Node();
            newNode.data = data;
            newNode.next = top;
            top = newNode;
            count++;
        }
        public object Pop()
        {
            if (IsEmpty())
            {
                return null;
            }
            object data = top.data;
            top = top.next;
            if (count >= 0)
                count--;
            return data;
        }
        
        public object Peek()
        {
            return top.data;
        }

        public class InfixToPostfix
        {
            private MyStack stack;

            public InfixToPostfix()
            {
                stack = new MyStack();
            }

            private bool IsOperator(char c)
            {
                return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
            }

            private int GetPrecedence(char op)
            {
                switch (op)
                {
                    case '+':
                    case '-':
                        return 1;
                    case '*':
                    case '/':
                        return 2;
                    case '^':
                        return 3;
                    default:
                        return 0;
                }
            }

            public string Convert(string str)
            {
                string postfix = "";

                foreach (char c in str )
                {
                    if (char.IsLetterOrDigit(c))
                    {
                        postfix += c;
                    }
                    else if (c == '(')
                    {
                        stack.Push(c);
                    }
                    else if (c == ')')
                    {
                        while (!stack.IsEmpty() && (char) stack.Peek() !=  '(')
                        {
                            postfix += stack.Pop();
                        }
                        if (!stack.IsEmpty() && (char) stack.Peek() ==  '(')
                            stack.Pop(); 
                    }
                    else if (IsOperator(c))
                    {
                        while (!stack.IsEmpty() && (char) stack.Peek() != '(' &&
                               GetPrecedence((char) stack.Peek()) >= GetPrecedence(c))
                        {
                            postfix += stack.Pop();
                        }
                        stack.Push(c);
                    }
                }

                while (!stack.IsEmpty())
                {
                    postfix += stack.Pop();
                }

                return postfix;
            }

            public int CalculatePostfix(string str)
            {

                foreach (char c in str)
                {
                    if (char.IsDigit(c))
                    {
                        stack.Push(c - '0');  // '0' = 48 trong ASCII, nên trừ đi để lấy giá trị số
                    }
                    else if (IsOperator(c))
                    {                        
                        int val2 = (int)stack.Pop();  
                        int val1 = (int)stack.Pop();  
                        switch (c)
                        {
                            case '+':
                                stack.Push(val1 + val2);
                                break;
                            case '-':
                                stack.Push(val1 - val2);
                                break;
                            case '*':
                                stack.Push(val1 * val2);
                                break;
                            case '/':
                                if (val2 == 0) throw new DivideByZeroException();
                                stack.Push(val1 / val2);
                                break;
                            case '^':
                                stack.Push((int)Math.Pow(val1, val2));
                                break;
                        }
                    }
                }
                return (int)stack.Pop();
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                InfixToPostfix converter = new InfixToPostfix();
                List<string> postfixes = new List<string>();

                string[] expressions = new string[]
                {
                    "2 + (3 * 4)",
                    "5 + ((1 + 2) * 4)",
                    "3 4 5 + *",
                    "10 5 / 2 +",
                    "7 8 * 4 2 / +"
                };

                foreach (string infix in expressions)
                {
                    string postfix = converter.Convert(infix);
                    postfixes.Add(postfix);
                    Console.WriteLine($"Infix: {infix}");
                    Console.WriteLine($"Postfix: {postfix}");
                    Console.WriteLine();
                }

                foreach (string op in postfixes)
                {
                    int val = converter.CalculatePostfix(op);
                    Console.WriteLine($"The last value of {op}: {val}");
                }

                Console.ReadLine();
            }
        }
    }
}

