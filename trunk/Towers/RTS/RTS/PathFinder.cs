using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public enum SearchStatus
    {
        Stopped,
        Searching,
        NoPath,
        PathFound,
    }

    class PathFinder
    {
        #region SearchNode
        private struct SearchNode
        {
            /// <summary>
            /// Location on the map
            /// </summary>
            public Point Position;

            /// <summary>
            /// Distance to goal estimate
            /// </summary>
            public int DistanceToGoal;

            /// <summary>
            /// Distance traveled from the start
            /// </summary>
            public int DistanceTraveled;

            public SearchNode(
                Point mapPosition, int distanceToGoal, int distanceTraveled)
            {
                Position = mapPosition;
                DistanceToGoal = distanceToGoal;
                DistanceTraveled = distanceTraveled;
            }
        }
        #endregion

        // How much time has passed since the last search step
        private float timeSinceLastSearchStep = 0f;
        // Holds search nodes that are avaliable to search
        private List<SearchNode> openList;
        // Holds the nodes that have already been searched
        private List<SearchNode> closedList;
        // Holds all the paths we've creted so far
        private Dictionary<Point, Point> paths;
        // The map we're searching
        private Map map;
        // Seconds per search step        
        public float timeStep = 0.1f;


        #region Properties

        // Tells us if the search is stopped, started, finished or failed
        public SearchStatus SearchStatus
        {
            get { return searchStatus; }
        }
        private SearchStatus searchStatus;

        public bool IsSearching
        {
            get { return searchStatus == SearchStatus.Searching; }
            set
            {
                if (searchStatus == SearchStatus.Searching)
                {
                    searchStatus = SearchStatus.Stopped;
                }
                else if (searchStatus == SearchStatus.Stopped)
                {
                    searchStatus = SearchStatus.Searching;
                }
            }
        }

        //public int TotalSearchSteps
        //{
        //    get { return totalSearchSteps; }
        //}
        //private int totalSearchSteps = 0;

        public void Initialize(Map mazeMap)
        {
            searchStatus = SearchStatus.Stopped;
            openList = new List<SearchNode>();
            closedList = new List<SearchNode>();
            paths = new Dictionary<Point, Point>();
            map = mazeMap;
        }
        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            if (searchStatus == SearchStatus.Searching)
            {
                timeSinceLastSearchStep += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastSearchStep >= timeStep)
                {
                    DoSearchStep();
                    timeSinceLastSearchStep = 0f;
                }
            }
        }

        private void DoSearchStep()
        {
            SearchNode newOpenListNode;

            bool foundNewNode = SelectNodeToVisit(out newOpenListNode);
            if (foundNewNode)
            {
                Point currentPos = newOpenListNode.Position;
                foreach (Point point in map.OpenMapTiles(currentPos))
                {
                    SearchNode mapTile = new SearchNode(point,
                        map.StepDistanceToEnd(point),
                        newOpenListNode.DistanceTraveled + 1);
                    if (!InList(openList, point) &&
                        !InList(closedList, point))
                    {
                        openList.Add(mapTile);
                        paths[point] = newOpenListNode.Position;
                    }
                }
                if (currentPos == map.EndTile)
                {
                    searchStatus = SearchStatus.PathFound;
                }
                openList.Remove(newOpenListNode);
                closedList.Add(newOpenListNode);
            }
            else
            {
                searchStatus = SearchStatus.NoPath;
            }
        }

        public void Reset()
        {
            searchStatus = SearchStatus.Stopped;
            //totalSearchSteps = 0;
            //Scale = map.Scale;
            openList.Clear();
            closedList.Clear();
            paths.Clear();
            openList.Add(new SearchNode(map.StartTile,
                Map.StepDistance(map.StartTile, map.EndTile)
                , 0));
        }

        private static bool InList(List<SearchNode> list, Point point)
        {
            bool inList = false;
            foreach (SearchNode node in list)
            {
                if (node.Position == point)
                {
                    inList = true;
                }
            }
            return inList;
        }

        private bool SelectNodeToVisit(out SearchNode result)
        {
            result = new SearchNode();
            bool success = false;
            float smallestDistance = float.PositiveInfinity;
            float currentDistance = 0f;
            if (openList.Count > 0)
            {
                foreach (SearchNode node in openList)
                {
                    currentDistance = Heuristic(node);
                        if (currentDistance <= smallestDistance)
                        {
                            if (currentDistance < smallestDistance)
                                {
                                    success = true;
                                    result = node;
                                    smallestDistance = currentDistance;
                                }
                                else if (currentDistance == smallestDistance &&
                                    node.DistanceTraveled > result.DistanceTraveled)
                                {
                                    success = true;
                                    result = node;
                                    smallestDistance = currentDistance;
                                }
                            }
                        }
                //}
            }
            return success;
        }

        private static float Heuristic(SearchNode location)
        {
            return location.DistanceTraveled + location.DistanceToGoal;
        }

        public LinkedList<Point> FinalPath()
        {
            LinkedList<Point> path = new LinkedList<Point>();
            if (searchStatus == SearchStatus.PathFound)
            {
                Point curPrev = map.EndTile;
                path.AddFirst(curPrev);
                while (paths.ContainsKey(curPrev))
                {
                    curPrev = paths[curPrev];
                    path.AddFirst(curPrev);
                }
            }
            return path;
        }
        #endregion
    }
}
