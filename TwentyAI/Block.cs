﻿using System.Collections.Generic;
using System.Drawing;

namespace TwentyAI
{
    class Block
    {
        private int _number;
        private bool[] _connect = new bool[4];//0:up 1:down 2:left 3:right
        private bool _isJammed;
        public Block(int num)
        {
            _number = num;
            _connect[0] = _connect[1] = _connect[2] = _connect[3] = false;
            _isJammed = false;
        }
        public Block(Block old)
        {
            _number = old.getNumber();
            _connect[0] = old.getConnect(0);
            _connect[1] = old.getConnect(1);
            _connect[2] = old.getConnect(2);
            _connect[3] = old.getConnect(3);
            _isJammed = old.getJammed();
        }
        public int getNumber()
        {
            return _number;
        }
        public void setNumber(int num)
        {
            _number = num;
        }
        public bool getConnect(int idx)
        {
            return _connect[idx];
        }
        public void setConnect(int idx, bool set)
        {
            _connect[idx] = set;
        }
        public void resetConnect()
        {
            _connect[0] = _connect[1] = _connect[2] = _connect[3] = false;
        }
        public bool getJammed()
        {
            return _isJammed;
        }
        public void setJammed(bool s)
        {
            _isJammed = s;
        }

    }
}
