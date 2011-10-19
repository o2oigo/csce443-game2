using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PathfindingData;

namespace RTS
{
    public enum MapTileType
    {
        MapEmpty,
        MapBarrier,
        MapStart,
        MapExit
    }

    public class Map
    {
        #region Fields

        private Vector2 tileSquareCenter;
        private Texture2D tileTexture;
        private Texture2D endTexture;
        private Vector2 dotTextureCenter;
        private Texture2D barrierTexture;

        private List<MapData> maps;
        private MapTileType[,] mapTiles;
        private int currentMap;
        private int numberColumns;
        private int numberRows;

        #endregion
        #region Properties

        public float TileSize
        {
            get { return tileSize; }
        }
        private float tileSize;

        public Point StartTile
        {
            get { return startTile; }
        }
        private Point startTile;

        public float Scale
        {
            get { return scale; }
        }
        private float scale;

        public float ScaleB
        {
            get { return scaleB; }
        }
        private float scaleB;

        public Point EndTile
        {
            get { return endTile; }
        }
        private Point endTile;

        public bool MapReload
        {
            get { return mapReload; }
            set { mapReload = value; }
        }
        private bool mapReload;

        #endregion

        #region Initialization

        public void LoadContent(ContentManager content)
        {

            barrierTexture = content.Load<Texture2D>("block");
            endTexture = content.Load<Texture2D>("Tower1");

            maps = new List<MapData>();
            maps.Add(content.Load<MapData>("map1"));
            maps.Add(content.Load<MapData>("map2"));
            maps.Add(content.Load<MapData>("map3"));
            maps.Add(content.Load<MapData>("map4"));

            ReloadMap();

            mapReload = true;
            tileSquareCenter = new Vector2(tileSize / 2);
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberRows; i++)
            {
                for (int j = 0; j < numberColumns; j++)
                {
                    Vector2 tilePosition = MapToWorld(j, i, false);
                    switch (mapTiles[j, i])
                    {
                        case MapTileType.MapBarrier:
                            spriteBatch.Draw(
                                barrierTexture, tilePosition, null, Color.White,
                                0f, Vector2.Zero, scaleB, SpriteEffects.None, .25f);
                            break;

                        case MapTileType.MapExit:
                            spriteBatch.Draw(
                                endTexture, tilePosition + tileSquareCenter, null,
                                Color.White, 0f, dotTextureCenter, scale,
                                SpriteEffects.None, .25f);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #region Useful Game Methods

        public int[] WorldToMap(Vector2 coordinate)
        {
            int column = ((int)coordinate.X / numberColumns*(int)tileSize) - 1;
            int row = ((int)coordinate.Y / numberRows*(int)tileSize) - 1;

            int[] array = new int[2] {column, row};
            return array;
           // return mapTiles[column, row];
        }

        public MapTileType TileTypeAt(Vector2 coordinate)
        {
            int[] array = WorldToMap(coordinate);
            return mapTiles[array[0], array[1]];
        }

        public bool switchTileType(Vector2 coordinate,MapTileType type)
        {
            int[] array = WorldToMap(coordinate);
            //TO ADD: check and return false if invalid
            mapTiles[array[0],array[1]] = type;
            return true;
        }

        #endregion

        #region PathFindingMethods

        public Vector2 MapToWorld(int column, int row, bool centered)
        {
            Vector2 screenPosition = new Vector2();

            if (InMap(column, row))
            {
                screenPosition.X = column * tileSize;
                screenPosition.Y = row * tileSize;
                if (centered)
                {
                    screenPosition += tileSquareCenter;
                }
            }
            else
            {
                screenPosition = Vector2.Zero;
            }
            return screenPosition;
        }

        public Vector2 MapToWorld(Point location, bool centered)
        {
            Vector2 screenPosition = new Vector2();

            if (InMap(location.X, location.Y))
            {
                screenPosition.X = location.X * tileSize;
                screenPosition.Y = location.Y * tileSize;
                if (centered)
                {
                    screenPosition += tileSquareCenter;
                }
            }
            else
            {
                screenPosition = Vector2.Zero;
            }
            return screenPosition;
        }

        private bool InMap(int column, int row)
        {
            return (row >= 0 && row < numberRows &&
                column >= 0 && column < numberColumns);
        }

        private bool IsOpen(int column, int row)
        {
            return InMap(column, row) && mapTiles[column, row] != MapTileType.MapBarrier;
        }

        public IEnumerable<Point> OpenMapTiles(Point mapLoc)
        {
            if (IsOpen(mapLoc.X, mapLoc.Y + 1))
                yield return new Point(mapLoc.X, mapLoc.Y + 1);
            if (IsOpen(mapLoc.X, mapLoc.Y - 1))
                yield return new Point(mapLoc.X, mapLoc.Y - 1);
            if (IsOpen(mapLoc.X + 1, mapLoc.Y))
                yield return new Point(mapLoc.X + 1, mapLoc.Y);
            if (IsOpen(mapLoc.X - 1, mapLoc.Y))
                yield return new Point(mapLoc.X - 1, mapLoc.Y);
        }

        public void UpdateMapViewport(Rectangle safeViewableArea)
        {
            tileSize = Math.Min(safeViewableArea.Height / (float)numberRows,
                safeViewableArea.Width / (float)numberColumns);

            scale = tileSize / (float)tileSize;
            scaleB = tileSize / (float)barrierTexture.Height;
            tileSquareCenter = new Vector2(tileSize / 2);
        }

        public static int StepDistance(Point pointA, Point pointB)
        {
            int distanceX = Math.Abs(pointA.X - pointB.X);
            int distanceY = Math.Abs(pointA.Y - pointB.Y);

            return distanceX + distanceY;
        }

        public int StepDistanceToEnd(Point point)
        {
            return StepDistance(point, endTile);
        }

        public void ReloadMap()
        {
            numberColumns = maps[currentMap].NumberColumns;
            numberRows = maps[currentMap].NumberRows;

            mapTiles = new MapTileType[maps[currentMap].NumberColumns, maps[currentMap].NumberRows];

            startTile = maps[currentMap].Start;
            mapTiles[startTile.X, startTile.Y] = MapTileType.MapStart;

            endTile = maps[currentMap].End;
            mapTiles[endTile.X, endTile.Y] = MapTileType.MapExit;

            int x = 0;
            int y = 0;

            for (int i = 0; i < maps[currentMap].Barriers.Count; i++)
            {
                x = maps[currentMap].Barriers[i].X;
                y = maps[currentMap].Barriers[i].Y;

                mapTiles[x, y] = MapTileType.MapBarrier;
            }

            mapReload = false;
        }
        #endregion

    }
}
