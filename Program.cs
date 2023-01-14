using System;
using System.Collections.Generic;
using System.Linq;
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
            //RunBuilder();
            //RunPrototype();s

            var result = ConvertToTitle(701);

            Console.WriteLine($"result: {result}");

            Console.WriteLine("Please enter any key to leave.");
            Console.ReadKey();
        }

        public static string ConvertToTitle(int columnNumber)
        {
            /// if columnNumber = 27
            /// 27 / 26 = 1 ... 1 => AA
            /// if columnNumber = 26
            /// 26 / 26 = 1 ... 0 => Z => A + 25 => (26 - 1) % 26
            
            /// 16 進位 => 0 ~ 15 => x / 16
            /// 
            /// 26 進位 => 1 ~ 26 => (x - 1) / 26

            StringBuilder builder = new StringBuilder();

            while (columnNumber > 0)
            {
                builder.Insert(0, (char)((columnNumber - 1) % 26 + 'A'));

                columnNumber = (columnNumber - 1) / 26;
            }

            return builder.ToString();
        }

        public static string Multiply(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0") return "0";
            int[] arr = new int[num1.Length + num2.Length];
            for (int i = num1.Length - 1; i >= 0; i--)
            {
                for (int j = num2.Length - 1; j >= 0; j--)
                {
                    int p1 = i + j;
                    int p2 = i + j + 1;

                    // 換算為實際 number
                    int mul = (num1[i] - '0') * (num2[j] - '0');

                    // arr[p2] = 上一圈 arr[p1]
                    int sum = mul + arr[p2]; 
                    arr[p2] = sum % 10;
                    arr[p1] += sum / 10;
                }
            }

            StringBuilder sb = new StringBuilder(arr.Length);

            foreach (int item in arr)
            {
                if (sb.Length > 0 || item != 0)
                {
                    sb.Append(item);
                }
            }

            return sb.ToString();
        }

        #region LeetCode
        public static int Search(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            // [7 0 1 2 4 5 6] 5
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (nums[mid] == target) return mid;
                else if (nums[mid] < target)
                {
                    if (nums[left] > nums[mid] && nums[right] < target)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
                else
                {
                    if (nums[right] < nums[mid] && nums[left] > target)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
            }
            return -1;
        }

        public static void NextPermutation(int[] nums)
        {
            int n = nums.Length;
            int k, l;

            for (k = n - 2; k >= 0; k--)
            {
                if (nums[k] < nums[k + 1]) break;
            }

            if (k < 0)
            {
                Array.Reverse(nums);
            }
            else
            {
                for (l = n - 1; l > k; l--)
                {
                    if (nums[l] > nums[k]) break;
                }
                int temp = nums[l];
                nums[l] = nums[k];
                nums[k] = temp;

                Array.Reverse(nums, k + 1, nums.Length - k - 1);
            }
        }

        public static ListNode SwapPairs(ListNode head)
        {
            ListNode zero = new ListNode(-1);
            zero.next = head;

            ListNode node = head;   // 跑迴圈用

            ListNode last = zero;  // 紀錄上一次迴圈的尾

            while (node != null && node.next != null)
            {
                ListNode left = node;
                ListNode right = node.next;
                // 先跑到下一回圈的開頭
                node = node.next?.next;

                last.next = right;
                // 執行交換
                Swap(left, right);

                last = left;
            }

            return zero.next;
        }

        public static void Swap(ListNode left, ListNode right)
        {
            left.next = right.next;
            right.next = left;
        }

        public static ListNode RemoveNthFromEend(ListNode head, int n)
        {
            List<int> list = new List<int>();
            ListNode result = head;

            while (result != null)
            {
                list.Add(head.val);
                result = result.next;
            }

            if (list.Count - 1 == 0) return null;


            result = head;
            head = result;
            if (list.Count == n)
            {
                head = result.next;
                return head;
            }

            for (int i = 0; i < list.Count - 1 - n; i++)
            {
                result = result.next;
            }

            result.next = result.next.next;
            return head;
        }

        public static IList<IList<int>> FourSum(int[] nums, int target)
        {
            List<IList<int>> list = new List<IList<int>>();

            //if (nums.Length == 4 && nums.Sum() == target)
            //{
            //    list.Add(nums);
            //    return list;
            //}

            // 排序
            nums = nums.OrderBy(x => x).ToArray();

            //if (nums[0] > target) return list;

            Console.WriteLine($"{string.Join(",", nums)}\n------------------------------------");

            for (int i = 0; i < nums.Length - 3; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) { continue; }

                for (int j = nums.Length - 1; j > i + 2; j--)
                {
                    if (j < nums.Length - 1 && nums[j] == nums[j + 1]) { continue; }

                    int left = i + 1;
                    int right = j - 1;

                    while (left < right)
                    {
                        int sum = nums[i] + nums[left] + nums[right] + nums[j];

                        if (sum < target)
                        {
                            left++;
                        }
                        else if (sum > target)
                        {
                            right--;
                        }
                        else
                        {
                            list.Add(new int[] { nums[i], nums[left], nums[right], nums[j] });

                            int low = nums[left];
                            int hight = nums[right];

                            while (left < right && nums[left] == low)
                            {
                                left++;
                            }

                            while (left < right && nums[right] == hight)
                            {
                                right--;
                            }
                        }
                    }
                }
            }

            return list;
        } 
        #endregion

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
        private static void RunFactory()
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
        private static void RunBuilder()
        {
            ComputerStore store = new ComputerStore(new ComputerFactory());
            Computer computer = store.MixSpec();

            Console.WriteLine($"{computer}");

            computer = store.AppleSpec();
            Console.WriteLine($"{computer}");
        }
        #endregion

        #region 原型模式
        private static void RunPrototype()
        {
            Tank tank = new Tank();
            Console.WriteLine($"{tank.GetPosition().GetX()} {tank.GetPosition().GetY()}");
            tank.SetPosition(5, 6);

            Tank tank2 = (Tank)tank.Clone();
            tank2.SetPosition(10, 5);

            Console.WriteLine($"{tank.GetPosition().GetX()} {tank.GetPosition().GetY()}");
            Console.WriteLine($"{tank2.GetPosition().GetX()} {tank2.GetPosition().GetY()}");
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
