using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CivEngineLib
{
    public class GameMap
    {
        private static readonly int predefX = 50, predefY = 10;

        private Tile[][] tileGrid;
        private long mapSeed;

        public GameMap(long seed, int sizeX, int sizeY)
        {
            this.mapSeed = seed;
            tileGrid = new Tile[sizeX][];

            // Fill the map
            
            for (int i = 0; i < sizeX; i++)
            {
                Tile[] tempGrid = new Tile[sizeY];
                for (int j = 0; j < sizeY; j++)
                {
                    tempGrid[j] = new Tile(Tile.TileType.Plains, new Dictionary<Tile.Resource, int>());
                }
                tileGrid[i] = tempGrid;
            }

            // Link the nodes
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Right, tileGrid[i][j + 1]);
                    } catch (Exception)
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Right, tileGrid[i][0]);
                    }

                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Left, tileGrid[i][j - 1]);
                    }
                    catch (Exception)
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Left, tileGrid[i][sizeY-1]);
                    }

                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Up, tileGrid[i-1][j]);
                    }
                    catch (Exception)
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Up, tileGrid[sizeX-1][j]);
                    }

                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Down, tileGrid[i+1][j]);
                    }
                    catch (Exception)
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.Down, tileGrid[0][j]);
                    }

                }
            }

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.DownLeft, tileGrid[i][j].GetNeighbour(Tile.NeighbourDirection.Down).GetNeighbour(Tile.NeighbourDirection.Left));
                    } catch (Exception)
                    {

                    }
                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.UpLeft, tileGrid[i][j].GetNeighbour(Tile.NeighbourDirection.Up).GetNeighbour(Tile.NeighbourDirection.Left));
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.UpRight, tileGrid[i][j].GetNeighbour(Tile.NeighbourDirection.Up).GetNeighbour(Tile.NeighbourDirection.Right));
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        tileGrid[i][j].SetNeighbour(Tile.NeighbourDirection.DownRight, tileGrid[i][j].GetNeighbour(Tile.NeighbourDirection.Down).GetNeighbour(Tile.NeighbourDirection.Right));
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            // Create other features and populate tiles with resources and stuff

            // Generate water: Pick a center and go around to build a blob
            Random r = new Random((int)seed);
            int oceanCount = r.Next(5);
            for (int i = 0; i < oceanCount; i++)
            {
                int currentX = r.Next(sizeX);
                int currentY = r.Next(sizeY);
                int[] ogCoords = { currentX, currentY };
                int oceanRadius = r.Next(4); // r.Next(3)
                Console.WriteLine("{0} : {1}", currentX, currentY);
                SetTileRecursive(Tile.TileType.Water, 50, oceanRadius, tileGrid[currentX][currentY]);
            }
        }

        private void SetTileRecursive(Tile.TileType type, int probability, int maxRadius, Tile startingTile)
        {
            if (maxRadius < 0)
                return;
            if (startingTile != null)
            {
                int rSet = new Random().Next(100);
                //Console.WriteLine(rSet);
                Thread.Sleep(100);
                if (rSet < probability)
                    startingTile.SetType(type);
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.Down) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.Down).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.Down));
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.Up) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.Up).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.Up));
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.Left) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.Left).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.Left));
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.Right) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.Right).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.Right));
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.DownLeft) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.DownLeft).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.DownLeft));
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.DownRight) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.DownRight).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.DownRight));
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.UpRight) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.UpRight).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.UpRight));
                if (startingTile.GetNeighbour(Tile.NeighbourDirection.UpLeft) != null && startingTile.GetNeighbour(Tile.NeighbourDirection.UpLeft).GetTileType() != type)
                    SetTileRecursive(type, probability, maxRadius - 1, startingTile.GetNeighbour(Tile.NeighbourDirection.UpLeft));
            }
        }
        public string PrintMap()
        {
            for (int i = 0; i < tileGrid.Length; i++)
            {
                string s = "";
                for (int j = 0; j < tileGrid[0].Length; j++)
                {
                    s += tileGrid[i][j];
                }
                Console.WriteLine(s);
            }
            return this.ToString();
        }
        public static GameMap GenerateMap()
        {
            Random r = new Random();
            long mapSeed = LongRandom(0L, long.MaxValue, r);
            return GenerateMap(mapSeed);
        }

        public static GameMap GenerateMap(long seed)
        {
            return new GameMap(seed, predefX, predefY);
        }

        // https://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way
        private static long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        public override string ToString()
        {
            return "Map Seed: " + this.mapSeed;
        }
    }
}