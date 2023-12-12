using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            InitSet();
        }

        private void InitSet()
        {
            DataTable gridData = new DataTable();

            gridControl1.DataSource = gridData;
            CreateData(gridData);
            gridData.Columns.Add("RowSelect", typeof(bool));

            GridInit();
        }

        private void GridInit()
        {
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView.OptionsSelection.CheckBoxSelectorField = "RowSelect";
            gridView.Columns["RowSelect"].Visible = false;
            gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
        }


        private void CreateData(DataTable dataTable)
        {
            for (int i = 1; i <= 7; i++)
            {
                dataTable.Columns.Add($"col{i}", typeof(int));
            }



            Random rm = new Random();
            for (int i = 0; i <= 10; i++)
            {
                var row = dataTable.NewRow();
                for (int colIndex = 1; colIndex <= 7; colIndex++)
                {
                    row[$"col{colIndex}"] = rm.Next(20);
                }
                dataTable.Rows.Add(row);
            }

        }

        private void Form3_Shown(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
        }
    }
}
