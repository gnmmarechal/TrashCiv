namespace CivEngineLib
{
    public class Player
    {
        private string playerName;
        private static int aiCount = 0;
        private bool isAI = false;

        public Player(string playerName)
        {
            this.playerName = playerName;
        }

        private Player()
        {
            aiCount++;
            this.playerName = "AI " + aiCount;
            this.isAI = true;
        }

        public static Player NewAIPlayer()
        {
            return new Player();
        }

        public bool IsAI()
        {
            return isAI;
        }

        public override string ToString()
        {
            return "Player: " + playerName;
        }
    }
}