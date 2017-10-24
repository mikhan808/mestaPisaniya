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
using System.Collections;


namespace Места_писания
{
    public partial class Form1 : Form
    {
        ArrayList dt;
        ArrayList mp;
        public Form1()
        {
            InitializeComponent();
            dt = new ArrayList();
            mp = new ArrayList();
            if (File.Exists("daty"))
            {
                string[] temp = File.ReadAllLines("daty");
                for (int i=0;i<temp.Length;i++)
                {
                    dt.Add(temp[i]);
                }
            }
            if (File.Exists("mesta"))
            {
                string[] temp = File.ReadAllLines("mesta");
                for (int i = 0; i < temp.Length; i++)
                {
                    mp.Add(temp[i]);
                }
            }


        }  
        
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                string str = comboBox1.Text + " " + comboBox2.Text + ":" + comboBox3.Text;
                if(mp.Contains(str))
                {
                    MessageBox.Show("данное место уже есть в списке");
                    panel1.Visible = false;
                }
                else
                {
                    mp.Add(str);
                    string temp = Convert.ToString(dateTimePicker1.Value.Date);
                    dt.Add(temp);
                    string[] mest = new string[ mp.Count];
                    for (int i=0;i<mest.Length;i++)
                    {
                        mest[i] = (string)mp[i];
                    }
                    string[] data = new string[dt.Count];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = (string)dt[i];
                    }
                    File.WriteAllLines("mesta",mest);
                    File.WriteAllLines("daty", data);
                    panel1.Visible = false;
                    update();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 151; i++)
                comboBox2.Items.Add(i);
            for (int i = 1; i < 177; i++)
                comboBox3.Items.Add(i);
            update();


        }
        ArrayList Stack = new ArrayList();
        ArrayList Stack1 = new ArrayList();
        string text = "";
        void poisk(string str)
        {
            int index = dt.IndexOf(str);
            if(index>=0)
            {
                listBox1.Items.Add(mp[index]);
                //text += (string)mp[index]+"\n";
                Stack.Add(mp[index]);
                Stack1.Add(dt[index]);
                mp.RemoveAt(index);
                dt.RemoveAt(index);
                poisk(str);
            }
        }
        void update()
        {
            string str = Convert.ToString(dateTimePicker2.Value.Date);
            text = "";
            listBox1.Items.Clear();
            poisk(str);
            //richTextBox1.Text = text;
            for (int i = 0; i < Stack.Count; i++)
            {
                mp.Add(Stack[i]);
                dt.Add(Stack1[i]);
            }
            Stack.Clear();
            Stack1.Clear();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            update();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            if (listBox1.SelectedIndex >=0)
            {
                int ind = listBox1.SelectedIndex;
                int ind_of = mp.IndexOf(listBox1.Items[ind]);
                mp.RemoveAt(ind_of);
                dt.RemoveAt(ind_of);
                string[] mest = new string[mp.Count];
                for (int i = 0; i < mest.Length; i++)
                {
                    mest[i] = (string)mp[i];
                }
                string[] data = new string[dt.Count];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (string)dt[i];
                }
                File.WriteAllLines("mesta", mest);
                File.WriteAllLines("daty", data);
                listBox1.Items.RemoveAt(ind);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Visible = true;
        }
    }
}
