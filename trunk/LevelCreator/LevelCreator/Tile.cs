using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LevelCreator
{
    class Tile : Button
    {
        public MapEditor.SelectedTile TileType;

        int nXposition;
        int nYposition;

        public Tile()
        {
        }

        public Tile(int x, int y, MapEditor.SelectedTile tile)
        {
            nXposition = x;
            nYposition = y;

            TileType = tile;
        }
    }
}
