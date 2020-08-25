using System;
using System.Collections.Generic;
using System.Drawing;

namespace CivEngineLib
{
    public class CivEngine
    {
        private List<Player> playerList = new List<Player>();

        private bool gameStartedFlag = false;
        private bool textClientFlag = false;

        private int gameTurn = 0;
        private GameMap gameMap = null;

        public CivEngine(bool useTextClient)
        {
            this.textClientFlag = useTextClient;
        }

        public void AddPlayer(Player p)
        {
            if (!playerList.Contains(p))
                playerList.Add(p);
        }

        public void SetMap(GameMap m)
        {
            if (!gameStartedFlag)
                gameMap = m;
        }
        public void StartGame()
        {
            if (!gameStartedFlag)
            {
                Log("Started game with " + playerList.Count + " players.");
                foreach (Player p in playerList)
                {
                    Log(p);
                }

                if (gameMap == null)
                {
                    Log("Generating new map!");
                    gameMap = GameMap.GenerateMap();
                    Log("Map info: " + gameMap);
                }
                gameMap.PrintMap();
                this.RunNextTurn();
            }
        }

        public System.Drawing.Bitmap GetMapBitmap()
        {
            System.Drawing.Bitmap b = new System.Drawing.Bitmap(this.gameMap.getX(), this.gameMap.getY());
            for (int i = 0; i < this.gameMap.getX(); i++)
            {
                for (int j = 0; j < this.gameMap.getY(); j++)
                {
                    Color c = Color.Black;
                    switch(this.gameMap.GetTile(i, j).GetTileType())
                    {
                        case Tile.TileType.Water:
                            c = Color.Blue;
                            break;
                        case Tile.TileType.Plains:
                            c = Color.Yellow;
                            break;
                    }
                    b.SetPixel(i, j, c);
                }
            }
            return b;
        }

        public void RunNextTurn()
        {
            gameTurn++;
            Log("TURN: " + gameTurn);
            LogSeparator();
        }
        private void Log(object s)
        {
            if (textClientFlag)
                Console.WriteLine("" + s);
        }

        private void LogSeparator()
        {
            this.Log("===============");
        }

    }
}
