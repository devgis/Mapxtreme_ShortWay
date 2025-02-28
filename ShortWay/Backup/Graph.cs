using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
namespace Devgis.ShortWay
{
	/// <summary>
	/// Written By liyafei
	/// ������ʵ��ͼ�Ĺ�����������
	/// Graph����HashTable����ʾ�߼�ͼ
	/// </summary>
	public class Graph
	{
        System.Collections.Generic.List<RoadInfo> lstRoadInfo;
		public int nodeCount = 0;
		private System.Collections.Hashtable hashGraph = new Hashtable();
        public Graph(System.Collections.Generic.List<RoadInfo> ListRoadInfo, int inodecount)
		{
            lstRoadInfo = ListRoadInfo;
            nodeCount = inodecount;
		}
		public int NodeCount
		{
			get
			{
				return nodeCount;
			}
		}

		public Hashtable HashGraph
		{
			get
			{
				return hashGraph;
			}
		}
		public void ConstructGraph()
		{
            foreach (RoadInfo rInfo in lstRoadInfo)
			{
                int FNODE = rInfo.FNode;
                int TNODE = rInfo.TNode;
                double length = rInfo.Length;

                //������в��������
                if (!hashGraph.ContainsKey(FNODE))
                {
                    Hashtable hashAdj = new Hashtable();
                    hashAdj.Add(TNODE, length);
                    hashGraph.Add(FNODE, hashAdj);
                }
                //���а������
                else
                {
                    Hashtable hashAdj = (Hashtable)hashGraph[FNODE];
                    if (!hashAdj.ContainsKey(TNODE))
                    {
                        hashAdj.Add(TNODE, length);
                    }
                }

                //������в��������
                if (!hashGraph.ContainsKey(TNODE))
                {
                    Hashtable hashAdj = new Hashtable();
                    hashAdj.Add(FNODE, length);
                    hashGraph.Add(TNODE, hashAdj);
                }

                //���а������
                else
                {
                    Hashtable hashAdj = (Hashtable)hashGraph[TNODE];
                    if (!hashAdj.ContainsKey(FNODE))
                    {
                        hashAdj.Add(FNODE, length);
                    }
                }
			}
        }
		public void ModifyGraph(int lineKey)
		{
			int FNODE=0,TNODE=0;
            foreach (RoadInfo rInfo in lstRoadInfo)
            {
                if (rInfo.Key == lineKey)
                {
                    FNODE = rInfo.FNode;
                    TNODE = rInfo.TNode;
                }
            }
			Hashtable hashAdj = (Hashtable)hashGraph[FNODE];
			if(hashAdj == null)
				return;
			hashAdj.Remove(TNODE);
		}
		public void RestoreGraph(int lineKey)
		{
			int FNODE=0,TNODE=0;
			double length=0;

            foreach (RoadInfo rInfo in lstRoadInfo)
            {
                if (rInfo.Key == lineKey)
                {
                    FNODE = rInfo.FNode;
                    TNODE = rInfo.TNode;
                    length = rInfo.Length;
                }
            }
            Hashtable hashAdj = (Hashtable)hashGraph[FNODE];
			if(hashAdj == null)
				return;
			hashAdj.Add(TNODE,length);
		}
	}
}
