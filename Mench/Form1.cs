using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mench
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //file exe path
        string temp_addr = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        int rand_number = 0;

        int sat = 0, sot = 6;
        public Color[] Rangha = new Color[4];
        public Label[] lblKhune = new Label[40];
        public Label[][] lblKhuneHome = new Label[4][];
        public Label[][] lblKhuneEnd = new Label[4][];

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = true;
            timer1.Enabled = true;
            button2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button1.Visible = true;
            timer1.Enabled = false;
            button1.Focus();
            dice();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            rand_number = rand.Next(1, 7);
            string temp = System.IO.Path.Combine(temp_addr, "dice");
            pictureBox1.ImageLocation = System.IO.Path.Combine(temp, (rand_number + ".png"));
        }

        void CreateLabel(int n)
        {
            lblKhune[n] = new Label();
            lblKhune[n].Size = new Size(40, 40);
            lblKhune[n].BackColor = ClsMain.clrMain;
            lblKhune[n].Location = new Point(sot * 46, sat * 46);
            lblKhune[n].Click += new EventHandler(lblKhune_Click);
            pnlAsli.Controls.Add(lblKhune[n]);
        }

        public void dice()
        {
            gam = rand_number;
            btnTas.Text = gam.ToString();
            if (!ClsMain.clsRang[Nobat].Mojavez(gam))
            {
                if (gam != 6)
                {
                    Nobat = (Nobat + 1) % 4;
                    btnTas.BackColor = Rangha[Nobat];
                }
                btnTas.Text = "";
            }
        }

        void Tarsim(int Samt, int n)
        {
            CreateLabel(n);
            switch (Samt)
            {
                case 0:
                    lblKhune[n++].BackColor = Color.FromArgb(100, Rangha[0]);
                    for (int i = 1; i <= 4; i++)
                    { sat++; CreateLabel(n++); }
                    for (int i = 1; i <= 4; i++)
                    { sot++; CreateLabel(n++); }
                    sat++; CreateLabel(n++);
                    sat++;
                    Tarsim(1, n);
                    break;
                case 1:
                    lblKhune[n++].BackColor = Color.FromArgb(100, Rangha[1]);
                    for (int i = 1; i <= 4; i++)
                    { sot--; CreateLabel(n++); }
                    for (int i = 1; i <= 4; i++)
                    { sat++; CreateLabel(n++); }
                    sot--; CreateLabel(n++);
                    sot--;
                    Tarsim(2, n);
                    break;
                case 2:
                    lblKhune[n++].BackColor = Color.FromArgb(100, Rangha[2]);
                    for (int i = 1; i <= 4; i++)
                    { sat--; CreateLabel(n++); }
                    for (int i = 1; i <= 4; i++)
                    { sot--; CreateLabel(n++); }
                    sat--; CreateLabel(n++);
                    sat--;
                    Tarsim(3, n);
                    break;
                case 3:
                    lblKhune[n++].BackColor = Color.FromArgb(100, Rangha[3]);
                    for (int i = 1; i <= 4; i++)
                    { sot++; CreateLabel(n++); }
                    for (int i = 1; i <= 4; i++)
                    { sat--; CreateLabel(n++); }
                    sot++; CreateLabel(n++);
                    break;
            }
        }
        void TarsimAsli()
        {
            for (int i = 0; i < 4; i++)
            {
                int y = 0, x = 0;
                switch (i)
                {
                    case 0: y = 0; x = 9; break;
                    case 1: y = 9; x = 9; break;
                    case 2: y = 9; x = 0; break;
                    case 3: y = 0; x = 0; break;
                }

                lblKhuneHome[i] = new Label[4];
                for (int j = 0; j < 4; j++)
                {
                    lblKhuneHome[i][j] = new Label();
                    lblKhuneHome[i][j].Size = new Size(40, 40);
                    switch (j)
                    {
                        case 0: lblKhuneHome[i][j].Location = new Point(x * 46, y * 46); break;
                        case 1: lblKhuneHome[i][j].Location = new Point(x * 46, y * 46 + 46); break;
                        case 2: lblKhuneHome[i][j].Location = new Point(x * 46 + 46, y * 46); break;
                        case 3: lblKhuneHome[i][j].Location = new Point(x * 46 + 46, y * 46 + 46); break;
                    }
                    lblKhuneHome[i][j].BackColor = Rangha[i];
                    pnlAsli.Controls.Add(lblKhuneHome[i][j]);
                    lblKhuneHome[i][j].Click += new EventHandler(lblKhuneHome_Click);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                int gamY = 0, gamX = 0, x = 0, y = 0;
                switch (i)
                {
                    case 0: gamY = 1; gamX = 0; x = 5; y = 1; break;
                    case 1: gamY = 0; gamX = -1; x = 9; y = 5; break;
                    case 2: gamY = -1; gamX = 0; x = 5; y = 9; break;
                    case 3: gamY = 0; gamX = 1; x = 1; y = 5; break;
                }

                lblKhuneEnd[i] = new Label[4];
                for (int j = 0; j < 4; j++)
                {
                    lblKhuneEnd[i][j] = new Label();
                    lblKhuneEnd[i][j].Size = new Size(40, 40);
                    lblKhuneEnd[i][j].Location = new Point(x * 46, y * 46);
                    x += gamX; y += gamY;

                    lblKhuneEnd[i][j].BackColor = Color.FromArgb(100, Rangha[i]);
                    pnlAsli.Controls.Add(lblKhuneEnd[i][j]);
                    lblKhuneEnd[i][j].Click += new EventHandler(lblKhuneEnd_Click);
                }
            }
        }

        void lblKhuneEnd_Click(object sender, EventArgs e)
        {
            if (gam != 0)
            {
                bool mosavi = false;
                int i, j = 0;
                for (i = 0; i < 4; i++)
                {
                    for (j = 0; j < 4; j++)
                        if (sender.Equals(lblKhuneEnd[i][j])) { mosavi = true; break; }
                    if (mosavi) break;
                }
                if (Nobat == i)
                    if (ClsMain.clsRang[i].EndChange(gam, j))
                    {
                        btnTas.Text = "";
                        if (gam != 6)
                        {
                            Nobat = (Nobat + 1) % 4;
                            btnTas.BackColor = Rangha[Nobat];
                        }
                        gam = 0;
                    }
            }
        }
        void lblKhuneHome_Click(object sender, EventArgs e)
        {
            if (gam != 0)
            {
                bool mosavi = false;
                int i, j = 0;
                for (i = 0; i < 4; i++)
                {
                    for (j = 0; j < 4; j++)
                        if (sender.Equals(lblKhuneHome[i][j])) { mosavi = true; break; }
                    if (mosavi) break;
                }
                if (Nobat == i && gam == 6)
                    if (ClsMain.clsRang[i].Vorud(j))
                    { btnTas.Text = ""; gam = 0; }
            }
        }
        void lblKhune_Click(object sender, EventArgs e)
        {
            if (gam != 0)
            {
                int a, i;
                for (a = 0; a < lblKhune.Length; a++)
                    if (sender.Equals(lblKhune[a])) break;
                for (i = 0; i < 4; i++)
                    if (lblKhune[a].BackColor == Rangha[i])
                    {
                        if (Nobat == i)
                            if (ClsMain.clsRang[i].Harkat(gam, a))
                            {
                                btnTas.Text = "";
                                if (gam != 6)
                                {
                                    Nobat = (Nobat + 1) % 4;
                                    btnTas.BackColor = Rangha[Nobat];
                                }
                                gam = 0;
                            }
                        break;
                    }
            }
        }

        int gam = 0; Random rnd = new Random(); int Nobat = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to mensch game that coded By Mr.Khanjani =>Enjoy the Game","Mensch",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Rangha[0] = Color.Blue; Rangha[1] = Color.Red;
            Rangha[2] = Color.Green; Rangha[3] = Color.Yellow;

            for (int i = 0; i < 4; i++)
                ClsMain.clsRang[i] = new ClsRang(i, Rangha[i]);
            Tarsim(0, 0);
            TarsimAsli();
            btnTas.BackColor = Rangha[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            help hr = new help();
            hr.ShowDialog();
        }

    }
}
