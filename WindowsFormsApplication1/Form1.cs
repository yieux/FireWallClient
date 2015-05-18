using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace WindowsFormsApplication1
{
    public partial class Form1 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            initData();
        }

        private void initData()
        {
            DataSet ds = new DataSet("123");
            DataTable dt = new DataTable();
            dt.Columns.Add("0");
            dt.Columns.Add("1");
            dt.Columns.Add("2");
            DataRow dr = dt.NewRow();
            dr[0] = "000";
            dr[1] = "111";
            dr[2] = "222";
            dt.Rows.Add(dr);
             dr = dt.NewRow();
            dr[0] = "_000";
            dr[1] = "_111";
            dr[2] = "_222";
            dt.Rows.Add(dr);

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("q");
            dt1.Columns.Add("w");
            dt1.Columns.Add("e");
            dr = dt1.NewRow();
            dr[0] = "000";
            dr[1] = "s";
            dr[2] = "d";
            dt1.Rows.Add(dr);
            dr = dt1.NewRow();
            dr[0] = "_000";
            dr[1] = "_s";
            dr[2] = "_d";
            dt1.Rows.Add(dr);
            dr = dt1.NewRow();
            dr[0] = "_000";
            dr[1] = "s";
            dr[2] = "d";
            dt1.Rows.Add(dr);
            dr = dt1.NewRow();
            dr[0] = "000";
            dr[1] = "_s";
            dr[2] = "_d";
            dt1.Rows.Add(dr);
            
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Relations.Add("relation1",ds.Tables[0].Columns["0"],ds.Tables[1].Columns[0],true);
            superGridControl1.PrimaryGrid.DataSource = ds;
            gridColumn1.EditorType = typeof(MyGridButtonXEditControl);
            发送.EditorType = typeof(MyGridButtonXEditControl); 
            GridPanel gp = this.superGridControl1.PrimaryGrid;
            GridRow gr;
            for (int i = 0; i < gp.Rows.Count; i++)
            {
                 gr= gp.Rows[i] as GridRow;
                gr.Cells[1].Value = "haha";
                

            }
             //gr = gp.Rows[0] as GridRow;
           // gr.Cells[1].Value = "haha";
            
            
        }

        private void superGridControl1_CellClick(object sender, GridCellClickEventArgs e)
        {
            MessageBox.Show("1");
            if (e.GridCell.GridColumn.Name == "gridColumn2")
                MessageBox.Show("fasong");
        }

        private void superGridControl1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }


        #region MyGridButtonXEditControl

        /// <summary>
        /// GridButtonXEditControl Class that controls the
        /// ButtonX color initialization and user button clicks.
        /// </summary>
        private class MyGridButtonXEditControl : GridButtonXEditControl
        {
            /// <summary>
            /// Constructor
            /// </summary>
            public MyGridButtonXEditControl()
            {
                // We want to be notified when the user clicks the button
                // so that we can change the underlying cell value to reflect
                // the mouse click.

                Click += MyGridButtonXEditControlClick;
            }

            #region InitializeContext

            /// <summary>
            /// Initializes the color table for the button
            /// </summary>
            /// <param name="cell"></param>
            /// <param name="style"></param>
            public override void
                InitializeContext(GridCell cell, CellVisualStyle style)
            {
                base.InitializeContext(cell, style);

                //bool running = Text.Equals("Stop") == false;

                //ColorTable = (running == true)
                //    ? eButtonColor.OrangeWithBackground
                //    : eButtonColor.BlueOrb;
            }

            #endregion

            #region MyGridButtonXEditControlClick

            /// <summary>
            /// Handles user clicks of the button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void MyGridButtonXEditControlClick(object sender, EventArgs e)
            {
                //bool running = (EditorCell.Value != null &&
                //    EditorCell.Value.Equals("Start"));
                if (EditorCell.GridColumn.Name == "gridColumn1")
                EditorCell.Value = "已清除";
                else
                    EditorCell.Value = "已发送";
                //EditorCell.Value = (running == true) ? "Stop" : "Start";
                //MessageBox.Show("chengong");
                
            }

            #endregion
        }

        #endregion



        private void superGridControl1_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            GridCell cell = e.GridCell;

            // If the cell changing value is in the "Power State" column
            // then adjust the row "Start/Stop" cell appropriately

            if (cell.GridColumn.Name.Equals("gridColumn1") == true)
                MessageBox.Show(cell.RowIndex.ToString());
        }
    }
}
