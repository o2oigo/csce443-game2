namespace LevelCreator
{
    partial class MapEditor
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEditor));
            this.tree_toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tree_ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dirt_ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.grass_ToolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.start_ToolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.end_ToolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.barrier_ToolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.paintTile_toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.fillTiles_toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadBackground_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.save_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tileSize_ComboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tree_toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tree_toolStrip1
            // 
            this.tree_toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tree_ToolStripButton1,
            this.dirt_ToolStripButton2,
            this.grass_ToolStripButton3,
            this.start_ToolStripButton4,
            this.end_ToolStripButton5,
            this.barrier_ToolStripButton6,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.paintTile_toolStripButton8,
            this.fillTiles_toolStripButton7});
            this.tree_toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.tree_toolStrip1.Name = "tree_toolStrip1";
            this.tree_toolStrip1.Size = new System.Drawing.Size(824, 25);
            this.tree_toolStrip1.TabIndex = 1;
            this.tree_toolStrip1.Text = "toolStrip1";
            this.tree_toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel1.Text = "Tile Type:";
            // 
            // tree_ToolStripButton1
            // 
            this.tree_ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tree_ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("tree_ToolStripButton1.Image")));
            this.tree_ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tree_ToolStripButton1.Name = "tree_ToolStripButton1";
            this.tree_ToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.tree_ToolStripButton1.Text = "toolStripButton1";
            this.tree_ToolStripButton1.ToolTipText = "Tree tile";
            this.tree_ToolStripButton1.Click += new System.EventHandler(this.tree_ToolStripButton1_Click);
            // 
            // dirt_ToolStripButton2
            // 
            this.dirt_ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dirt_ToolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("dirt_ToolStripButton2.Image")));
            this.dirt_ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dirt_ToolStripButton2.Name = "dirt_ToolStripButton2";
            this.dirt_ToolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.dirt_ToolStripButton2.Text = "toolStripButton2";
            this.dirt_ToolStripButton2.ToolTipText = "Path tile";
            this.dirt_ToolStripButton2.Click += new System.EventHandler(this.dirt_ToolStripButton2_Click);
            // 
            // grass_ToolStripButton3
            // 
            this.grass_ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.grass_ToolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("grass_ToolStripButton3.Image")));
            this.grass_ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.grass_ToolStripButton3.Name = "grass_ToolStripButton3";
            this.grass_ToolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.grass_ToolStripButton3.Text = "toolStripButton3";
            this.grass_ToolStripButton3.ToolTipText = "Grass tile (buildable tile)";
            this.grass_ToolStripButton3.Click += new System.EventHandler(this.grass_ToolStripButton3_Click);
            // 
            // start_ToolStripButton4
            // 
            this.start_ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.start_ToolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("start_ToolStripButton4.Image")));
            this.start_ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.start_ToolStripButton4.Name = "start_ToolStripButton4";
            this.start_ToolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.start_ToolStripButton4.Text = "toolStripButton4";
            this.start_ToolStripButton4.ToolTipText = "Start tile";
            this.start_ToolStripButton4.Click += new System.EventHandler(this.start_ToolStripButton4_Click);
            // 
            // end_ToolStripButton5
            // 
            this.end_ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.end_ToolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("end_ToolStripButton5.Image")));
            this.end_ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.end_ToolStripButton5.Name = "end_ToolStripButton5";
            this.end_ToolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.end_ToolStripButton5.Text = "toolStripButton5";
            this.end_ToolStripButton5.ToolTipText = "End tile";
            this.end_ToolStripButton5.Click += new System.EventHandler(this.end_ToolStripButton5_Click);
            // 
            // barrier_ToolStripButton6
            // 
            this.barrier_ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.barrier_ToolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("barrier_ToolStripButton6.Image")));
            this.barrier_ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barrier_ToolStripButton6.Name = "barrier_ToolStripButton6";
            this.barrier_ToolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.barrier_ToolStripButton6.Text = "toolStripButton6";
            this.barrier_ToolStripButton6.ToolTipText = "Barrier tile";
            this.barrier_ToolStripButton6.Click += new System.EventHandler(this.barrier_ToolStripButton6_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabel2.Text = "Tool Type:";
            // 
            // paintTile_toolStripButton8
            // 
            this.paintTile_toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.paintTile_toolStripButton8.Image = global::LevelCreator.Properties.Resources.PaintTileIcon;
            this.paintTile_toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paintTile_toolStripButton8.Name = "paintTile_toolStripButton8";
            this.paintTile_toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.paintTile_toolStripButton8.Text = "toolStripButton8";
            this.paintTile_toolStripButton8.Click += new System.EventHandler(this.paintTile_toolStripButton8_Click);
            // 
            // fillTiles_toolStripButton7
            // 
            this.fillTiles_toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fillTiles_toolStripButton7.Image = global::LevelCreator.Properties.Resources.FillBucket;
            this.fillTiles_toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillTiles_toolStripButton7.Name = "fillTiles_toolStripButton7";
            this.fillTiles_toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.fillTiles_toolStripButton7.Text = "toolStripButton7";
            this.fillTiles_toolStripButton7.ToolTipText = "Fill tool";
            this.fillTiles_toolStripButton7.Click += new System.EventHandler(this.fillTiles_toolStripButton7_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(824, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.LoadBackground_ToolStripMenuItem,
            this.save_ToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // LoadBackground_ToolStripMenuItem
            // 
            this.LoadBackground_ToolStripMenuItem.Name = "LoadBackground_ToolStripMenuItem";
            this.LoadBackground_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LoadBackground_ToolStripMenuItem.Text = "Load Level...";
            this.LoadBackground_ToolStripMenuItem.Click += new System.EventHandler(this.LoadLevel_ToolStripMenuItem_Click);
            // 
            // save_ToolStripMenuItem
            // 
            this.save_ToolStripMenuItem.Name = "save_ToolStripMenuItem";
            this.save_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.save_ToolStripMenuItem.Text = "Save";
            this.save_ToolStripMenuItem.Click += new System.EventHandler(this.save_ToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tile Size:";
            // 
            // tileSize_ComboBox1
            // 
            this.tileSize_ComboBox1.FormattingEnabled = true;
            this.tileSize_ComboBox1.Location = new System.Drawing.Point(407, 27);
            this.tileSize_ComboBox1.Name = "tileSize_ComboBox1";
            this.tileSize_ComboBox1.Size = new System.Drawing.Size(43, 21);
            this.tileSize_ComboBox1.TabIndex = 7;
            this.tileSize_ComboBox1.SelectedIndexChanged += new System.EventHandler(this.tileSize_ComboBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 706);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(824, 770);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tileSize_ComboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tree_toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapEditor";
            this.Text = "Map Editor";
            this.Resize += new System.EventHandler(this.MapEditor_Resize);
            this.tree_toolStrip1.ResumeLayout(false);
            this.tree_toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tile[,] MapOfTiles;

        private System.Windows.Forms.ToolStrip tree_toolStrip1;
        private System.Windows.Forms.ToolStripButton tree_ToolStripButton1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton dirt_ToolStripButton2;
        private System.Windows.Forms.ToolStripButton grass_ToolStripButton3;
        private System.Windows.Forms.ToolStripButton start_ToolStripButton4;
        private System.Windows.Forms.ToolStripButton end_ToolStripButton5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tileSize_ComboBox1;
        private System.Windows.Forms.ToolStripButton barrier_ToolStripButton6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadBackground_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem save_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton fillTiles_toolStripButton7;
        private System.Windows.Forms.ToolStripButton paintTile_toolStripButton8;
    }
}

