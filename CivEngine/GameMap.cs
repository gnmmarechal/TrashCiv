using System;

namespace CivEngineLib
{
    public class GameMap
    {
        private static readonly int predefX = 100, predefY = 100;

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
                    tempGrid[j] = new Tile(Tile.TileType.Plains, new System.Collections.Generic.Dictionary<Tile.Resource, int>());
                }
                tileGrid[i] = tempGrid;
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