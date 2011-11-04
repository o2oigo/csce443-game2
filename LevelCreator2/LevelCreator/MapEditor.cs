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


namespace LevelCreator
{
    public partial class MapEditor : Form
    {
        public enum SelectedTile { TreeTile, DirtTile, GrassTile, StartTile, EndTile };
        private bool bMouseIsDown;

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
        
        

        public MapEditor()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            label2.Visible = false;

            bMouseIsDown = false;
            UncroppedBackgroundImage = new Bitmap("H:\\ms_windows\\cpsc_redirected\\My Documents\\Visual Studio 2010\\Projects\\CSCE 443 - Project 2\\LevelCreator2\\LevelCreator\\exampleLevel.bmp", true);
            resolutionY = UncroppedBackgroundImage.Height;
            resolutionX = UncroppedBackgroundImage.Width;
            InitializeTileSizes();
            InitializeEditorTileSize();
//            LoadMap();
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
            label2.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = HeightInTiles * WidthInTiles;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            for (int i = 0; i < HeightInTiles; i++)
            {
                for (int j = 0; j < WidthInTiles; j++)
                {
                    panel1.SuspendLayout();
                    MapOfTiles[i, j] = new Tile(i, j, SelectedTile.GrassTile);
                    MapOfTiles[i, j].BackColor = System.Drawing.SystemColors.Control;
                    MapOfTiles[i, j].FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    MapOfTiles[i, j].FlatAppearance.BorderSize = 0;
                    MapOfTiles[i, j].FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    MapOfTiles[i, j].FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));

                    MapOfTiles[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    MapOfTiles[i, j].Location = new System.Drawing.Point((j * nButtonSideLength), (i * nButtonSideLength));
                    MapOfTiles[i, j].Name = "button" + i + j;
                    MapOfTiles[i, j].Size = new System.Drawing.Size(nButtonSideLength, nButtonSideLength);
                    MapOfTiles[i, j].UseVisualStyleBackColor = false;
                    MapOfTiles[i, j].Click += new System.EventHandler(this.tile_button_Click);
//                    MapOfTiles[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.tile_mouseDown);
//                    MapOfTiles[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(this.tile_MouseUp);
//                    MapOfTiles[i, j].MouseMove += new MouseEventHandler(this.tile_MouseMove);
//                    MapOfTiles[i, j].DragEnter += new DragEventHandler(this.tile_DragEnter);
                    MapOfTiles[i, j].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    //                    this.MapOfTiles[i, j].BackgroundImage = global::LevelCreator.Properties.Resources.grassIcon;
                    MapOfTiles[i, j].ForeColor = Color.DarkGray;

                    // Assign next section of image to tiles
                    Rectangle rect = new Rectangle(j * nTileSize, i * nTileSize, nTileSize, nTileSize);
                    Bitmap cropped = UncroppedBackgroundImage.Clone(rect, UncroppedBackgroundImage.PixelFormat);

                    MapOfTiles[i, j].BackgroundImage = cropped;

                    panel1.Controls.Add(MapOfTiles[i, j]);
                    panel1.ResumeLayout(false);
                    panel1.PerformLayout();
                    progressBar1.PerformStep();

                }
            }
            progressBar1.Visible = false;
            label2.Visible = false;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void tile_DragEnter(object sender, EventArgs e)
        {

        }

        private void tile_MouseMove(object sender, EventArgs e)
        {
            if(bMouseIsDown)
            {
                tile_button_Click(sender, e);
            }
        }

        private void tile_mouseDown(object sender, EventArgs e) 
        {
            bMouseIsDown = true;
        }

        private void tile_MouseUp(object sender, EventArgs e)
        {
            bMouseIsDown = false;
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

        private void MapEditor_KeyPress(object sender, KeyPressEventArgs e)
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
                Stream stream = new FileStream(sPathToLevelFiles + "_" + comboBox1.SelectedText + ".lvl", FileMode.Create);
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

            openFileDialog1.InitialDirectory = "H:\\ms_windows\\cpsc_redirected\\My Documents\\Visual Studio 2010\\Projects\\CSCE 443 - Project 2\\LevelCreator2\\LevelCreator";
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
    }
}
