using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using MapInfo.Windows.Dialogs;
using MapInfo.Data;
using MapInfo.Mapping;
using MapInfo.Tools;
using MapInfo.Engine;

namespace Devgis.ShortWay
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ShortWay : System.Windows.Forms.Form
	{
        //DataTable dtRoad;
        Hashtable hashAllMap=new Hashtable();
        Hashtable hashMapName = new Hashtable();
        #region 系统定义的
        private MapInfo.Windows.Controls.MapControl mapControl1;
        private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton ZoomIn;
		private System.Windows.Forms.ToolBarButton ZoomOut;
		private System.Windows.Forms.ToolBarButton Pan;
		private System.Windows.Forms.ToolBarButton EntireView;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton DestroyRoad;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
        private Graph graph;
        private Hashtable hashGraph;
		private int nodeCount;
		private Dijkstra dij;
		private int tag=0;
        private int FromNode=30;
		private System.Windows.Forms.ToolBarButton LayerControl;
		private int ToNode;
        private Button btSearch;
        private ComboBox cbStart;
        private ComboBox cbEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Button btUpdateNode;
        private Button btSearchWhere;
        private ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private Panel panel1;
        private Panel panel2;
		private int LineKey;
        public ShortWay()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortWay));
            this.mapControl1 = new MapInfo.Windows.Controls.MapControl();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.LayerControl = new System.Windows.Forms.ToolBarButton();
            this.ZoomIn = new System.Windows.Forms.ToolBarButton();
            this.ZoomOut = new System.Windows.Forms.ToolBarButton();
            this.Pan = new System.Windows.Forms.ToolBarButton();
            this.EntireView = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.DestroyRoad = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btSearch = new System.Windows.Forms.Button();
            this.cbStart = new System.Windows.Forms.ComboBox();
            this.cbEnd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btUpdateNode = new System.Windows.Forms.Button();
            this.btSearchWhere = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.IgnoreLostFocusEvent = false;
            this.mapControl1.Location = new System.Drawing.Point(0, 28);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(815, 485);
            this.mapControl1.TabIndex = 0;
            this.mapControl1.Text = "mapControl1";
            this.mapControl1.Tools.LeftButtonTool = null;
            this.mapControl1.Tools.MiddleButtonTool = null;
            this.mapControl1.Tools.RightButtonTool = null;
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.LayerControl,
            this.ZoomIn,
            this.ZoomOut,
            this.Pan,
            this.EntireView,
            this.toolBarButton1,
            this.DestroyRoad});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(815, 28);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // LayerControl
            // 
            this.LayerControl.ImageIndex = 1;
            this.LayerControl.Name = "LayerControl";
            // 
            // ZoomIn
            // 
            this.ZoomIn.ImageIndex = 2;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.ToolTipText = "放大";
            // 
            // ZoomOut
            // 
            this.ZoomOut.ImageIndex = 3;
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.ToolTipText = "缩小";
            // 
            // Pan
            // 
            this.Pan.ImageIndex = 4;
            this.Pan.Name = "Pan";
            this.Pan.ToolTipText = "平移";
            // 
            // EntireView
            // 
            this.EntireView.ImageIndex = 5;
            this.EntireView.Name = "EntireView";
            this.EntireView.ToolTipText = "全图";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // DestroyRoad
            // 
            this.DestroyRoad.ImageIndex = 7;
            this.DestroyRoad.Name = "DestroyRoad";
            this.DestroyRoad.ToolTipText = "损毁道路";
            this.DestroyRoad.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(449, 2);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 2;
            this.btSearch.Text = "最优搜索";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // cbStart
            // 
            this.cbStart.FormattingEnabled = true;
            this.cbStart.Location = new System.Drawing.Point(218, 4);
            this.cbStart.Name = "cbStart";
            this.cbStart.Size = new System.Drawing.Size(84, 20);
            this.cbStart.TabIndex = 3;
            // 
            // cbEnd
            // 
            this.cbEnd.FormattingEnabled = true;
            this.cbEnd.Location = new System.Drawing.Point(348, 4);
            this.cbEnd.Name = "cbEnd";
            this.cbEnd.Size = new System.Drawing.Size(84, 20);
            this.cbEnd.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "起点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "终点";
            // 
            // btUpdateNode
            // 
            this.btUpdateNode.Location = new System.Drawing.Point(732, 2);
            this.btUpdateNode.Name = "btUpdateNode";
            this.btUpdateNode.Size = new System.Drawing.Size(75, 23);
            this.btUpdateNode.TabIndex = 7;
            this.btUpdateNode.Text = "更新";
            this.btUpdateNode.UseVisualStyleBackColor = true;
            this.btUpdateNode.Visible = false;
            this.btUpdateNode.Click += new System.EventHandler(this.btUpdateNode_Click);
            // 
            // btSearchWhere
            // 
            this.btSearchWhere.Location = new System.Drawing.Point(647, 2);
            this.btSearchWhere.Name = "btSearchWhere";
            this.btSearchWhere.Size = new System.Drawing.Size(75, 23);
            this.btSearchWhere.TabIndex = 8;
            this.btSearchWhere.Text = "可用搜索";
            this.btSearchWhere.UseVisualStyleBackColor = true;
            this.btSearchWhere.Click += new System.EventHandler(this.btSearchWhere_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50"});
            this.comboBox1.Location = new System.Drawing.Point(577, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(64, 20);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(530, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "路段数";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 485);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(301, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 13;
            // 
            // ShortWay
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(815, 513);
            this.Controls.Add(this.mapControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btSearchWhere);
            this.Controls.Add(this.btUpdateNode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbEnd);
            this.Controls.Add(this.cbStart);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.toolBar1);
            this.Name = "ShortWay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "路径分析Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShortWay_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShortWay_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion 
        
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == LayerControl)
			{
                layerDlg();
			}
			if(e.Button == ZoomIn)
			{
				this.mapControl1.Tools.LeftButtonTool = "ZoomIn";
			}
			if(e.Button == ZoomOut)
			{
				this.mapControl1.Tools.LeftButtonTool = "ZoomOut";
			}
			if(e.Button == Pan)
			{
				this.mapControl1.Tools.LeftButtonTool = "Pan";
			}
			if(e.Button == EntireView)
			{
				MapInfo.Mapping.IMapLayerFilter iml = MapInfo.Mapping.MapLayerFilterFactory.FilterByType(typeof(MapInfo.Mapping.FeatureLayer));
				MapInfo.Mapping.MapLayerEnumerator mle = this.mapControl1.Map.Layers.GetMapLayerEnumerator(iml);
				this.mapControl1.Map.SetView(mle);
			}
			if(e.Button == DestroyRoad)
			{
				if(graph == null)
					PublicDim.ShowErrorMessage("请先构建图Create Graph");
				else
					graph.ModifyGraph(LineKey);
				PublicDim.ShowErrorMessage("清除这条道路");
			}
		}

		private void layerDlg()
		{
			LayerControlDlg lcDlg = new LayerControlDlg();
			lcDlg.Map = mapControl1.Map;
			lcDlg.ShowDialog(this);
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			
			Table temp = MapInfo.Engine.Session.Current.Catalog.GetTable("Temp");
			ArrayList MinPath = dij.GetMinPath(ToNode);
			for(int i=0;i<MinPath.Count-1;i++)
			{
				int FromNodeID = Convert.ToInt32(MinPath[i]);
				int ToNodeID = Convert.ToInt32(MinPath[i+1]);
				MIConnection connection = new MIConnection();
				MICommand command = connection.CreateCommand();	
				//command.CommandText = "Select * From RoadChina Where FNODE="+FromNodeID+" And TNODE="+ToNodeID;
                command.CommandText = "Select * From RoadChina Where (FNODE=" + FromNodeID + " And TNODE=" + ToNodeID + ") or (FNODE=" + ToNodeID  + " And TNODE=" + FromNodeID +")";
				connection.Open();
				MapInfo.Data.IResultSetFeatureCollection fc = command.ExecuteFeatureCollection();
				if(fc.Count==0)
					PublicDim.ShowErrorMessage("有向图中没有这条通路");
				foreach(Feature f in fc)
				{
					MapInfo.Geometry.FeatureGeometry geo = f.Geometry;
					MapInfo.Styles.LineWidth lw = new MapInfo.Styles.LineWidth(3,MapInfo.Styles.LineWidthUnit.Pixel);
					MapInfo.Styles.SimpleLineStyle sls = new MapInfo.Styles.SimpleLineStyle(lw,5,System.Drawing.Color.Red);
					MapInfo.Data.Feature line = new Feature(geo,sls);
					temp.InsertFeature(line);
				}
			}
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			if(graph == null)
				PublicDim.ShowErrorMessage("请先构建图Create Graph");
			else
			    graph.RestoreGraph(LineKey);
			PublicDim.ShowErrorMessage("恢复道路");
		}

        private void ShortWay_Load(object sender, EventArgs e)
        {
            #region 加载道路
            //MIConnection mConnection = new MIConnection();
            //mConnection.Open();
            //Table tRoad = MapHelper.openTable("gis_road", mConnection);
            //FeatureLayer flRoad = new FeatureLayer(tRoad);
            //mapControl1.Map.Layers.Add(flRoad);
            //mConnection.Close();
            //string MapPath = @"E:\CODE\SALE\功能\ShortWay\Data\map.mws";
            string MapPath = Path.Combine(Application.StartupPath, @"Data\map.mws");
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MapPath);
            mapControl1.Map.Load(mwsLoader);

            FeatureLayer flNode = mapControl1.Map.Layers["LineNode"] as FeatureLayer;
            flNode.Enabled = false;
            #endregion

            Table table=Session.Current.Catalog.GetTable("RoadChina");
            foreach (Feature feature in (table as MapInfo.Data.ITableFeatureCollection))
            {
                int FNODE = Convert.ToInt32(feature["FNODE"]);
                int TNODE = Convert.ToInt32(feature["TNODE"]);
                double length = Convert.ToDouble((feature.Geometry as MapInfo.Geometry.MultiCurve).Length(MapInfo.Geometry.DistanceUnit.Meter));
                string sName = feature["KEY"].ToString();
                if(sName.Trim()!="")
                {
                    int[] iRodeInfo1 = new int[2];
                    int[] iRodeInfo2 = new int[2];
                    iRodeInfo1[0]=FNODE;
                    iRodeInfo1[1]=TNODE;
                    hashMapName.Add(iRodeInfo1, sName);
                    iRodeInfo2[0] = TNODE;
                    iRodeInfo2[1] = FNODE;
                    hashMapName.Add(iRodeInfo2, sName);
                }

                //如果表中不包含起点
                if (!hashAllMap.ContainsKey(FNODE))
                {
                    Hashtable hashAdj = new Hashtable();
                    hashAdj.Add(TNODE, length);
                    hashAllMap.Add(FNODE, hashAdj);
                }
                //表中包含起点
                else
                {
                    Hashtable hashAdj = (Hashtable)hashAllMap[FNODE];
                    if (!hashAdj.ContainsKey(TNODE))
                    {
                        hashAdj.Add(TNODE, length);
                    }
                }

                //如果表中不包含起点
                if (!hashAllMap.ContainsKey(FNODE))
                {
                    Hashtable hashAdj = new Hashtable();
                    hashAdj.Add(TNODE, length);
                    hashAllMap.Add(FNODE, hashAdj);
                }
                //表中包含起点
                else
                {
                    Hashtable hashAdj = (Hashtable)hashAllMap[FNODE];
                    if (!hashAdj.ContainsKey(TNODE))
                    {
                        hashAdj.Add(TNODE, length);
                    }
                }

                //如果表中不包含起点
                if (!hashAllMap.ContainsKey(TNODE))
                {
                    Hashtable hashAdj = new Hashtable();
                    hashAdj.Add(FNODE, length);
                    hashAllMap.Add(TNODE, hashAdj);
                }

                //表中包含起点
                else
                {
                    Hashtable hashAdj = (Hashtable)hashAllMap[TNODE];
                    if (!hashAdj.ContainsKey(FNODE))
                    {
                        hashAdj.Add(FNODE, length);
                    }
                }
            }

            Table tblNode = Session.Current.Catalog.GetTable("LineNode");
            System.Collections.Generic.List<int> listNode1 = new System.Collections.Generic.List<int>();
            System.Collections.Generic.List<int> listNode2= new System.Collections.Generic.List<int>();
            foreach (Feature feature in (tblNode as MapInfo.Data.ITableFeatureCollection))
            {
                int iKey = Convert.ToInt32(feature["ID"]);
                listNode1.Add(iKey);
                listNode2.Add(iKey);
            }
            cbStart.DataSource = listNode1;
            cbEnd.DataSource = listNode2;

            //dtRoad = (Session.Current.Catalog.GetTable("RoadChina"). as TableInfoAdoNet).DataTable;
            //#region 缓存道路数据
            //dtRoad = OracleHelper.GetDataTable("select key,fnode,tnode,length,name from gis_road");
            //#endregion
            //#region 构造哈希表
            ////PublicDim.ShowErrorMessage("start load");
            
            //foreach (DataRow dr in dtRoad.Rows)
            //{
            //    int FNODE = Convert.ToInt32(dr["FNODE"]);
            //    int TNODE = Convert.ToInt32(dr["TNODE"]);
            //    double length = Convert.ToDouble(dr["LENGTH"]);
            //    //string sName=dr["NAME"].ToString();
            //    //if(sName.Trim()!="")
            //    //{
            //    //    int[] iRodeInfo1 = new int[2];
            //    //    int[] iRodeInfo2 = new int[2];
            //    //    iRodeInfo1[0]=FNODE;
            //    //    iRodeInfo1[1]=TNODE;
            //    //    hashMapName.Add(iRodeInfo1, sName);
            //    //    iRodeInfo2[0] = TNODE;
            //    //    iRodeInfo2[1] = FNODE;
            //    //    hashMapName.Add(iRodeInfo2, sName);
            //    //}
                
            //    ////如果表中不包含起点
            //    //if (!hashAllMap.ContainsKey(FNODE))
            //    //{
            //    //    Hashtable hashAdj = new Hashtable();
            //    //    hashAdj.Add(TNODE, length);
            //    //    hashAllMap.Add(FNODE, hashAdj);
            //    //}
            //    ////表中包含起点
            //    //else
            //    //{
            //    //    Hashtable hashAdj = (Hashtable)hashAllMap[FNODE];
            //    //    if (!hashAdj.ContainsKey(TNODE))
            //    //    {
            //    //        hashAdj.Add(TNODE, length);
            //    //    }
            //    //}

            //    //如果表中不包含起点
            //    if (!hashAllMap.ContainsKey(FNODE))
            //    {
            //        Hashtable hashAdj = new Hashtable();
            //        hashAdj.Add(TNODE, length);
            //        hashAllMap.Add(FNODE, hashAdj);
            //    }
            //    //表中包含起点
            //    else
            //    {
            //        Hashtable hashAdj = (Hashtable)hashAllMap[FNODE];
            //        if (!hashAdj.ContainsKey(TNODE))
            //        {
            //            hashAdj.Add(TNODE, length);
            //        }
            //    }

            //    //如果表中不包含起点
            //    if (!hashAllMap.ContainsKey(TNODE))
            //    {
            //        Hashtable hashAdj = new Hashtable();
            //        hashAdj.Add(FNODE, length);
            //        hashAllMap.Add(TNODE, hashAdj);
            //    }

            //    //表中包含起点
            //    else
            //    {
            //        Hashtable hashAdj = (Hashtable)hashAllMap[TNODE];
            //        if (!hashAdj.ContainsKey(FNODE))
            //        {
            //            hashAdj.Add(FNODE, length);
            //        }
            //    }
            //}
            ////PublicDim.ShowErrorMessage("load OK");
            //#endregion
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            if (cbStart.Text.Equals(cbEnd.Text))
            {
                PublicDim.ShowErrorMessage("起点终点不能相同！");
                return;
            }

            #region 清除上次图层
            Session.Current.Selections.DefaultSelection.Clear();
            #endregion 
            #region 获取起点终点
            if (cbStart.Text.Trim() == "" || cbEnd.Text.Trim() == "")
            {
                PublicDim.ShowErrorMessage("请选择起点和终点！");
                return;
            }
            else
            {
                try
                {
                    FromNode = Convert.ToInt32(cbStart.Text);
                    ToNode = Convert.ToInt32(cbEnd.Text);
                }
                catch
                {
                    PublicDim.ShowErrorMessage("请选择起点和终点！");
                    return;
                }

            }
            #endregion
            #region 创建图
            ////////////////////////////////////////////////////////////////////////////////////////string sSql = "select key,fnode,tnode,length from gis_road";
            ////////////////////////////////////////////////////////////////////////////////////////DataTable dtRoad = OracleHelper.GetDataTable(sSql);
            ////////////////////////////////////////////////////////////////////////////////////////sSql = "select count(*) from gis_road_node";
            ////////////////////////////////////////////////////////////////////////////////////////DataTable dtNode = OracleHelper.GetDataTable(sSql);

            Table tableRoad = Session.Current.Catalog.GetTable("RoadChina");
            //List<RoadInfo>
            System.Collections.Generic.List<RoadInfo> lstRoadInfo = new System.Collections.Generic.List<RoadInfo>();
            foreach (Feature feature in (tableRoad as MapInfo.Data.ITableFeatureCollection))
            {
                RoadInfo rInfo = new RoadInfo();
                rInfo.Key= Convert.ToInt32(feature["Key"]);
                rInfo.FNode = Convert.ToInt32(feature["FNODE"]);
                rInfo.TNode = Convert.ToInt32(feature["TNODE"]);
                rInfo.Length = Convert.ToDouble((feature.Geometry as MapInfo.Geometry.MultiCurve).Length(MapInfo.Geometry.DistanceUnit.Meter));
                lstRoadInfo.Add(rInfo);
            }
            int iCount = 0;
            Table tableNode = Session.Current.Catalog.GetTable("LineNode");
            SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchAll();
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tableNode, si);
            iCount = ifs.Count;

            graph = new Graph(lstRoadInfo, iCount);
            graph.ConstructGraph();

            this.hashGraph = graph.HashGraph;
            graph.nodeCount = iCount + 1;
            this.nodeCount = graph.NodeCount;
            

            dij = new Dijkstra(hashGraph, FromNode, nodeCount);
            try
            {
                dij.GenerateMinPath();
            }
            catch(Exception ex)
            {
                PublicDim.ShowErrorMessage(ex.Message);
                return;
            }

            #endregion
            #region 查找
            bool bFind = true;
            ArrayList MinPath = dij.GetMinPath(ToNode);
            if (MinPath.Count <= 0)
            {
                PublicDim.ShowErrorMessage("没有可通达的最短路径！");
                return;
            }
            for (int i = 0; i < MinPath.Count - 1; i++)
            {
                int FromNodeID = Convert.ToInt32(MinPath[i]);
                int ToNodeID = Convert.ToInt32(MinPath[i + 1]);
                SelectRoad(FromNodeID, ToNodeID);
                //String sWhere = "(FNODE=" + FromNodeID + " And TNODE=" + ToNodeID + ") or (FNODE=" + ToNodeID + " And TNODE=" + FromNodeID + ")";
                ////Table tableRoad = Session.Current.Catalog.GetTable("RoadChina");
                //SearchInfo siWhere = MapInfo.Data.SearchInfoFactory.SearchWhere(sWhere);
                //ifs = MapInfo.Engine.Session.Current.Catalog.Search(tableRoad, siWhere);
                //if (ifs.Count <= 0)
                //{
                //    //PublicDim.ShowErrorMessage("有向图中没有这条通路");
                //    bFind = false;
                //    break;
                //}
                //else
                //{
                //    Session.Current.Selections.DefaultSelection.Add(ifs);
                //    System.Threading.Thread.Sleep(50);
                //}
            }

            if (!bFind)
            {
                PublicDim.ShowErrorMessage("没有可通达的最短路径！");
            }

            #endregion
        }

        private void btUpdateNode_Click(object sender, EventArgs e)
        {
            #region 调用更新窗体
            GetNode formGNode = new GetNode();
            formGNode.ShowDialog();
            Application.Restart();
            #endregion
        }

        private void btSearchWhere_Click(object sender, EventArgs e)
        {
            if (cbStart.Text.Equals(cbEnd.Text))
            {
                PublicDim.ShowErrorMessage("起点终点不能相同！");
                return;
            }
            //long l1=DateTime.Now.Ticks;
            #region 初始信息
            Cursor.Current = Cursors.WaitCursor;
            #endregion
            #region 获取起点终点
            if (cbStart.Text.Trim() == "" || cbEnd.Text.Trim() == "")
            {
                PublicDim.ShowErrorMessage("请选择起点和终点！");
                return;
            }
            else
            {
                try
                {
                    FromNode = Convert.ToInt32(cbStart.Text);
                    ToNode = Convert.ToInt32(cbEnd.Text);
                }
                catch
                {
                    PublicDim.ShowErrorMessage("请选择起点和终点！");
                    return;
                }

            }
            #endregion
            #region 获取路径
            int iSearchLevel = 0;
            try
            {
                iSearchLevel = Convert.ToInt32(comboBox1.Text.Trim());
            }
            catch
            {
                PublicDim.ShowErrorMessage("请选择搜索级别");
                return;
            }
            if (iSearchLevel < 1)
            {
                PublicDim.ShowErrorMessage("搜索级别至少为1");
                return;
            }
            ArrayList al = SearchAllWay(FromNode, ToNode, iSearchLevel);
            try
            {
                if (al.Count == 0)
                {
                    PublicDim.ShowErrorMessage("无可用路径");
                    return;
                }
            }
            catch
            {
                PublicDim.ShowErrorMessage("无可用路径");
                return;
            }
            #endregion
            #region 绑定数据
            ShowDetail frmShowDetail = new ShowDetail(al);
            frmShowDetail.RoadSelect += new EventHandler<EventArgs>(frmShowDetail_RoadSelect);
            frmShowDetail.ShowDialog();
            //for (int i = 0; i < al.Count; i++)
            //{
            //    ArrayList alRoad = al[i] as ArrayList;
            //    string sRoad = "";
            //    for (int j = 0; j < alRoad.Count; j++)
            //    {
            //        //int[] iRodeInfo1 = new int[2];
            //        //int[] iRodeInfo2 = new int[2];
            //        //iRodeInfo1[0]=FNODE;
            //        //iRodeInfo1[1]=TNODE;
            //        //hashMapName.Add(iRodeInfo1, sName);
            //        //iRodeInfo2[0] = TNODE;
            //        //iRodeInfo2[1] = FNODE;
            //        //string sName=                    
            //        if (j == alRoad.Count - 1)
            //        {
            //            sRoad += alRoad[j].ToString();
            //            listBox1.Items.Add(sRoad);
            //            sRoad = "";
            //        }
            //        else
            //        {
            //            sRoad += alRoad[j].ToString() + "-";
            //        }
            //    }
            //}
            Cursor.Current = Cursors.Default;
            #endregion
            //TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - l1);
            //PublicDim.ShowErrorMessage(ts.TotalSeconds.ToString());
            //PublicDim.ShowErrorMessage(al.Count.ToString());
            //ArrayList UseRoad=;
        }

        void frmShowDetail_RoadSelect(object sender, EventArgs e)
        {
            Session.Current.Selections.DefaultSelection.Clear();
            String[] Slist = sender.ToString().Split('-');
            if(Slist.Length<2)
                return;
            //按线高亮
            for (int i = 0; i < Slist.Length - 1; i++)
            {
                SelectRoad(Slist[i], Slist[i + 1]);
            }

            ////按点高亮
            //for (int i = 0; i < Slist.Length;i++ )
            //{
            //    SelectNode(Slist[i]);
            //}

            
        }
        //功能：分析两个变电站之间的所有路径信息
        //参数：bdzStar-起点变电站对象，bdzEnd-终点变电站对象
        //参数：nLevel-遍历检索的层次深度
        //返回：路径经由的变电站对象列表
        private ArrayList SearchAllWay(int iStartNodeId, int iEndNodeID, int iLevel)
        {
            ArrayList ar = new ArrayList();
            try
            {
                int r = 0;
                float da;   //临时两个点之间的距离

                //首先获取除了起点变电站以外的所有变电站信息备用
                ////////string strSql = "select id from gis_road_node where id<>"+iStartNodeId.ToString()+" order by id";//by yafei排除边缘结点
                ////////DataTable dt = OracleHelper.GetDataTable(strSql);

                Table table = Session.Current.Catalog.GetTable("LineNode");
                ITableFeatureCollection fNodeCollection = table as MapInfo.Data.ITableFeatureCollection;

                //取得图元数量
                int iNodeCount = 0;
                foreach (Feature f in fNodeCollection)
                {
                    iNodeCount++;
                }

                if (iNodeCount <= 0)
                {
                    return ar;
                }

                r = iNodeCount + 1;

                int[] aBdzId = new int[r];   //存放所有变电站ID
                int[] aBdzName = new int[r]; //存放所有变电站名称

                aBdzId[0] = iStartNodeId;
                aBdzName[0] = iStartNodeId;

                //for (int i = 1;i < r; i++)
                //{
                //    Feature fNode=fNodeCollection[i - 1] as Feature;
                //    aBdzId[i] = Convert.ToInt32(fNode["ID"]);
                //    aBdzName[i] = Convert.ToInt32(fNode["ID"]);
                //}
                int i = 1;
                foreach (Feature fNode in fNodeCollection)
                {
                    aBdzId[i] = Convert.ToInt32(fNode["ID"]);
                    aBdzName[i] = Convert.ToInt32(fNode["ID"]);
                    i++;
                }

                ////找出终点索引号
                //int nIndex = 0;

                //for (int i = 1; i < r; i++)
                //{
                //    if (aBdzId[i] ==iEndNodeID)
                //    {
                //        nIndex = i;
                //        break;
                //    }
                //}

                MapPath mp = new MapPath(r, iLevel);

                ArrayList arPath = mp.GetAllWays(hashAllMap, iStartNodeId, iEndNodeID);
                foreach (ArrayList childPath in arPath)
                {
                    ArrayList aBdz = new ArrayList();
                    foreach (int num in childPath)
                    {
                        aBdz.Add(aBdzName[num]);
                    }
                    ar.Add(aBdz);   //将每一条路径中相关变电站信息全部追加到整个路径链表中
                }

                return ar;
            }
            catch(Exception ex)
            {
                PublicDim.ShowErrorMessage(ex.Message);
                return ar;
            }
        }

        #region 检测两个两点之间通路的数量，路径分析用
        Hashtable hsTemp = new Hashtable();
        private float getLength(int iStartNodeId, int iEndNodeId)
        {
            hsTemp = (Hashtable)hashAllMap[iStartNodeId];
            if (!hsTemp.ContainsKey(iEndNodeId))
            {
                return 0;
            }
            else
                return (float)hsTemp[iEndNodeId];//dLength;
        }
        #endregion

        /// <summary>
        /// 根据起点终点选择线段
        /// </summary>
        /// <param name="FromNodeID"></param>
        /// <param name="ToNodeID"></param>
        private void SelectRoad(int FromNodeID, int ToNodeID)
        {
            String sWhere = "(FNODE=" + FromNodeID + " And TNODE=" + ToNodeID + ") or (FNODE=" + ToNodeID + " And TNODE=" + FromNodeID + ")";
            Table tableRoad = Session.Current.Catalog.GetTable("RoadChina");
            SearchInfo siWhere = MapInfo.Data.SearchInfoFactory.SearchWhere(sWhere);
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tableRoad, siWhere);
            Session.Current.Selections.DefaultSelection.Add(ifs);
            System.Threading.Thread.Sleep(50);
        }

        /// <summary>
        /// 根据起点终点选择线段
        /// </summary>
        /// <param name="FromNodeID"></param>
        /// <param name="ToNodeID"></param>
        private void SelectRoad(string FromNodeID, string ToNodeID)
        {
            String sWhere = "(FNODE=" + FromNodeID + " And TNODE=" + ToNodeID + ") or (FNODE=" + ToNodeID + " And TNODE=" + FromNodeID + ")";
            Table tableRoad = Session.Current.Catalog.GetTable("RoadChina");
            SearchInfo siWhere = MapInfo.Data.SearchInfoFactory.SearchWhere(sWhere);
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tableRoad, siWhere);
            Session.Current.Selections.DefaultSelection.Add(ifs);
            System.Threading.Thread.Sleep(50);
        }

        /// <summary>
        /// 根据起点终点选择线段
        /// </summary>
        /// <param name="FromNodeID"></param>
        /// <param name="ToNodeID"></param>
        private void SelectNode(string NodeID)
        {
            String sWhere = "ID='" + NodeID+"'";
            Table tableNode = Session.Current.Catalog.GetTable("LineNode");
            SearchInfo siWhere = MapInfo.Data.SearchInfoFactory.SearchWhere(sWhere);
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tableNode, siWhere);
            Session.Current.Selections.DefaultSelection.Add(ifs);
            System.Threading.Thread.Sleep(50);
        }

        private void ShortWay_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.Start("http://flysoft.taobao.com/");
        }
	}
}
