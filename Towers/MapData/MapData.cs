#region File Description
//-----------------------------------------------------------------------------
// MapData.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace PathfindingData
{
    public class MapData
    {
        public int NumberRows;
        public int NumberColumns;
        public List<Point> Start;
        public Point End;
        public List<Point> Grass;
        public List<Point> Dirt;
        public List<Point> Trees;
        public List<Point> Buildings;



        public MapData()
        {
        }

        public MapData(
            int columns, int rows, List<Point> startPosition,
            Point endPosition, List<Point> grassList,List<Point> dirtList, List<Point> treeList, List<Point> buildingList)
        {
            NumberColumns = columns;
            NumberRows = rows;
            Start = startPosition;
            End = endPosition;
            Grass = grassList;
            Dirt = dirtList;
            //Barriers = barriersList;
            Trees = treeList;
            Buildings = buildingList;
        }
    }
}
