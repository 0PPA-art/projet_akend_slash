using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace outiljeux2D
{
    public partial class Form1 : Form
    {

        private List<Color> colors = new List<Color> { Color.Red, Color.Green, Color.Blue, Color.Black };

        private int currentColorIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // Variable pour vérifier si la souris est enfoncée
        private bool isMouseDown = false;

        // Variable pour la couleur actuelle
        private Color currentColor = Color.Black;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Active le dessin
            isMouseDown = true;
            DrawPoint(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                DrawPoint(e.X, e.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // Désactive le dessin
            isMouseDown = false;
        }

        private void DrawPoint(int x, int y)
        {
            using (Graphics g = this.CreateGraphics())
            {
                SolidBrush brush = new SolidBrush(currentColor);
                g.FillEllipse(brush, x - 2, y - 2, 4, 4); // Point de 4x4 pixels
            }
        }

        private void ChangeColorButton_Click(object sender, MouseEventArgs e)
        {
            // Ouvre une boîte de dialogue pour choisir une couleur
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentColor = colorDialog.Color; // Met à jour la couleur actuelle
                }
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            isMouseDown = true;
            isMouseDown = false;

            // Passe à la couleur suivante
            currentColorIndex = (currentColorIndex + 1) % colors.Count;
            currentColor = colors[currentColorIndex];
        }

    }
}
