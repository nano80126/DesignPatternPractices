using System;

namespace DesignPatternPractices.CreationalPatterns
{
    /// 
    /// 確保一個系統只有實例化一個物件
    /// 1. Greed Singleton
    /// 2. Lazy Initialization
    /// 3. Double Check Locking / DCL
    /// 4. Using Lazy<T>
    /// 若 Freight 不為園子操作，要加上 lock，否則多執行緒下，也有可能造成結果錯誤


    /// <summary>
    /// Greed Singleton
    /// </summary>
    class Supermarket
    {
        private int quantity = 100;
        private static Supermarket uniqueInstance = new Supermarket();

        public static Supermarket GetInstance => uniqueInstance;

        /// <summary>
        /// 建構涵式
        /// </summary>
        private Supermarket()
        {

        }

        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public int GetQuantity()
        {
            return quantity;
        }
    }

    /// <summary>
    /// Lazy initialization (非執行緒安全)
    /// </summary>
    public sealed class SupermarketLazy
    {
        private int quantity = 100;
        private static SupermarketLazy uniqueInstance = null;

        public static SupermarketLazy GetInstance
        {
            get
            {
                if (uniqueInstance == null)
                {
                    uniqueInstance = new SupermarketLazy();
                    Console.WriteLine($"建立 SupermerketLazy");
                }
                return uniqueInstance;
            }
        }

        /// <summary>
        /// 建構涵式
        /// </summary>
        private SupermarketLazy()
        {

        }

        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public int GetQuantity()
        {
            return quantity;
        }
    }


    /// <summary>
    /// Lazy Initialization Double Check Locking
    /// </summary>
    public sealed class SupermarketLazyDCL
    {
        private int quantity = 100;
        private static SupermarketLazyDCL uniqueInstance = null;
        private static readonly object _lock = new object();

        public static SupermarketLazyDCL GetInstance
        {
            get
            {
                if (uniqueInstance == null)
                {
                    lock (_lock)
                    {
                        if (uniqueInstance == null)
                        {
                            uniqueInstance = new SupermarketLazyDCL();
                            Console.WriteLine($"建立 SupermerketLazy");
                        }
                    }
                }
                return uniqueInstance;
            }
        }

        /// <summary>
        /// 建構涵式
        /// </summary>
        private SupermarketLazyDCL()
        {

        }

        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public int GetQuantity()
        {
            return this.quantity;
        }
    }

    /// <summary>
    /// Lazy Initialization Using Lazy<T>
    /// </summary>
    public sealed class SupermarketLazy2
    {
        private int _quantity;

        private SupermarketLazy2() { }

        private static readonly Lazy<SupermarketLazy2> lazy = new Lazy<SupermarketLazy2>(() =>
        {
            SupermarketLazy2 instance = new SupermarketLazy2()
            {
                _quantity = 100
            };
            return instance;
        });

        public static SupermarketLazy2 Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void SetQuantity(int quantity)
        {
            _quantity = quantity;
        }

        public int GetQuantity()
        {
            return _quantity;
        }
    }

    class Freight
    {
        public SupermarketLazyDCL supermarket;

        private static readonly object _lock = new object();
        public Freight(SupermarketLazyDCL supermarket)
        {
            this.supermarket = supermarket;
        }

        public void MoveIn(int i)
        {
            lock (_lock)
            {
                supermarket.SetQuantity(supermarket.GetQuantity() + i); 
            }
        }

        public void MoveOut(int i)
        {
            supermarket.SetQuantity(supermarket.GetQuantity() - i);
        }
    }
}
