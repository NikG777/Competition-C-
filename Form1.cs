using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Competitionspace;
using Competitorspace;
using System.IO;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private Competition _competition;
        private string textfile;
        public Form1()
        {
            InitializeComponent();
            tabPage2.Parent = null;
            _competition = new Competition();
            // Инициализируем файлики
            FileInfo[] files = _competition.GetTxts().GetFiles("*.txt");
            foreach (FileInfo fi in files)
            {
                listBox1.Items.Add(fi.Name.Replace(".txt", "").ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название конкурса!");
            }
            else if (textBox2.Text.Trim().Length <= 0 || textBox2.Text == "0")
            {
                MessageBox.Show("Введите количество участников!");
            }
            else if (textBox6.Text.Trim().Length <= 0 || textBox6.Text == "0")
            {
                MessageBox.Show("Введите количество призовых мест!");
            }
            else if (Int32.Parse(textBox6.Text) > Int32.Parse(textBox2.Text))
            {
                MessageBox.Show("Призовых мест не может быть больше участников!");
            }
            else
            {
                if (!System.IO.File.Exists(textBox1.Text + ".txt") && !System.IO.File.Exists(_competition.GetTxts()+ @"\Завершенные конкурсы" + @"\" + textBox1.Text + ".txt"))
                { 
                    _competition.SetNameCompetition(textBox1.Text);
                    _competition.SetCountUsers(Int32.Parse(textBox2.Text));
                    _competition.SetCountWinners(Int32.Parse(textBox6.Text));
                    _competition.SetTxt();
                    tabPage1.Parent = null;
                    tabPage2.Parent = tabControl1;
                    label6.Text = textBox1.Text;
                    label8.Text = textBox2.Text + "/" + textBox6.Text;
                    button8.Enabled = true;
                    textBox4.Enabled = true;
                    button9.Enabled = true;
                    button10.Enabled = true;
                }
                else 
                {
                    MessageBox.Show("Конкурс с таким названием уже создан ранее!");
                }
            }
        }

        // Регистрация участника
        private void button9_Click(object sender, EventArgs e)
        {
                if (textBox4.Text.Trim().Length > 0)
                {
                    int newIndexUser = _competition.GetNumberUser();
                    _competition.AddUser(textBox4.Text, 0.ToString(), newIndexUser);
                    string[] items = { newIndexUser.ToString(), textBox4.Text.ToString(), 0.ToString() };
                    listView1.Items.Add(new ListViewItem(items));
                    button10.Enabled = true;

                    //MessageBox.Show(_competition.GetCountUsersNow() + " / " + _competition.getCountUsers());

                    if (_competition.GetCountUsersNow() >= _competition.GetCountUsers())
                    {
                        button9.Enabled = false;
                        _competition.SetRegUsers(1);
                    }
                    _competition.SetFinishRegUsers();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Переменная ввода
            char letter = e.KeyChar;
            // Проеверяем введеный символ методами
            if (char.IsLetter(letter) || char.IsControl(letter) || char.IsPunctuation(letter) || char.IsWhiteSpace(letter))
            {
                // Разрешаем ввод
                e.Handled = false;
            }
            else
            {
                // Задерживаем символ
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (char.IsNumber(number) || char.IsControl(number))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (char.IsNumber(number) || char.IsControl(number))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }
        // Проверка на ввод
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Переменная ввода
            char letter = e.KeyChar;
            char previus_letter = 'a';
            // Проеверяем введеный символ методами
            if (char.IsLetter(letter) || char.IsControl(letter))
            {
                // Разрешаем ввод
                e.Handled = false;
                previus_letter = letter;
            }
            else
            {
                // Задерживаем символ
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textfile = listBox1.Text;
            _competition.SetNameCompetition(textfile);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabPage1.Parent = null;
            tabPage2.Parent = tabControl1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabPage2.Parent = null;
            tabPage1.Parent = tabControl1;
        }

        // Выбор конкурса
        private void button4_Click(object sender, EventArgs e)
        {
            try {
                string nameCompetition;
                nameCompetition = listBox1.SelectedItem.ToString();
                if (System.IO.File.Exists(nameCompetition + ".txt"))
                {
                    _competition.IncludeSettingsInFiles(nameCompetition);
                    int maxUsers = _competition.GetCountUsers();
                    int maxUsersWin = _competition.GetCountWinners();

                    label6.Text = nameCompetition;
                    label8.Text = maxUsers.ToString() + "/" + maxUsersWin.ToString();
                    button8.Enabled = true;

                    _competition.SetAllUserFromFile(nameCompetition);
                    listView1.Items.Clear();
                    foreach (Competitor u1 in _competition.GetUsersList())
                    {
                        string[] items = { u1.GetIndex().ToString(), u1.GetName().ToString(), u1.GetBall().ToString() };
                        listView1.Items.Add(new ListViewItem(items));
                    }

                    tabPage1.Parent = null;
                    tabPage2.Parent = tabControl1;

                    // Если регистрация участников завершена, то показываем это
                    if (_competition.GetRegUsers() == 1)
                    {
                        textBox4.Enabled = false;
                        textBox4.Text = "";
                        textBox3.Enabled = false;
                        textBox3.Text = "";
                        button9.Enabled = false;
                        button10.Enabled = false;

                        button5.Enabled = false;
                        button11.Enabled = true;
                    }
                    else
                    {
                        button10.Enabled = true;
                    }

                }
            }
            catch(NullReferenceException) { MessageBox.Show("Вы ничего не выбрали!"); }
            }

        private void button8_Click(object sender, EventArgs e)
        {
            tabPage2.Parent = null;
            tabPage1.Parent = tabControl1;
            button8.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox6.Text = "";
            FileInfo[] files = _competition.GetTxts().GetFiles("*.txt");
            listBox1.Items.Clear();
            listView1.Items.Clear();
            foreach (FileInfo fi in files)
            {
                listBox1.Items.Add(fi.Name.Replace(".txt", "").ToString());
            }
            _competition = new Competition();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Исключение участника из конкурса
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string users; int nuser;
                users = listView1.SelectedItems[0].Text;
                nuser = listView1.SelectedItems[0].Index;
                _competition.DeleteUser(users);
                _competition.GetUsersList().RemoveAt(nuser);
                listView1.SelectedItems[0].Remove();
            }
            catch (Exception)
            {
                MessageBox.Show("Нужно выбрать участника");
            }
        }

        // Завершение конкурса
        private void button3_Click_1(object sender, EventArgs e)
        {
            _competition.SetFinish(1);
            button7.Enabled = true;
            button5.Enabled = false;
            button3.Enabled = false;
            textBox3.Enabled = false;
            button12.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _competition.SortUsers();
            listView1.Items.Clear();
                foreach (Competitor u1 in _competition.GetUsersList())
                {
                    string[] items = { u1.GetIndex().ToString(), u1.GetName().ToString(), u1.GetBall().ToString() };
                    listView1.Items.Add(new ListViewItem(items));
                }
            try
            {
                _competition.SetItogFile();
                button7.Enabled = false;
                MessageBox.Show(_competition.GetTextUser(true));
            }
            catch (SystemException) { MessageBox.Show("Этот конкурс уже закончен"); }
        }

        // Завершение регистрации участников
        private void button10_Click(object sender, EventArgs e)
        {
            textBox4.Enabled = false;
            textBox4.Text = "";
            textBox3.Enabled = false;
            textBox3.Text = "";
            button9.Enabled = false;
            button10.Enabled = false;

            _competition.SetRegUsers(1);
            _competition.SetFinishRegUsers();

            button5.Enabled = false;
            button11.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox3.Enabled = true;
            button12.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            button11.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string userId;
            if (textBox3.Text != "")
            {
                try
                {
                    userId = listView1.SelectedItems[0].Text;
                    var item = listView1.SelectedItems[0];
                    item.SubItems[2].Text = textBox3.Text;
                    _competition.SetBallUser(Convert.ToInt32(userId), Convert.ToInt32(textBox3.Text));
                    _competition.SetFinishRegUsers();
                    textBox3.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Нужно выбрать участника");
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Переменная ввода
            char letter = e.KeyChar;
            // Проеверяем введеный символ методами
            if (char.IsNumber(letter) || char.IsControl(letter))
            {
                // Разрешаем ввод
                e.Handled = false;
            }
            else
            {
                // Задерживаем символ
                e.Handled = true;
            }
        }
    }
}
