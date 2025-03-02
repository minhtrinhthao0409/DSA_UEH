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
        public int Count()
        {
            int count = 0;
            MyStack temp = new MyStack();
            object ob = (int)Pop();
            while (ob != null)
            {
                temp.Push(ob);
                count++;
                ob = Pop();
            }
            while (!temp.IsEmpty())
                Push(temp.Pop());
            return count;
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

        }
        class Program
        {
            static void Main(string[] args)
            {
                InfixToPostfix converter = new InfixToPostfix();

                // Test cases
                string[] expressions = new string[]
                {
            "A+B*C",
            "(A+B)*C",
            "A+B+C",
            "A^B^C",
            "(A+B)*(C+D)"
                };

                foreach (string infix in expressions)
                {
                    string postfix = converter.Convert(infix);
                    Console.WriteLine($"Infix: {infix}");
                    Console.WriteLine($"Postfix: {postfix}");
                    Console.WriteLine();
                }

            }
        }
    }
}



/* 
 
 
 
 
 */
