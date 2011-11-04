using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;




namespace LevelCreator
{
    public partial class MapEditor : Form
    {
        public enum SelectedTile { TreeTile, DirtTile, GrassTile, StartTile, EndTile };
        private bool bMouseIsDown;
        private bool bPaintTiles;

        SelectedTile drawTile;

        Bitmap UncroppedBackgroundImage;
        private int nButtonSideLength;
        private int nTileSize;
        private int resolutionY;
        private int resolutionX;
        private int HeightInTiles;
        private int WidthInTiles;

        // Directory information
        private string sDirectoryName;
        private string sFilenameNoExt;
        private string sPathToLevelFiles;

        // Current XnaContent
        XnaContent currentContent;


        public MapEditor()
        {
            InitializeComponent();
            progressBar1.Visible = false;

            bPaintTiles = false;
            bMouseIsDown = false;
            UncroppedBackgroundImage = new Bitmap("H:\\ms_windows\\cpsc_redirected\\My Documents\\Visual Studio 2010\\Projects\\CSCE 443 - Project 2\\LevelCreator2\\LevelCreator\\exampleLevel.bmp", true);
            resolutionY = UncroppedBackgroundImage.Height;
            resolutionX = UncroppedBackgroundImage.Width;
            InitializeTileSizes();
            InitializeEditorTileSize();
        }

        private void InitializeEditorTileSize()
        {
            // Set Editor's tile size equal to Game's tile size initially
            nButtonSideLength = Convert.ToInt32(comboBox1.SelectedItem);
            toolStripMenuItem2.Checked = true;
        }

        private void InitializeTileSizes()
        {
            comboBox1.Items.Add("16");
            comboBox1.Items.Add("32");
            comboBox1.Items.Add("64");

            // Sets default
            comboBox1.SelectedIndex = 1;    // Sets default tile size to 32x32 pixels
        }


