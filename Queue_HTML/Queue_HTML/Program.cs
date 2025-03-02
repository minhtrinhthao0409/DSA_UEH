using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Queue_HTML
{
    public class Node2
    {
        public Node2 prev, next;
        public object data;
    }
    public class MyQueue
    {
        Node2 rear, front;
        public int count = 0;

        public bool IsEmpty()
        {
            return rear == null || front == null;
        }
        public void Enqueue(object ele)
        {
            Node2 n = new Node2();
            n.data = ele;
            if (rear == null)
            {
                rear = n; front = n;
            }
            else
            {
                rear.prev = n;
                n.next = rear; rear = n;
            }
            count++;
        }
        public Node2 Dequeue()
        {
            if (front == null) return null;
            Node2 d = front;
            front = front.prev;
            if (front == null)
                rear = null;
            else
                front.next = null;
            if (count >= 0)
                count--;
            return d;
        }
        public object Peek()
        {
            return front.data;
        }
    }
    public class HtmlTagMatcher
    {
        public static bool IsHtmlMatched(string html)
        {
            MyQueue queue = new MyQueue();
            int i = 0;

            while (i < html.Length)
            {
                if (html[i] == '<' && i + 1 < html.Length)
                {
                    bool isClosingTag = html[i + 1] == '/';
                    i = isClosingTag ? i + 2 : i + 1;

                    string tagName = "";
                    while (i < html.Length && html[i] != '>' && html[i] != ' ')
                    {
                        tagName += html[i];
                        i++;
                    }

                    while (i < html.Length && html[i] != '>')
                    {
                        i++;
                    }
                    if (!string.IsNullOrEmpty(tagName))
                    {
                        if (!isClosingTag)
                        {
                            queue.Enqueue(tagName);
                        }
                        else
                        {
                            if (queue.IsEmpty())
                                return false;

                            Node2 topNode = queue.Dequeue();
                            string openingTag = topNode.data.ToString();

                            if (openingTag != tagName)
                                return false;
                        }
                    }
                }
                i++;
            }

            return queue.IsEmpty(); 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string html1 = "<div><p>Hello</p></div>";
            string html2 = "<div><p>Hello</div>";
            string html3 = "<div></div><p></p>";

            Console.WriteLine(HtmlTagMatcher.IsHtmlMatched(html1)); // True
            Console.WriteLine(HtmlTagMatcher.IsHtmlMatched(html2)); // False
            Console.WriteLine(HtmlTagMatcher.IsHtmlMatched(html3)); // True

        }
    }
}

    
    
    
        
    

