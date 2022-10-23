using System;
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
            RunFactory();


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
    }
}
