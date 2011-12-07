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
        MapDirt,
        MapGrass,
        MapTree,
        MapLamp,
        MapBarrier,
        MapStart,
        MapExit
    }

    public class Map
    {
        #region Fields

        private Vector2 tileSquareCenter;
        private List<Texture2D> endList = new List<Texture2D>();
        //private Texture2D tileTexture;
        //private Texture2D endTexture;
        //private Vector2 dotTextureCenter;
        //private Texture2D barrierTexture;
        //private Texture2D treeTexture;

        private EndPoint endp;
        public EndPoint EndPt
        {
            get { return endp; }
        }


        private List<MapData> maps;
        private MapTileType[,] mapTiles;
        private int currentMap = 0;
        private int numberColumns;
        private int numberRows;

        Random rand = new Random();

        #endregion
        #region Properties

        public float TileSize
        {
            get { return tileSize; }
        }
        private float tileSize;

        public List<Point> StartTile
        {
            get { return startTile; }
        }
        private List<Point> startTile;

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

        public List<Point> getTrees()
        {
            return maps[currentMap].Trees;
        }

        public List<Point> getLamps()
        {
            return maps[currentMap].Lamps;
        }

        public Vector2 getBaseCoordinate()
        {
            return MapToWorld(maps[currentMap].End, true);
        }

        #endregion

        #region Initialization

        public void LoadContent(ContentManager content)
        {
            //endTexture = content.Load<Texture2D>("Tower1");
            maps = new List<MapData>();
            maps.Add(content.Load<MapData>("map1"));
            maps.Add(content.Load<MapData>("map2"));
            maps.Add(content.Load<MapData>("map3"));
            maps.Add(content.Load<MapData>("map4"));

            endList.Add(content.Load<Texture2D>("house"));
            endList.Add(content.Load<Texture2D>("house"));
            endList.Add(content.Load<Texture2D>("house"));

            ReloadMap();

            mapReload = true;
            tileSquareCenter = new Vector2(tileSize / 2);
        }
        #endregion

        public Rectangle endRectangle()
        {
            Vector2 tmp = MapToWorld(endTile,false);
            Rectangle rct = new Rectangle((int)tmp.X - endList[currentMap].Width/5, (int)tmp.Y, (int)(endList[currentMap].Width*0.75), endList[currentMap].Height / 6);
            return rct;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(endList[currentMap], MapToWorld(endTile, true), null, Color.White, 0f, new Vector2(endList[currentMap].Width/2, endList[currentMap].Height-25), 1.0f, SpriteEffects.None, 0f);
        }
        //    for (int i = 0; i < numberRows; i++)
        //    {
        //        for (int j = 0; j < numberColumns; j++)
        //        {
        //            Vector2 tilePosition = MapToWorld(j, i, false);
        //            switch (mapTiles[j, i])
        //            {
        //                //case MapTileType.MapBarrier:
        //                //    spriteBatch.Draw(
        //                //        barrierTexture, tilePosition, null, Color.White,
        //                //        0f, Vector2.Zero, scaleB, SpriteEffects.None, .25f);
        //                //    break;
        //
        //                case MapTileType.MapExit:
        //                    spriteBatch.Draw(
        //                        endTexture, tilePosition + tileSquareCenter, null,
        //                        Color.White, 0f, dotTextureCenter, scale,
        //                        SpriteEffects.None, .25f);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    //DrawTrees(spriteBatch);
        //}

        #region Useful Game Methods

        public string TileString(Vector2 coordinate)
        {
            int[] array = WorldToMap(coordinate);
            if (mapTiles[array[0], array[1]] == MapTileType.MapGrass)
            {
                return "GRASS";
            }
            else if (mapTiles[array[0], array[1]] == MapTileType.MapTree)
            {
                return "TREE";
            }
            else if (mapTiles[array[0], array[1]] == MapTileType.MapDirt)
            {
                return "DIRT";
            }
            else if (mapTiles[array[0], array[1]] == MapTileType.MapExit)
            {
                return "BASE";
            }
            else if (mapTiles[array[0], array[1]] == MapTileType.MapStart)
            {
                return "START";
            }
            else return "EMPTY";
        }

        public int[] WorldToMap(Vector2 coordinate)
        {
            int column = (int)(coordinate.X / tileSize);
            int row = (int)(coordinate.Y / tileSize);


            if (InMap(column, row))
            {
                int[] array = new int[2] { column, row };
                return array;
            }
            else
            {
                int[] array = new int[2] { numberColumns - 1, numberRows - 1 };
                return array;
            }
        }

        public MapTileType TileTypeAt(Vector2 coordinate)
        {
            int[] array = WorldToMap(coordinate);
            return mapTiles[array[0], array[1]];
        }

        public bool switchTileType(Vector2 coordinate, MapTileType type)
        {
            int[] array = WorldToMap(coordinate);
            //TO ADD: check and return false if invalid
            mapTiles[array[0], array[1]] = type;
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
            return InMap(column, row) && (mapTiles[column, row] == MapTileType.MapDirt || mapTiles[column, row] == MapTileType.MapStart || mapTiles[column, row] == MapTileType.MapExit);
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

            scaleB = tileSize / (float)tileSize;
            //scaleB = tileSize / (float)barrierTexture.Height;
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

        public void NextMap(int currentLevel)
        {
            if (currentMap < maps.Count()-1) currentMap++;
            ReloadMap();
            mapReload = true;     
            tileSquareCenter = new Vector2(tileSize / 2);
        }

        public void ReloadMap()
        {
            numberColumns = maps[currentMap].NumberColumns;
            numberRows = maps[currentMap].NumberRows;

            mapTiles = new MapTileType[maps[currentMap].NumberColumns, maps[currentMap].NumberRows];


            startTile = maps[currentMap].Start;
            foreach (Point i in startTile)
            {
                mapTiles[i.X, i.Y] = MapTileType.MapStart;
            }

            endTile = maps[currentMap].End;
            mapTiles[endTile.X, endTile.Y] = MapTileType.MapExit;

            int x = 0;
            int y = 0;

            for (int i = 0; i < maps[currentMap].Grass.Count; i++)
            {
                x = maps[currentMap].Grass[i].X;
                y = maps[currentMap].Grass[i].Y;

                mapTiles[x, y] = MapTileType.MapGrass;
            }
            for (int i = 0; i < maps[currentMap].Dirt.Count; i++)
            {
                x = maps[currentMap].Dirt[i].X;
                y = maps[currentMap].Dirt[i].Y;

                mapTiles[x, y] = MapTileType.MapDirt;
            }
            for (int i = 0; i < maps[currentMap].Trees.Count; i++)
            {
                x = maps[currentMap].Trees[i].X;
                y = maps[currentMap].Trees[i].Y;

                mapTiles[x, y] = MapTileType.MapTree;
            }
            for (int i = 0; i < maps[currentMap].Lamps.Count; i++)
            {
                x = maps[currentMap].Lamps[i].X;
                y = maps[currentMap].Lamps[i].Y;

                mapTiles[x, y] = MapTileType.MapLamp;
            }
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
