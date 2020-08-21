using System.Collections.Generic;

namespace CivEngineLib
{
    public class Tile
    {
        private TileType tileType;
        private Dictionary<Resource, int> resourceList;
        private Tile upNeighbour, downNeighbour, leftNeighbour, rightNeighbour, upLeftNeighbour, downLeftNeighbour, upRightNeighbour, downRightNeighbour;

        public enum NeighbourDirection
        {
            Up, Down, Left, Right, UpRight, DownRight, UpLeft, DownLeft
        }
        public enum TileType
        {
            Plains, Water
        }

        public enum Resource
        {
            Iron, Fish
        }
        public Tile(TileType tileType, Dictionary<Resource, int> resourceList)
        {
            this.tileType = tileType;
        }

        public void SetResource(Resource r, int count)
        {
            resourceList.Add(r, count);
        }

        public Tile GetNeighbour(NeighbourDirection n)
        {
            switch (n)
            {
                case NeighbourDirection.Up:
                    return upNeighbour;
                case NeighbourDirection.Down:
                    return downNeighbour;
                case NeighbourDirection.Left:
                    return leftNeighbour;
                case NeighbourDirection.Right:
                    return rightNeighbour;
                case NeighbourDirection.UpRight:
                    return upRightNeighbour;
                case NeighbourDirection.UpLeft:
                    return upLeftNeighbour;
                case NeighbourDirection.DownRight:
                    return downRightNeighbour;
                case NeighbourDirection.DownLeft:
                    return downLeftNeighbour;
            }
            return null;
        }

        public override string ToString()
        {
            return "[" + this.tileType + "]";
        }

    }
}