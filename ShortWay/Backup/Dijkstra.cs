using System;
using System.Collections;
using System.Windows.Forms;
namespace Devgis.ShortWay
{
    /// <summary>
    ///本程序是Dijkstra算法的核心，采用Hashtable作为图的邻接表存储
    ///采用SortedList作为存储从起点到其他点距离的自动排序，注意需要将
    ///key和length颠倒放进SortedList，因为SortedList是按键排序的，
    ///采用这些方式可以提高内存的使用和搜索的效率
    /// </summary>
    public class Dijkstra
    {
        //保存到各个点的最短距离,同时用来标记已经求得的终点
        private double[] distance;
        //存储当前已经求得的最短路径终点TNODE和length
        private SortedList sortedNodeList;
        //存储源点到各个终点的所经过的各个点的路径
        private ArrayList[] paths;
        //图的邻接表的表示
        private Hashtable hashGraph;
        //表识起点的ID
        private int fnode;
        //表识图的节点个数
        private int nodeCount;
        public Dijkstra(Hashtable graph, int fnode, int count)
        {
            this.hashGraph = graph;
            this.fnode = fnode;
            this.nodeCount = count;
        }

        private void Initial()
        {
            //       source = new int[nodeCount];
            distance = new double[nodeCount];
            sortedNodeList = new SortedList();
            paths = new ArrayList[nodeCount];
            Hashtable hashAdj = (Hashtable)hashGraph[fnode];
            if (hashAdj == null)
            {
                throw new Exception("点不存在!");
            }
            IDictionaryEnumerator iEnum = hashAdj.GetEnumerator();
            while (iEnum.MoveNext())
            {
                int tnode = (int)iEnum.Key;
                double length = (double)iEnum.Value;
                sortedNodeList.Add(length, tnode);
            }
            for (int i = 0; i < nodeCount; i++)
            {
                paths[i] = new ArrayList();
                paths[i].Add(fnode);
                paths[i].Add(i);
                if (i == fnode)
                    distance[i] = 0;
                else
                    distance[i] = -1;
            }
        }

        public void GenerateMinPath()
        {
            Initial();
            for (int i = 0; i < nodeCount - 2; i++)
            {
                if (sortedNodeList.Count != 0)
                {
                    double minLen = (double)sortedNodeList.GetKey(0);
                    int destination = (int)sortedNodeList.GetByIndex(0);
                    sortedNodeList.RemoveAt(0);
                    distance[destination] = minLen;
                    Hashtable hashAdj = (Hashtable)hashGraph[destination];
                    if (hashAdj == null)
                        continue;
                    IDictionaryEnumerator iEnum = hashAdj.GetEnumerator();
                    while (iEnum.MoveNext())
                    {
                        int adjNode = (int)iEnum.Key;
                        double len = (double)iEnum.Value;
                        if (distance[adjNode] == -1 && !sortedNodeList.ContainsValue(adjNode))
                        {
                            double dis = minLen + len;
                            sortedNodeList.Add(dis, adjNode);
                            paths[adjNode].RemoveRange(1, paths[adjNode].Count - 2);
                            paths[adjNode].InsertRange(1, paths[destination].GetRange(1, paths[destination].Count - 1));
                        }
                        else if (distance[adjNode] == -1 && sortedNodeList.ContainsValue(adjNode))
                        {
                            int index = sortedNodeList.IndexOfValue(adjNode);
                            double currLen = (double)sortedNodeList.GetKey(index);
                            double dis = minLen + len;
                            if (dis < currLen)
                            {
                                sortedNodeList.RemoveAt(index);
                                sortedNodeList.Add(dis, adjNode);
                                paths[adjNode].RemoveRange(1, paths[adjNode].Count - 2);
                                paths[adjNode].InsertRange(1, paths[destination].GetRange(1, paths[destination].Count - 1));
                            }
                        }
                        else
                            continue;
                    }
                }
            }
        }

        //获得从源点到其他各个点的最短的距离
        public double[] GetMinDistance()
        {
            return distance;
        }

        //获取从源点到其他各个点的最短距离的路径
        public ArrayList[] GetAllMinPath()
        {
            return paths;
        }

        //获取从源点到指定点的最短距离的路径
        public ArrayList GetMinPath(int v0)
        {
            if (v0 < 0 || v0 >= nodeCount)
            {
                throw (new Exception("节点不正确"));
            }
            return paths[v0];
        }

    }
}