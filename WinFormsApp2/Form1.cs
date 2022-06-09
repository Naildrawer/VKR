namespace WinFormsApp2
{
    public partial class vkr : Form
    {
        public const int maxlinkcap = 300;
        public const int maxflow = 40;
        public const int maxlink = 30;
        public static double Result1;
        public static double Result2;
        public static double Result3;

        public static void Сaufrob(int v, int nos, double[] a, int[] br, double[] pi)
        {
            double[] p = new double[maxlinkcap];
            double sum1;

            for (int ik = 1; ik <= nos; ik++)
                pi[ik] = 0;

            for (int ik = 0; ik <= v; ik++)
                p[ik] = 0;

            p[0] = 1;
            for (int ik = 1; ik <= v; ik++)
            {
                sum1 = 0;
                for (int ii = 1; ii <= nos; ii++)
                {
                    if (ik - br[ii] >= 0)
                        if (p[ik - br[ii]] > 0)
                            sum1 = sum1 + p[ik - br[ii]] * br[ii] * a[ii];
                    p[ik] = sum1 / ik;
                }
            }

            sum1 = 0;
            for (int ik = 0; ik <= v; ik++)
                sum1 = sum1 + p[ik];
            for (int ik = 0; ik <= v; ik++)
                p[ik] = p[ik] / sum1;

            for (int ik = 1; ik <= nos; ik++)
                for (int ii = 0; ii <= br[ik] - 1; ii++)
                    if (a[ik] > 1e-5)
                        pi[ik] = pi[ik] + p[v - ii];
                    else
                        pi[ik] = 0;
        }

        public static double Eva(int v, double a, double delta)
        {
            double g = 1;
            double s = 1;

            for (int i = 1; i <= v; i++)
            {
                g = g * ((v - i + 1) / a);
                s = s + g;
                if (s > 1e+20)
                    return 1e-20;
                else if (g < delta)
                    break;
            }
            return 1 / s;
        }


        public vkr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] b = new int[maxflow];
            if (comboBox1.SelectedIndex == 0)
                b[1] = 1;
            else if (comboBox1.SelectedIndex == 1)
                b[1] = 2;
            else if (comboBox1.SelectedIndex == 2)
                b[1] = 3;
            else if (comboBox1.SelectedIndex == 3)
                b[1] = 4;
            else if (comboBox1.SelectedIndex == 4)
                b[1] = 5;
            else if (comboBox1.SelectedIndex == 5)
                b[1] = 6;
            else if (comboBox1.SelectedIndex == 6)
                b[1] = 7;
            else if (comboBox1.SelectedIndex == 7)
                b[1] = 8;
            else if (comboBox1.SelectedIndex == 8)
                b[1] = 9;
            else if (comboBox1.SelectedIndex == 9)
                b[1] = 10;
            b[2] = 4;
            b[3] = 6;
            b[4] = 7;
            b[5] = 9;


            double[] lambda = new double[maxlink];


            lambda[1] = 15;
            if (comboBox2.SelectedIndex == 1)
                lambda[2] = 10;
            lambda[3] = 20;
            lambda[4] = 10;
            lambda[5] = 20;

            if (comboBox1.SelectedIndex == 0)
                lambda[2] = 5;
            lambda[3] = 10;
            lambda[4] = 5;
            lambda[5] = 10;

            if (comboBox1.SelectedIndex == 2)
                lambda[2] = 15;
            lambda[3] = 30;
            lambda[4] = 15;
            lambda[5] = 30;


            try
            {
                using StreamWriter writer = new StreamWriter("FirstData.txt");
                {
                    int[,] d = new int[maxlink, maxflow];


                    d[1, 1] = 1;
                    d[1, 2] = 1;
                    d[1, 3] = 0;
                    d[1, 4] = 0;
                    d[1, 5] = 0;

                    d[2, 1] = 1;
                    d[2, 2] = 1;
                    d[2, 3] = 0;
                    d[2, 4] = 0;
                    d[2, 5] = 0;

                    d[3, 1] = 1;
                    d[3, 2] = 1;
                    d[3, 3] = 0;
                    d[3, 4] = 0;
                    d[3, 5] = 0;

                    d[4, 1] = 1;
                    d[4, 2] = 0;
                    d[4, 3] = 0;
                    d[4, 4] = 1;
                    d[4, 5] = 0;

                    d[5, 1] = 0;
                    d[5, 2] = 0;
                    d[5, 3] = 1;
                    d[5, 4] = 1;
                    d[5, 5] = 1;

                    d[6, 1] = 0;
                    d[6, 2] = 0;
                    d[6, 3] = 1;
                    d[6, 4] = 1;
                    d[6, 5] = 1;

                    d[7, 1] = 0;
                    d[7, 2] = 0;
                    d[7, 3] = 1;
                    d[7, 4] = 0;
                    d[7, 5] = 1;

                    int nflow = 5;
                    int nlink = 7;

                    int[] link = new int[maxflow];
                    link[1] = 112;
                    link[2] = 112;
                    link[3] = 64;
                    link[4] = 32;
                    link[5] = 44;
                    link[6] = 48;
                    link[7] = 48;


                    double[,] losseskj = new double[maxlink, maxflow];
                    double[] inputkj = new double[maxflow];
                    double[] zkj = new double[maxflow];
                    double[,] aux = new double[maxlink, maxflow];
                    double[] pp = new double[maxlinkcap];
                    double[] pika = new double[maxlink];

                    for (int noc = 1; noc <= 1; noc++)
                    {
                        for (int j = 1; j <= nlink; j++)
                            for (int k = 1; k <= nflow; k++)
                                losseskj[j, k] = 0;

                        double dif = 1;
                        double tol = 1e-10;
                        int noit = 0;


                        while (dif > tol)
                        {
                            noit = noit + 1;
                            for (int j = 1; j <= nlink; j++)
                            {
                                //we are analysing link j

                                for (int k = 1; k <= nflow; k++)
                                    inputkj[k] = 0;

                                for (int k = 1; k <= nflow; k++)
                                    if (d[j, k] > 0)
                                    {
                                        zkj[k] = lambda[k];
                                        for (int jj = 1; jj <= nlink; jj++)
                                            if (d[jj, k] > 0)
                                                zkj[k] = zkj[k] * (1 - losseskj[jj, k]);
                                        inputkj[k] = zkj[k] / (1 - losseskj[j, k]);
                                    }

                                for (int k = 1; k <= nflow; k++)
                                    aux[j, k] = losseskj[j, k];
                                Сaufrob(link[j], nflow, inputkj, b, pp);

                                for (int k = 1; k <= nflow; k++)
                                {
                                    losseskj[j, k] = pp[k];
                                    //aux1[k][j]=pp[k];
                                    //pp[k] - the losses of k-th flow on j-th link

                                }
                            }
                            double sum = 0;
                            double sum1 = 0;
                            for (int j = 1; j <= nlink; j++)
                                for (int k = 1; k <= nflow; k++)
                                {
                                    sum = sum + Math.Abs(aux[j, k] - losseskj[j, k]);
                                    sum1 = sum1 + losseskj[j, k];
                                }
                            dif = sum / sum1;
                        }

                        for (int k = 1; k <= nflow; k++)
                        {
                            pika[k] = 1;

                            for (int j = 1; j <= nlink; j++)
                                if (d[j, k] > 0)
                                    pika[k] = pika[k] * (1 - losseskj[j, k]);
                            pika[k] = 1 - pika[k];

                            writer.WriteLine("flow number" + k + ' ' + "call blocking=" + pika[k] + '\n');
                            string Newstr = ("Доля потерянных заявок при предосавлении услуги BOD " + pika[1].ToString("0.00000") + '\n');
                            label1.Text = (Newstr);
                            Result1 = (int)pika[1];

                        }
                        writer.Flush();
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            int[] b = new int[maxflow];
            if (comboBox4.SelectedIndex == 0)
                b[1] = 1;
            else if (comboBox4.SelectedIndex == 1)
                b[1] = 2;
            else if (comboBox4.SelectedIndex == 2)
                b[1] = 3;
            else if (comboBox4.SelectedIndex == 3)
                b[1] = 4;
            else if (comboBox4.SelectedIndex == 4)
                b[1] = 5;
            else if (comboBox4.SelectedIndex == 5)
                b[1] = 6;
            else if (comboBox4.SelectedIndex == 6)
                b[1] = 7;
            else if (comboBox4.SelectedIndex == 7)
                b[1] = 8;
            else if (comboBox4.SelectedIndex == 8)
                b[1] = 9;
            else if (comboBox4.SelectedIndex == 9)
                b[1] = 10;
            b[2] = 7;
            b[3] = 4;
            b[4] = 2;
            b[5] = 1;


            double[] lambda = new double[maxlink];


            lambda[1] = 15;
            if (comboBox3.SelectedIndex == 1)
                lambda[2] = 10;
            lambda[3] = 20;
            lambda[4] = 10;
            lambda[5] = 20;

            if (comboBox3.SelectedIndex == 0)
                lambda[2] = 5;
            lambda[3] = 10;
            lambda[4] = 5;
            lambda[5] = 10;

            if (comboBox3.SelectedIndex == 2)
                lambda[2] = 15;
            lambda[3] = 30;
            lambda[4] = 15;
            lambda[5] = 30;

            try
            {
                using StreamWriter writer = new StreamWriter("SecondData.txt");
                {
                    int[,] d = new int[maxlink, maxflow];


                    d[1, 1] = 1;
                    d[1, 2] = 0;
                    d[1, 3] = 0;
                    d[1, 4] = 0;
                    d[1, 5] = 0;

                    d[2, 1] = 1;
                    d[2, 2] = 0;
                    d[2, 3] = 0;
                    d[2, 4] = 0;
                    d[2, 5] = 1;

                    d[3, 1] = 1;
                    d[3, 2] = 1;
                    d[3, 3] = 1;
                    d[3, 4] = 0;
                    d[3, 5] = 0;

                    d[4, 1] = 0;
                    d[4, 2] = 0;
                    d[4, 3] = 1;
                    d[4, 4] = 1;
                    d[4, 5] = 0;

                    d[5, 1] = 0;
                    d[5, 2] = 1;
                    d[5, 3] = 1;
                    d[5, 4] = 1;
                    d[5, 5] = 0;

                    d[6, 1] = 0;
                    d[6, 2] = 0;
                    d[6, 3] = 1;
                    d[6, 4] = 1;
                    d[6, 5] = 1;

                    d[7, 1] = 0;
                    d[7, 2] = 0;
                    d[7, 3] = 0;
                    d[7, 4] = 0;
                    d[7, 5] = 1;

                    int nflow = 5;
                    int nlink = 7;

                    int[] link = new int[maxflow];
                    link[1] = 48;
                    link[2] = 44;
                    link[3] = 32;
                    link[4] = 44;
                    link[5] = 48;
                    link[6] = 48;
                    link[7] = 64;

                    double[,] losseskj = new double[maxlink, maxflow];
                    double[] inputkj = new double[maxflow];
                    double[] zkj = new double[maxflow];
                    double[,] aux = new double[maxlink, maxflow];
                    double[] pp = new double[maxlinkcap];
                    double[] pika = new double[maxlink];

                    for (int noc = 1; noc <= 1; noc++)
                    {
                        for (int j = 1; j <= nlink; j++)
                            for (int k = 1; k <= nflow; k++)
                                losseskj[j, k] = 0;

                        double dif = 1;
                        double tol = 1e-10;
                        int noit = 0;


                        while (dif > tol)
                        {
                            noit = noit + 1;
                            for (int j = 1; j <= nlink; j++)
                            {
                                //we are analysing link j

                                for (int k = 1; k <= nflow; k++)
                                    inputkj[k] = 0;

                                for (int k = 1; k <= nflow; k++)
                                    if (d[j, k] > 0)
                                    {
                                        zkj[k] = lambda[k];
                                        for (int jj = 1; jj <= nlink; jj++)
                                            if (d[jj, k] > 0)
                                                zkj[k] = zkj[k] * (1 - losseskj[jj, k]);
                                        inputkj[k] = zkj[k] / (1 - losseskj[j, k]);
                                    }

                                for (int k = 1; k <= nflow; k++)
                                    aux[j, k] = losseskj[j, k];
                                Сaufrob(link[j], nflow, inputkj, b, pp);

                                for (int k = 1; k <= nflow; k++)
                                {
                                    losseskj[j, k] = pp[k];
                                    //aux1[k][j]=pp[k];
                                    //pp[k] - the losses of k-th flow on j-th link

                                }
                            }
                            double sum = 0;
                            double sum1 = 0;
                            for (int j = 1; j <= nlink; j++)
                                for (int k = 1; k <= nflow; k++)
                                {
                                    sum = sum + Math.Abs(aux[j, k] - losseskj[j, k]);
                                    sum1 = sum1 + losseskj[j, k];
                                }
                            dif = sum / sum1;
                        }

                        for (int k = 1; k <= nflow; k++)
                        {
                            pika[k] = 1;


                            for (int j = 1; j <= nlink; j++)
                                if (d[j, k] > 0)
                                    pika[k] = pika[k] * (1 - losseskj[j, k]);
                            pika[k] = 1 - pika[k];

                            writer.WriteLine("flow number" + k + ' ' + "call blocking=" + pika[k] + '\n');
                            string Newstr = ("Доля потерянных заявок при предосавлении услуги BOD " + pika[1].ToString("0.00000") + '\n');
                            label6.Text = (Newstr);
                            Result2 = pika[1];
                        }
                        writer.Flush();
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] b = new int[maxflow];
            if (comboBox1.SelectedIndex == 0)
                b[1] = 1;
            else if (comboBox6.SelectedIndex == 1)
                b[1] = 2;
            else if (comboBox6.SelectedIndex == 2)
                b[1] = 3;
            else if (comboBox6.SelectedIndex == 3)
                b[1] = 4;
            else if (comboBox6.SelectedIndex == 4)
                b[1] = 5;
            else if (comboBox6.SelectedIndex == 5)
                b[1] = 6;
            else if (comboBox6.SelectedIndex == 6)
                b[1] = 7;
            else if (comboBox6.SelectedIndex == 7)
                b[1] = 8;
            else if (comboBox6.SelectedIndex == 8)
                b[1] = 9;
            else if (comboBox6.SelectedIndex == 9)
                b[1] = 10;
            b[2] = 7;
            b[3] = 6;
            b[4] = 8;
            b[5] = 7;


            double[] lambda = new double[maxlink];


            lambda[1] = 15;
            if (comboBox5.SelectedIndex == 1)
                lambda[2] = 10;
            lambda[3] = 20;
            lambda[4] = 10;
            lambda[5] = 20;

            if (comboBox5.SelectedIndex == 0)
                lambda[2] = 5;
            lambda[3] = 10;
            lambda[4] = 5;
            lambda[5] = 10;

            if (comboBox5.SelectedIndex == 2)
                lambda[2] = 15;
            lambda[3] = 30;
            lambda[4] = 15;
            lambda[5] = 30;


            try
            {
                using StreamWriter writer = new StreamWriter("ThirdData.txt");
                {
                    int[,] d = new int[maxlink, maxflow];


                    d[1, 1] = 1;
                    d[1, 2] = 0;
                    d[1, 3] = 0;
                    d[1, 4] = 0;
                    d[1, 5] = 0;

                    d[2, 1] = 1;
                    d[2, 2] = 1;
                    d[2, 3] = 0;
                    d[2, 4] = 0;
                    d[2, 5] = 0;

                    d[3, 1] = 1;
                    d[3, 2] = 1;
                    d[3, 3] = 0;
                    d[3, 4] = 1;
                    d[3, 5] = 0;

                    d[4, 1] = 0;
                    d[4, 2] = 0;
                    d[4, 3] = 0;
                    d[4, 4] = 1;
                    d[4, 5] = 0;

                    d[5, 1] = 0;
                    d[5, 2] = 0;
                    d[5, 3] = 1;
                    d[5, 4] = 1;
                    d[5, 5] = 1;

                    d[6, 1] = 0;
                    d[6, 2] = 0;
                    d[6, 3] = 0;
                    d[6, 4] = 1;
                    d[6, 5] = 1;

                    d[7, 1] = 0;
                    d[7, 2] = 0;
                    d[7, 3] = 0;
                    d[7, 4] = 0;
                    d[7, 5] = 1;

                    int nflow = 5;
                    int nlink = 7;

                    int[] link = new int[maxflow];
                    link[1] = 48;
                    link[2] = 44;
                    link[3] = 88;
                    link[4] = 32;
                    link[5] = 64;
                    link[6] = 80;
                    link[7] = 80;

                    double[,] losseskj = new double[maxlink, maxflow];
                    double[] inputkj = new double[maxflow];
                    double[] zkj = new double[maxflow];
                    double[,] aux = new double[maxlink, maxflow];
                    double[] pp = new double[maxlinkcap];
                    double[] pika = new double[maxlink];

                    for (int noc = 1; noc <= 1; noc++)
                    {
                        for (int j = 1; j <= nlink; j++)
                            for (int k = 1; k <= nflow; k++)
                                losseskj[j, k] = 0;

                        double dif = 1;
                        double tol = 1e-10;
                        int noit = 0;


                        while (dif > tol)
                        {
                            noit = noit + 1;
                            for (int j = 1; j <= nlink; j++)
                            {
                                //we are analysing link j

                                for (int k = 1; k <= nflow; k++)
                                    inputkj[k] = 0;

                                for (int k = 1; k <= nflow; k++)
                                    if (d[j, k] > 0)
                                    {
                                        zkj[k] = lambda[k];
                                        for (int jj = 1; jj <= nlink; jj++)
                                            if (d[jj, k] > 0)
                                                zkj[k] = zkj[k] * (1 - losseskj[jj, k]);
                                        inputkj[k] = zkj[k] / (1 - losseskj[j, k]);
                                    }

                                for (int k = 1; k <= nflow; k++)
                                    aux[j, k] = losseskj[j, k];
                                Сaufrob(link[j], nflow, inputkj, b, pp);

                                for (int k = 1; k <= nflow; k++)
                                {
                                    losseskj[j, k] = pp[k];
                                    //aux1[k][j]=pp[k];
                                    //pp[k] - the losses of k-th flow on j-th link

                                }
                            }
                            double sum = 0;
                            double sum1 = 0;
                            for (int j = 1; j <= nlink; j++)
                                for (int k = 1; k <= nflow; k++)
                                {
                                    sum = sum + Math.Abs(aux[j, k] - losseskj[j, k]);
                                    sum1 = sum1 + losseskj[j, k];
                                }
                            dif = sum / sum1;
                        }

                        for (int k = 1; k <= nflow; k++)
                        {
                            pika[k] = 1;


                            for (int j = 1; j <= nlink; j++)
                                if (d[j, k] > 0)
                                    pika[k] = pika[k] * (1 - losseskj[j, k]);
                            pika[k] = 1 - pika[k];

                            writer.WriteLine("flow number" + k + ' ' + "call blocking=" + pika[k] + '\n');
                            string Newstr = ("Доля потерянных заявок при предосавлении услуги BOD " + pika[1].ToString("0.00000") + '\n');
                            label9.Text = (Newstr);
                            Result3 = (int)pika[1];
                        }
                        writer.Flush();
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            double a1 = Result1;
            double a2 = Result2;
            double a3 = Result3;

            if (a1 < a2 & a1 < a3)
                label10.Text = "Лучше использовать первый срез";
            else if (a2 < a1 & a2 < a3)
                label10.Text = "Лучше использовать второй срез";
            else
                label10.Text = "Лучше использовать третий срез";
        }
    }
}