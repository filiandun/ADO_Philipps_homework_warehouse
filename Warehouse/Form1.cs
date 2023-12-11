using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Warehouse
{
    public partial class Form1 : Form
    {
        Table table;

        public Form1()
        {
            InitializeComponent();

            this.table = new Table(ref this.dataGridView);
            this.table.OpenConnection();

            this.table.Update();
        }

        private void updateTableButton_Click(object sender, EventArgs e)
        {           
            this.table.Update();

            this.dataGridView.Update(); // не отображаются изменения даже с этими строками, приходится перезапускать программу
            this.dataGridView.Refresh();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.dataGridView.CurrentCell.ToString());
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.updateTableButton_Click(sender, e);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            this.updateTableButton_Click(sender, e);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView.CurrentRow.Index != -1)
            {
                this.table.Delete((int) this.dataGridView.Rows[this.dataGridView.CurrentCell.RowIndex].Cells["id"].Value);

                this.updateTableButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Вы не выбрали строку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}
