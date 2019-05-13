﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lokanta_Otomasyonu
{
    public partial class frmSiparis : Form
    {
        public frmSiparis()
        {
            InitializeComponent();
        }


        //Hesap Makinesi
        int tableId; int AdditionId;
        private void Form1_Load(object sender, EventArgs e)
        {
            // Murat gönderince kontorl et
            lblMasaNo.Text = cGenel.ButtonValue;

            cMasalar ms = new cMasalar();
            tableId = ms.TableGetbyNumber(cGenel._ButtonName);
            if (ms.TableGetbyState(tableId, 2) == true)
            {
                cAdisyon Ad = new cAdisyon();
                AdditionId = Ad.getByAddition(tableId);
                cSiparis orders = new cSiparis();
                orders.getByOrder(lvSiparisler, AdditionId);
            }
            // Buraya kadar kontrol et

            btn1.Click += new EventHandler(islem);
            btn2.Click += new EventHandler(islem);
            btn3.Click += new EventHandler(islem);
            btn4.Click += new EventHandler(islem);
            btn5.Click += new EventHandler(islem);
            btn6.Click += new EventHandler(islem);
            btn7.Click += new EventHandler(islem);
            btn8.Click += new EventHandler(islem);
            btn9.Click += new EventHandler(islem);
            btn0.Click += new EventHandler(islem);

        }
        void islem(Object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btn1":
                    txtAdet.Text += (1).ToString();
                    break;
                case "btn2":
                    txtAdet.Text += (2).ToString();
                    break;
                case "btn3":
                    txtAdet.Text += (3).ToString();
                    break;
                case "btn4":
                    txtAdet.Text += (4).ToString();
                    break;
                case "btn5":
                    txtAdet.Text += (5).ToString();
                    break;
                case "btn6":
                    txtAdet.Text += (6).ToString();
                    break;
                case "btn7":
                    txtAdet.Text += (7).ToString();
                    break;
                case "btn8":
                    txtAdet.Text += (8).ToString();
                    break;
                case "btn9":
                    txtAdet.Text += (9).ToString();
                    break;
                case "btn0":
                    txtAdet.Text += (0).ToString();
                    break;
                default:
                    MessageBox.Show("Sayı Gir");
                    break;
            }

        }

        cUrunCesitleri Uc = new cUrunCesitleri();

        private void btnAnaYemek1_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnAnaYemek1);
        }

        private void btnCorba2_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnCorba2);
        }

        private void btnSalata3_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnSalata3);
        }

        private void btnİcecek4_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnİcecek4);
        }

        private void btnTatlilar5_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnTatlilar5);
        }

        private void btnKahvalti6_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnKahvalti6);
        }

        private void btnFirin7_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnFirin7);
        }

        private void btnBalik8_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnBalik8);
        }

        int sayac = 0; int sayac2 = 0;
        private void lvMenu_DoubleClick(object sender, EventArgs e)
        {
            if (txtAdet.Text == "")
            {
                txtAdet.Text="1";
            }
            if (lvMenu.Items.Count > 0)
            {
                sayac = lvSiparisler.Items.Count;
                lvSiparisler.Items.Add(lvMenu.SelectedItems[0].Text);
                lvSiparisler.Items[sayac].SubItems.Add(txtAdet.Text);
                lvSiparisler.Items[sayac].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvSiparisler.Items[sayac].SubItems.Add((Convert.ToDecimal(lvMenu.SelectedItems[0].SubItems[1].Text) * Convert.ToDecimal(txtAdet.Text)).ToString());
                lvSiparisler.Items[sayac].SubItems.Add("0");
                sayac2 = lvYeniEklenenler.Items.Count;
                lvSiparisler.Items[sayac].SubItems.Add(sayac2.ToString());
              
                lvYeniEklenenler.Items.Add(AdditionId.ToString());
                lvYeniEklenenler.Items[sayac2].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvYeniEklenenler.Items[sayac2].SubItems.Add(txtAdet.Text);
                lvYeniEklenenler.Items[sayac2].SubItems.Add(tableId.ToString());
                lvYeniEklenenler.Items[sayac2].SubItems.Add(sayac2.ToString());

                sayac2++;

                txtAdet.Text = "";

            }

        }

        ArrayList silinler = new ArrayList();

        private void btnSiparis_Click(object sender, EventArgs e)
        {
            /**
             1 - Masa Boş
             2 - Masa Dolu
             3 - Masa Rezerve
             4 - Dolu Rezerve
            */
            cMasalar masa = new cMasalar();
            frmMasalar ms = new frmMasalar();
            cAdisyon newAddition = new cAdisyon();
            cSiparis saveOrder = new cSiparis();
           
            bool sonuc = false;
            if (masa.TableGetbyState(tableId, 1) == true)
            {
                newAddition.ServisTurNo = 1;
                

                newAddition.PersonelId = 1;
                newAddition.MasaId = tableId;
                newAddition.Tarih = DataTime.Now;
                sonuc = newAddition.setByAdditionNew(newAddition);
                masa.setChangeTableState(cGenel.ButtonName, 2);

                if (lvSiparisler.Items.Count > 0)
                {
                    for (int = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        saveOrder.AdisyonID = newAddition.getByAddition(tabelId);
                        saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        saveOrder.setSaveOrder(saveOrder);
                    }
                    this.Close();
                    ms.Show();
                    
                }
            }
            else if (masa.TableGetbyState(tableId, 2) == true || masa.TableGetbyState(tableId, 4) == true)
            {
                
                if (lvYeniEklenenler.Items.Count > 0)
                {
                    for (int i = 0; i < lvYeniEklenenler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        saveOrder.AdisyonID = newAddition.getByAddition(tabelId);
                        saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        saveOrder.setSaveOrder(saveOrder);
                    }
                    cGenel._AdisyonId = Convert.ToString(newAddition.getByAddition(tabelId));
                }
                if (silinler.Count > 0)
                {
                    foreach (string item in silinler)
                    {
                        saveOrder.set.DeleteOrder(Convert.ToInt32(item));
                    }
                }

                this.Close();
                ms.Show();
            }
            else if (masa.TableGetbyState(tableId, 3) == true )
            {
                
                newAddition.ServisTurNo = 1;
                newAddition.PersonelId = 1;
                newAddition.MasaId = tableId;
                newAddition.Tarih = DataTime.Now;
                sonuc = newAddition.setByAdditionNew(newAddition);
                masa.setChangeTableState(cGenel._ButtonName, 4);

                if (lvSiparisler.Items.Count > 0)
                {
                    for (int = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        saveOrder.AdisyonID = newAddition.getByAddition(tabelId);
                        saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        saveOrder.setSaveOrder(saveOrder);
                    }
                    this.Close();
                    ms.Show();
                    
                }
            }

           


        }

        private void lvSiparisler_DoubleClick(object sender, EventArgs e)
        {
            if (lvSiparisler.Items.Count > 0)
            {
                if (lvSiparisler.SelectedItems[0].SubItems[4].Text != "0")
                {
                    cSiparis saveOrder = new cSiparis();
                    saveOrder.setDeleteOrder(Convert.ToInt32(lvSiparisler.Items[0].SubItems[4].Text));
                }
                else
                {
                    for (int i = 0; i < lvYeniEklenenler.Items.Count; i++)
                    {
                        if (lvYeniEklenenler.Items[i].SubItems[4].Text == lvSiparisler.SelectedItems[0].SubItems[5].Text)
                        {
                            lvYeniEklenenler.Items.RemoveAt(i);
                        }
                    }
 
                }
            }

            lvSiparisler.Items.RemoveAt(lvSiparisler.SelectedItems[0].Index);
        }
        

        //cMasalar'a bu metodu ekle.

        public void setChangeTableState(string ButonName, int state)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update masalar Set DURUM=@Durum where ID=@MasaNo ", con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string aa = ButonName;
            int uzunluk = aa.Length;
            cmd.Parameters.Add("@Durum", sqlDbType.Int).Value = state;
            cmd.Parameters.Add("@MasaNo", sqlDbType.Int).Value = aa.Substring(uzunluk - 1, 1);
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            if (txtAra.Text == "")
            {
                txtAra.Text = "";
            }
            else
            {
                cUrunCesitleri cu = new cUrunCesitleri();
                cu.getByProductSearch(lvMenu, Convert.ToInt32(txtAra.Text));
            }
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            cGenel._ServisTurNo = 1;
            cGenel._AdisyonId = AdditionId.ToString();
            frmBill frm = new frmBill();
            this.Close();
            frm.Show();
        }


        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private void İslem(object sender, EventArgs e)
        {

        }





        
    }
}