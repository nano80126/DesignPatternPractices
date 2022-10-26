using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DesignPatternPractices.CreationalPatterns;

namespace DesignPatternPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //RunSingletonThread();
            //RunSingletonTask();
            //RunFactory();

            //RunFactory();
            //Console.WriteLine("-----------------------------");
            //RunAbstractFactory();

            RunBuilder();

            Console.WriteLine("Please enter any key to leave.");
            Console.ReadKey();
        }

        #region 單例模式
        /// <summary>
        /// 使用Thread 執行單例模式
        /// </summary>
        private static void RunSingletonThread()
        {
            //SupermarketLazyDCL supermarketLazyDcl1 = SupermarketLazyDCL.GetInstance;
            //SupermarketLazyDCL supermarketLazyDcl2 = SupermarketLazyDCL.GetInstance;

            Thread th = new Thread(() =>
            {
                SupermarketLazyDCL supermarketLazyDcl1 = SupermarketLazyDCL.GetInstance;
                Freight freight = new Freight(supermarketLazyDcl1);
                freight.MoveIn(30);
                Console.WriteLine($"1 號已送達！商品數量 {freight.supermarket.GetQuantity()}");
            });

            Thread th2 = new Thread(() =>
            {
                SupermarketLazyDCL supermarketLazyDcl2 = SupermarketLazyDCL.GetInstance;
                Freight freight = new Freight(supermarketLazyDcl2);
                freight.MoveIn(40);
                Console.WriteLine($"2 號已送達！商品數量 {freight.supermarket.GetQuantity()}");
            });

            th.Start();
            th2.Start();

            th.Join();
            th2.Join();
        }

        /// <summary>
        /// 使用 Task 執行單例模式
        /// </summary>
        private static void RunSingletonTask()
        {
            Task t1 = Task.Run(() =>
            {
                SupermarketLazyDCL supermarketLazyT = SupermarketLazyDCL.GetInstance;
                Freight freight = new Freight(supermarketLazyT);
                freight.MoveIn(50);
                Console.WriteLine($"3 號已送達！商品數量 {freight.supermarket.GetQuantity()}");
            });

            Task t2 = Task.Run(() =>
            {
                SupermarketLazyDCL supermarketLazyT = SupermarketLazyDCL.GetInstance;
                Freight freight = new Freight(supermarketLazyT);
                freight.MoveIn(60);
                Console.WriteLine($"4 號已送達！商品數量 {freight.supermarket.GetQuantity()}");
            });


            Task.WaitAll(t1, t2);
        }
        #endregion

        #region 工廠模式
        private static void RunFactory ()
        {
            Restaurant rSteak = new Restaurant(new SteakFactory());
            rSteak.MealOrder();

            Restaurant rPork = new Restaurant(new PorkFactory());
            rPork.MealOrder();
        }
        #endregion
        
        #region 抽象工廠模式
        private static void RunAbstractFactory()
        {
            AbstractRestaurant rSteak = new TWRestaurant();
            rSteak.MealOrder(nameof(Steak));

            AbstractRestaurant rPork = new ITRestaurant();
            rPork.MealOrder(nameof(Pork));
        }
        #endregion

        #region 生成器模式

        private static void RunBuilder ()
        {
            ComputerFactory cf = new ComputerFactory();
            ComputerStore store = new ComputerStore(cf);
            Computer computer = store.MixSpec();

            Console.WriteLine($"{computer}");
        }
        #endregion


        #region AddTwoNumbers (LEECODE 2)
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode node1 = l1;
            ListNode node2 = l2;

            int first = node1.val + node2.val;
            ListNode res = new ListNode(first % 10);
            ListNode result = res;

            int plus = first / 10;

            //Console.WriteLine($"{res.val} {plus}");
            while (node1?.next != null || node2?.next != null)
            {
                node1 = node1?.next;
                node2 = node2?.next;

                int value = ((node1 != null ? node1.val : 0) + (node2 != null ? node2.val : 0));
                //Console.WriteLine($"{node1.val} {node2.val} {value} {plus}");

                res.next = new ListNode();
                res = res.next;
                res.val += (value + plus) % 10;
                //Console.WriteLine($"res: {res.val}");
                plus = (value + plus) / 10;
            }
            return result;
        }
        #endregion
    }
}
