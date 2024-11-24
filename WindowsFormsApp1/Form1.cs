using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Variables pour la position et la taille du carré
        private int squareX = 50;
        private int squareY = 50;
        private int squareSize = 50;
        private int moveStep = 2; // Distance de déplacement normale
        private int acceleratedStep = 5; // Distance de déplacement avec Shift

        // Variables pour la manipulation avec la souris
        private bool isDragging = false;
        private int offsetX;
        private int offsetY;

        // Dictionnaire pour suivre l'état des touches
        private Dictionary<Keys, bool> keyStates = new Dictionary<Keys, bool>
        {
            { Keys.Up, false },
            { Keys.Down, false },
            { Keys.Left, false },
            { Keys.Right, false },
            { Keys.W, false },
            { Keys.A, false },
            { Keys.S, false },
            { Keys.D, false },
            { Keys.ShiftKey, false } // Touche Shift
        };

        // Timer pour gérer l'animation
        private Timer animationTimer;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Pour réduire le scintillement
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp); // Abonnement à l'événement MouseUp
            this.KeyDown += new KeyEventHandler(Form1_KeyDown); // Abonnement à l'événement KeyDown
            this.KeyUp += new KeyEventHandler(Form1_KeyUp); // Abonnement à l'événement KeyUp

            // Assurez-vous que le formulaire peut recevoir les entrées du clavier
            this.KeyPreview = true;

            // Initialiser et configurer le Timer
            animationTimer = new Timer();
            animationTimer.Interval = 16; // 16 ms pour environ 60 FPS
            animationTimer.Tick += new EventHandler(OnAnimationTick);
            animationTimer.Start(); // Démarrer le Timer
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Dessiner le carré
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Blue, squareX, squareY, squareSize, squareSize);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Vérifier si le clic est à l'intérieur du carré
            if (e.Button == MouseButtons.Left &&
                e.X >= squareX && e.X <= squareX + squareSize &&
                e.Y >= squareY && e.Y <= squareY + squareSize)
            {
                isDragging = true;
                offsetX = e.X - squareX;
                offsetY = e.Y - squareY;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Mettre à jour la position du carré pendant le drag
                squareX = e.X - offsetX;
                squareY = e.Y - offsetY;

                // Redessiner le formulaire pour afficher la nouvelle position du carré
                this.Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // Arrêter de dragger le carré
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Mettre à jour l'état de la touche appuyée
            if (keyStates.ContainsKey(e.KeyCode))
            {
                keyStates[e.KeyCode] = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Mettre à jour l'état de la touche relâchée
            if (keyStates.ContainsKey(e.KeyCode))
            {
                keyStates[e.KeyCode] = false;
            }
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            UpdateSquarePosition();
        }

        private void UpdateSquarePosition()
        {
            // Déterminer la vitesse de déplacement
            int currentMoveStep = keyStates[Keys.ShiftKey] ? acceleratedStep : moveStep;

            // Déplacer le carré en fonction des touches actives
            if (keyStates[Keys.Up] || keyStates[Keys.W])
            {
                squareY -= currentMoveStep;
            }
            if (keyStates[Keys.Down] || keyStates[Keys.S])
            {
                squareY += currentMoveStep;
            }
            if (keyStates[Keys.Left] || keyStates[Keys.A])
            {
                squareX -= currentMoveStep;
            }
            if (keyStates[Keys.Right] || keyStates[Keys.D])
            {
                squareX += currentMoveStep;
            }

            // Empêcher le carré de sortir des limites du formulaire
            if (squareX < 0) squareX = 0;
            if (squareY < 0) squareY = 0;
            if (squareX + squareSize > this.ClientSize.Width) squareX = this.ClientSize.Width - squareSize;
            if (squareY + squareSize > this.ClientSize.Height) squareY = this.ClientSize.Height - squareSize;

            // Redessiner le formulaire pour afficher la nouvelle position du carré
            this.Invalidate();
        }
    }
}