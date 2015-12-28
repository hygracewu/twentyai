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
        private bool movable(Point start, Point dest, ref List<Point> route)
        {
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
                leafNode = (Point) frontier.Dequeue();

                if (transitionTable.ContainsKey(leafNode))
                {
                    actionList = new List<Point>(transitionTable[leafNode]);
                }

                /*Debug.Write(leafNode + " - ");
                Debug.Write("actionlist: ");
                for (int g = 0; g < actionList.Count; g++)
                    Debug.Write(actionList[g] + " ");
                Debug.Write("\n");*/


                if (leafNode == dest)
                {
                    route = actionList;
                    route.Add(dest);
                    return true;
                }

                isBlank[leafNode.X, leafNode.Y] = false;
                List<Point> leaves = new List<Point>();
                if (leafNode.X > 0 && isBlank[leafNode.X - 1, leafNode.Y] == true)
                    leaves.Add(new Point(leafNode.X - 1, leafNode.Y));
                if (leafNode.Y > 0 && isBlank[leafNode.X, leafNode.Y - 1] == true)
                    leaves.Add(new Point(leafNode.X, leafNode.Y - 1));
                if (leafNode.X < 6 && isBlank[leafNode.X + 1, leafNode.Y] == true)
                    leaves.Add(new Point(leafNode.X + 1, leafNode.Y));
                if (leafNode.Y < 7 && isBlank[leafNode.X, leafNode.Y + 1] == true)
                    leaves.Add(new Point(leafNode.X, leafNode.Y + 1));

                List<Point> newActionList = new List<Point>();
                newActionList = actionList;
                newActionList.Add(leafNode);

                while(leaves.Count != 0)
                {
                    if (frontier.Contains(leaves[0]) == false)
                    {
                        frontier.Enqueue(leaves[0]);
                        transitionTable.Add(leaves[0], newActionList);
                        /*Debug.Write("PUSH" + leaves[0] + " - ");
                        Debug.Write("actionlist: ");
                        for (int f = 0; f < newActionList.Count; f++)
                            Debug.Write(newActionList[f] + " ");
                        Debug.Write("\n");*/
                    }
                    leaves.RemoveAt(0);
                }

            }


            for (int i = 8 - 1; i >= 0; i--)
            {
                for (int j = 0; j < 7; j++)
                {
                    Debug.Write(Current[j, i] + " ");
                }
                Debug.Write("\n");
            }
            Debug.Write("\n");
            Debug.WriteLine("FALSE");
            return false;
        }

        private void moveBlock(Point start, Point dest)
        {
            Debug.WriteLine(start.ToString() + "->" + dest.ToString());
            List<Point> route = new List<Point>();
            if (movable(start, dest, ref route))
            {
                /*
                for (int i = 8 - 1; i >= 0; i--)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Debug.Write(Current[j, i] + " ");
                    }
                    Debug.Write("\n");
                }
                Debug.Write("route: ");
                foreach(Point p in route)
                {
                    Debug.Write(p.ToString());
                    Debug.Write(", ");
                }
                Debug.Write("\n");
                Debug.Write("\n");
                */
                DragAlongRoute(ref route);
            }
        }

    }
}
