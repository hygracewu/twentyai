using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TwentyAI
{
    public partial class Form1 : Form
    {
        static bool[,] isBlank = new bool[7, 8];
        List<Point> reachlist = new List<Point>();
        private bool movable(Block[,] state, Point start, Point dest, ref List<Point> route)
        {
            if (state[start.X, start.Y].getNumber() == 0)
                return false;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(state[i, j].getNumber() == 0)
                        isBlank[i, j] = true;
                    else
                        isBlank[i, j] = false;
                }
            }
            //isBlank[dest.X, dest.Y] = true;

            /*for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 7; i++)
                {
                    Debug.WriteIf(isBlank[i, j], "T" + " ");
                    Debug.WriteIf(!isBlank[i, j], "F" + " ");
                }
                Debug.Write("\n");
            }*/

            Queue frontier = new Queue();
            frontier.Enqueue(start);
            List<Point> explored = new List<Point>();
            List<Point> actionList = new List<Point>();
            Dictionary<Point, List<Point>> transitionTable = new Dictionary<Point, List<Point>>();
            Point leafNode = new Point();
            while (frontier.Count != 0)
            {
                leafNode = (Point)frontier.Dequeue();
                //Debug.Write(leafNode);

                if (transitionTable.ContainsKey(leafNode))
                {
                    actionList = new List<Point>(transitionTable[leafNode]);
                }
                
                if (leafNode == dest)
                {
                    route = actionList;
                    route.Add(dest);
                    return true;
                }
                explored.Add(leafNode);

                isBlank[leafNode.X, leafNode.Y] = false;
                List<Point> leaves = new List<Point>();
                reachlist.Clear();
                if (leafNode.X > 0 && reachable(state, leafNode, start, dest, 2) == true)//left
                    leaves.Add(new Point(leafNode.X - 1, leafNode.Y));
                reachlist.Clear();
                if (leafNode.Y > 0 && reachable(state, leafNode, start, dest, 1) == true)//down
                    leaves.Add(new Point(leafNode.X, leafNode.Y - 1));
                reachlist.Clear();
                if (leafNode.X < 6 && reachable(state, leafNode, start, dest, 3) == true)//right
                    leaves.Add(new Point(leafNode.X + 1, leafNode.Y));
                reachlist.Clear();
                if (leafNode.Y < 7 && reachable(state, leafNode, start, dest, 0) == true)//up
                    leaves.Add(new Point(leafNode.X, leafNode.Y + 1));

                List<Point> newActionList = new List<Point>();
                newActionList = actionList;
                newActionList.Add(leafNode);
                
                while (leaves.Count != 0)
                {
                    if (frontier.Contains(leaves[0]) == false && explored.Contains(leaves[0]) == false)
                    {
                        frontier.Enqueue(leaves[0]);
                        transitionTable.Add(leaves[0], newActionList);
                    }
                    leaves.RemoveAt(0);
                }
            }
            return false;
        }
        private bool reachable(Block[,] state, Point now, Point start, Point dest, int dir)
        {
            if (reachlist.Contains(start))
                return true;
            reachlist.Add(start);

            bool r = true;
            switch (dir)
            {
                case 0://up
                    if(state[start.X, start.Y].getConnect(0) == true)
                    {
                        if (reachable(state, new Point(now.X, now.Y+1), new Point(start.X, start.Y+1), dest,  0) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.Y >= 7 || isBlank[now.X, now.Y + 1] == false)
                            r = false;
                        if (now.Y < 7 && state[start.X, start.Y].getNumber() == state[now.X, now.Y + 1].getNumber())
                            r = true;
                    }
                    if(state[start.X, start.Y].getConnect(2) == true)
                    {
                        if (reachable(state, new Point(now.X-1, now.Y), new Point(start.X-1, start.Y), dest, 0) == false)
                            r = false;
                    }
                    if (state[start.X, start.Y].getConnect(3) == true)
                    {
                        if (reachable(state, new Point(now.X + 1, now.Y), new Point(start.X + 1, start.Y), dest, 0) == false)
                            r = false;
                    }
                    break;
                case 1://down
                    if (state[start.X, start.Y].getConnect(1) == true)
                    {
                        if (reachable(state, new Point(now.X, now.Y - 1), new Point(start.X, start.Y - 1), dest, 1) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.Y <= 0 || isBlank[now.X, now.Y - 1] == false)
                            r = false;
                        if (now.Y > 0 && state[start.X, start.Y].getNumber() == state[now.X, now.Y - 1].getNumber())
                            r = true;
                    }
                    if (state[start.X, start.Y].getConnect(2) == true)
                    {
                        if (reachable(state, new Point(now.X - 1, now.Y), new Point(start.X - 1, start.Y), dest, 1) == false)
                            r = false;
                    }
                    if (state[start.X, start.Y].getConnect(3) == true)
                    {
                        if (reachable(state, new Point(now.X + 1, now.Y), new Point(start.X + 1, start.Y), dest, 1) == false)
                            r = false;
                    }
                    break;
                case 2://left
                    if (state[start.X, start.Y].getConnect(2) == true)
                    {
                        if (reachable(state, new Point(now.X-1, now.Y), new Point(start.X-1, start.Y), dest, 2) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.X <= 0 || isBlank[now.X-1, now.Y] == false)
                            r = false;
                        if (now.X > 0 && state[start.X, start.Y].getNumber() == state[now.X-1, now.Y].getNumber())
                            r = true;
                    }
                    if (state[start.X, start.Y].getConnect(0) == true)
                    {
                        if (reachable(state, new Point(now.X, now.Y+1), new Point(start.X, start.Y+1), dest, 2) == false)
                            r = false;
                    }
                    if (state[start.X, start.Y].getConnect(1) == true)
                    {
                        if (reachable(state, new Point(now.X, now.Y-1), new Point(start.X, start.Y-1), dest, 2) == false)
                            r = false;
                    }
                    break;
                case 3://right
                    if (state[start.X, start.Y].getConnect(3) == true)
                    {
                        if (reachable(state, new Point(now.X + 1, now.Y), new Point(start.X + 1, start.Y), dest, 3) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.X >= 6 || isBlank[now.X + 1, now.Y] == false)
                            r = false;
                        if (now.X < 6 && state[start.X, start.Y].getNumber() == state[now.X+1, now.Y].getNumber())
                            r = true;
                    }
                    if (state[start.X, start.Y].getConnect(0) == true)
                    {
                        if (reachable(state, new Point(now.X, now.Y + 1), new Point(start.X, start.Y + 1), dest, 3) == false)
                            r = false;
                    }
                    if (state[start.X, start.Y].getConnect(1) == true)
                    {
                        if (reachable(state, new Point(now.X, now.Y - 1), new Point(start.X, start.Y - 1), dest, 3) == false)
                            r = false;
                    }
                    break;
                default:
                    Debug.Write("No such direction!");
                    return false;
            }
            return r;
        }
        
        private void moveBlock()
        {
            getCurrent();
            List<List<Point>> finalAction = new List<List<Point>>();
            AStarSearch(ref finalAction, 2);
            for(int i = 0; i < finalAction.Count; i++)
            {
                List<Point> route = new List<Point>();
                if (movable(Current, finalAction[i][0], finalAction[i][1], ref route))
                {
                    DragAlongRoute(ref route);
                }
                getCurrent();
            }
        }

        static Point s, d;
        List<Point> r;
        private void testMovable()
        {
            s.X = Convert.ToInt32(this.startX.Text);
            s.Y = Convert.ToInt32(this.startY.Text);
            d.X = Convert.ToInt32(this.destX.Text);
            d.Y = Convert.ToInt32(this.destY.Text);

            this.sn.Text = Convert.ToString(Current[s.X, s.Y].getNumber());
            this.dn.Text = Convert.ToString(Current[d.X, d.Y].getNumber());
            Debug.WriteLine(s);
            Debug.WriteLine(d);

            bool m = movable(Current, s, d, ref r);

            if (m)
            {
                this.textMovable.Text = "True";
                Debug.Write("Route: ");
                for (int i = 0; i < r.Count; i++)
                {
                    Debug.Write(r[i] + " ");
                }
                Debug.Write("\n");
            }
            else
                this.textMovable.Text = "False";
        }

        private void testUpdate()
        {
            s.X = Convert.ToInt32(this.startX.Text);
            s.Y = Convert.ToInt32(this.startY.Text);
            d.X = Convert.ToInt32(this.destX.Text);
            d.Y = Convert.ToInt32(this.destY.Text);
            List<Point> test = new List<Point>();
            test.Add(s);
            test.Add(d);

            Block[,] testResult = new Block[7, 8];
            update(Current, test, ref testResult);
            Current = testResult;
            Debug.WriteLine("*******************************");

            currentOutput();
        }
        private void testGetSuccessors()
        {
            getCurrent();
            List<List<Point>> a = new List<List<Point>>();
            List<Block[,]> l = new List<Block[,]>();
            getSuccessors(ref a, ref l, Current);
            Debug.WriteLine("Count: " + l.Count);
            for(int q = 0; q < a.Count; q++)
            {
                for(int w = 0; w < 2; w++)
                {
                    Debug.Write(a[q][w] + " ");
                }
                Debug.Write("\n");

                string oo = "";
                for (int i = 8 - 1; i >= 0; i--)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        oo += l[q][j, i].getNumber().ToString("00");
                        if (l[q][j, i].getConnect(3))
                            oo += "--";
                        else
                            oo += "  ";
                    }
                    oo += "\n";
                    for (int j = 0; j < 7; j++)
                    {
                        if (l[q][j, i].getConnect(1))
                            oo += "|";
                        else
                            oo += " ";
                        oo += "   ";
                    }
                    oo += "\n";
                }
                Debug.Write(oo);
                Debug.Write("\n");
            }
        }
    }
}
