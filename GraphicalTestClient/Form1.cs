using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CivEngineLib;

namespace GraphicalTestClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CivEngine c = new CivEngine(true);
            Player p = new Player("Mario");
            c.AddPlayer(p);
            for (int i = 0; i < 2; i++)
            {
                c.AddPlayer(Player.NewAIPlayer());
            }
            c.StartGame();

            pictureBox1.Image = c.GetMapBitmap();
        }
    }
}
