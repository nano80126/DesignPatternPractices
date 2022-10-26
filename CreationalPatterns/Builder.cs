using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractices.CreationalPatterns
{
    ///
    /// Product             最終被建立出來的物件類別
    /// Builder             用來定義建構物件過程中個必要步驟(方法)的介面
    /// ConcreteBuilder     實作 Builder 介面，實際用來建構物件的類別
    /// Director            負責指揮 ConcreteBuilder 該如果建構物件
    /// 


    public class Computer
    {
        private string cpu;
        private string mb;
        private string hdd;
        private string vga;
        private string keyboard;
        private string mouse;

        public Computer(string cpu, string mb, string hdd, string vga, string keyboard, string mouse)
        {
            this.cpu = cpu;
            this.mb = mb;
            this.hdd = hdd;
            this.vga = vga;
            this.keyboard = keyboard;
            this.mouse = mouse;
        }

        public Computer(string cpu, string mb, string hdd, string vga, string mouse)
        {
            this.cpu = cpu;
            this.mb = mb;
            this.hdd = hdd;
            this.vga = vga;
            this.mouse = mouse;
        }

        public Computer(string cpu, string mb, string hdd, string vga)
        {
            this.cpu = cpu;
            this.mb = mb;
            this.hdd = hdd;
            this.vga = vga;
        }


        public override string ToString()
        {
            return $"Computer: \n" +
                $"------------------ \n" +
                $"cpu: {cpu} \n" +
                $"mb: {mb} \n" +
                $"hdd: {hdd} \n" +
                $"vga: {vga} \n" +
                $"keyboard: {keyboard} \n" +
                $"mouse: {mouse} \n";
        }
    }


    public abstract class ComputerBuilder
    {
        protected Computer computer;

        public abstract ComputerBuilder SetCPU(string cpu);
        public abstract ComputerBuilder SetMB(string mb);
        public abstract ComputerBuilder SetHDD(string hdd);
        public abstract ComputerBuilder SetVGA(string vga);
        public abstract ComputerBuilder SetKeyBoard(string keyboard);
        public abstract ComputerBuilder SetMouse(string mouse);

        public abstract Computer Build();
    }

    public class ComputerFactory : ComputerBuilder
    {
        private string cpu;
        private string mb;
        private string hdd;
        private string vga;
        private string keyboard;
        private string mouse;

        public override Computer Build()
        {
            return new Computer(cpu, mb, hdd, vga, keyboard, mouse);
        }

        public override ComputerBuilder SetCPU(string cpu)
        {
            this.cpu = cpu;
            return this;
        }

        public override ComputerBuilder SetMB(string mb)
        {
            this.mb = mb;
            return this;
        }

        public override ComputerBuilder SetHDD(string hdd)
        {
            this.hdd = hdd;
            return this;
        }

        public override ComputerBuilder SetKeyBoard(string keyboard)
        {
            this.keyboard = keyboard;
            return this;
        }

        public override ComputerBuilder SetMouse(string mouse)
        {
            this.mouse = mouse;
            return this;
        }

        public override ComputerBuilder SetVGA(string vga)
        {
            this.vga = vga;
            return this;
        }
    }


    public class ComputerStore
    {
        private ComputerBuilder computerBuilder;

        public ComputerStore(ComputerBuilder computerBuilder)
        {
            this.computerBuilder = computerBuilder;
        }

        public Computer MixSpec()
        {
            Computer computer = computerBuilder.SetCPU("intel").SetMB("Apple").SetHDD("acer").SetVGA("msi").Build();
            return computer;
        }


        public Computer AppleSpec()
        {
            Computer computer = computerBuilder.SetCPU("Apple").SetMB("Apple").SetHDD("Apple").SetVGA("Apple").Build();
            return computer;
        }
    }

}
