using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TwentyAI
{
    public partial class Form1 : Form
    {
        private void AStarSearch(ref List<List<Point>> finalActionList, int depth)
        {
            PriorityQueue<Block[,]> frontier = new PriorityQueue<Block[,]>();
            frontier.Push(Current,1);
            List<Block[,]> explored = new List<Block[,]>();
            List<List<Point>> actionList = new List<List<Point>>();
            Dictionary<Block[,], List<List<Point>>> transitionTable = new Dictionary<Block[,], List<List<Point>>>();
            Block[,] leafNode = new Block[7,8];
            while (frontier.Count() != 0 || depth > 0)
            {
                depth -= 1;
                leafNode = frontier.Pop();
                if (transitionTable.ContainsKey(leafNode))
                {
                    actionList = new List<List<Point>>(transitionTable[leafNode]);
                }
                if (isGoal(leafNode))
                {
                    finalActionList = actionList;
                    return;
                }
                explored.Add(leafNode);
                List<List<Point>> actions = new List<List<Point>>();
                List<Block[,]> leaves = new List<Block[,]>();
                getSuccessors(ref actions, ref leaves, leafNode);

                while(leaves.Count != 0)
                {
                    Block[,] leaf = leaves[0];
                    leaves.RemoveAt(0);
                    List<Point> leafAction = actions[0];
                    actions.RemoveAt(0);
                    List<List<Point>> leafActionList = actionList;
                    leafActionList.Add(leafAction);
                    int leafCost = heuristic(leaf);
                    if(explored.Contains(leaf) ==false && frontier.Contains(leaf) == false)
                    {
                        frontier.Push(leaf, leafCost);
                        transitionTable.Add(leaf, leafActionList);
                    }
                    leaves.Remove(leaf);
                }
            }

        }
        private void getSuccessors(ref List<List<Point>> action, ref List<Block[,]> leaves, Block[,] leafNode)
        {
            //TODO
            //function "update(Block[,] state, List<Point> action, ref Block[,] result)" is useful
        }
        private int heuristic(Block[,] state)
        {
            //TODO
            return 0;
        }

        /***************************************/
        /* The code below should not be changed.    */
        /***************************************/
        private bool isGoal(Block[,] node)
        {
            Dictionary<int, int> numberHashTable = new Dictionary<int, int>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (numberHashTable.ContainsKey(node[i, j].getNumber()))
                        return false;
                    else
                        numberHashTable.Add(node[i, j].getNumber(), 1);
                }
            }
            return true;
        }
        List<Point> reputlist = new List<Point>();
        private void update(Block[,] state, List<Point> action, ref Block[,] result)
        {
            if (state[action[0].X, action[0].Y].getNumber() == 0)
            {
                Debug.WriteLine("ERROR, not a block!");
            }
            if (state[action[1].X, action[1].Y].getNumber() == 0)
            {
                reputlist.Clear();
                reput(ref state, new Point(action[0].X, action[0].Y), new Point(action[1].X, action[1].Y));
            }
            else
            {
                merge(ref state, action[1], state[action[1].X, action[1].Y].getNumber() + 1, true);
                merge(ref state, action[0], 0, false);
                state[action[0].X, action[0].Y].setNumber(0);

                reputlist.Clear();
                if (state[action[0].X, action[0].Y].getConnect(0))
                    reput(ref state, new Point(action[0].X, action[0].Y + 1), new Point(action[1].X, action[1].Y + 1));
                reputlist.Clear();
                if (state[action[0].X, action[0].Y].getConnect(1))
                    reput(ref state, new Point(action[0].X, action[0].Y - 1), new Point(action[1].X, action[1].Y - 1));
                reputlist.Clear();
                if (state[action[0].X, action[0].Y].getConnect(2))
                    reput(ref state, new Point(action[0].X - 1, action[0].Y), new Point(action[1].X - 1, action[1].Y));
                reputlist.Clear();
                if (state[action[0].X, action[0].Y].getConnect(3))
                    reput(ref state, new Point(action[0].X + 1, action[0].Y), new Point(action[1].X + 1, action[1].Y));

                state[action[0].X, action[0].Y].resetConnect();
            }

            while (true)
            {
                Queue queue = new Queue();
                List<Point> ignore = new List<Point>();
                for(int i = 0; i < 7; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        if (state[i, j].getNumber() == 0 || ignore.Contains(new Point(i, j)))
                            continue;
                        if (state[i, j - 1].getNumber() == 0 || state[i, j].getNumber() == state[i, j - 1].getNumber())
                        {
                            reputlist.Clear();
                            if (fallable(ref state, new Point(i, j)))
                            {
                                queue.Enqueue(new Point(i, j));
                                addIgnore(ref state, ref ignore, ref queue, new Point(i, j));
                            }
                        }
                    }
                }

                if (queue.Count == 0)
                    break;

                while (queue.Count != 0)
                {
                    Point leaf = (Point)queue.Dequeue();
                    reputlist.Clear();
                    fall(ref state, leaf);
                }

            }
            
            result = state;
        }
        private void addIgnore(ref Block[,] state, ref List<Point> ignore, ref Queue queue, Point p)
        {
            if (ignore.Contains(p) || queue.Contains(p))
                return;
            ignore.Add(p);
            if (state[p.X, p.Y].getConnect(0))
                addIgnore(ref state, ref ignore, ref queue, new Point(p.X, p.Y + 1));
            if (state[p.X, p.Y].getConnect(1))
                addIgnore(ref state, ref ignore, ref queue, new Point(p.X, p.Y - 1));
            if (state[p.X, p.Y].getConnect(2))
                addIgnore(ref state, ref ignore, ref queue, new Point(p.X - 1, p.Y));
            if (state[p.X, p.Y].getConnect(3))
                addIgnore(ref state, ref ignore, ref queue, new Point(p.X + 1, p.Y));
        }
        private void merge(ref Block[,] state, Point pos, int num, bool resetConnectedOrNot)
        {
            state[pos.X, pos.Y].setNumber(num);
            if (state[pos.X, pos.Y].getConnect(0))
                state[pos.X, pos.Y + 1].setConnect(1, false);
            if (state[pos.X, pos.Y].getConnect(1))
                state[pos.X, pos.Y - 1].setConnect(0, false);
            if (state[pos.X, pos.Y].getConnect(2))
                state[pos.X - 1, pos.Y].setConnect(3, false);
            if (state[pos.X, pos.Y].getConnect(3))
                state[pos.X + 1, pos.Y].setConnect(2, false);
            if(resetConnectedOrNot)
                state[pos.X, pos.Y].resetConnect();
        }
        private void reput(ref Block[,] state, Point ori, Point des)
        {
            if (reputlist.Contains(ori))
                return;
            reputlist.Add(ori);

            if (state[des.X, des.Y].getNumber() == state[ori.X, ori.Y].getNumber())
                merge(ref state, des, state[des.X, des.Y].getNumber()+1, true);
            else
                state[des.X, des.Y] = new Block(state[ori.X, ori.Y]);
            if (state[ori.X, ori.Y].getConnect(0))
                reput(ref state, new Point(ori.X, ori.Y + 1), new Point(des.X, des.Y + 1));
            if (state[ori.X, ori.Y].getConnect(1))
                reput(ref state, new Point(ori.X, ori.Y - 1), new Point(des.X, des.Y - 1));
            if (state[ori.X, ori.Y].getConnect(2))
                reput(ref state, new Point(ori.X - 1, ori.Y), new Point(des.X - 1, des.Y));
            if (state[ori.X, ori.Y].getConnect(3))
                reput(ref state, new Point(ori.X + 1, ori.Y), new Point(des.X + 1, des.Y));

            state[ori.X, ori.Y].setNumber(0);
            state[ori.X, ori.Y].resetConnect();
        }
       private bool fallable(ref Block[,] state, Point p)
        {
            if (reputlist.Contains(p))
                return true;
            reputlist.Add(p);

            bool r = true;
            if(state[p.X, p.Y - 1].getNumber() != 0 && state[p.X, p.Y - 1].getNumber() != state[p.X, p.Y].getNumber())
            {
                if (state[p.X, p.Y].getConnect(1) == true)
                    r = true;
                else
                    r = false;
            }
            if ( (state[p.X, p.Y].getConnect(0) && fallable(ref state, new Point(p.X, p.Y + 1)) == false) ||
                  (state[p.X, p.Y].getConnect(2) && fallable(ref state, new Point(p.X - 1, p.Y)) == false) ||
                  (state[p.X, p.Y].getConnect(3) && fallable(ref state, new Point(p.X + 1, p.Y)) == false))
                r = false;
            return r;
        }
        private void fall(ref Block[,] state, Point p)
        {
            if (reputlist.Contains(p))
                return;
            reputlist.Add(p);

            if(state[p.X, p.Y-1].getNumber() == state[p.X, p.Y].getNumber())
            {
                merge(ref state, new Point(p.X, p.Y - 1), state[p.X, p.Y].getNumber() + 1, true);
                merge(ref state, new Point(p.X, p.Y), 0, false);
                if (state[p.X, p.Y].getConnect(2) == true)
                    fall(ref state, new Point(p.X - 1, p.Y));
                if (state[p.X, p.Y].getConnect(3) == true)
                    fall(ref state, new Point(p.X + 1, p.Y));
                if (state[p.X, p.Y].getConnect(0) == true)
                    fall(ref state, new Point(p.X, p.Y + 1));
                else
                    state[p.X, p.Y].resetConnect();
            }
            else if(state[p.X, p.Y - 1].getNumber() == 0)
            {
                if (state[p.X, p.Y].getConnect(2) == true)
                    fall(ref state, new Point(p.X - 1, p.Y));
                if (state[p.X, p.Y].getConnect(3) == true)
                    fall(ref state, new Point(p.X + 1, p.Y));
                state[p.X, p.Y-1] = new Block(state[p.X, p.Y]);
                if (state[p.X, p.Y].getConnect(0) == true)
                    fall(ref state, new Point(p.X, p.Y + 1));
                else
                {
                    state[p.X, p.Y].resetConnect();
                    state[p.X, p.Y].setNumber(0);
                }
            }
            else if(state[p.X, p.Y].getConnect(1) == true)
            {
                state[p.X, p.Y - 1] = state[p.X, p.Y];
                state[p.X, p.Y].setNumber(0);
                state[p.X, p.Y].resetConnect();
            }
            else
            {
                Debug.WriteLine("ERROR, not fallable!");
            }
        }
    }
}
