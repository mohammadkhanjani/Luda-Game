using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Mench
{
    class ClsRang
    {
        int iMakan, iHome, iAval, iAkhar;
        public int[] iBaste = new int[4];
        public Color clrRang;
        bool[] blUseHome = new bool[4];

        public ClsRang(int i, Color clr)
        {
            iHome = 10 * i;
            clrRang = clr;
            for (int j = 0; j < 4; j++) iBaste[j] = -1;
        }

        void Move(int akhar, bool blHome)
        {
            if (iAval % 10 != 0)
                ClsMain.frmMain.lblKhune[iAval].BackColor = ClsMain.clrMain;
            else
                ClsMain.frmMain.lblKhune[iAval].BackColor = Color.FromArgb(100, ClsMain.clsRang[iAval / 10].clrRang);
            if (blHome)
            {
                for (int i = 0; i < 4; i++)
                    if (iBaste[i] == iAval) iBaste[i] = -1;
                blUseHome[akhar] = true;
                ClsMain.frmMain.lblKhuneEnd[iHome / 10][akhar].BackColor = clrRang;
                ClsMain.frmMain.lblKhune[iAval].BackColor = ClsMain.clrMain;

                bool tamam = true;
                foreach (bool item in blUseHome)
                    if (!item) { tamam = false; break; }
                if (tamam) MessageBox.Show("Boooooord");
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    if (iBaste[i] == iAval) { iBaste[i] = akhar; break; }
                ClsMain.frmMain.lblKhune[akhar].BackColor = clrRang;
            }
        }
        public bool Harkat(int g, int m)
        {
            iAval = m;
            iMakan = (m - iHome + 40) % 40;
            iAkhar = (iAval + g) % 40;
            bool blGoHome = false;
            bool blUse = false;
            if (iMakan + g >= 40) blGoHome = true;
            if (blGoHome)
            {
                int a = iMakan + g - 40;
                if (a < 4 && !blUseHome[a])
                {
                    Move(a, true);
                    return true;
                }
            }
            else
            {
                foreach (ClsRang item in ClsMain.clsRang)
                    foreach (int item2 in item.iBaste)
                        if (item2 == iAkhar)
                        {
                            blUse = true;
                            if (item.clrRang != clrRang)
                            {
                                item.Hazf(iAkhar);
                                Move(iAkhar, false);
                                return true;
                            } break;
                        }
                if (!blUse)
                {
                    Move(iAkhar, false);
                    return true;
                }
            }
            return false;
        }

        public bool Vorud(int i)
        {
            bool blBaste = false;
            foreach (ClsRang item in ClsMain.clsRang)
            {
                foreach (int item2 in item.iBaste)
                    if (item2 == iHome)
                    {
                        blBaste = true;
                        if (item.clrRang != clrRang)
                        {
                            item.Hazf(iHome);
                            iBaste[i] = iHome;
                            ClsMain.frmMain.lblKhune[iHome].BackColor = clrRang;
                            ClsMain.frmMain.lblKhuneHome[iHome / 10][i].BackColor = Color.FromArgb(100, clrRang);
                            ClsMain.frmMain.lblKhuneHome[iHome / 10][i].Enabled = false;
                            return true;
                        } break;
                    }
                if (blBaste) break;
            }
            if (!blBaste)
            {
                iBaste[i] = iHome;
                ClsMain.frmMain.lblKhune[iHome].BackColor = clrRang;
                ClsMain.frmMain.lblKhuneHome[iHome / 10][i].BackColor = Color.FromArgb(100, clrRang);
                ClsMain.frmMain.lblKhuneHome[iHome / 10][i].Enabled = false;
                return true;
            }
            return false;
        }
        public bool EndChange(int g, int i)
        {
            if (i + g < 4 && !blUseHome[i + g])
            {
                blUseHome[i] = false;
                blUseHome[i + g] = true;
                ClsMain.frmMain.lblKhuneEnd[iHome / 10][i].BackColor = Color.FromArgb(100, clrRang);
                ClsMain.frmMain.lblKhuneEnd[iHome / 10][i + g].BackColor = clrRang;
                return true;
            }
            return false;
        }
        public void Hazf(int a)
        {
            int i;
            for (i = 0; i < 4; i++)
                if (iBaste[i] == a) { iBaste[i] = -1; break; }
            ClsMain.frmMain.lblKhuneHome[iHome / 10][i].Enabled = true;
            ClsMain.frmMain.lblKhuneHome[iHome / 10][i].BackColor = clrRang;
        }

        public bool Mojavez(int g)
        {
            for (int i = 0; i < 4; i++)
                if (iBaste[i] != -1)
                {
                    iAval = iBaste[i];
                    iMakan = (iAval - iHome + 40) % 40;
                    iAkhar = (iAval + g) % 40;
                    bool blGoHome = false;
                    if (iMakan + g >= 40) blGoHome = true;
                    if (blGoHome)
                    {
                        int a = iMakan + g - 40;
                        if (a < 4 && !blUseHome[a])
                            return true;
                    }
                    else
                    {
                        foreach (ClsRang item in ClsMain.clsRang)
                            foreach (int item2 in item.iBaste)
                                if (item2 == iAkhar)
                                {
                                    if (item.clrRang != clrRang)
                                        return true;
                                    else return false;
                                }
                        return true;
                    }

                }
                else if (g == 6)
                {
                    bool blUse = false;
                    for (int j = 0; j < 4; j++)
                        if (iBaste[j] == iHome) { blUse = true; break; }
                    if (!blUse) return true;
                }
            return false;
        }
    }
}
