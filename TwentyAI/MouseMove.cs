﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;


namespace TwentyAI
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 SendInput(Int32 cInputs, ref INPUT pInputs, Int32 cbSize);

        [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 28)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public INPUTTYPE dwType;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBOARDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MOUSEINPUT
        {
            public Int32 dx;
            public Int32 dy;
            public Int32 mouseData;
            public MOUSEFLAG dwFlags;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct KEYBOARDINPUT
        {
            public Int16 wVk;
            public Int16 wScan;
            public KEYBOARDFLAG dwFlags;
            public Int32 time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HARDWAREINPUT
        {
            public Int32 uMsg;
            public Int16 wParamL;
            public Int16 wParamH;
        }

        public enum INPUTTYPE : int
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags()]
        public enum MOUSEFLAG : int
        {
            MOVE = 0x1,
            LEFTDOWN = 0x2,
            LEFTUP = 0x4,
            RIGHTDOWN = 0x8,
            RIGHTUP = 0x10,
            MIDDLEDOWN = 0x20,
            MIDDLEUP = 0x40,
            XDOWN = 0x80,
            XUP = 0x100,
            VIRTUALDESK = 0x400,
            WHEEL = 0x800,
            ABSOLUTE = 0x8000
        }

        [Flags()]
        public enum KEYBOARDFLAG : int
        {
            EXTENDEDKEY = 1,
            KEYUP = 2,
            UNICODE = 4,
            SCANCODE = 8
        }

        static public void LeftDown()
        {
            INPUT leftdown = new INPUT();

            leftdown.dwType = 0;
            leftdown.mi = new MOUSEINPUT();
            leftdown.mi.dwExtraInfo = IntPtr.Zero;
            leftdown.mi.dx = 0;
            leftdown.mi.dy = 0;
            leftdown.mi.time = 0;
            leftdown.mi.mouseData = 0;
            leftdown.mi.dwFlags = MOUSEFLAG.LEFTDOWN;

            SendInput(1, ref leftdown, Marshal.SizeOf(typeof(INPUT)));
        }
        static public void LeftUp()
        {
            INPUT leftup = new INPUT();

            leftup.dwType = 0;
            leftup.mi = new MOUSEINPUT();
            leftup.mi.dwExtraInfo = IntPtr.Zero;
            leftup.mi.dx = 0;
            leftup.mi.dy = 0;
            leftup.mi.time = 0;
            leftup.mi.mouseData = 0;
            leftup.mi.dwFlags = MOUSEFLAG.LEFTUP;

            SendInput(1, ref leftup, Marshal.SizeOf(typeof(INPUT)));
        }
        static public void LeftClick()
        {
            LeftDown();
            Thread.Sleep(20);
            LeftUp();
        }
        static public void DragTo(int sor_X, int sor_Y, int des_X, int des_Y)
        {
            Cursor.Position = new Point(sor_X, sor_Y); //click left button at source position
            LeftDown();
            Thread.Sleep(50);
            Cursor.Position = new Point(des_X, des_Y); //release left button at destination position
            LeftUp();
        }
        static public void DragAlongRoute(ref List<Point> route)
        {
            //Debug.Write(route[0]);
            //Debug.Write(route[route.Count-1]);
            Cursor.Position = cursorPosition[route[0].X, route[0].Y];
            LeftDown();
            route.RemoveAt(0);
            while (route.Count != 0)
            {
                Thread.Sleep(45);
                Cursor.Position = cursorPosition[route[0].X, route[0].Y];
                route.RemoveAt(0);
            }
            Thread.Sleep(45);
            LeftUp();
            Thread.Sleep(200);
        }
    }
}
