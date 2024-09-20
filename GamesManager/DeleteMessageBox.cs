using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesManager
{
    public partial class DeleteMessageBox : Form
    {
        public Queue<string> DeletionQueue;
        public DeleteMessageBoxResult Result;

        private Button deleteAllButton;
        private Button reviewButton;
        private Button cancelButton;
        private TableLayoutPanel tableLayoutPanel;

        public DeleteMessageBox(Queue<string> deletionQueue)
        {
            InitializeComponent();
            this.DeletionQueue = deletionQueue;
            AddButtons();
            SetText();
        }

        private void AddButtons()
        {
            // Initialize buttons
            deleteAllButton = new Button { Text = "Delete all" };
            deleteAllButton.Click += DeleteAllButton_Click;
            reviewButton = new Button { Text = "Review and confirm", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly };
            reviewButton.Click += ReviewButton_Click;
            cancelButton = new Button { Text = "Cancel" };
            cancelButton.Click += CancelButton_Click;

            // Initialize TableLayoutPanel
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom,
                RowCount = 1,
                ColumnCount = 3,
                AutoSize = true
            };

            // Add buttons to TableLayoutPanel
            tableLayoutPanel.Controls.Add(deleteAllButton, 0, 0);
            tableLayoutPanel.Controls.Add(reviewButton, 1, 0);
            tableLayoutPanel.Controls.Add(cancelButton, 2, 0);

            // Add TableLayoutPanel to the form
            this.Controls.Add(tableLayoutPanel);
        }

        private void SetText()
        {
            string text = "The following shortcuts are about to be deleted, since their targets no longer exist:\n";
            foreach (string file in DeletionQueue)
            {
                text += $"- {file}\n";
            }
            text += "Do you want to continue?";
            this.label1.Text = text;
        }

        private void DeleteAllButton_Click(object sender, EventArgs e)
        {
            do
            {
                string path = DeletionQueue.Dequeue();
                File.Delete(path);
            }
            while (DeletionQueue.Count > 0);
            Result = DeleteMessageBoxResult.DeleteAll;
            this.Close();
        }

        private void ReviewButton_Click(object sender, EventArgs e)
        {
            do
            {
                string path = DeletionQueue.Dequeue();
                DialogResult result = MessageBox.Show($"Do you want to delete the following file?\n- {path}", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    File.Delete(path);
                }
            }
            while (DeletionQueue.Count > 0);
            Result = DeleteMessageBoxResult.Review;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Result = DeleteMessageBoxResult.Cancel;
            this.Close();
        }

        public static DeleteMessageBoxResult Show(Queue<string> deletionQueue)
        {
            DeleteMessageBox box = new DeleteMessageBox(deletionQueue);
            box.ShowDialog();
            return box.Result;
        }

        public enum DeleteMessageBoxResult
        {
            DeleteAll,
            Review,
            Cancel
        }
    }
}
