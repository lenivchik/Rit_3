using System;

namespace ConsoleApp3
{

    public class ListNode<T> 
    {
        public readonly T Value;
        public readonly ListNode<T> Next;
        public ListNode(T value, ListNode<T> next = null) 
        { 
            Value = value; 
            Next = next; 
        }
    }

    public class LinkedList<T>
    {
        private ListNode<T> Head;

        public LinkedList(ListNode<T> head = null)
        {
            Head = head;
        }

        public void Print_List()
        {
            var current = Head;
            while (current != null)
            {
                Console.Write($"{current.Value} ") ;
                current = current.Next;
            }
            Console.Write("\n");
        }
         //Метод для замены элемента в списке; предполагаеся, что у каждого элемента уникальное значение 
        public  LinkedList<T> Replace( T item, T newvalue)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    var newNode = new ListNode<T>(newvalue, current.Next);
                    return new LinkedList<T>(AddNode(current, newNode));
                }
                current = current.Next;
            }

            return this;
        }

        private ListNode<T> AddNode(ListNode<T> chng_node, ListNode<T> newNode)
        {
            var old_node = chng_node;
            var aim_node = newNode;

            while (!aim_node.Value.Equals(Head.Value))
            {
                var old_node_parent = FindParent(old_node);
                aim_node = new ListNode<T>(old_node_parent.Value, aim_node);
                old_node = aim_node;
            }
            return aim_node;
        }

        private ListNode<T> FindParent(ListNode<T> node)
        {
            if(node.Next == null)
            {
                return node;
            }
            var parent = Head;
            while (!(parent.Next.Value.Equals(node.Value)))
                parent = parent.Next;
            return parent;
        }
        //рекурсивное объединение

        public LinkedList<T> Concat(LinkedList<T> list2)
        {
            var root = AddNode(this.FindTail(), list2.Head);
            return new LinkedList<T>(root);
        }

        private ListNode<T> FindTail()
        {
            var current = Head;
            while (current.Next != null)
                current = current.Next;
            return current;
        }

        public LinkedList<T> ConcatRecursiv(LinkedList<T> list)
        {
            var root = ReplaseRec(Head, this.FindTail(), list.Head);
            return new LinkedList<T>(root);
        }

        private ListNode<T> ReplaseRec(ListNode<T>node, ListNode<T> tail_node, ListNode<T> newNode)
        {
            if (node.Equals(tail_node))
                return new ListNode<T>(tail_node.Value, newNode);
            else
                return new ListNode<T>(node.Value, ReplaseRec(node.Next, tail_node, newNode));
        }



        //рекурсивная замена

        public LinkedList<T> ReplaceRecursiv(T oldnode, T newnode)
        {
            var root = ReplaseRec(Head, oldnode, newnode);
            return new LinkedList<T>(root);
        }
        private ListNode<T> ReplaseRec(ListNode<T> node, T oldnode,T newnode )
        {
            if (node.Value.Equals(oldnode))           
                return new ListNode<T>(newnode, node.Next);            
            else
                return new ListNode<T>(node.Value, ReplaseRec(node.Next, oldnode, newnode));
        }


        public int Length()
        {
            int i = 0;
            var current = Head;
            while(current != null)
            {
                i++;
                current = current.Next;
            }
            return i;
        }
        public bool checkValues(LinkedList<T> list)
        {
            var current1 = Head;
            var current2 = list.Head;

            while (current1.Next != null)
            {
                if( !current1.Value.Equals(current2.Value))
                    return false;
                current1 = current1.Next;
                current2= current2.Next;
            }
            return true;
        }

        //public LinkedList<T> Add(T value)
        //{
        //    var root = AddRec(Head, value);
        //    return new LinkedList<T>(root);
        //}
        //public ListNode<T> AddRec(ListNode<T> node, T value, ListNode<T> tail=null)
        //{
        //    if (node == null)
        //    {
        //        return new ListNode<T>(value,tail);
        //    }
        //    return AddRec(node.Next, value, new ListNode<T>(node.Value, tail));
        //}

    }




    internal class Program
    {

        public static LinkedList<T> Replace<T>(LinkedList<T> list, T item, T newvalue)
        {
            return list.Replace(item, newvalue);
        }

        public static bool Check<T>(LinkedList<T> list1, LinkedList<T> list2)
        {
            if (list1.Length() == list2.Length())
            {
                if (list1.checkValues(list2))
                    return true;
            }
            return false;
        }

        public static LinkedList<T> ReplaceRecursiv<T>(LinkedList<T> list, T item, T newvalue)
        {
            return list.ReplaceRecursiv(item, newvalue);
        }

        public static LinkedList<T> Concat<T>(LinkedList<T> list1, LinkedList<T> list2)
        {
            return list1.Concat(list2);
        }

        public static LinkedList<T> ConcatRecursiv<T>(LinkedList<T> list1, LinkedList<T> list2)
        {
            return list1.ConcatRecursiv(list2);
        }

        static void Main(string[] args)
        {
            var list1 = new LinkedList<int>(
                new ListNode<int>(1,
                    new ListNode<int>(2,
                        new ListNode<int>(3,
                            new ListNode<int>(4,
                                new ListNode<int>(5,
                                    new ListNode<int>(6)
                                )
                            )
                        )
                    )       
                )
            );

            var list3 = new LinkedList<int>(
    new ListNode<int>(2,
        new ListNode<int>(6,
            new ListNode<int>(3,
                new ListNode<int>(4,
                    new ListNode<int>(5,
                        new ListNode<int>(6)
                    )
                )
            )
        )
    )
);
            Console.WriteLine("List without replaces");
            list1.Print_List();

            Console.WriteLine("New list with replaces");
            var list2 = Replace(list1, 3, 10);
            list2.Print_List();
            //рекурсивная замена
            Console.WriteLine("New list with recursiv replace");
            var list2_1 = ReplaceRecursiv(list1, 3, 10);
            list2_1.Print_List();
            if (Check(list2, list2_1 ))
            Console.WriteLine("*Lists ar equal");


            Console.WriteLine("List to concat");
            list1.Print_List();
            list3.Print_List();

            Console.WriteLine("New concated list");
            var list4 = Concat(list1, list3);
            list4.Print_List();
            //рекурсивная вставка
            Console.WriteLine("New recursiv concated list");
            var list4_1 = ConcatRecursiv(list1, list3);
            list4_1.Print_List();
            if (Check(list4, list4_1))
                Console.WriteLine("*Lists ar equal");

            Console.ReadLine(); 
        }
    }
}
