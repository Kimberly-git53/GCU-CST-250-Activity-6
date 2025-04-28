namespace FrmInventory
{
    partial class FrmInventory
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupEntryForm = new GroupBox();
            btnAdd = new Button();
            txtValue = new TextBox();
            txtName = new TextBox();
            lblValue = new Label();
            lblName = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            groupFilterAndSort = new GroupBox();
            rbtnMostValuable = new RadioButton();
            rbtnLeastValuable = new RadioButton();
            rbtnShowAll = new RadioButton();
            gridInventory = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            lblStats = new Label();
            rangeMinMax = new FileIO.RangeTrackBar();
            groupEntryForm.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupFilterAndSort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridInventory).BeginInit();
            SuspendLayout();
            // 
            // groupEntryForm
            // 
            groupEntryForm.Controls.Add(btnAdd);
            groupEntryForm.Controls.Add(txtValue);
            groupEntryForm.Controls.Add(txtName);
            groupEntryForm.Controls.Add(lblValue);
            groupEntryForm.Controls.Add(lblName);
            groupEntryForm.Location = new Point(30, 45);
            groupEntryForm.Name = "groupEntryForm";
            groupEntryForm.Size = new Size(335, 187);
            groupEntryForm.TabIndex = 0;
            groupEntryForm.TabStop = false;
            groupEntryForm.Text = "Entry Form";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(108, 138);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtValue
            // 
            txtValue.Location = new Point(83, 87);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(234, 27);
            txtValue.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Location = new Point(82, 35);
            txtName.Name = "txtName";
            txtName.Size = new Size(235, 27);
            txtName.TabIndex = 2;
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.Location = new Point(25, 90);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(48, 20);
            lblValue.TabIndex = 1;
            lblValue.Text = "Value:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(19, 38);
            lblName.Name = "lblName";
            lblName.Size = new Size(56, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name: ";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1197, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(224, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(224, 26);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // groupFilterAndSort
            // 
            groupFilterAndSort.Controls.Add(rbtnMostValuable);
            groupFilterAndSort.Controls.Add(rbtnLeastValuable);
            groupFilterAndSort.Controls.Add(rbtnShowAll);
            groupFilterAndSort.Location = new Point(41, 262);
            groupFilterAndSort.Name = "groupFilterAndSort";
            groupFilterAndSort.Size = new Size(205, 132);
            groupFilterAndSort.TabIndex = 2;
            groupFilterAndSort.TabStop = false;
            groupFilterAndSort.Text = "Filter and Sort";
            // 
            // rbtnMostValuable
            // 
            rbtnMostValuable.AutoSize = true;
            rbtnMostValuable.Location = new Point(22, 93);
            rbtnMostValuable.Name = "rbtnMostValuable";
            rbtnMostValuable.Size = new Size(136, 24);
            rbtnMostValuable.TabIndex = 2;
            rbtnMostValuable.TabStop = true;
            rbtnMostValuable.Text = "3 Most Valuable";
            rbtnMostValuable.UseVisualStyleBackColor = true;
            rbtnMostValuable.CheckedChanged += rbtnMostValuable_CheckedChanged;
            // 
            // rbtnLeastValuable
            // 
            rbtnLeastValuable.AutoSize = true;
            rbtnLeastValuable.Location = new Point(21, 63);
            rbtnLeastValuable.Name = "rbtnLeastValuable";
            rbtnLeastValuable.Size = new Size(137, 24);
            rbtnLeastValuable.TabIndex = 1;
            rbtnLeastValuable.TabStop = true;
            rbtnLeastValuable.Text = "3 Least Valuable";
            rbtnLeastValuable.UseVisualStyleBackColor = true;
            rbtnLeastValuable.CheckedChanged += rbtnLeastValuable_CheckedChanged;
            // 
            // rbtnShowAll
            // 
            rbtnShowAll.AutoSize = true;
            rbtnShowAll.Location = new Point(22, 29);
            rbtnShowAll.Name = "rbtnShowAll";
            rbtnShowAll.Size = new Size(88, 24);
            rbtnShowAll.TabIndex = 0;
            rbtnShowAll.TabStop = true;
            rbtnShowAll.Text = "Show All";
            rbtnShowAll.UseVisualStyleBackColor = true;
            rbtnShowAll.CheckedChanged += rbtnShowAll_CheckedChanged;
            // 
            // gridInventory
            // 
            gridInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridInventory.Location = new Point(421, 57);
            gridInventory.Name = "gridInventory";
            gridInventory.RowHeadersWidth = 51;
            gridInventory.Size = new Size(734, 342);
            gridInventory.TabIndex = 3;
            // 
            // txtSearch
            // 
            txtSearch.ForeColor = SystemColors.WindowFrame;
            txtSearch.Location = new Point(493, 418);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(339, 27);
            txtSearch.TabIndex = 4;
            txtSearch.Text = "Search";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(859, 416);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblStats
            // 
            lblStats.AutoSize = true;
            lblStats.Location = new Point(981, 452);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(44, 20);
            lblStats.TabIndex = 6;
            lblStats.Text = "Stats:";
            // 
            // rangeMinMax
            // 
            rangeMinMax.LabelColor = Color.Gray;
            rangeMinMax.Location = new Point(30, 429);
            rangeMinMax.LowerColor = Color.DarkRed;
            rangeMinMax.LowerValue = 10;
            rangeMinMax.MaxValue = 100;
            rangeMinMax.MinimumSize = new Size(100, 50);
            rangeMinMax.MinValue = 0;
            rangeMinMax.Name = "rangeMinMax";
            rangeMinMax.Size = new Size(457, 62);
            rangeMinMax.TabIndex = 7;
            rangeMinMax.Text = "rangeTrackBar2";
            rangeMinMax.TickColor = Color.LightGray;
            rangeMinMax.TickFrequency = 10;
            rangeMinMax.TrackColor = Color.LightGray;
            rangeMinMax.UpperColor = Color.Teal;
            rangeMinMax.UpperValue = 90;
            rangeMinMax.Click += rangeMinMax_Click;
            // 
            // FrmInventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1197, 499);
            Controls.Add(rangeMinMax);
            Controls.Add(lblStats);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(gridInventory);
            Controls.Add(groupFilterAndSort);
            Controls.Add(groupEntryForm);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmInventory";
            Text = "Inventory";
            Load += FrmInventory_Load;
            groupEntryForm.ResumeLayout(false);
            groupEntryForm.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupFilterAndSort.ResumeLayout(false);
            groupFilterAndSort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridInventory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupEntryForm;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button btnAdd;
        private TextBox txtValue;
        private TextBox txtName;
        private Label lblValue;
        private Label lblName;
        private GroupBox groupFilterAndSort;
        private RadioButton rbtnMostValuable;
        private RadioButton rbtnLeastValuable;
        private RadioButton rbtnShowAll;
        private DataGridView gridInventory;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label lblStats;
        private FileIO.RangeTrackBar rangeTrackBar1;
        private FileIO.RangeTrackBar rangeMinMax;
    }
}
