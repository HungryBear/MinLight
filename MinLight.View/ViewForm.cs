using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MinLight.View
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using MinLight.Entities;
    using MinLight.Managers;

    public partial class ViewForm : Form
    {
        public SceneManager Manager;

        public int iteration;

        public SceneContext context;

        public Image imagePlane;

        public Sampler rnd;

        private bool renderStarted, hasImage;

        public ViewForm(string fileName)
        {
            InitializeComponent();
            Manager = new SceneManager();
            context = Manager.LoadScene(fileName, ctlCanvas.ClientRectangle.Width, ctlCanvas.ClientRectangle.Height);
            this.imagePlane = context.Image;
            iteration = 0;
            rnd = new DotNetSampler();
            ctlCanvas.Image = new Bitmap(ctlCanvas.ClientRectangle.Width, ctlCanvas.ClientRectangle.Height);

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                renderStarted = !renderStarted;
            }
        }

        public void OnIndle(Object sender, EventArgs args)
        {
            while (AppStillIdle & renderStarted)
            {
                this.RenderIteration();
            }
        }

        private void RenderIteration()
        {
            var sw = Stopwatch.StartNew();

            this.hasImage = false;
            this.context.Camera.GetFrame(this.context.Scene, this.rnd, this.imagePlane);
            this.iteration++;

            this.hasImage = true;
            this.UpdateCanvas((Bitmap)ctlCanvas.Image);
            this.Refresh();
            sw.Stop();
            this.Text = string.Format("{0} SPP {1} Rays {2:F3} KRays / sec ", this.iteration, StatsCounter.RaysTraced, 0.001*StatsCounter.RaysTraced / sw.Elapsed.TotalSeconds);
            StatsCounter.Reset();
        }

        private bool AppStillIdle
        {
            get
            {
                PeekMsg msg;
                return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PeekMsg
        {
            public IntPtr hWnd;
            public Message msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out PeekMsg msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        private void ctlCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!hasImage) return;

            Bitmap img = null;

            if (ctlCanvas.Image == null)
            {
                ctlCanvas.Image = img = new Bitmap(ctlCanvas.ClientRectangle.Width, ctlCanvas.ClientRectangle.Height);
            }
            else
            {
                img = (Bitmap)ctlCanvas.Image;
            }
            this.UpdateCanvas(img);
            ctlCanvas.Update();
        }

        private void UpdateCanvas(Bitmap img)
        {
            var imageData = this.imagePlane.GetImageBytes(this.iteration);
            for (int y = 0; y < this.ctlCanvas.ClientRectangle.Height; y++)
            {
                for (int x = 0; x < this.ctlCanvas.ClientRectangle.Width; x++)
                {
                    img.SetPixel(x, y, Color.FromArgb(imageData[x, y, 0], imageData[x, y, 1], imageData[x, y, 2]));
                }
            }
        }
    }
}
