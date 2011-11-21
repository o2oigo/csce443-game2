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
        public enum SelectedTile { TreeTile, DirtTile, GrassTile, StartTile, EndTile, BarrierTile };
        public enum SelectedTool { PaintTile, FillTiles };

        SelectedTile selectedTileType;
        SelectedTool selectedTool;

        Bitmap UnmodifiedBackgroundImage;
        Bitmap ModifiedBackgroundImage;

        private double dScaledTileWidth;
        private double dScaledTileHeight;

        private int nButtonSideLength;
        private int nTileSize;
        private int resolutionY;
        private int resolutionX;
        private int HeightInTiles;
        private int WidthInTiles;
        private bool bMouseIsDown;

        // Directory information
        private string sDirectoryName;
        private string sFilenameNoExt;
        private string sPathToLevelFiles;

        // Current XnaContent
        XnaContent currentContent;

        public MapEditor()
        {
            InitializeComponent();
            bMouseIsDown = false;
            selectedTool = SelectedTool.PaintTile;
            save_ToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            tileSize_ComboBox1.Enabled = false;
            checkToolStripButtons();
            InitializeTileSizes();
        }

        private void InitializeTileSizes()
        {
            tileSize_ComboBox1.Items.Add("16");
            tileSize_ComboBox1.Items.Add("32");
            tileSize_ComboBox1.Items.Add("64");

            // Sets default
            tileSize_ComboBox1.SelectedIndex = 0;    // Sets default tile size to 32x32 pixels
        }

        private void LoadMap()
        {
            MapOfTiles = new Tile[WidthInTiles, HeightInTiles];

            for (int i = 0; i < WidthInTiles; i++)
            {
                for (int j = 0; j < HeightInTiles; j++)
                {
                    MapOfTiles[i, j] = new Tile(i, j, SelectedTile.GrassTile);
                }
            }
        }


        // Tile selection
        private void tree_ToolStripButton1_Click(object sender, EventArgs e)
        {
            selectedTileType = SelectedTile.TreeTile;
            checkToolStripButtons();
        }

        private void dirt_ToolStripButton2_Click(object sender, EventArgs e)
        {
            selectedTileType = SelectedTile.DirtTile;
            checkToolStripButtons();
        }

        private void grass_ToolStripButton3_Click(object sender, EventArgs e)
        {
            selectedTileType = SelectedTile.GrassTile;
            checkToolStripButtons();
        }

        private void start_ToolStripButton4_Click(object sender, EventArgs e)
        {
            selectedTileType = SelectedTile.StartTile;
            checkToolStripButtons();
        }

        private void end_ToolStripButton5_Click(object sender, EventArgs e)
        {
            selectedTileType = SelectedTile.EndTile;
            checkToolStripButtons();
        }

        private void barrier_ToolStripButton6_Click(object sender, EventArgs e)
        {
            selectedTileType = SelectedTile.BarrierTile;
            checkToolStripButtons();
        }


        // Tool selection
        private void paintTile_toolStripButton8_Click(object sender, EventArgs e)
        {
            selectedTool = SelectedTool.PaintTile;
            checkToolStripButtons();
        }

        private void fillTiles_toolStripButton7_Click(object sender, EventArgs e)
        {
            selectedTool = SelectedTool.FillTiles;
            checkToolStripButtons();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tree_ToolStripButton1.Checked = false;
            dirt_ToolStripButton2.Checked = false;
            grass_ToolStripButton3.Checked = false;
            start_ToolStripButton4.Checked = false;
            end_ToolStripButton5.Checked = false;
            barrier_ToolStripButton6.Checked = false;
            fillTiles_toolStripButton7.Checked = false;
            paintTile_toolStripButton8.Checked = false;
        }

        private void checkToolStripButtons()
        {
            switch (selectedTileType)
            {
                case SelectedTile.TreeTile:
                    tree_ToolStripButton1.Checked = true;
                    break;
                case SelectedTile.DirtTile:
                    dirt_ToolStripButton2.Checked = true;
                    break;
                case SelectedTile.GrassTile:
                    grass_ToolStripButton3.Checked = true;
                    break;
                case SelectedTile.StartTile:
                    start_ToolStripButton4.Checked = true;
                    break;
                case SelectedTile.EndTile:
                    end_ToolStripButton5.Checked = true;
                    break;
                case SelectedTile.BarrierTile:
                    barrier_ToolStripButton6.Checked = true;
                    break;
                default:
                    break;
            }

            switch (selectedTool)
            {
                case SelectedTool.FillTiles:
                    fillTiles_toolStripButton7.Checked = true;
                    break;
                case SelectedTool.PaintTile:
                    paintTile_toolStripButton8.Checked = true;
                    break;
                default:
                    break;
            }
        }


        // Tile size selection
        private void tileSize_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object selectedTileSize = tileSize_ComboBox1.SelectedItem;
            nTileSize = Convert.ToInt32(selectedTileSize.ToString());
            nButtonSideLength = nTileSize;

            WidthInTiles = resolutionX / nTileSize;
            HeightInTiles = resolutionY / nTileSize;

            if (MapOfTiles != null)
            {
                LoadMap();
            }
        }


        // This is the class that will be serialized.
        public class XnaContent
        {
            public class AssetContainer
            {
                [XmlAttribute("Type")]
                public string Type
                { get; set; }

                [XmlElement("NumberRows")]
                public string NumberRows
                { get; set; }

                [XmlElement("NumberColumns")]
                public string NumberColumns
                { get; set; }

                [XmlElement("Start")]
                public string Start
                { get; set; }

                [XmlElement("End")]
                public string End
                { get; set; }

                [XmlElement("Grass")]
                public string Grass
                { get; set; }

                [XmlElement("Dirt")]
                public string Dirt
                { get; set; }

                [XmlElement("Trees")]
                public string Trees
                { get; set; }

                [XmlElement("Barriers")]
                public string Barriers
                { get; set; }
            }

            public AssetContainer Asset = new AssetContainer();
        }



        private void MapEditor_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = this.Width - 40;
            pictureBox1.Height = this.Height - 100;
            dScaledTileWidth = (double)pictureBox1.Width / WidthInTiles;
            dScaledTileHeight = (double)pictureBox1.Height / HeightInTiles;
        }


        // Click and drag feature implemented here
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseIsDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseIsDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMouseIsDown)
            {
                // Determine which tile was clicked
                int nRow = (int)(e.Y / dScaledTileHeight);
                int nColumn = (int)(e.X / dScaledTileWidth);

                // Paint that tile and set tile type
                if ((0 <= nColumn) && (nColumn < WidthInTiles) && (0 <= nRow) &&(nRow < HeightInTiles))
                {
                    if (MapOfTiles[nColumn, nRow].TileType != selectedTileType)
                    {
                        switch (selectedTool)
                        {
                            case SelectedTool.PaintTile:
                                PaintTile(nColumn, nRow);
                                break;
                            case SelectedTool.FillTiles:
                                FillTiles(nColumn, nRow);
                                break;
                            default:
                                break;
                        }

                        pictureBox1.Refresh();
                    }
                }
            }
        }


        // Perform tool function on click
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            // Check that a level has been loaded
            if (MapOfTiles != null)
            {
                // Determine which tile was clicked
                int nRow = (int)(e.Y / dScaledTileHeight);
                int nColumn = (int)(e.X / dScaledTileWidth);

                // Paint that tile and set tile type
                if (MapOfTiles[nColumn, nRow].TileType != selectedTileType)
                {
                    switch (selectedTool)
                    {
                        case SelectedTool.PaintTile:
                            PaintTile(nColumn, nRow);
                            break;
                        case SelectedTool.FillTiles:
                            FillTiles(nColumn, nRow);
                            break;
                        default:
                            break;
                    }

                    pictureBox1.Refresh();
                }
            }
        }

        private void PaintTile(int x, int y)
        {
            Point tilePosition = new Point(x * nTileSize, y * nTileSize);

            // Reset tile before painting
            for (int i = 0; i < nTileSize; i++)
            {
                for (int j = 0; j < nTileSize; j++)
                {
                    Color originalPixel = UnmodifiedBackgroundImage.GetPixel(tilePosition.X + j, tilePosition.Y + i);
                    ModifiedBackgroundImage.SetPixel(tilePosition.X + j, tilePosition.Y + i, originalPixel);
                }
            }

            // Paint tile
            switch (selectedTileType)
            {
                case SelectedTile.TreeTile:
                    for (int i = 0; i < nTileSize; i++)
                    {
                        for (int j = 0; j < nTileSize; j++)
                        {
                            if ((i + j) % 2 == 0) ModifiedBackgroundImage.SetPixel(tilePosition.X + j, tilePosition.Y + i, Color.DarkOliveGreen);
                        }
                    }
                    MapOfTiles[x, y].TileType = SelectedTile.TreeTile;
                    break;
                case SelectedTile.DirtTile:
                    for (int i = 0; i < nTileSize; i++)
                    {
                        for (int j = 0; j < nTileSize; j++)
                        {
                            if ((i + j) % 8 == 0) ModifiedBackgroundImage.SetPixel(tilePosition.X + j, tilePosition.Y + i, Color.BurlyWood);
                        }
                    }
                    MapOfTiles[x, y].TileType = SelectedTile.DirtTile;
                    break;
                case SelectedTile.GrassTile:
                    for (int i = 0; i < nTileSize; i++)
                    {
                        for (int j = 0; j < nTileSize; j++)
                        {
                            if ((i + j) % 4 == 0) ModifiedBackgroundImage.SetPixel(tilePosition.X + j, tilePosition.Y + i, Color.LightGreen);
                        }
                    }
                    MapOfTiles[x, y].TileType = SelectedTile.GrassTile;
                    break;
                case SelectedTile.StartTile:
                    for (int i = 0; i < nTileSize; i++)
                    {
                        for (int j = 0; j < nTileSize; j++)
                        {
                            if ((i + j) % 2 == 0) ModifiedBackgroundImage.SetPixel(tilePosition.X + j, tilePosition.Y + i, Color.DarkGreen);
                        }
                    }
                    MapOfTiles[x, y].TileType = SelectedTile.StartTile;
                    break;
                case SelectedTile.EndTile:
                    for (int i = 0; i < nTileSize; i++)
                    {
                        for (int j = 0; j < nTileSize; j++)
                        {
                            if ((i + j) % 2 == 0) ModifiedBackgroundImage.SetPixel(tilePosition.X + j, tilePosition.Y + i, Color.Red);
                        }
                    }
                    MapOfTiles[x, y].TileType = SelectedTile.EndTile;
                    break;
                case SelectedTile.BarrierTile:
                    for (int i = 0; i < nTileSize; i++)
                    {
                        for (int j = 0; j < nTileSize; j++)
                        {
                            if ((i + j) % 4 == 0) ModifiedBackgroundImage.SetPixel(tilePosition.X + j, tilePosition.Y + i, Color.DarkRed);
                        }
                    }
                    MapOfTiles[x, y].TileType = SelectedTile.BarrierTile;
                    break;
                default:
                    break;
            }
        }

        private void FillTiles(int x, int y)
        {
            SelectedTile previousTileType = MapOfTiles[x, y].TileType;
            PaintTile(x, y);
            int tempX = x;
            int tempY = y;

            int topTileY = tempY-1;
            int bottomTileY = tempY+1;
            int leftTileX = tempX-1;
            int rightTileX = tempX+1;

            // Check top tile first, recursively fill upwards
            if ((0 <= topTileY) && (topTileY < HeightInTiles) &&
                (MapOfTiles[tempX, topTileY].TileType == previousTileType)) // Top tile equals previous tile type
            {
                FillTiles(tempX, topTileY);
            }

            // Check bottom tile
            if ((0 <= bottomTileY) && (bottomTileY < HeightInTiles) &&
                    (MapOfTiles[tempX, bottomTileY].TileType == previousTileType))
            {
                FillTiles(tempX, bottomTileY);
            }

            // Check left tile
            if ((0 <= leftTileX) && (leftTileX < WidthInTiles) &&
                    (MapOfTiles[leftTileX, tempY].TileType == previousTileType))
            {
                FillTiles(leftTileX, tempY);
            }

            // Check right tile
            if ((0 <= rightTileX) && (rightTileX < WidthInTiles) &&
                    (MapOfTiles[rightTileX, tempY].TileType == previousTileType))
            {
                FillTiles(rightTileX, tempY);
            }
        }


        // Load and Save section
        private void LoadLevel_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = ;
            openFileDialog1.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveAsToolStripMenuItem.Enabled = true;
                save_ToolStripMenuItem.Enabled = true;

                string pathToFile = openFileDialog1.FileName;
                UnmodifiedBackgroundImage = new Bitmap(pathToFile, true);   // Keep a copy of the unmodified background image
                ModifiedBackgroundImage = new Bitmap(pathToFile, true);     // This image shows changes

                // Display background image
                pictureBox1.BackgroundImage = ModifiedBackgroundImage;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

                // Set image parameters
                resolutionY = ModifiedBackgroundImage.Height;
                resolutionX = ModifiedBackgroundImage.Width;
                WidthInTiles = resolutionX / nTileSize;
                HeightInTiles = resolutionY / nTileSize;

                // For use in calculating which tile clicked
                dScaledTileWidth = (double)pictureBox1.Width / WidthInTiles;
                dScaledTileHeight = (double)pictureBox1.Height / HeightInTiles;

                sDirectoryName = Path.GetDirectoryName(pathToFile);
                sFilenameNoExt = Path.GetFileNameWithoutExtension(pathToFile);
                sPathToLevelFiles = Path.Combine(sDirectoryName, sFilenameNoExt);

                // Check to see if level files exist
                if (!File.Exists(sPathToLevelFiles + "_16.xml") &&
                    !File.Exists(sPathToLevelFiles + "_32.xml") &&
                    !File.Exists(sPathToLevelFiles + "_64.xml"))
                {
                    MessageBox.Show("No level file(s) found. A new one will be generated for current tile size.");
                }

                LoadMap();
                LoadTileFile();
            }

        }

        private void LoadTileFile()
        {
//            string path = sPathToLevelFiles + "_" + tileSize_ComboBox1.SelectedText + ".xml";
            string path = sPathToLevelFiles + "_16.xml";

            if (File.Exists(path))
            {
                XnaContent content;
                XmlSerializer serializer = new XmlSerializer(typeof(XnaContent));

                using (StreamReader reader = new StreamReader(path))
                {
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
                                MapOfTiles[x, y].TileType = SelectedTile.GrassTile;

                                // Paint the tile
                                selectedTileType = SelectedTile.GrassTile;
                                PaintTile(x, y);

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
                                MapOfTiles[x, y].TileType = SelectedTile.DirtTile;

                                // Paint the tile
                                selectedTileType = SelectedTile.DirtTile;
                                PaintTile(x, y);

                                // Reset coordinates
                                x = -1;
                                y = -1;
                            }
                        }
                    }

                    // Load in Tree Tiles
                    for (int i = 0; i < currentContent.Asset.Trees.Length; i++)
                    {
                        // Parse XML for numbers
                        if (currentContent.Asset.Trees[i] != ' ')
                        {
                            temp += currentContent.Asset.Trees[i];
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
                                MapOfTiles[x, y].TileType = SelectedTile.TreeTile;

                                // Paint the tile
                                selectedTileType = SelectedTile.TreeTile;
                                PaintTile(x, y);

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
                                MapOfTiles[x, y].TileType = SelectedTile.StartTile;

                                // Paint the tile
                                selectedTileType = SelectedTile.StartTile;
                                PaintTile(x, y);

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
                                MapOfTiles[x, y].TileType = SelectedTile.EndTile;

                                // Paint the tile
                                selectedTileType = SelectedTile.EndTile;
                                PaintTile(x, y);

                                // Reset coordinates
                                x = -1;
                                y = -1;
                            }
                        }
                    }

                    // Load Barrier Tiles
                    for (int i = 0; i < currentContent.Asset.Barriers.Length; i++)
                    {
                        // Parse XML for numbers
                        if (currentContent.Asset.Barriers[i] != ' ')
                        {
                            temp += currentContent.Asset.Barriers[i];
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
                                MapOfTiles[x, y].TileType = SelectedTile.BarrierTile;

                                // Paint the tile
                                selectedTileType = SelectedTile.BarrierTile;
                                PaintTile(x, y);

                                // Reset coordinates
                                x = -1;
                                y = -1;
                            }
                        }
                    }

                    reader.Close();
                    this.Refresh();
                }
            }
            else
            {
                // This means there's currently no level file, so paint all tiles with grass
                selectedTileType = SelectedTile.GrassTile;

                for (int i = 0; i < WidthInTiles; i++)
                {
                    for (int j = 0; j < HeightInTiles; j++)
                    {
                        PaintTile(i, j);
                    }
                }
                this.Refresh();
            }
        }


        private void save_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLevel();
        }

        private void SaveLevel()
        {
            if (sPathToLevelFiles != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XnaContent));
                XnaContent content = new XnaContent();

                content.Asset.Type = "PathfindingData.MapData";
                content.Asset.NumberRows = HeightInTiles.ToString();
                content.Asset.NumberColumns = WidthInTiles.ToString();
                content.Asset.Start = "";
                content.Asset.End = "";
                content.Asset.Grass = "";
                content.Asset.Dirt = "";
                content.Asset.Trees = "";
                content.Asset.Barriers = "";

                // Write the level file
                for (int i = 0; i < WidthInTiles; i++)
                {
                    for (int j = 0; j < HeightInTiles; j++)
                    {

                        switch (MapOfTiles[i, j].TileType)
                        {
                            case SelectedTile.TreeTile:
                                content.Asset.Trees += i.ToString() + ' ' + j.ToString() + ' ';
                                break;
                            case SelectedTile.DirtTile:
                                content.Asset.Dirt += i.ToString() + ' ' + j.ToString() + ' ';
                                break;
                            case SelectedTile.GrassTile:
                                content.Asset.Grass += i.ToString() + ' ' + j.ToString() + ' ';
                                break;
                            case SelectedTile.StartTile:
                                content.Asset.Start += i.ToString() + ' ' + j.ToString() + ' ';
                                break;
                            case SelectedTile.EndTile:
                                content.Asset.End += i.ToString() + ' ' + j.ToString() + ' ';
                                break;
                            case SelectedTile.BarrierTile:
                                content.Asset.Barriers += i.ToString() + ' ' + j.ToString() + ' ';
                                break;
                            default:
                                break;
                        }
                    }
                }

                // Save image of tiles only
                ModifiedBackgroundImage.Save(sPathToLevelFiles + "_WITHTILES" + "_" + tileSize_ComboBox1.SelectedItem + ".png", System.Drawing.Imaging.ImageFormat.Png);
                //NewBackgroundImage.Save("C:\\test.png", System.Drawing.Imaging.ImageFormat.Png);

                // Create an XmlTextWriter using a FileStream.
                Stream stream = new FileStream(sPathToLevelFiles + "_" + tileSize_ComboBox1.SelectedItem + ".xml", FileMode.Create);
                XmlWriter writer = new XmlTextWriter(stream, Encoding.Unicode);

                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, content);
                writer.Close();
            }
            else
            {
                MessageBox.Show("No path to level files defined yet. Need to implement this feature.");
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string pathToFile = "";

            saveFileDialog1.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    pathToFile = saveFileDialog1.FileName;
                    myStream.Close();
                }
            }

            if (pathToFile != "")
            {
                saveAsToolStripMenuItem.Enabled = true;
                save_ToolStripMenuItem.Enabled = true;

                UnmodifiedBackgroundImage = LevelCreator.Properties.Resources.NewLevel;   // Keep a copy of the unmodified background image
                ModifiedBackgroundImage = LevelCreator.Properties.Resources.NewLevel;     // This image shows changes

                // Display background image
                pictureBox1.BackgroundImage = ModifiedBackgroundImage;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

                // Set image parameters
                resolutionY = ModifiedBackgroundImage.Height;
                resolutionX = ModifiedBackgroundImage.Width;
                WidthInTiles = resolutionX / nTileSize;
                HeightInTiles = resolutionY / nTileSize;

                // For use in calculating which tile clicked
                dScaledTileWidth = (double)pictureBox1.Width / WidthInTiles;
                dScaledTileHeight = (double)pictureBox1.Height / HeightInTiles;

                sDirectoryName = Path.GetDirectoryName(pathToFile);
                sFilenameNoExt = Path.GetFileNameWithoutExtension(pathToFile);
                sPathToLevelFiles = Path.Combine(sDirectoryName, sFilenameNoExt);

                // Check to see if level files exist
                if (!File.Exists(sPathToLevelFiles + "_16.xml") &&
                    !File.Exists(sPathToLevelFiles + "_32.xml") &&
                    !File.Exists(sPathToLevelFiles + "_64.xml"))
                {
                    MessageBox.Show("No level file(s) found. A new one will be generated for current tile size.");
                }

                ModifiedBackgroundImage.Save(sPathToLevelFiles + ".png", System.Drawing.Imaging.ImageFormat.Png);
                LoadMap();
                LoadTileFile();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sPathToLevelFiles != null)
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string pathToFile = "";

                saveFileDialog1.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        // Change all the paths for files
                        pathToFile = saveFileDialog1.FileName;
                        sDirectoryName = Path.GetDirectoryName(pathToFile);
                        sFilenameNoExt = Path.GetFileNameWithoutExtension(pathToFile);
                        sPathToLevelFiles = Path.Combine(sDirectoryName, sFilenameNoExt);

                        // Save level file
                        SaveLevel();
                        myStream.Close();

                        // Save level background
                        UnmodifiedBackgroundImage.Save(sPathToLevelFiles + ".png", System.Drawing.Imaging.ImageFormat.Png);


                    }
                }
            }
        }

    }
}
