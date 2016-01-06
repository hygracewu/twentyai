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
        private void AStarSearch(ref List<List<Point>> finalActionList, int depth)
        {
            PriorityQueue<Block[,]> frontier = new PriorityQueue<Block[,]>();
            frontier.Push(Current, 1);
            List<Block[,]> explored = new List<Block[,]>();
            List<List<Point>> actionList = new List<List<Point>>();
            Dictionary<Block[,], List<List<Point>>> transitionTable = new Dictionary<Block[,], List<List<Point>>>();
            Block[,] leafNode = new Block[7, 8];
            while (frontier.Count() != 0 && depth > 0)
            {
                //Debug.WriteLine("depth: " + depth);
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

                //Debug.WriteLine("leaves count: " + leaves.Count);
                while (leaves.Count != 0)
                {
                    Block[,] leaf = leaves[0];
                    leaves.RemoveAt(0);
                    List<Point> leafAction = actions[0];
                    //Debug.WriteLine("leafAction: " + leafAction[0] + " " + leafAction[leafAction.Count-1]);
                    actions.RemoveAt(0);
                    List<List<Point>> leafActionList = new List<List<Point>>(actionList);
                    leafActionList.Add(leafAction);
                    //Debug.WriteLine("-----" + leafActionList.Count);
                    int leafCost = evaluationFunction(leaf) + heuristic(leafAction);
                    if (explored.Contains(leaf) == false && frontier.Contains(leaf) == false)
                    {
                        frontier.Push(leaf, leafCost);
                        transitionTable.Add(leaf, leafActionList);
                    }
                    leaves.Remove(leaf);
                }
            }
            finalActionList = actionList;
            return;
        }
        private void getSuccessors(ref List<List<Point>> action, ref List<Block[,]> leaves, Block[,] leafNode)
        {
            updateJammed(ref leafNode);
            List<Point>[] blockHash = new List<Point>[21];
            for (int j = 0; j < 21; j++)
                blockHash[j] = new List<Point>();
            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (leafNode[i, j].getJammed() == false)
                        blockHash[leafNode[i, j].getNumber()].Add(new Point(i, j));
                }
            }

            bool num = false;
            for (int i = 20; i >= 1; i--)
            {
                if (blockHash[i].Count > 1)
                {
                    List<List<Point>> candidateList = new List<List<Point>>();
                    getCandidates(blockHash[i], ref candidateList, leafNode);
                    for (int j = 0; j < candidateList.Count; j++)
                    {
                        action.Add(candidateList[j]);
                        num = true;
                        Block[,] sta = new Block[7, 8];
                        update(leafNode, candidateList[j], ref sta);
                        leaves.Add(sta);
                    }
                }
            }

            updateJammed2(ref leafNode);
            int[] topPos = new int[7] { 7, 7, 7, 7, 7, 7, 7};
            //getTopPos(leafNode, ref topPos);
            for (int i = 20; i >= 1; i--)
            {
                if (blockHash[i].Count > 0)
                {
                    for (int j = 0; j < blockHash[i].Count; j++)
                    {
                        if (num && blockHash[i][j].X % 2 == 0)
                            continue;
                        for (int k = 0; k < 7; k+=2)
                        {
                            if (blockHash[i][j].X == k)
                                continue;
                            List<Point> temp = new List<Point>();
                            if (movable(leafNode, blockHash[i][j], new Point(k, topPos[k]), ref temp))
                            {
                                List<Point> act = new List<Point>();
                                act.Add(blockHash[i][j]);
                                act.Add(new Point(k, topPos[k]));
                                action.Add(act);

                                Block[,] sta = new Block[7, 8];
                                update(leafNode, act, ref sta);
                                leaves.Add(sta);

                            }
                        }
                    }
                }
            }

        }
        private int heuristic(List<Point> action)
        {
            return Math.Abs( (action[0].X - action[1].X) + (action[0].Y - action[0].Y) );
        }
        private int evaluationFunction(Block[,] state)
        {
            //TODO
            //priority高->低

            //連結數少
            int totalConnect = 0;
            //方塊pair數少
            Dictionary<int, int> numberHashTable = new Dictionary<int, int>();
            int pairNum = 0;
            int movablePairNum = 0;
            //可動方塊數多
            int jammedNum = 0;
            //最下層column數少一點
            int bottomNum = 0;
            //高度(最上面1~2層扣分)
            int[] topNum = new int[8];
            //方塊個數少(?(不太重要
            int blockNum = 0;
            //最下層數字小一點(不太重要
            int bottomSum = 0;

            int blockSum = 0;

            List<Point>[] blockHash = new List<Point>[21];
            for (int j = 0; j < 21; j++)
                blockHash[j] = new List<Point>();
            
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //連結數少
                    totalConnect += state[i, j].getTotalConnect();
                    //方塊pair數少
                    if (numberHashTable.ContainsKey(state[i, j].getNumber()))
                    {
                        if (state[i, j].getNumber() != 0)
                            ++pairNum;
                    }
                    else
                    {
                        numberHashTable.Add(state[i, j].getNumber(), 1);
                    }
                    if (state[i, j].getJammed() == false)
                        blockHash[state[i, j].getNumber()].Add(new Point(i, j));
                    //可動方塊數多
                    if (state[i, j].getJammed())
                        ++jammedNum;
                    //方塊個數少(?(不太重要
                    if (state[i, j].getNumber() != 0)
                        ++blockNum;
                    //高度(最上面1~2層扣分)
                    if (state[i, j].getNumber() != 0)
                        ++topNum[j];

                    blockSum += state[i, j].getNumber();
                }

                //最下層column數少一點
                if (state[i, 0].getNumber() != 0)
                    ++bottomNum;
                //最下層數字小一點(不太重要
                bottomSum += state[i, 0].getNumber();
            }

            for (int i = 20; i >= 1; i--)
            {
                if (blockHash[i].Count > 1)
                {
                    List<List<Point>> candidateList = new List<List<Point>>();
                    getCandidates(blockHash[i], ref candidateList, state);
                    for (int j = 0; j < candidateList.Count; j++)
                    {
                        ++movablePairNum;
                    }
                }
            }

            totalConnect /= 2;

            //方塊自由度高(好麻煩="=


            //算分數(爛度(越大越爛
            int score = (
                        + totalConnect * 800
                        //+ pairNum * 100
                        + jammedNum * 10
                        + bottomNum * 30
                        + topNum[6] * 50
                        + topNum[7] * 500
                        + blockNum * 1000
                        + bottomSum * 1
                        - movablePairNum * 100
                        + blockSum * 50
                        );
            for (int i = 0; i < 6; ++i)
            {
                score += i * 5 * topNum[i];
            }

            /*Debug.WriteLine("###" + score);
            Debug.WriteLine(totalConnect);// * 100
            Debug.WriteLine(pairNum);// * 50
            Debug.WriteLine(jammedNum);// * 10
            Debug.WriteLine(bottomNum);// * 30
            Debug.WriteLine(top7Num);// * 50
            Debug.WriteLine(top8Num);// * 10000
            Debug.WriteLine(blockNum);// * 5
            Debug.WriteLine(bottomSum);// * 3
            */

            return score;
        }

        /**************************************/
        /* The code below should not be changed.  */
        /**************************************/
        private bool isGoal(Block[,] node)
        {
            Dictionary<int, int> numberHashTable = new Dictionary<int, int>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (node[i, j].getNumber() == 0)
                        continue;
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
            //result = state;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result[i, j] = new Block(state[i, j]);
                }
            }

            if (result[action[0].X, action[0].Y].getNumber() == 0)
            {
                Debug.WriteLine("ERROR, not a block!");
                return;
            }
            if (result[action[1].X, action[1].Y].getNumber() == 0)
            {
                reputlist.Clear();
                reput(ref result, new Point(action[0].X, action[0].Y), new Point(action[1].X, action[1].Y));
            }
            else
            {
                merge(ref result, action[1], result[action[1].X, action[1].Y].getNumber() + 1, true);
                merge(ref result, action[0], 0, false);
                result[action[0].X, action[0].Y].setNumber(0);

                reputlist.Clear();
                if (result[action[0].X, action[0].Y].getConnect(0))
                    reput(ref result, new Point(action[0].X, action[0].Y + 1), new Point(action[1].X, action[1].Y + 1));
                reputlist.Clear();
                if (result[action[0].X, action[0].Y].getConnect(1))
                    reput(ref result, new Point(action[0].X, action[0].Y - 1), new Point(action[1].X, action[1].Y - 1));
                reputlist.Clear();
                if (result[action[0].X, action[0].Y].getConnect(2))
                    reput(ref result, new Point(action[0].X - 1, action[0].Y), new Point(action[1].X - 1, action[1].Y));
                reputlist.Clear();
                if (result[action[0].X, action[0].Y].getConnect(3))
                    reput(ref result, new Point(action[0].X + 1, action[0].Y), new Point(action[1].X + 1, action[1].Y));

                result[action[0].X, action[0].Y].resetConnect();
            }

            while (true)
            {
                Queue queue = new Queue();
                List<Point> ignore = new List<Point>();
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 7; j > 0; j--)
                    {
                        if (result[i, j].getNumber() == 0 || ignore.Contains(new Point(i, j)))
                            continue;
                        if (result[i, j - 1].getNumber() == 0 || result[i, j].getNumber() == result[i, j - 1].getNumber())
                        {
                            reputlist.Clear();
                            if (fallable(ref result, new Point(i, j)))
                            {
                                queue.Enqueue(new Point(i, j));
                                addIgnore(ref result, ref ignore, ref queue, new Point(i, j));
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
                    fall(ref result, leaf);
                }

            }

            //result =  result;
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
            {
                if (pos.Y < 6)
                    state[pos.X, pos.Y + 1].setConnect(1, false);
            }
            if (state[pos.X, pos.Y].getConnect(1))
            {
                if (pos.Y > 0)
                    state[pos.X, pos.Y - 1].setConnect(0, false);
            }
            if (state[pos.X, pos.Y].getConnect(2))
            {
                if (pos.X > 0)
                    state[pos.X - 1, pos.Y].setConnect(3, false);
            }
            if (state[pos.X, pos.Y].getConnect(3))
            {
                if (pos.X < 5)
                    state[pos.X + 1, pos.Y].setConnect(2, false);
            }
            if (resetConnectedOrNot)
                state[pos.X, pos.Y].resetConnect();
        }
        private void reput(ref Block[,] state, Point ori, Point des)
        {
            if (reputlist.Contains(ori))
                return;
            reputlist.Add(ori);

            if (des.X < 0 || des.X > 6 || ori.X < 0 || ori.X > 6) return;
            if (des.Y < 0 || des.Y > 7 || ori.Y < 0 || ori.Y > 7) return;

            //Debug.WriteLine("300: " + des.X + " " + des.Y + " " + ori.X + " " + ori.Y);
            if (state[des.X, des.Y].getNumber() == state[ori.X, ori.Y].getNumber())
                merge(ref state, des, state[des.X, des.Y].getNumber() + 1, true);
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

            if (p.X < 0 || p.X > 6) return false;
            if (p.Y < 0 || p.Y > 7) return false;

            bool r = true;
            if (state[p.X, p.Y - 1].getNumber() != 0 && state[p.X, p.Y - 1].getNumber() != state[p.X, p.Y].getNumber())
            {
                if (state[p.X, p.Y].getConnect(1) == true)
                    r = true;
                else
                    r = false;
            }
            if ((state[p.X, p.Y].getConnect(0) && fallable(ref state, new Point(p.X, p.Y + 1)) == false) ||
                  (state[p.X, p.Y].getConnect(2) && fallable(ref state, new Point(p.X - 1, p.Y)) == false) ||
                  (state[p.X, p.Y].getConnect(3) && fallable(ref state, new Point(p.X + 1, p.Y)) == false))
                r = false;
            return r;
        }
        private bool fall(ref Block[,] state, Point p)
        {
            if (reputlist.Contains(p))
                return true;
            reputlist.Add(p);

            if(state[p.X, p.Y].getNumber() == 0)
            {
                //Debug.Write("ZERO \n");
                return false;
            }

            if (state[p.X, p.Y - 1].getNumber() == state[p.X, p.Y].getNumber())
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
            else if (state[p.X, p.Y - 1].getNumber() == 0)
            {
                if (state[p.X, p.Y].getConnect(2) == true)
                    fall(ref state, new Point(p.X - 1, p.Y));
                if (state[p.X, p.Y].getConnect(3) == true)
                    fall(ref state, new Point(p.X + 1, p.Y));
                state[p.X, p.Y - 1] = new Block(state[p.X, p.Y]);
                if (state[p.X, p.Y].getConnect(0) == true)
                    fall(ref state, new Point(p.X, p.Y + 1));
                else
                {
                    state[p.X, p.Y].resetConnect();
                    state[p.X, p.Y].setNumber(0);
                }
            }
            else if (state[p.X, p.Y].getConnect(1) == true)
            {
                state[p.X, p.Y - 1] = state[p.X, p.Y];
                state[p.X, p.Y].setNumber(0);
                state[p.X, p.Y].resetConnect();
            }
            else
            {
                //Debug.WriteLine("ERROR, not fallable!");
            }
            return true;
        }
        private void getCandidates(List<Point> blockList, ref List<List<Point>> candidateList, Block[,] state)
        {
            List<Point> temp = new List<Point>();
            for (int i = 0; i < blockList.Count; i++)
            {
                for (int j = 0; j < blockList.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (movable(state, blockList[i], blockList[j], ref temp))
                    {
                        List<Point> temp2 = new List<Point>();
                        temp2.Add(blockList[i]);
                        temp2.Add(blockList[j]);
                        candidateList.Add(temp2);
                    }
                }
            }
        }
        private void getTopPos(Block[,] state, ref int[] topPos)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (state[i, j].getNumber() == 0)
                    {
                        topPos[i] = j;
                        break;
                    }
                }
            }
        }

    }
}