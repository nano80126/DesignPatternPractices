using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractices.CreationalPatterns
{
    public class Position : ICloneable
    {
        private int _x;
        private int _y;

        public int GetX()
        {
            return _x;
        }

        public void SetX(int x)
        {
            _x = x;
        }

        public int GetY()
        {
            return _y;
        }

        public void SetY(int y)
        {
            _y = y;
        }

        public object Clone()
        {
            return (Position)MemberwiseClone();
        }
    }

    public class Tank : ICloneable
    {
        private Position _pos;

        public Tank()
        {
            Console.WriteLine($"Tank create success!");
        }

        public Position GetPosition()
        {
            if (_pos == null) { _pos = new Position(); }
            return _pos;
        }

        public void SetPosition(int x, int y)
        {
            if (_pos == null)
            {
                _pos = new Position();
            }
            _pos.SetX(x);
            _pos.SetY(y);
        }

        public object Clone()
        {
            Console.WriteLine($"Tank clone success");
            Tank tank = (Tank)MemberwiseClone();
            tank._pos = (Position)_pos.Clone();
            return tank;
        }
    }
}
