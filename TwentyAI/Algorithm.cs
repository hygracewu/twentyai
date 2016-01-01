using System.Collections;
using System.Collections.Generic;
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
        private bool isGoal(Block[,] node)
        {
            Dictionary<int, int> numberHashTable = new Dictionary<int, int>();
            for(int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (numberHashTable.ContainsKey(node[i, j].getNumber()))
                        return false;
                    else
                        numberHashTable.Add(node[i, j].getNumber(), 1);
                }
            }
            return true;
        }
        private void getSuccessors(ref List<List<Point>> action, ref List<Block[,]> leaves, Block[,] leafNode)
        {
            //TODO
        }
        private int heuristic(Block[,] state)
        {
            //TODO
            return 0;
        }
        private void update(Block[,] state, List<Point> action, ref Block[,] result)
        {
            //TODO
            //useful function for getSuccessors()
        }
    }
}
