using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competitorspace
{
    class Competitor
    {
        private int _ball;
        private int _index;
        private string _name;
        public void SetBall(int ball)
        {
            _ball = ball;
        }
        public int GetBall()
        {
            return _ball;
        }
        public void SetIndex(int index)
        {
            _index = index;
        }
        public int GetIndex()
        {
            return _index;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public string GetName()
        {
            return _name;
        }
        
    }
}
