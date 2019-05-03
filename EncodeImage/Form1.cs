using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Numerics;

namespace EncodeImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
            Bitmap image1;
        int part = 4;
        List<int[]> tablesRand_inv = new List<int[]>();
        List<int[]> tablesRand = new List<int[]>();
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void LoadImage()
        {
                image1 = new Bitmap(@"D:\WOLFS.bmp", true);
            pictureBox1.Image = image1;

            Bitmap image_enc = image1;
                   int z = 0;
            Bitmap bmap = (Bitmap)image1.Clone();
            Color[] c=new Color[bmap.Height *bmap.Width];
            byte[,] b_ar = new byte[bmap.Height, bmap.Width];
            byte[] copy_arr = new byte[bmap.Height * bmap.Width];
            Color[] copy1=new Color[bmap.Height * bmap.Width];
            Color[] copy = new Color[bmap.Height * bmap.Width];

            int x = 0;
            int j = 0;

            for (int i = 0; i < image1.Width; i++) //получаем значение светотени(оттенка цвета) пикселя
            {
                for (j = 0; j <image1.Height; j++)
                {
                    c[x] = image1.GetPixel(i, j);  
                    x++;
                }
            }
           /* StreamWriter sw = new StreamWriter(@"D:\\R_picture.txt");
            for(int i=0;i<c.Length;i++)
              {
                  sw.WriteLine(c[i]);
              }
            sw.Close();*/
           

           
            int n = 0;
            /*

                        for (int k = 0; k < 2000; k++)

                        {
                            for (int i = 0; i < c.Length - k; i++)
                            {

                                copy[i + k] = c[i];

                            }
                            x = 0;
                            for (int i = 0; i < k; i++)
                            {
                                copy[i] = System.Drawing.Color.FromArgb(c[i + j].A, 0, 0, 0);
                            }

                            BigInteger Rk1 = new BigInteger();
                            BigInteger Rsum = new BigInteger();
                            Rsum = 0;
                            Rk1 = 0;
                            int mid = Dispersion(c, k);

                            for (n = 0; n < c.Length - k; n++)
                            {

                                if (n + k < c.Length - k)
                                {
                                    Rk1 += (copy[n].G - mid) * (c[n].G - mid);

                                }
                                else
                                {
                                    break;
                                }


                            }
                            sw1.WriteLine(Rk1 + "\t" + k);
                            Rk1 = 0;
                            Rsum = 0;
                            n = 0;
                     sw1.Close();
                            }
  */        
         StreamWriter sw1 = new StreamWriter(@"D:\\numersmath.txt");

            int[] rand_ar = new int[125023];
            Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
            rand_ar = Enumerable.Range(0, 125023).OrderBy(po => rand.Next()).Take(125023).ToArray();

            for(int i=0;i<rand_ar.Length;i++)
            {
                sw1.WriteLine(rand_ar[i]);
            }
            sw1.Close();
 
            x = 0;
            int p = 0;
           // Tables(c);
            int[] tables4 = new int[part * 256];
            int[] tables4_inv = new int[part * 256];
            int[] tables4_1 = new int[part * 256];
            int[] tables4_inv1 = new int[part * 256];

            using (StreamReader sr = new StreamReader(@"D:\\table.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "*******")
                    {
                        x += 256;
                        continue;
                    }
                    else
                    {
                        string[] qwe2 = line.Split('\t');
                        tables4[x + int.Parse(qwe2[0])] = int.Parse(qwe2[1]);
                    }
                }
            }

            x = 0;


          using (StreamReader sr = new StreamReader(@"D:\\table1.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "*******")
                    {
                        x += 256;
                        continue;
                    }
                    else
                    {
                        string[] qwe2 = line.Split('\t');
                        tables4_1[x + int.Parse(qwe2[0])] = int.Parse(qwe2[1]);
                    }
                }
            }

            x = 0;


            using (StreamReader sq = new StreamReader(@"D:\\table_inv.txt"))
            {
                string line;

                while ((line = sq.ReadLine()) != null)
                {
                    if (line == "*******")
                    {
                        x += 256;
                        continue;
                    }
                    else
                    {
                        string[] qwe2 = line.Split('\t');
                        tables4_inv[x + int.Parse(qwe2[0])] = int.Parse(qwe2[1]);
                    }
                }
            }

            x = 0;

            using (StreamReader sq = new StreamReader(@"D:\\table_inv1.txt"))
            {
                string line;

                while ((line = sq.ReadLine()) != null)
                {
                    if (line == "*******")
                    {
                        x += 256;
                        continue;
                    }
                    else
                    {
                        string[] qwe2 = line.Split('\t');
                        tables4_inv1[x + int.Parse(qwe2[0])] = int.Parse(qwe2[1]);
                    }
                }
            }

           

            j = 0;
            for (int i = 0; i < image1.Width * image1.Height; i += part) 
            {
                for (j=0; j < part; j++)
                {
                    x = tables4[(j * (tables4.Length / part)) + (c[i + j].G)];
                    c[i + j] = System.Drawing.Color.FromArgb(c[i + j].A, x, x, x);
                }
            }
            int[] shuffle = new int[c.Length];
            int[] shuffle1 = new int[c.Length];
            c = Shuffle(c, out shuffle);
            x = 0;


            x = 0;
            for (int i = 0; i < pictureBox1.Image.Width; i++)
            {
                for (j = 0; j < pictureBox1.Image.Height; j++)
                {
                    ((Bitmap)pictureBox1.Image).SetPixel(i, j, c[x]);
                    x++;
                }
            }
            pictureBox1.Refresh();
            pictureBox1.Image.Save("D:\\encode1r.bmp");

            /*   StreamWriter sw4 = new StreamWriter(@"D:\\numbers_1r.txt");
                             for(int i=0;i<c.Length;i++)
                             {
                                 sw4.WriteLine(c[i]);
                             }
                           sw4.Close();


               StreamWriter sw3 = new StreamWriter(@"D:\\R_correl_encode 1r.txt");
                n = 0;


               for (int k = 0; k < 2000; k++)

               {
                   for (int i = 0; i < c.Length - k; i++)
                   {

                       copy[i + k] = c[i];

                   }
                   x = 0;
                   for (int i = 0; i < k; i++)
                   {
                       copy[i] = System.Drawing.Color.FromArgb(c[i + j].A, 0, 0, 0);
                   }

                   BigInteger Rk1 = new BigInteger();
                   BigInteger Rsum = new BigInteger();
                   Rsum = 0;
                   Rk1 = 0;
                   int mid = Dispersion(c, k);

                   for (n = 0; n < c.Length - k; n++)
                   {

                       if (n + k < c.Length - k)
                       {
                           Rk1 += (copy[n].G - mid) * (c[n].G - mid);

                       }
                       else
                       {
                           break;
                       }


                   }
                   sw3.WriteLine(Rk1 + "\t" + k);
                   Rk1 = 0;
                   Rsum = 0;
                   n = 0;
               }
               sw3.Close();


            */



            j = 0;
               for (int i = 0; i < image1.Width * image1.Height; i += part)
               {
                   for (j = 0; j < part; j++)
                   {
                       x = tables4_1[(j * (tables4.Length / part)) + (c[i + j].G)];
                       c[i + j] = System.Drawing.Color.FromArgb(c[i + j].A, x, x, x);
                   }
               }


               c = Shuffle(c, out shuffle1);
               
            x = 0;
            for (int i = 0; i < pictureBox1.Image.Width; i++)
            {
                for (j = 0; j < pictureBox1.Image.Height; j++)
                {
                    ((Bitmap)pictureBox1.Image).SetPixel(i, j, c[x]);
                    x++;
                }
            }
            pictureBox1.Refresh();
            pictureBox2.Image = pictureBox1.Image;
            pictureBox1.Image.Save("D:\\encode.bmp");




            x = 0;
        /*    Bitmap im_enc= new Bitmap(@"D:\\encode_im.bmp", true);
            StreamWriter sw2 = new StreamWriter(@"D:\\R_correl_encode.txt");
             n = 0;

          for(int k=0;k<2000;k++)

            {
            for (int i = 0; i < c.Length-k; i++) 
            {

                    copy[i+k] = c[i];
                
            }
            x = 0;
                for(int i=0;i<k;i++)
                {
                    copy[i] = System.Drawing.Color.FromArgb(c[i + j].A, 0,0,0); 
                }
            
                   BigInteger Rk1 = new BigInteger();
                   BigInteger Rsum = new BigInteger();
                   Rsum = 0;
                   Rk1 = 0;
                 int mid = Dispersion(c, k);
                 
                for (n=0; n < c.Length-k; n++)
                {
                    
                    if (n + k < c.Length-k)
                    {
                        Rk1 += (copy[n].G- mid) * (c[n].G-mid );

                    }
                    else
                    {
                        break;
                    }


                }
                       sw2.WriteLine(Rk1 + "\t" + k);
                        Rk1 = 0;
                       Rsum = 0;
                      n = 0;
                    }
                    sw2.Close();



            StreamWriter sw = new StreamWriter(@"D:\\numb.txt");
            for (int i = 0; i < c.Length; i++)
            {
                sw.WriteLine(c[i]);
            }
            sw.Close();
*/

            c = DecodeShuffle(c, shuffle1);
            c = DecodeTables(c, tables4_inv1);
            c = DecodeShuffle(c, shuffle);
            c = DecodeTables(c, tables4_inv);
            x = 0;
                for (int i = 0; i < pictureBox2.Image.Width; i++)
                {
                    for (j = 0; j < pictureBox2.Image.Height; j++)
                    {
                        ((Bitmap)pictureBox2.Image).SetPixel(i, j, c[x]);
                        x++;
                    }
                }
                pictureBox2.Refresh();
                   pictureBox1.Image.Save("D:\\encode_WOLFS.bmp");




        }


        private double MidDisp(Color [] c, int k)
        {
            BigInteger m = new BigInteger();
            m = 0;
            double r = 0;
            for(int i =0;i<c.Length-k;i++)
            {
                m += c[i].G * c[i].G;
            }
            m /= c.Length - k;
            r = (double)m / (double)(c.Length - k);
            return r;
        }
        private int Dispersion(Color [] c, int k)
        {
            BigInteger mid = new BigInteger();
            mid = 0;
            int i = 0;
            int r = 0;
            for(;i<c.Length-k ;i++)
            {
                mid +=c[i].G;
            }
            r= (int)mid / c.Length;
            return r;
             
        }
        private void Tables(Color[] byte_arr)//данная функция нужна для генерации таблиц подстановки, потом ее просто можно не вызывать
        {
            int i = 0;
            List<int> tables = new List<int>();
            int[] rand_ar = new int[256];
            int[] rand_arinv = new int[256];
            Random rnd = new Random();
            int[] check = new int[256];
            StringBuilder s_1 = new StringBuilder();
            bool check1 = false; 
            i = 0;
            StreamWriter sr = new StreamWriter(@"D:\\T15.txt");//тут пишите путь к файлам таблиц подстановки
            StreamWriter sw = new StreamWriter(@"D:\\T16.txt");
            for (int count=0;count<part;count++)
        {

                for (; i < rand_ar.Length; i++)
                {
                   tables.Add(i);
                }
                Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
                rand_ar = Enumerable.Range(0, 256).OrderBy(p => rand.Next()).Take(256).ToArray(); 

                for (int e= rand_ar.Length-1; e>=0;e--)
                    {
                   
                      //  int j = rnd.Next(rand_ar.Length);
                    Check:
                    /*for (i=0;i<tables.Count;i++)
                    {
                        if(tables[i]==j)
                        {
                            check1 = false;
                            break;
                        }
                        else
                        {
                            check1 = true;
                        }
                    }
                        if(check1)
                    {
                        j = rnd.Next(rand_ar.Length);
                        goto Check;
                    }*/
                         sr.WriteLine(e+"\t"+ rand_ar[e]);
                         sw.WriteLine(rand_ar[e] + "\t" + e);
                        // tables.Remove(j);
                        // i = 0;
                   
                }
                sr.WriteLine("*******");
                sw.WriteLine("*******");
                
          }
            sr.Close();
            sw.Close();

        }


    public static Color[] Shuffle(Color[] arr, out int[] shuffle)
        {
            shuffle = new int[arr.Length];
            Color[] pic = new Color[arr.Length];
            Random rand = new Random();

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                shuffle[i] = i;
            }

                var random = new Random(DateTime.Now.Millisecond);
                shuffle = shuffle.OrderBy(x => random.Next()).ToArray();
                for (int i = arr.Length - 1; i >= 0; i--)
            {
                int j = shuffle[i];
                Color tmp = arr[i];
                pic[j] = tmp;
            }
            return pic;
        }

        private Color [] DecodeTables(Color[] arr, int[] tables4_inv)
        {
            int x = 0;

            for (int i = 0; i < image1.Width * image1.Height; i += part) //пока мы все пиксель не пройдем мы берем блок, который равен SizeBlock, и шифруем
            {
                for (int j = 0; j < part; j++)
                {
                    x = tables4_inv[(j * (tables4_inv.Length / part)) + (arr[i + j].G)];
                    arr[i + j] = System.Drawing.Color.FromArgb(arr[i + j].A, x, x, x);
                }
            }

            return arr;



        }
        private Color[] DecodeShuffle(Color[] arr, int[] shuffle)
        {
            int[] inv_shuf = new int[shuffle.Length];
            Color[] picture = new Color[arr.Length];
            for (int i= shuffle.Length-1; i>=0;i--)
            {
                int er = shuffle[i];
                inv_shuf[shuffle[i]] = i;
            }
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                Color y = arr[i];
                picture[inv_shuf[i]] = y;
                
            }
            return picture;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadImage();
        }
    }
}
