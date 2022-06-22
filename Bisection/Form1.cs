using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bisection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        int matrixSize;
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                matrixSize = int.Parse(tbInputNumber.Text);
            }
            catch
            {
                MessageBox.Show("Masukkan nilai dengan benar!");
                return;
            }
            tbInputNumber.Text = "";

            label6.Text = matrixSize.ToString();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            //pertanyaan
            for (int i = 0; i <= matrixSize; i++)
            {
                var column = new DataGridViewColumn();
                column.HeaderText = (i == matrixSize ? "C" : "X^" + (matrixSize-i).ToString());
                column.CellTemplate = new DataGridViewTextBoxCell();
                column.Width = 30;
                dataGridView1.Columns.Add(column);
                

            }
            dataGridView1.Rows.Add();

            for (int i = 0; i < 3; i++)
            {
                var col_guess = new DataGridViewColumn();
                col_guess.HeaderText = (i == 2 ? "Error Tolerance" : "X" + (i+1).ToString());
                col_guess.CellTemplate = new DataGridViewTextBoxCell();
                col_guess.Width = 80;
                dataGridView2.Columns.Add(col_guess);

            }
            dataGridView2.Rows.Add();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                double y1 = 0, y2 = 0;
                double x1 = double.Parse((string)dataGridView2.Rows[0].Cells[0].Value);
                double x2 = double.Parse((string)dataGridView2.Rows[0].Cells[1].Value);
                double tollerable_error = double.Parse((string)dataGridView2.Rows[0].Cells[2].Value);
                Console.WriteLine(x1);
                Console.WriteLine(x2);

                double x0 = 0, y0 = 0;
                int pwr = matrixSize;
                //store in an array
                double[] coe = new double[pwr + 1];
                //calculate values of y1 and y2 using for loop
                for (int i = 0; i < coe.Length; i++)
                {
                    coe[i] = double.Parse((string)dataGridView1.Rows[0].Cells[pwr-i].Value);//int.Parse(Console.ReadLine());
                    Console.WriteLine(coe[i]);
                    y1 += (double)(coe[i] * Math.Pow(x1, i));
                    y2 += (double)(coe[i] * Math.Pow(x2, i));
                } //determine negative

                //Console.WriteLine(y1 * y2);
                if ((y1 * y2) < 0)
                {
                    int iter = 1;
                    Console.WriteLine("There is a root between intervals");
                    while (true)
                    {//midpoint (x0)
                        x0 = (x1 + x2) / 2;
                        
                        //get output equation
                        for (int i = 0; i < coe.Length; i++)
                        { y0 += (float)(coe[i] * Math.Pow(x0, i)); }


                        if (y0 * y1 < 0)//check for -ve product
                        { 
                            x2 = x0; 
                        }
                        else {
                            x1 = x0;
                        }

                        //approximate y to 6 decimal places
                        y0 = (float)Math.Round(y0, 6);
                        //if y0=0 therefore root is x0
                        Console.Write("iteration-{0}, X0 =", iter);
                        Console.Write(x0); Console.Write("and f(x0) = ");
                        Console.WriteLine(y0);
                        String list_box_msg = "iteration-" + iter.ToString() + ", X0 = " + x0.ToString() + " and f(x0) = " + y0.ToString();
                        listBox1.Items.Add(list_box_msg);


                        if (Math.Abs(y0) <= tollerable_error) {
                            label1.Text = "The Root is " + x0;
                            Console.WriteLine("\n The Root is {0}", x0); break; 
                            
                        }
                        y0 = 0;
                        iter++;
                        //reset y0 back to zero in order to avoid accumulation of result
                    }
                }
                else { 
                    Console.WriteLine("There is NO root between intervals");
                    label1.Text = "There is NO root between intervals";
                }
            }

            catch { Console.WriteLine("\a\a\a Error \nInvalid Input");
                MessageBox.Show(" Error \nInvalid Input");
            }
        }

        private void groupEquation_Enter(object sender, EventArgs e)
        {

        }


    }

}
