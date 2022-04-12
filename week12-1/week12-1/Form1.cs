using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week12_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initGrid();
            populateGrid();
        }

        private void initGrid()
        {
            grid.Columns.Clear();

            DataGridViewColumn column;

            column = new DataGridViewTextBoxColumn();
            column.Name = "grade4";
            column.HeaderText = "4 based grade";
            column.Width = 100;
            grid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "grade100";
            column.HeaderText = "100 based grade";
            column.Width = 120;
            grid.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.Name = "select";
            column.HeaderText = "select";
            column.Width = 80;
            grid.Columns.Add(column);

            column = new DataGridViewButtonColumn();
            column.Name = "display";
            column.HeaderText = "display";
            column.Width = 80;
            grid.Columns.Add(column);
        }

        private void populateGrid()
        {
            grid.Rows.Clear();

            object[] rowItem;

            rowItem = new object[] { "4,00", "100,00", true, "Display"};
            grid.Rows.Add(rowItem);

            rowItem = new object[] { "3,50", "85,00", false, "Display" };
            grid.Rows.Add(rowItem);

            rowItem = new object[] { "1,80", "68,00", true, "Display" };
            grid.Rows.Add(rowItem);
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];

            try
            {
                if (cell.Value == null)
                {
                    throw new ArgumentNullException("Value cannot be null!");
                }

                if(cell.OwningColumn.Name == "grade4" || cell.OwningColumn.Name == "grade100")
                {
                    double temp;
                    if (double.TryParse(cell.Value.ToString(), out temp))
                    {
                        if (cell.OwningColumn.Name == "grade4" && temp >= 0 && temp <= 4)
                        {
                            // database operation can be in there
                            label1.Text = $"grid[{e.RowIndex}][{e.ColumnIndex}] -> {temp}";
                        }
                        else if (cell.OwningColumn.Name == "grade100" && temp >= 0 && temp <= 100)
                        {
                            // database operation can be in there
                            label1.Text = $"grid[{e.RowIndex}][{e.ColumnIndex}] -> {temp}";
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("Value is out of range");
                        }

                    }
                    else
                    {
                        throw new InvalidCastException("Value is not a valid double");
                    }
                }                
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void grid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var cell = grid.Rows[e.RowIndex].Cells["grade4"];
            var row = grid.Rows[e.RowIndex];

            if (cell.Value == null)
                return;

            double temp;
            if (double.TryParse(cell.Value.ToString(), out temp))
            {
                if(temp >= 2)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var column = grid.Columns[e.ColumnIndex];
            var cell = grid.Rows[e.RowIndex].Cells["grade4"];

            if (cell.Value == null)
                return;

            if (column is DataGridViewButtonColumn && column.Name == "display")
            {
                double temp = double.Parse(cell.Value.ToString());
                
                    string msg = string.Format("You selected the grid[{0}][{1}]: {2:#0.00}", e.RowIndex, e.ColumnIndex, temp);
                MessageBox.Show(msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double max = 0;

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["grade4"].Value == null)
                    continue;

                double temp = double.Parse(row.Cells["grade4"].Value.ToString());
                if (temp > max)
                    max = temp;
            }

            MessageBox.Show(string.Format("Max value of the grades 4 is : {0:0.00}", max));
        }
    }
}
