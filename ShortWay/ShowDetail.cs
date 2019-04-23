using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Devgis.ShortWay
{
    public partial class ShowDetail : Form
    {
        public String SelectRoadInfo;
        public ArrayList AllRoad;
        public event EventHandler<EventArgs> RoadSelect;
        public ShowDetail(ArrayList AllRoad)
        {
            InitializeComponent();
            this.AllRoad = AllRoad;
        }

        private void ShowDetail_Load(object sender, EventArgs e)
        {
            if (AllRoad == null || AllRoad.Count <= 0)
                return;
            for (int i = 0; i < AllRoad.Count; i++)
            {
                ArrayList alRoad = AllRoad[i] as ArrayList;
                string sRoad = "";
                for (int j = 0; j < alRoad.Count; j++)
                {                 
                    if (j == alRoad.Count - 1)
                    {
                        sRoad += alRoad[j].ToString();
                        listBox1.Items.Add(sRoad);
                        sRoad = "";
                    }
                    else
                    {
                        sRoad += alRoad[j].ToString() + "-";
                    }
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ;
            if (this.RoadSelect != null)
            {
                this.RoadSelect(listBox1.SelectedItem, e);
            }
        }
    }
}