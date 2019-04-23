using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapInfo.Data;
using MapInfo.Mapping;
using MapInfo.Styles;
using MapInfo.Geometry;

namespace Devgis.ShortWay
{
    public partial class GetNode : Form
    {
        Table tLine;
        CoordSys csc;
        Table tNode;
        public GetNode()
        {
            InitializeComponent();
            mapControl1.Map.ViewChangedEvent += new MapInfo.Mapping.ViewChangedEventHandler(Map_ViewChangedEvent);
            Map_ViewChangedEvent(this, null);
        }

        void Map_ViewChangedEvent(object sender, MapInfo.Mapping.ViewChangedEventArgs e)
        {
            // Display the zoom level
            Double dblZoom = System.Convert.ToDouble(String.Format("{0:E2}", mapControl1.Map.Zoom.Value));
            if (statusStrip1.Items.Count > 0)
            {
                statusStrip1.Items[0].Text = "缩放: " + dblZoom.ToString() + " " + MapInfo.Geometry.CoordSys.DistanceUnitAbbreviation(mapControl1.Map.Zoom.Unit);
            }
        }

        private void ShortWay_Load(object sender, EventArgs e)
        {
            #region 初始化图层；
            csc=this.mapControl1.Map.GetDisplayCoordSys();
            MIConnection mc = new MIConnection();
            mc.Open();
            //////////////////////////////////////////////tLine = MapHelper.openTable("gis_road", mc);
            //////////////////////////////////////////////tNode = MapHelper.openTable("gis_road_node", mc);
            LabelLayer lbLineLayer = new LabelLayer("道路标签层");
            LabelSource source = new LabelSource(tLine);
            source.DefaultLabelProperties.Caption = "Name";
            source.DefaultLabelProperties.Style.Font.Name = "宋体";
            source.DefaultLabelProperties.Style.Font.Size = 8;
            source.DefaultLabelProperties.Visibility.AllowOverlap = false;
            source.DefaultLabelProperties.Visibility.AllowDuplicates = false;
            source.DefaultLabelProperties.Visibility.AllowOutOfView = false;
            source.DefaultLabelProperties.Layout.Offset = 6;
            source.DefaultLabelProperties.Style.Font.TextEffect = TextEffect.Halo;
            source.DefaultLabelProperties.CalloutLine.Use = false;
            lbLineLayer.Sources.Append(source);
            FeatureLayer flLine = new FeatureLayer(tLine);
            //FeatureLayer flNode = new FeatureLayer(tNode);
            mapControl1.Map.Layers.Add(flLine);
            //mapControl1.Map.Layers.Add(flNode);
            mapControl1.Map.Layers.Add(lbLineLayer);
            #endregion
        }

