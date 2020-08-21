using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivEngineLib;

namespace TrashCiv
{
    class Program
    {
        static void Main(string[] args)
        {
            CivEngine c = new CivEngine(true);
            Player p = new Player("Mario");
            c.AddPlayer(p);
            for (int i = 0; i < 2; i++)
            {
                c.AddPlayer(Player.NewAIPlayer());
            }
            c.StartGame();

            Console.ReadKey();
        }
    }
}
