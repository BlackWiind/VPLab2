using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPLab2
{
    public partial class Form1 : Form
    {
        bool drawing;
        bool dragging;
        private int xPos;
        private int yPos;
        GraphicsPath currentPath;
        Point oldLocation;
        Pen currentPen;

        public Form1()
        {
            InitializeComponent();
            ImageEdit.NewImage(picCanvas);
            drawing = false;
            currentPen = new Pen(Color.Black);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolNewButton_Click(object sender, EventArgs e)
        {
            ImageEdit.NewImage(picCanvas);
        }

        private void toolOpenButton_Click(object sender, EventArgs e)
        {
            ImageEdit.OpenFile(picCanvas);
        }

        private void toolSaveButton_Click(object sender, EventArgs e)
        {
            ImageEdit.SaveFile(picCanvas);
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }

            if (e.Button == MouseButtons.Right)
            {
                dragging = true;
                xPos = e.X;
                yPos = e.Y;
            }
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drawing = false;
                try
                {
                    currentPath.Dispose();
                }
                catch { };
            }

            if (e.Button == MouseButtons.Right)
            {
                dragging = false;
            }  
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Graphics g = Graphics.FromImage(picCanvas.Image);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                picCanvas.Invalidate();
            }

            if (dragging &&  sender != null)
            {
                Control c = sender as Control;

                int maxX = picCanvas.Size.Width * -1 + panel2.Size.Width;
                int maxY = picCanvas.Size.Height * -1 + panel2.Size.Height;

                int newposLeft = e.X + c.Left - xPos;
                int newposTop = e.Y + c.Top - yPos;

                if (newposTop < 0)
                {
                    newposTop = 0;
                }
                if (newposLeft < 0)
                {
                    newposLeft = 0;
                }
                if (newposLeft > maxX)
                {
                    newposLeft = maxX;
                }
                if (newposTop > maxY)
                {
                    newposTop = maxY;
                }
                c.Top = newposTop;
                c.Left = newposLeft;
               
            }
        }
    }
}