        private void btUpdateNode_Click(object sender, EventArgs e)
        {
            btUpdateNode.Enabled = false;
            #region 清除道路点;
            //////////////////////////////////////////////String sSQL = "delete from gis_road_node";
            //////////////////////////////////////////////OracleHelper.ExecSql(sSQL);
            #endregion
            #region 定义临时表
            DataTable dtTemp = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "ID";
            dc.DataType = Int32.MaxValue.GetType();
            dtTemp.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "X";
            dc.DataType = Double.MaxValue.GetType();
            dtTemp.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Y";
            dc.DataType = Double.MaxValue.GetType();
            dtTemp.Columns.Add(dc);


            DataTable dtUpdateLine = new DataTable();
            dc = new DataColumn();
            dc.ColumnName = "KEY";
            dc.DataType = Int32.MaxValue.GetType();
            dtUpdateLine.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "FNODE";
            dc.DataType = Int32.MaxValue.GetType();
            dtUpdateLine.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "TNODE";
            dc.DataType = Int32.MaxValue.GetType();
            dtUpdateLine.Columns.Add(dc);
            #endregion
            #region 初始道路点;
            IFeatureEnumerator iFEnum = (tLine as IFeatureCollection).GetFeatureEnumerator();
            SimpleVectorPointStyle cPointStyle = new MapInfo.Styles.SimpleVectorPointStyle(34, Color.DarkGreen, 5);
            int iRowNo=1;
            DataRow dr;

            while (iFEnum.MoveNext())
            {
                Feature currFeature = iFEnum.Current;
                currFeature.Edited = true;
                MultiCurve mCurve = currFeature.Geometry as MultiCurve;
                int iCount = mCurve.CurveCount;
                Feature ftPoint1=new Feature(tNode.TableInfo.Columns);
                Feature ftPoint2=new Feature(tNode.TableInfo.Columns);
                MapInfo.Geometry.Point ptStart = new MapInfo.Geometry.Point(csc, mCurve[0].StartPoint);
                DataRow drUpdateLine = dtUpdateLine.NewRow();
                drUpdateLine["KEY"] = currFeature["KEY"];
                int iCheckResult=checkValue(ptStart.X, ptStart.Y, dtTemp);
                if (iCheckResult == 0)
                {
                    dr = dtTemp.NewRow();
                    dr["ID"] = iRowNo;
                    dr["X"] = ptStart.X;
                    dr["Y"] = ptStart.Y;
                    dtTemp.Rows.Add(dr);
                    drUpdateLine["FNODE"] = iRowNo;
                    iRowNo++;
                }
                else
                {
                    drUpdateLine["FNODE"] = iCheckResult;
                }
                MapInfo.Geometry.Point ptEnd = new MapInfo.Geometry.Point(csc, mCurve[iCount - 1].EndPoint);
                iCheckResult = checkValue(ptEnd.X, ptEnd.Y, dtTemp);
                if (iCheckResult == 0)
                {
                    dr = dtTemp.NewRow();
                    dr["ID"] = iRowNo;
                    dr["X"] = ptEnd.X;
                    dr["Y"] = ptEnd.Y;
                    dtTemp.Rows.Add(dr);
                    drUpdateLine["TNODE"] = iRowNo;
                    iRowNo++;
                }
                else
                {
                    drUpdateLine["TNODE"] = iCheckResult;
                }
                dtUpdateLine.Rows.Add(drUpdateLine);

            }
            //////////////////////////////////////////////////////////////////////////////////////////System.Data.OracleClient.OracleConnection oConnection = OracleHelper.OracleCon();
            //////////////////////////////////////////////////////////////////////////////////////////if (oConnection.State != ConnectionState.Open)
            //////////////////////////////////////////////////////////////////////////////////////////{
            //////////////////////////////////////////////////////////////////////////////////////////    oConnection.Open();
            //////////////////////////////////////////////////////////////////////////////////////////}
            ////////////////////////////////////////////////////////////////////////////////////////String sSql = "delete from gis_road_node";
            ////////////////////////////////////////////////////////////////////////////////////////OracleHelper.ExecSql(sSql, oConnection);
            ////////////////////////////////////////////////////////////////////////////////////////foreach (DataRow drPoint in dtTemp.Rows)
            ////////////////////////////////////////////////////////////////////////////////////////{
            ////////////////////////////////////////////////////////////////////////////////////////    sSql = string.Format("insert into gis_road_node (id,x,y) values({0},{1},{02})", drPoint[0].ToString(), drPoint[1].ToString(), drPoint[2].ToString());
            ////////////////////////////////////////////////////////////////////////////////////////    OracleHelper.ExecSql(sSql, oConnection);
            ////////////////////////////////////////////////////////////////////////////////////////}

            ////////////////////////////////////////////////////////////////////////////////////////foreach (DataRow drUpLine in dtUpdateLine.Rows)
            ////////////////////////////////////////////////////////////////////////////////////////{
            ////////////////////////////////////////////////////////////////////////////////////////    sSql = string.Format("update gis_road set fnode={0},tnode={1} where key={2}", drUpLine[1].ToString(), drUpLine[2].ToString(), drUpLine[0].ToString());
            ////////////////////////////////////////////////////////////////////////////////////////    OracleHelper.ExecSql(sSql, oConnection);
            ////////////////////////////////////////////////////////////////////////////////////////}
            MessageBox.Show("更新成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            #endregion
            #region 更新道路长度
            ////////////////////////////////////////////////////////////////////////////////////////sSql = "update gis_road set length=(sdo_lrs.geom_segment_length(geoloc)/1000)";
            ////////////////////////////////////////////////////////////////////////////////////////OracleHelper.ExecSql(sSql, oConnection);
            #endregion
            #region 关闭连接
            ////////////////////////////////////////////////////////////////////////////////////////if (oConnection.State == ConnectionState.Open)
            ////////////////////////////////////////////////////////////////////////////////////////{
                ////////////////////////////////////////////////////////////////////////////////////////    oConnection.Close();
            ////////////////////////////////////////////////////////////////////////////////////////}
            #endregion
            btUpdateNode.Enabled = true;
        }
        private int checkValue(double x, double y, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if ((Convert.ToDouble(dr[1]) - x) * (Convert.ToDouble(dr[1]) - x) + (Convert.ToDouble(dr[2]) - y) * (Convert.ToDouble(dr[2]) - y) <= 0.00000000004)
                {
                    return Convert.ToInt32(dr[0]);
                }
            }
            return 0;
        }
    }
}