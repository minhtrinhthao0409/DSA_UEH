using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_EvaluatePostfix
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
            /*object t = Pop();
            Push(t);
            return t;*/
            return top.data;
        }
    }

    public class EvaluatePostfix
    {
        private MyStack stack;

        public EvaluatePostfix()
        {
            stack = new MyStack();
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
        }

        public int Convert(string str)
        {

            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    stack.Push(c - '0');  // '0' = 48 trong ASCII, nên trừ đi để lấy giá trị số
                }
                else if (IsOperator(c))
                {
                    // Đảo thứ tự val2 và val1 vì stack LIFO
                    int val2 = (int)stack.Pop();  // val2 pop trước nên là toán hạng thứ hai
                    int val1 = (int)stack.Pop();  // val1 pop sau nên là toán hạng đầu tiên
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
            EvaluatePostfix evaluate = new EvaluatePostfix();


            int lastVal = evaluate.Convert("231*+9-");
            Console.WriteLine(lastVal);
        }
    }
}
