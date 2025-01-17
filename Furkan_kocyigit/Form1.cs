using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Furkan_kocyigit
{
     ///Ad-Soyad:Furkan Koçyiğit No:233908008 Bölüm:Bilgisayar Mühendisliği
    public partial class Form1 : Form
    {
        bool b1kontrol = false;//işlemci başlat butonu için
        bool b3kontrol = false;//biten process butonu kontrolü
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
            timer1.Interval = 1000;
            timer2.Interval = 1000;
            timer3.Interval = 1000;
            timer4.Interval = 1000;
           
         
        }
        
        public class Dugum 
        {
            public string veri;
            public Dugum Sonraki;
            public Dugum önceki;
            public Dugum(string veri)
            {
                this.veri = veri;
                this.Sonraki = null;
                this.önceki = null;
            }
        }
        public class yıgın
        {
            Dugum bas;
            public yıgın()
            {
                this.bas = null;

            }
            public void push(Dugum d1)
            {
                d1.Sonraki = bas;
                bas = d1;
            }
            public void yazdır2(TextBox txtbox, Dugum bas1, Dugum bas2, Dugum bas3)
            {
                Dugum temp1 = bas1;
                Dugum temp2 = bas2;
                Dugum temp3 = bas3;

                txtbox.Clear();

                while (temp1 != null || temp2 != null || temp3 != null)
                {

                    if (temp1 != null)
                    {
                        txtbox.Text += temp1.veri.PadRight(20);///textboxu sağ tarfa göre sütunlara bölmemizi sağlar
                        temp1 = temp1.Sonraki;
                    }
                    if (temp2 != null)
                    {
                        txtbox.Text += temp2.veri.PadRight(20);
                        temp2 = temp2.Sonraki;
                    }
                    if (temp3 != null)
                    {
                        txtbox.Text += temp3.veri.PadRight(20);
                        temp3 = temp3.Sonraki;
                    }

                    txtbox.Text += Environment.NewLine;
                    txtbox.SelectionStart = txtbox.Text.First();///her seferinde baş kısmı gösterme
                    txtbox.ScrollToCaret();/// textboxun ayarlandığı yerden devam etmesi
                }
            }
            public Dugum kontrolet(Dugum d1, CheckBox checkBox)
            {
                Dugum d2 = null;
                if (checkBox.Checked == false)
                {
                    return d2;
                }
                return d1;
            }
            public Dugum gonder1()
            {
                Dugum d2 = bas;
                return d2;
            }
        }
        public class Kuyruk
        {
            Dugum bas;
            Dugum son;
            public Kuyruk()
            {
                this.bas = null;
                this.son = null;
            }
            public void ekle(Dugum d1)
            {
                if (son == null && bas == null)
                {
                    bas = d1;
                    son = d1;
                }
                else
                {
                    son.Sonraki = d1;
                    d1.önceki = son;
                    son = d1;
                    son.Sonraki = null;
                }
            }
            public void sil()
            {
                if (bas != null)
                {
                    bas = bas.Sonraki;
                    if (bas != null)
                    {
                        bas.önceki = null;
                    }
                    else
                    {
                        son = null;  // Liste boşsa son da null olur
                    }
                }
            }
            public string polustur1()
            {
                Random random = new Random();
                String s1 = "P1-" + random.Next(0, 5).ToString();
                return s1;
            }
            public string polustur2()
            {
                Random random = new Random();
                String s1 = "P2-" + random.Next(0, 5).ToString();
                return s1;
            }
            public string polustur3()
            {
                Random random = new Random();
                String s1 = "P3-" + random.Next(0, 5).ToString();
                return s1;
            }
            public void yazdır(ListBox listBox)
            {
                listBox.Items.Clear();
                Dugum temp = bas;
                while (temp != null)
                {
                    listBox.Items.Add(temp.veri + "\n");
                    temp = temp.Sonraki;
                }    
            }
            public void yazdır2(TextBox txtbox)
            {
              
                txtbox.Clear();
                Dugum temp = bas;
                while (temp != null)
                {
                    txtbox.AppendText(temp.veri + "-->");
                    temp = temp.Sonraki;
                }
                txtbox.SelectionStart = txtbox.Text.Length;///Satır sonuna gitme işlemi
               
            
            }
            public int kontrol()
            {
                Dugum temp = bas;
                int sayaç = 0;
                while (temp != null)
                {
                    sayaç++;
                    temp = temp.Sonraki;
                }
                return sayaç;
            }
            public string[]sırala(string [] d1)
            {
                int n = d1.Length;
                string m1=null;
                string m2=null;
                int s1 = 0;
                int s2 = 0;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        m1= d1[j];
                        m2= d1[j+1];
                        s1 = m1[3];
                        s2 = m2[3];
                        if (s1 >s2)
                        {
                            string temp = d1[j];
                            d1[j] = d1[j + 1];
                            d1[j + 1] = temp;
                        }
                    }
                }
                return d1;
            }
            public Dugum gonder1()
            {
                Dugum d2 = bas;
                return d2;
            }
        }
        Kuyruk k1=new Kuyruk();//işlemci process kuyruğu 
        Kuyruk k2 = new Kuyruk();//proses 1 kuyruğu 
        Kuyruk k3 = new Kuyruk();//proses 2 kuyruğu 
        Kuyruk k4 = new Kuyruk();//proses 3 kuyruğu 
        yıgın y5 = new yıgın();/// 1. process'in tamamlanan işlemleri  için;
        yıgın y6 = new yıgın();/// 2. process'in tamamlanan işlemleri  için;
        yıgın y7 = new yıgın();/// 3. process'in tamamlanan işlemleri  için
        private void button1_Click(object sender, EventArgs e)
        {
            b1kontrol = true;
            timer4.Enabled = true;        
            timer5.Enabled = true;
        }
        ///işlemci barı 
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (b1kontrol == true)
            {
                
                timer5.Interval = 1050 - (trackBar1.Value * 100);
               
            }
        }
        ///process 1 barı 
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
           
            timer1.Interval = 1100 - (trackBar2.Value * 100);
            
        }
        //process 2 barı 
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            
            timer2.Interval = 1100 - (trackBar3.Value * 100);
            
        }
        //process 3 barı 
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            
            timer3.Interval = 1100 - (trackBar4.Value * 100);
           
        }
        private void timer1_Tick(object sender, EventArgs e)
        { 
              ///1.process
            string m1 = k2.polustur1();
            Dugum d1 = new Dugum(m1);
            k2.ekle(d1);
            k2.yazdır(listBox1);
             
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ///2.process
            string m2 = k3.polustur2();
            Dugum d2 = new Dugum(m2);
            k3.ekle(d2);
            k3.yazdır(listBox2);
           
           
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            ///3.process
            string m3 = k4.polustur3();
            Dugum d3 = new Dugum(m3);
            k4.ekle(d3);
            k4.yazdır(listBox3);
          
           
        }
       
        private void timer4_Tick(object sender, EventArgs e)
        {
                timer4.Interval = ((timer1.Interval + timer2.Interval + timer3.Interval) / 3)-50;
                Dugum m1 = k2.gonder1();
                Dugum m2 = k3.gonder1();
                Dugum m3 = k4.gonder1();
                Dugum m4 =k1.gonder1();
                if (m1 != null && m2 != null && m3 != null)
                {
                    string[] d1 = { m1.veri, m2.veri, m3.veri };
                    string[] d2 = k1.sırala(d1);
                    Dugum kd = new Dugum(d2[0]);//en küçük değer
                    Dugum od = new Dugum(d2[1]);//ortanca değer
                    Dugum bd = new Dugum(d2[2]);//en büyük değer
                    k1.ekle(kd);
                    k2.sil();
                    k1.ekle(od);
                    k3.sil();
                    k1.ekle(bd);
                    k4.sil();
                    k1.yazdır2(textBox1);
                    if (m4 == null) 
                    {
                        textBox1.Clear();
                        timer5.Stop();
                    }
                    else 
                    {
                          if (!timer5.Enabled) 
                          {
                                timer5.Interval = 1010 - (trackBar1.Value * 100);
                                timer5.Start();

                          }
                    }
                }
                else 
                {
                    if (m1 == null)
                    {
                       
                       timer1.Start();
                        
                    }
                    else if (m2 == null)
                    {
                        
                       timer2.Start();
                        
                    }
                    else
                    {
                        
                       timer3.Start();
                        
                    }
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b1kontrol = false;
            timer4.Enabled = false;
            timer5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer5.Enabled = true;
            b3kontrol = true;

        }
       
        private void timer5_Tick(object sender, EventArgs e)
        {
            int s1 = k1.kontrol();
            if (s1 >= 250)
            {
                textBox1.BackColor = Color.Red;
                timer5.Interval = (1050 - (trackBar1.Value * 100))/2;
            }
            else
            {
                textBox1.BackColor = Color.Green;
               
            }
            Dugum m7 = k1.gonder1();
            string m6 = null;
            if (m7 != null) 
            {
               m6 = m7.veri;
            }
            if (m6!=null) 
            {
                if (m6[1] == '1')
                {
                    Dugum dugum = new Dugum(m6);
                    y5.push(dugum);
                    k1.sil();
                }
                else if (m6[1] == '2')
                {
                    Dugum dugum = new Dugum(m6);
                    y6.push(dugum);
                    k1.sil();
                }
                else
                {
                    Dugum dugum = new Dugum(m6);
                    y7.push(dugum);
                    k1.sil();
                }
            }
            bool k = b3kontrol;
            if (k == true)
            {
                //// biten process kuyruklarını yazdırma //
                Dugum d12 = y5.gonder1();
                Dugum d23 = y6.gonder1();
                Dugum d33 = y7.gonder1();
                Dugum d4 = y5.kontrolet(d12, checkBox1);
                Dugum d5 = y5.kontrolet(d23, checkBox2);
                Dugum d6 = y5.kontrolet(d33, checkBox3);
                y5.yazdır2(textBox2, d4, d5, d6);
               
            }
           
        }
    }
}
