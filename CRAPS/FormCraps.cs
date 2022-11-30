using System;
using System.IO;
using System.Windows.Forms;

namespace CRAPS
{
    public partial class FormCraps : Form
    {
        public FormCraps()
        {
            InitializeComponent();
        }

        //Indicates how many throws had this game.
        int throws = 0;
        //Indicates when the player should have stopped.
        int throwStop = 0;
        
        int thePoint = 0;
        bool won;
        bool gameOver;

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            string line = "";
            int lineCount = 0;

            string path = Application.StartupPath + @"\craps.txt";
            StreamReader streamReader = new StreamReader(path);

            bool finished = false;

            while (!finished)
            {
                line = streamReader.ReadLine();

                if (line == null)
                {
                    finished = true;
                }
                else
                {
                    lineCount++;
                    CheckGameResult(line);
                    TxtResult.Text += "#" + lineCount + ": " + Display() + Environment.NewLine;
                }
            }
        }

        private void CheckGameResult(string input)
        {
            gameOver = false;
            won = false;

            string[] rolls = input.Split(' ');
            throws = Convert.ToInt32(rolls[0]);
            thePoint = Convert.ToInt32(rolls[1]);

            //If the first throw was 7 or 11 the player won.
            //If it was 2, 3 or 12 he lost.
            if (Convert.ToInt32(rolls[1]) == 7 || Convert.ToInt32(rolls[1]) == 11)
            {
                won = true;
                gameOver = true;
                throwStop = 1;
            }
            else if (Convert.ToInt32(rolls[1]) == 2 || Convert.ToInt32(rolls[1]) == 3 || Convert.ToInt32(rolls[1]) == 12)
            {
                won = false;
                gameOver = true;
                throwStop = 1;
            }
            else
            {
                for (int i = 2; i < rolls.Length; i++)
                {
                    if (Convert.ToInt32(rolls[i]) == thePoint)
                    {
                        won = true;
                        gameOver = true;
                        throwStop = i;
                    }
                    else if (Convert.ToInt32(rolls[i]) == 7)
                    {
                        won = false;
                        gameOver = true;
                        throwStop = i;
                    }
                }
            }   
        }

        private string Display()
        {
            string result = "";

            if (won == true && gameOver == true)
            {
                result = throws == throwStop ? "Won!" : "Won, stop rolling!";
            }
            else if (won == false && gameOver == true)
            {
                result = throws == throwStop ? "Lost!" : "Lost, stop rolling!";
            }
            else if (gameOver == false)
            {
                result = "Keep rolling!";
            }

            return result;
        }
    }
}
