using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TwentyAI
{
    public partial class Form1 : Form
    {
        static bool[,] isBlank = new bool[7, 8];
        static List<Point> reachlist;
        private bool movable(Point start, Point dest, ref List<Point> route)
        {
            if (Current[start.X, start.Y].getNumber() == 0)
                return false;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(Current[i, j].getNumber() == 0)
                        isBlank[i, j] = true;
                    else
                        isBlank[i, j] = false;
                }
            }
            isBlank[dest.X, dest.Y] = true;

            Queue frontier = new Queue();
            frontier.Enqueue(start);
            List<Point> actionList = new List<Point>();
            Dictionary<Point, List<Point>> transitionTable = new Dictionary<Point, List<Point>>();
            Point leafNode = new Point();
            while (frontier.Count != 0)
            {
                leafNode = (Point)frontier.Dequeue();

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

                isBlank[leafNode.X, leafNode.Y] = false;
                List<Point> leaves = new List<Point>();
                reachlist.Clear();
                if (leafNode.X > 0 && reachable(leafNode, start, 2) == true)//left
                    leaves.Add(new Point(leafNode.X - 1, leafNode.Y));
                reachlist.Clear();
                if (leafNode.Y > 0 && reachable(leafNode, start, 1) == true)//down
                    leaves.Add(new Point(leafNode.X, leafNode.Y - 1));
                reachlist.Clear();
                if (leafNode.X < 6 && reachable(leafNode, start, 3) == true)//right
                    leaves.Add(new Point(leafNode.X + 1, leafNode.Y));
                reachlist.Clear();
                if (leafNode.Y < 7 && reachable(leafNode, start, 0) == true)//up
                    leaves.Add(new Point(leafNode.X, leafNode.Y + 1));

                List<Point> newActionList = new List<Point>();
                newActionList = actionList;
                newActionList.Add(leafNode);

                while (leaves.Count != 0)
                {
                    if (frontier.Contains(leaves[0]) == false)
                    {
                        frontier.Enqueue(leaves[0]);
                        transitionTable.Add(leaves[0], newActionList);
                    }
                    leaves.RemoveAt(0);
                }
            }
            return false;
        }
        private bool reachable(Point now, Point start, int dir)
        {
            if (reachlist.Contains(start))
                return true;
            reachlist.Add(start);

            bool r = true;
            switch (dir)
            {
                case 0://up
                    if(Current[start.X, start.Y].getConnect(0) == true)
                    {
                        if (reachable(new Point(now.X, now.Y+1), new Point(start.X, start.Y+1), 0) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.Y >= 7 || isBlank[now.X, now.Y + 1] == false)
                            r = false;
                    }
                    if(Current[start.X, start.Y].getConnect(2) == true)
                    {
                        if (reachable(new Point(now.X-1, now.Y), new Point(start.X-1, start.Y), 0) == false)
                            r = false;
                    }
                    if (Current[start.X, start.Y].getConnect(3) == true)
                    {
                        if (reachable(new Point(now.X + 1, now.Y), new Point(start.X + 1, start.Y), 0) == false)
                            r = false;
                    }
                    break;
                case 1://down
                    if (Current[start.X, start.Y].getConnect(1) == true)
                    {
                        if (reachable(new Point(now.X, now.Y - 1), new Point(start.X, start.Y - 1), 1) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.Y <= 0 || isBlank[now.X, now.Y - 1] == false)
                            r = false;
                    }
                    if (Current[start.X, start.Y].getConnect(2) == true)
                    {
                        if (reachable(new Point(now.X - 1, now.Y), new Point(start.X - 1, start.Y), 1) == false)
                            r = false;
                    }
                    if (Current[start.X, start.Y].getConnect(3) == true)
                    {
                        if (reachable(new Point(now.X + 1, now.Y), new Point(start.X + 1, start.Y), 1) == false)
                            r = false;
                    }
                    break;
                case 2://left
                    if (Current[start.X, start.Y].getConnect(2) == true)
                    {
                        if (reachable(new Point(now.X-1, now.Y), new Point(start.X-1, start.Y), 2) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.X <= 0 || isBlank[now.X-1, now.Y] == false)
                            r = false;
                    }
                    if (Current[start.X, start.Y].getConnect(0) == true)
                    {
                        if (reachable(new Point(now.X, now.Y+1), new Point(start.X, start.Y+1), 2) == false)
                            r = false;
                    }
                    if (Current[start.X, start.Y].getConnect(1) == true)
                    {
                        if (reachable(new Point(now.X, now.Y-1), new Point(start.X, start.Y-1), 2) == false)
                            r = false;
                    }
                    break;
                case 3://right
                    if (Current[start.X, start.Y].getConnect(3) == true)
                    {
                        if (reachable(new Point(now.X + 1, now.Y), new Point(start.X + 1, start.Y), 3) == false)
                            r = false;
                    }
                    else
                    {
                        if (now.X >= 6 || isBlank[now.X + 1, now.Y] == false)
                            r = false;
                    }
                    if (Current[start.X, start.Y].getConnect(0) == true)
                    {
                        if (reachable(new Point(now.X, now.Y + 1), new Point(start.X, start.Y + 1), 3) == false)
                            r = false;
                    }
                    if (Current[start.X, start.Y].getConnect(1) == true)
                    {
                        if (reachable(new Point(now.X, now.Y - 1), new Point(start.X, start.Y - 1), 3) == false)
                            r = false;
                    }
                    break;
                default:
                    Debug.Write("No such direction!");
                    return false;
            }
            return r;
        }

        static Point start, dest;
        private void moveBlock()
        {
            getCurrent();
            List<List<Point>> finalAction = new List<List<Point>>();
            AStarSearch(ref finalAction, 2);
            for(int i = 0; i < finalAction.Count; i++)
            {
                List<Point> route = new List<Point>();
                if (movable(finalAction[i][0], finalAction[i][1], ref route))
                {
                    DragAlongRoute(ref route);
                }
            }
        }

    }
}