        private void LoadMap()
        {   
            MapOfTiles = new Tile[HeightInTiles, WidthInTiles];

            progressBar1.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = HeightInTiles * WidthInTiles;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            panel1.SuspendLayout();
            for (int i = 0; i < HeightInTiles; i++)
            {
                for (int j = 0; j < WidthInTiles; j++)
                {
                    //WorkerThread oWorkerThreads = new WorkerThread(i,j);
                    //Thread oThread = new Thread(new ThreadStart(oWorkerThreads.Thread_GenerateLineOfTiles));

                    MapOfTiles[i, j] = new Tile(i, j, SelectedTile.GrassTile);
                    MapOfTiles[i, j].BackColor = System.Drawing.SystemColors.Control;
                    MapOfTiles[i, j].FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    MapOfTiles[i, j].FlatAppearance.BorderSize = 0;
                    MapOfTiles[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    MapOfTiles[i, j].Location = new System.Drawing.Point((j * nButtonSideLength), (i * nButtonSideLength));
                    MapOfTiles[i, j].Name = "button" + i + j;
                    MapOfTiles[i, j].Size = new System.Drawing.Size(nButtonSideLength, nButtonSideLength);
                    MapOfTiles[i, j].UseVisualStyleBackColor = false;
                    MapOfTiles[i, j].Click += new System.EventHandler(this.tile_button_Click);
                    MapOfTiles[i, j].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    MapOfTiles[i, j].ForeColor = Color.DarkGray;

                    Rectangle rect = new Rectangle(j*nTileSize, i*nTileSize, nTileSize, nTileSize);
                    Bitmap temp = UncroppedBackgroundImage.Clone(rect, UncroppedBackgroundImage.PixelFormat);
                    MapOfTiles[i, j].BackgroundImage = temp;
                    panel1.Controls.Add(MapOfTiles[i, j]);
                    progressBar1.PerformStep();
                }
            }
            panel1.ResumeLayout(false);
            panel1.PerformLayout();

            //LoadTileBackgrounds();
            LoadTileFile();
            progressBar1.Visible = false;
        }

        //public class ImageCopier
        //{
        //    Rectangle rect;
        //    Bitmap background;
        //    Tile[,] MapOfTiles;
        //    int i;
        //    int j;

        //    public ImageCopier(Bitmap bgimage, Rectangle r, Tile[,] t, int a, int b)
        //    {
        //        background = bgimage;
        //        rect = r;
        //        MapOfTiles = t;
        //        i = a;
        //        j = b;
        //    }
        //    public void CopyImage()
        //    {
        //        rect.X = j * rect.Width;
        //        rect.Y = i * rect.Height;

        //        MapOfTiles[i, j].BackgroundImage = background.Clone(rect, background.PixelFormat);
        //    }
        //};

        //private void LoadTileBackgrounds()
        //{
        //    Rectangle rect = new Rectangle(0, 0, nTileSize, nTileSize);
        //    progressBar1.Value = 1; // Reset progress bar

        //    ImageCopier [] imageCopiers = new ImageCopier[10];
        //    Thread[] WorkerThreads = new Thread[10];
        //    Bitmap[] backgroundCopies = new Bitmap[10];

        //    int maxThreads = 1;

        //    //while (!oThread.IsAlive);       // Wait for thread to come alive before continuing

        //    for (int i = 0; i < HeightInTiles; i++)
        //    {
        //        for (int j = 0; j < WidthInTiles; j++)
        //        {
        //            // Assign next section of image to tiles
        //            if (imageCopiers[0] == null)
        //            {
        //                imageCopiers[0] = new ImageCopier(UncroppedBackgroundImage, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), MapOfTiles, i, j);
        //            }
        //            WorkerThreads[0] = new Thread(new ThreadStart(imageCopiers[0].CopyImage));
        //            WorkerThreads[0].Start();
        //            WorkerThreads[0].Join();
        //            progressBar1.PerformStep();

        //            //MapOfTiles[i, j].BackgroundImage = UncroppedBackgroundImage.Clone(rect, UncroppedBackgroundImage.PixelFormat);

                    

        //        }
        //    }
        //}

        private void LoadTileFile()
        {
            string path = sPathToLevelFiles + "_" + comboBox1.SelectedText + ".lvl";
            XnaContent content;
            XmlSerializer serializer = new XmlSerializer(typeof(XnaContent));

            StreamReader reader = new StreamReader(path);
            content = (XnaContent)serializer.Deserialize(reader);
            currentContent = content;

                    

            string temp = "";
            int number = -1;
            int x = -1;
            int y = -1;
            
            // Load in Grass Tiles
            for (int i = 0; i < currentContent.Asset.Grass.Length; i++)
            {
                // Parse XML for numbers
                if (currentContent.Asset.Grass[i] != ' ')
                {
                    temp += currentContent.Asset.Grass[i];
                }
                else
                {
                    // Convert string to number
                    number = System.Convert.ToInt32(temp);
                    temp = "";

                    // Assign number to x OR y tile coordinate
                    if (x < 0)
                    {
                        x = number;
                    }
                    else if (y < 0)
                    {
                        y = number;
                    }

                    if ((x >= 0) && (y >= 0))
                    {
                        // Assign tile
                        Bitmap shadedTile = (Bitmap)this.MapOfTiles[y, x].BackgroundImage;

                        // Shade the tile
                        for (int j = 0; j < shadedTile.Height; j++)
                        {
                            for (int k = 0; k < shadedTile.Width; k++)
                            {
                                if((j+k)%2 == 0) shadedTile.SetPixel(j, k, Color.LightGreen);
                            }
                        }
                        this.MapOfTiles[y, x].BackgroundImage = shadedTile;
                        this.MapOfTiles[y, x].TileType = SelectedTile.GrassTile;

                        // Reset coordinates
                        x = -1;
                        y = -1;
                    }
                }
            }


            // Load in Dirt Files
            for (int i = 0; i < currentContent.Asset.Dirt.Length; i++)
            {
                // Parse XML for numbers
                if (currentContent.Asset.Dirt[i] != ' ')
                {
                    temp += currentContent.Asset.Dirt[i];
                }
                else
                {
                    // Convert string to number
                    number = System.Convert.ToInt32(temp);
                    temp = "";

                    // Assign number to x OR y tile coordinate
                    if (x < 0)
                    {
                        x = number;
                    }
                    else if (y < 0)
                    {
                        y = number;
                    }

                    if ((x >= 0) && (y >= 0))
                    {
                        // Assign tile
                        Bitmap shadedTile = (Bitmap)this.MapOfTiles[y, x].BackgroundImage;

                        // Shade the tile
                        for (int j = 0; j < shadedTile.Height; j++)
                        {
                            for (int k = 0; k < shadedTile.Width; k++)
                            {
                                if ((j + k) % 2 == 0) shadedTile.SetPixel(j, k, Color.LightSalmon);
                            }
                        }
                        this.MapOfTiles[y, x].BackgroundImage = shadedTile;
                        this.MapOfTiles[y, x].TileType = SelectedTile.DirtTile;

                        // Reset coordinates
                        x = -1;
                        y = -1;
                    }
                }
            }

            // Load in Tree Tiles
            for (int i = 0; i < currentContent.Asset.Tree.Length; i++)
            {
                // Parse XML for numbers
                if (currentContent.Asset.Tree[i] != ' ')
                {
                    temp += currentContent.Asset.Tree[i];
                }
                else
                {
                    // Convert string to number
                    number = System.Convert.ToInt32(temp);
                    temp = "";

                    // Assign number to x OR y tile coordinate
                    if (x < 0)
                    {
                        x = number;
                    }
                    else if (y < 0)
                    {
                        y = number;
                    }

                    if ((x >= 0) && (y >= 0))
                    {
                        // Assign tile
                        Bitmap shadedTile = (Bitmap)this.MapOfTiles[y, x].BackgroundImage;

                        // Shade the tile
                        for (int j = 0; j < shadedTile.Height; j++)
                        {
                            for (int k = 0; k < shadedTile.Width; k++)
                            {
                                if ((j + k) % 2 == 0) shadedTile.SetPixel(j, k, Color.DarkGreen);
                            }
                        }
                        this.MapOfTiles[y, x].BackgroundImage = shadedTile;
                        this.MapOfTiles[y, x].TileType = SelectedTile.TreeTile;

                        // Reset coordinates
                        x = -1;
                        y = -1;
                    }
                }
            }


            // Load in Start Point Tiles
            for (int i = 0; i < currentContent.Asset.Start.Length; i++)
            {
                // Parse XML for numbers
                if (currentContent.Asset.Start[i] != ' ')
                {
                    temp += currentContent.Asset.Start[i];
                }
                else
                {
                    // Convert string to number
                    number = System.Convert.ToInt32(temp);
                    temp = "";

                    // Assign number to x OR y tile coordinate
                    if (x < 0)
                    {
                        x = number;
                    }
                    else if (y < 0)
                    {
                        y = number;
                    }

                    if ((x >= 0) && (y >= 0))
                    {
                        // Assign tile
                        this.MapOfTiles[y, x].BackgroundImage = ((System.Drawing.Image)(Properties.Resources.greenLightIcon));
                        this.MapOfTiles[y, x].TileType = SelectedTile.StartTile;

                        // Reset coordinates
                        x = -1;
                        y = -1;
                    }
                }
            }

            // Load End Point
            for (int i = 0; i < currentContent.Asset.End.Length; i++)
            {
                // Parse XML for numbers
                if (currentContent.Asset.End[i] != ' ')
                {
                    temp += currentContent.Asset.End[i];
                }
                else
                {
                    // Convert string to number
                    number = System.Convert.ToInt32(temp);
                    temp = "";

                    // Assign number to x OR y tile coordinate
                    if (x < 0)
                    {
                        x = number;
                    }
                    else if (y < 0)
                    {
                        y = number;
                    }

                    if ((x >= 0) && (y >= 0))
                    {
                        // Assign tile
                        this.MapOfTiles[y, x].BackgroundImage = ((System.Drawing.Image)(Properties.Resources.redLightIcon));
                        this.MapOfTiles[y, x].TileType = SelectedTile.EndTile;

                        // Reset coordinates
                        x = -1;
                        y = -1;
                    }
                }
            }

            reader.Close();
            this.Refresh();
        }

        private void tile_button_Click(object sender, EventArgs e)
        {
            Tile caller = sender as Tile;
            Bitmap shadedTile = (Bitmap)caller.BackgroundImage;

            switch (drawTile)
            {
                case SelectedTile.TreeTile:
                    for (int i = 0; i < shadedTile.Height; i++)
                    {
                        for (int j = 0; j < shadedTile.Width; j++)
                        {
                            if((i+j)%2 == 0) shadedTile.SetPixel(i, j, Color.DarkGreen);
                        }
                    }
                    caller.BackgroundImage = shadedTile;
                    caller.TileType = SelectedTile.TreeTile;
                    break;
                case SelectedTile.DirtTile:
                    for (int i = 0; i < shadedTile.Height; i++)
                    {
                        for (int j = 0; j < shadedTile.Width; j++)
                        {
                            if((i+j)%2 == 0) shadedTile.SetPixel(i, j, Color.LightSalmon);
                        }
                    }
                    caller.BackgroundImage = shadedTile;
                    caller.TileType = SelectedTile.DirtTile;
                    break;
                case SelectedTile.GrassTile:
                    for (int i = 0; i < shadedTile.Height; i++)
                    {
                        for (int j = 0; j < shadedTile.Width; j++)
                        {
                            if((i+j)%2 == 0) shadedTile.SetPixel(i, j, Color.LightGreen);
                        }
                    }
                    caller.BackgroundImage = shadedTile;
                    caller.TileType = SelectedTile.GrassTile;
                    break;
                case SelectedTile.StartTile:
                    caller.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.greenLightIcon));
                    caller.TileType = SelectedTile.StartTile;
                    break;
                case SelectedTile.EndTile:
                    caller.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.redLightIcon));
                    caller.TileType = SelectedTile.EndTile;
                    break;
                default:
                    caller.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.grassIcon));
                    caller.TileType = SelectedTile.GrassTile;
                    break;
            }
        }

        private void MapEditor_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            drawTile = SelectedTile.TreeTile;
            toolStripButton1.Checked = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            drawTile = SelectedTile.DirtTile;
            toolStripButton2.Checked = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            drawTile = SelectedTile.GrassTile;
            toolStripButton3.Checked = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            drawTile = SelectedTile.StartTile;
            toolStripButton4.Checked = true;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            drawTile = SelectedTile.EndTile;
            toolStripButton5.Checked = true;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
        }



        // This is the class that will be serialized.
        public class XnaContent
        {
            public class AssetContainer
            {
                public string NumberRows;
                public string NumberColumns;
                public string Start;
                public string End;
                public string Grass;
                public string Dirt;
                public string Tree;
            }

            public AssetContainer Asset = new AssetContainer();
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sPathToLevelFiles != null)
            {
                //saveFileDialog1.Filter = "Level File|*.lvl";
                //saveFileDialog1.Title = "Save the Level File";
                //saveFileDialog1.ShowDialog();

                XmlSerializer serializer = new XmlSerializer(typeof(XnaContent));
                XnaContent content = new XnaContent();

                content.Asset.NumberRows = HeightInTiles.ToString();
                content.Asset.NumberColumns = WidthInTiles.ToString();
                content.Asset.Start = "";
                content.Asset.End = "";
                content.Asset.Grass = "";
                content.Asset.Dirt = "";
                content.Asset.Tree = "";

                // Write the level file
                for (int i = 0; i < HeightInTiles; i++)
                {
                    for (int j = 0; j < WidthInTiles; j++)
                    {

                        switch (this.MapOfTiles[i, j].TileType)
                        {
                            case SelectedTile.TreeTile:
                                content.Asset.Tree += j.ToString() + ' ' + i.ToString() + ' ';
                                break;
                            case SelectedTile.DirtTile:
                                content.Asset.Dirt += j.ToString() + ' ' + i.ToString() + ' ';
                                break;
                            case SelectedTile.GrassTile:
                                content.Asset.Grass += j.ToString() + ' ' + i.ToString() + ' ';
                                break;
                            case SelectedTile.StartTile:
                                content.Asset.Start += j.ToString() + ' ' + i.ToString() + ' ';
                                break;
                            case SelectedTile.EndTile:
                                content.Asset.End += j.ToString() + ' ' + i.ToString() + ' ';
                                break;
                            default:
                                break;
                        }
                    }
                }

                //            string filename = "thisisthefilerighthere.xml";

                // Create an XmlTextWriter using a FileStream.
                Stream stream = new FileStream(sPathToLevelFiles + "_" + comboBox1.SelectedItem + ".lvl", FileMode.Create);
                XmlWriter writer = new XmlTextWriter(stream, Encoding.Unicode);

                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, content);
                writer.Close();
            }
            else
            {
                MessageBox.Show("No path defined yet. Need to implement this feature.");
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object selectedTileSize = comboBox1.SelectedItem;
            nTileSize = Convert.ToInt32(selectedTileSize.ToString());
            nButtonSideLength = nTileSize;

            if (MapOfTiles != null)
            {
                RemovePreviousTiles();
            }
            
            WidthInTiles = resolutionX / nTileSize;
            HeightInTiles = resolutionY / nTileSize;
            
            if (MapOfTiles != null)
            {
                LoadMap();
            }
        }

        private void RemovePreviousTiles()
        {
            for (int i = 0; i < HeightInTiles; i++)
            {
                for (int j = 0; j < WidthInTiles; j++)
                {
                    this.Controls.Remove(MapOfTiles[i, j]);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = true;

            // Zoom 100% selected
            nButtonSideLength = nTileSize;
            if (MapOfTiles != null)
            {
                RemovePreviousTiles();
            }
            LoadMap();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            toolStripMenuItem3.Checked = true;

            // Zoom 75% selected
            nButtonSideLength = (int)(nTileSize * 0.75);
            if (MapOfTiles != null)
            {
                RemovePreviousTiles();
            }
            LoadMap();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripMenuItem4.Checked = true;

            // Zoom 50% selected
            nButtonSideLength = (int)(nTileSize * 0.5);
            if (MapOfTiles != null)
            {
                RemovePreviousTiles();
            }
            LoadMap();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            toolStripMenuItem5.Checked = true;

            // Zoom 25% selected
            nButtonSideLength = (int)(nTileSize * 0.25);
            if (MapOfTiles != null)
            {
                RemovePreviousTiles();
            }
            LoadMap();
        }

        private void zoomToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\deleteMe\\LevelCreator2 - Copy\\LevelCreator";
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string pathToFile = openFileDialog1.FileName;
                UncroppedBackgroundImage = new Bitmap(pathToFile, true);
                resolutionY = UncroppedBackgroundImage.Height;
                resolutionX = UncroppedBackgroundImage.Width;

                sDirectoryName = Path.GetDirectoryName(pathToFile);
                sFilenameNoExt = Path.GetFileNameWithoutExtension(pathToFile);
                sPathToLevelFiles = Path.Combine(sDirectoryName, sFilenameNoExt);

                // Check to see if level files exist
                if (!File.Exists(sPathToLevelFiles + "_16.lvl") &&
                    !File.Exists(sPathToLevelFiles + "_32.lvl") &&
                    !File.Exists(sPathToLevelFiles + "_64.lvl"))
                {
                    MessageBox.Show("No level file(s) found. A new one will be generated for current tile size.");
                }
                else
                {
                   
                }
                

                LoadMap();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MapEditor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void MapEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey)
            {
                bPaintTiles = !bPaintTiles;
                if (bPaintTiles)
                {
                    paintTilesFlag_textBox1.Text = "ON";
                }
                else
                {
                    paintTilesFlag_textBox1.Text = "OFF";
                }
            }
        }     
    }
}
