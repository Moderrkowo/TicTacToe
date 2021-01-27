using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kolko_I_Krzyzyk
{
    public partial class TicTacToeForm : Form
    {
        // Variables
        private List<Button> _Buttons;
        private bool _Movement = false; // true = X, false = O

        // Contstructor
        public TicTacToeForm()
        {
            InitializeComponent();
            _Buttons = new List<Button>()
            {
                button1,
                button2,
                button3,
                button4,
                button5,
                button6,
                button7,
                button8,
                button9
            };
            ChangePlayerGameMovement();
            StartNewGame();
        }

        // Methods
        #region TicTacToe Buttons Methods

        private void CheckButton(Button btn)
        {
            // Set Button
            btn.Text = GetPlayerSymbol(_Movement).ToString();
            btn.Enabled = false;

            // Variables
            string tekstWygranej = null;
            bool czyKtosWygral = CheckBoardWinner();

            // Check Ending
            if (czyKtosWygral)
            {
                tekstWygranej = string.Format("Wygrał gracz {0}! Chcesz rozpocząć jeszcze raz?", GetPlayerSymbol(_Movement));
            }
            else
            {
                if (IsAllButtonsClickedOnBoard())
                {
                    tekstWygranej = string.Format("Nikt nie wygrał.. Chcesz rozpocząć jeszcze raz?", GetPlayerSymbol(_Movement));
                }
            }
            if(tekstWygranej != null)
            {
                SetAllButtons(false);
                DialogResult result = MessageBox.Show(null, tekstWygranej, "Koniec gry", MessageBoxButtons.OK);
                btnNewGame.Visible = true;
                SetBoardColorWin();
            }
            else
            {
                ChangePlayerGameMovement();
            }
        }
        private void StartNewGame()
        {
            SetAllButtons(true);
            //_Movement = false;
            ChangePlayerGameMovement();
        }
        private void SetAllButtons(bool enabled)
        {
            foreach(Button btns in _Buttons)
            {
                btns.Enabled = enabled;
                if (enabled)
                {
                    btns.Text = "";
                    btns.ForeColor = Color.FromArgb(255, 255, 255);
                }
                continue;
            }
        }
        private bool IsAllButtonsClickedOnBoard()
        {
            int clicked = 0;
            foreach (Button btns in _Buttons)
            {
                if (!btns.Enabled)
                {
                    clicked++;
                }
            }
            return clicked == _Buttons.Count;
        }
        private void SetBoardColorWin()
        {
            Color winColor = Color.FromArgb(231, 76, 60);
            if (button1.Text == button2.Text && button2.Text == button3.Text && button1.Text != "")
            {
                button1.ForeColor = winColor;
                button2.ForeColor = winColor;
                button3.ForeColor = winColor;
            }
            else if (button4.Text == button5.Text && button5.Text == button6.Text && button4.Text != "")
            {
                button4.ForeColor = winColor;
                button5.ForeColor = winColor;
                button6.ForeColor = winColor;
            }
            else if (button7.Text == button8.Text && button8.Text == button9.Text && button7.Text != "")
            {
                button7.ForeColor = winColor;
                button8.ForeColor = winColor;
                button9.ForeColor = winColor;
            }
            else if (button1.Text == button4.Text && button4.Text == button7.Text && button4.Text != "")
            {
                button1.ForeColor = winColor;
                button4.ForeColor = winColor;
                button7.ForeColor = winColor;
            }
            else if (button2.Text == button5.Text && button5.Text == button8.Text && button5.Text != "")
            {
                button2.ForeColor = winColor;
                button5.ForeColor = winColor;
                button8.ForeColor = winColor;
            }
            else if (button3.Text == button6.Text && button6.Text == button9.Text && button6.Text != "")
            {
                button3.ForeColor = winColor;
                button6.ForeColor = winColor;
                button9.ForeColor = winColor;
            }
            else if (button1.Text == button5.Text && button5.Text == button9.Text && button5.Text != "")
            {
                button1.ForeColor = winColor;
                button5.ForeColor = winColor;
                button9.ForeColor = winColor;
            }
            else if (button3.Text == button5.Text && button5.Text == button7.Text && button5.Text != "")
            {
                button3.ForeColor = winColor;
                button5.ForeColor = winColor;
                button7.ForeColor = winColor;
            }
        }
        private bool CheckBoardWinner()
        {
            Color winColor = Color.FromArgb(231, 76, 60);
            if (button1.Text == button2.Text && button2.Text == button3.Text && button1.Text != "")
            {
                button1.ForeColor = winColor;
                button2.ForeColor = winColor;
                button3.ForeColor = winColor;
                return true;
            }
            else if (button4.Text == button5.Text && button5.Text == button6.Text && button4.Text != "")
            {
                button4.ForeColor = winColor;
                button5.ForeColor = winColor;
                button6.ForeColor = winColor;
                return true;
            }
            else if (button7.Text == button8.Text && button8.Text == button9.Text && button7.Text != "")
            {
                button7.ForeColor = winColor;
                button8.ForeColor = winColor;
                button9.ForeColor = winColor;
                return true;
            }
            else if (button1.Text == button4.Text && button4.Text == button7.Text && button4.Text != "")
            {
                button1.ForeColor = winColor;
                button4.ForeColor = winColor;
                button7.ForeColor = winColor;
                return true;
            }
            else if (button2.Text == button5.Text && button5.Text == button8.Text && button5.Text != "")
            {
                button2.ForeColor = winColor;
                button5.ForeColor = winColor;
                button8.ForeColor = winColor;
                return true;
            }
            else if (button3.Text == button6.Text && button6.Text == button9.Text && button6.Text != "")
            {
                button3.ForeColor = winColor;
                button6.ForeColor = winColor;
                button9.ForeColor = winColor;
                return true;
            }
            else if (button1.Text == button5.Text && button5.Text == button9.Text && button5.Text != "")
            {
                button1.ForeColor = winColor;
                button5.ForeColor = winColor;
                button9.ForeColor = winColor;
                return true;
            }
            else if (button3.Text == button5.Text && button5.Text == button7.Text && button5.Text != "")
            {
                button3.ForeColor = winColor;
                button5.ForeColor = winColor;
                button7.ForeColor = winColor;
                return true;
            }
            else
            {
                return false;
            }
        }
        private char GetPlayerSymbol(bool czyjRuch)
        {
            switch (czyjRuch)
            {
                case true:
                    return 'X';
                case false:
                    return 'O';
                default:
                    return '-';
            }
        }
        private void ChangePlayerGameMovement()
        {
            _Movement = !_Movement;
            labelKtoMaRuch.Text = string.Format("Kto ma ruch: {0}", GetPlayerSymbol(_Movement));
        }

        #endregion

        // Buttons
        #region TicTacToe Buttons

        // All buttons click
        private void btnBoard_Click(object sender, EventArgs e)
        {
            CheckButton((Button)sender);
        }
        private void btnBoard_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Enabled)
            {
                btn.ForeColor = Color.FromArgb(127, 140, 141);
                btn.Text = GetPlayerSymbol(_Movement).ToString();
            }
        }
        private void btnBoard_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Enabled)
            {
                btn.ForeColor = Color.FromArgb(255, 255, 255);
                btn.Text = "";
            }
        }
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            btnNewGame.Visible = false;
            StartNewGame();
        }

        #endregion
    }
}
