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

        Bitmap backgroundImage;


        public MapEditor()
        {
            InitializeComponent();
            bMouseIsDown = false;

            // Initialize background image if there is one
            backgroundImage = new Bitmap("c:\\CSCE443\\LevelCreator\\LevelCreator\\exampleLevel.bmp", true);

            Rectangle rect = new Rectangle(0, 0, 16, 16);
            Bitmap cropped = backgroundImage.Clone(rect, backgroundImage.PixelFormat);
           


            int nTileSideLength = 10;

            MapOfTiles = new Tile[HeightInTiles, WidthInTiles];

            for (int i = 0; i < HeightInTiles; i++)
            {
                for (int j = 0; j < WidthInTiles; j++)
                {

                    this.SuspendLayout();
                    this.MapOfTiles[i, j] = new Tile(i, j, SelectedTile.GrassTile);
                    this.MapOfTiles[i, j].BackColor = System.Drawing.SystemColors.Control;
                    this.MapOfTiles[i, j].FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    this.MapOfTiles[i, j].FlatAppearance.BorderSize = 0;
                    this.MapOfTiles[i, j].FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    this.MapOfTiles[i, j].FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                    
                    this.MapOfTiles[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    this.MapOfTiles[i, j].Location = new System.Drawing.Point((12 + j*nTileSideLength), (52 + i*nTileSideLength));
                    this.MapOfTiles[i, j].Name = "button" + i + j;
                    this.MapOfTiles[i, j].Size = new System.Drawing.Size(nTileSideLength, nTileSideLength);
                    this.MapOfTiles[i, j].UseVisualStyleBackColor = false;
                    this.MapOfTiles[i, j].Click += new System.EventHandler(this.tile_button_Click);
                    this.MapOfTiles[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.tile_button_Click);
                    this.MapOfTiles[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(this.tile_MouseUp);
                    this.MapOfTiles[i, j].MouseMove +=new MouseEventHandler(this.tile_MouseMove);
                    this.MapOfTiles[i, j].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//                    this.MapOfTiles[i, j].BackgroundImage = global::LevelCreator.Properties.Resources.grassIcon;
                    this.MapOfTiles[i, j].BackgroundImage = cropped;
                    this.MapOfTiles[i, j].ForeColor = Color.DarkGray;
                    this.Controls.Add(this.MapOfTiles[i, j]);
                    this.ResumeLayout(false);
                    this.PerformLayout();

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void tile_MouseMove(object sender, EventArgs e)
        {

        }

        private void tile_MouseUp(object sender, EventArgs e)
        {
            bMouseIsDown = false;
        }

        private void tile_button_Click(object sender, EventArgs e)
        {
            bMouseIsDown = true;
            Tile caller = sender as Tile;

            switch (drawTile)
            {
                case SelectedTile.TreeTile:
                    caller.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.treeIcon));
                    caller.TileType = SelectedTile.TreeTile;
                    break;
                case SelectedTile.DirtTile:
                    caller.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.dirtIcon));
                    caller.TileType = SelectedTile.DirtTile;
                    break;
                case SelectedTile.GrassTile:
                    caller.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.grassIcon));
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

            string filename = "thisisthefilerighthere.xml";

            // Create an XmlTextWriter using a FileStream.
            Stream stream = new FileStream(filename, FileMode.Create);
            XmlWriter writer = new XmlTextWriter(stream, Encoding.Unicode);

            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, content);
            writer.Close();  

            
        }


    }
}
