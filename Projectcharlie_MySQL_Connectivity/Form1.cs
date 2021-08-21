using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace Week3_Projectcharlie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // to set a global connection and the code would not be repeated
        MySqlConnection con = new MySqlConnection("server=127.0.0.1;" +
                                         "uid=root;" +
                                         "pwd=Password;" +
                                         "database=visitorinfo");

        bool buttonwasclicked = false;
        int ClickCounter = 1;

        private void Form1_Load(object sender, EventArgs e)
        {
            // connection will be opened whenever we run the program
            con.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = 0;
            char let;
            string email = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            string NZ_Mobile = (@"^[0-9]{10}$");
            if (textBox1.Text == "")
            {
                MessageBox.Show("*First Name can't be empty*");
                textBox1.BackColor = Color.OrangeRed;
                textBox1.Focus();

            }
            else if (int.TryParse(textBox1.Text, out num) == true)
            {
                MessageBox.Show("*First Name can't contain numbers/symbols*");
                textBox1.BackColor = Color.OrangeRed;
                textBox1.Focus();

            }
            else if (char.TryParse(textBox1.Text, out let) == true)
            {
                MessageBox.Show("*First Name can't contain numbers/symbols*");
                textBox1.BackColor = Color.OrangeRed;
                textBox1.Focus();

            }
            else if (textBox5.Text == "")
            {
                textBox1.BackColor = Color.White;
                MessageBox.Show("*Last Name needs to be filled*");
                textBox5.BackColor = Color.OrangeRed;
                textBox5.Focus();
            }
            else if (int.TryParse(textBox5.Text, out num) == true)
            {
                textBox1.BackColor = Color.White;
                MessageBox.Show("*Last Name can't contain numbers/symbols*");
                textBox5.BackColor = Color.OrangeRed;
                textBox5.Focus();

            }
            else if (char.TryParse(textBox5.Text, out let) == true)
            {
                textBox1.BackColor = Color.White;
                MessageBox.Show("*Last Name can't contain numbers/symbols*");
                textBox5.BackColor = Color.OrangeRed;
                textBox5.Focus();

            }
            else if (textBox4.Text == "")
            {
                textBox5.BackColor = Color.White;
                MessageBox.Show("*Mobile number required*");
                textBox4.BackColor = Color.OrangeRed;
                textBox4.Focus();
            }
            else if (int.TryParse(textBox4.Text, out num) == false)
            {
                textBox5.BackColor = Color.White;
                MessageBox.Show("*Mobile number can't contain letters/symbols*");
                textBox4.BackColor = Color.OrangeRed;
                textBox4.Focus();
            }
            else if (char.TryParse(textBox4.Text, out let) == true)
            {
                textBox5.BackColor = Color.White;
                MessageBox.Show("*Mobile number can't contain letters/symbols*");
                textBox4.BackColor = Color.OrangeRed;
                textBox4.Focus();

            }
            else if (Regex.IsMatch(textBox4.Text, NZ_Mobile) == false)
            {
                textBox5.BackColor = Color.White;
                MessageBox.Show("*Please enter a valid Mobile number of ten digits " +
                    "\n                     and must start with 0*");
                textBox4.BackColor = Color.OrangeRed;
                textBox4.Focus();

            }
            else if (textBox3.Text == "")
            {

                textBox4.BackColor = Color.White;
                MessageBox.Show("*Email Address required*");
                textBox3.BackColor = Color.OrangeRed;
                textBox3.Focus();
            }
            else if (Regex.IsMatch(textBox3.Text, email) == false)
            {
                textBox4.BackColor = Color.White;
                MessageBox.Show("*Please enter a vaild email address*");
                textBox3.BackColor = Color.OrangeRed;
                textBox3.Focus();
            }
            else if (dateTimePicker1.Value < DateTime.Today)
            {
                textBox3.BackColor = Color.White;
                MessageBox.Show("*Please select a vaild date*");

            }

            else if (numericUpDown1.Value == 0)
            {
                textBox3.BackColor = Color.White;
                MessageBox.Show("*Please enter the Meeting time:*");
                numericUpDown1.BackColor = Color.OrangeRed;
                numericUpDown2.BackColor = Color.OrangeRed;
            }
            else if (comboBox2.SelectedIndex == -1)
            {
                numericUpDown1.BackColor = Color.White;
                numericUpDown2.BackColor = Color.White;
                MessageBox.Show("Please select AM or PM");

            }
            else if (comboBox1.SelectedIndex == -1)
            {
                numericUpDown1.BackColor = Color.White;
                numericUpDown2.BackColor = Color.White;
                MessageBox.Show("*Please select a person to meet*");
            }
            else if (buttonwasclicked == false)
            {
                MessageBox.Show("*Please select a meeting aim*");
            }
            else if (button2.Text == "Select an option")
            {
                MessageBox.Show("*Please select a meeting aim*");
            }
            else
            {

                listBox1.Items.Add("First Name: " + textBox1.Text);
                listBox1.Items.Add("Last Name: " + textBox5.Text);
                listBox1.Items.Add("Mobile Number: " + textBox4.Text);
                listBox1.Items.Add("Email Address: " + textBox3.Text);
                listBox1.Items.Add("Meeting Date: " + dateTimePicker1.Value.ToShortDateString());
                listBox1.Items.Add("Meeting Time: " + numericUpDown1.Value + ":" + numericUpDown2.Value + " " + comboBox2.SelectedItem);
                listBox1.Items.Add("Meeting with: " + comboBox1.SelectedItem);
                listBox1.Items.Add("Meeting Aim: " + button2.Text);
                listBox1.Items.Add("    ***************************");

                // MySQL Connection - Insert Function

                if (con.State == ConnectionState.Open) // to ensure connection is open or not
                {
                    string query = "INSERT INTO visitor(Visitor_Name, Visitor_SurName, " +
                        "Visitor_Mobile, Visitor_Email, " +
                        "Meeting_Date, Meeting_Time, " +
                        "Meeting_Aim, Staff_ID) " +
                        "VALUES('" + textBox1.Text + "','" + textBox5.Text
                        + "','" + textBox4.Text + "','" + textBox3.Text
                        + "','" + dateTimePicker1.Value.ToShortDateString()
                        + "','" + numericUpDown1.Value + ": " + numericUpDown2.Value
                        + comboBox2.SelectedItem + "','" + button2.Text + "', '" +
                        (comboBox1.SelectedIndex + 1) + "')";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data INSERTED successfully");
                }
                else
                {
                    MessageBox.Show("Connection not established");
                }

                resetFields();
                button3.Enabled = true;
                button4.Enabled = true;
                button6.Enabled = true;

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            buttonwasclicked = true;

            Form2 sv = new Form2();


            if (sv.ShowDialog() == DialogResult.OK)
            {
                if (sv.radioButton1.Checked == true)
                    button2.Text = sv.radioButton1.Text;
                else if (sv.radioButton2.Checked == true)
                    button2.Text = sv.radioButton2.Text;
                else if (sv.radioButton3.Checked == true)
                    button2.Text = sv.radioButton3.Text;
                else if (sv.radioButton4.Checked == true)
                    button2.Text = sv.radioButton4.Text;
                else if (!sv.radioButton1.Checked && !sv.radioButton2.Checked && !sv.radioButton3.Checked && !sv.radioButton4.Checked)
                {
                    MessageBox.Show("*Please select one option*");
                }
            }
            else
            {
                button2.Text = "Select an option";

            }
        }
        void validations()
        {

        }
        void resetFields()
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;

            comboBox1.Text = "Select one";
            comboBox2.Text = "Select one";

            button2.Text = "Select an option";

            dateTimePicker1.ResetText();

            textBox3.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox1.BackColor = Color.White;
            comboBox1.BackColor = Color.LightGray;
            comboBox2.BackColor = Color.LightGray;
            button2.BackColor = Color.LightGray;
            numericUpDown1.BackColor = Color.White;
            numericUpDown2.BackColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            
             if (MessageBox.Show("Do you want to REMOVE this detail", "REMOVE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (listBox1.Items.Count == 0)
                {
                    button3.Enabled = false;
                    MessageBox.Show("*The List is all clear*");
                }
                else if (listBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("*Please Select something to remove.*");
                }
                else if (listBox1.SelectedIndex > -1)
                {
                    
                   int index = listBox1.SelectedIndex;
                    if (index % 9 == 0)
                    {
                        // to remove the selected data
                        if (con.State == ConnectionState.Open)
                        {
                            string query = "DELETE FROM visitor WHERE visitor_name='" + textBox1.Text + "' ";
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.ExecuteNonQuery();

                            for (int i = index; i < index + 9; i++)
                            {
                                listBox1.Items.RemoveAt(index);
                            }

                            MessageBox.Show("Data REMOVED successfully");
                        }
                        else
                        {
                            MessageBox.Show("Connection not established");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select the visitor's First Name to Remove all thier details");
                    }
                }

            
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to DELETE all the details", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (listBox1.Items.Count == 0)
                {
                    button4.Enabled = false;
                    MessageBox.Show("*The List is all clear*");
                }
                else
                { listBox1.Items.Clear();

                    // to delete all data stored in the database
                    if (con.State == ConnectionState.Open)
                    {
                        string Delete_query = "DELETE FROM Visitor";
                        MySqlCommand cmd = new MySqlCommand(Delete_query, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data DELETED successfully");
                    }
                    else
                    {
                        MessageBox.Show("Connection not established");
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //if (listBox1.Items.Count < 0 )
            //{
            if (ClickCounter % 2 != 0)
            {
                // to display the data stored in the database
                if (con.State == ConnectionState.Open)
                {
                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = con;
                        command.CommandText = "SELECT * FROM visitor" +
                            " INNER JOIN Staff on visitor.Staff_ID = staff.Staff_ID  ";
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listBox1.Items.Add("First Name: " + reader["Visitor_Name"]);
                                listBox1.Items.Add("Last Name: " + reader["Visitor_SurName"]);
                                listBox1.Items.Add(("Mobile Number: ") + reader["Visitor_Mobile"]);
                                listBox1.Items.Add(("Email Address: ") + reader["Visitor_Email"]);
                                listBox1.Items.Add(("Meeting Date: ") + reader["Meeting_Date"]);
                                listBox1.Items.Add(("Meeting Time: ") + reader["Meeting_Time"]);
                                listBox1.Items.Add("Meeting With: " + reader["Staff_Name"]);
                                listBox1.Items.Add(("Meeting Aim: ") + reader["Meeting_Aim"]);
                                listBox1.Items.Add("    ***************************  ");

                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Connection not established");
                }
                button6.Text = "CLEAR the list";
                ClickCounter++;
                button3.Enabled = true;
            }
            else
            {
                listBox1.Items.Clear();
                button6.Text = "DISPLAY stored data";
                ClickCounter++;
            }
            // }
            /* else
             {
                 MessageBox.Show("There is no data to DISPLAY");
                 button6.Enabled = false;
                 button6.Text = "DISPLAY stored data";
                 ClickCounter++;
             }*/
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int num = 0;
            char let;
            string email = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
           + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
           + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            string NZ_Mobile = (@"^[0-9]{10}$");
            if (listBox1.SelectedIndex > -1)
            {
                int index = listBox1.SelectedIndex;

                // update details- Mysql Connection
                if (index % 9 == 0)
                {

                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("*First Name can't be empty*");
                        textBox1.BackColor = Color.OrangeRed;
                        textBox1.Focus();

                    }
                    else if (int.TryParse(textBox1.Text, out num) == true)
                    {
                        MessageBox.Show("*First Name can't contain numbers/symbols*");
                        textBox1.BackColor = Color.OrangeRed;
                        textBox1.Focus();

                    }
                    else if (char.TryParse(textBox1.Text, out let) == true)
                    {
                        MessageBox.Show("*First Name can't contain numbers/symbols*");
                        textBox1.BackColor = Color.OrangeRed;
                        textBox1.Focus();
                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "First Name: " + textBox1.Text);
                        string update = "Update visitor Set Visitor_Name = '" + textBox1.Text + "' Where Visitor_Name = '" + textBox2.Text + "' ";
                        MySqlCommand cmd = new MySqlCommand(update, con);
                        MySqlDataAdapter up = new MySqlDataAdapter(cmd);
                        up.UpdateCommand = con.CreateCommand();
                        up.UpdateCommand.CommandText = update;
                        if (up.UpdateCommand.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Detail UPDATED successfully");
                        }
                    }
                }
                else if (index % 9 == 1)
                {
                    if (textBox5.Text == "")
                    {
                        textBox1.BackColor = Color.White;
                        MessageBox.Show("*Last Name needs to be filled*");
                        textBox5.BackColor = Color.OrangeRed;
                        textBox5.Focus();
                    }
                    else if (int.TryParse(textBox5.Text, out num) == true)
                    {
                        textBox1.BackColor = Color.White;
                        MessageBox.Show("*Last Name can't contain numbers/symbols*");
                        textBox5.BackColor = Color.OrangeRed;
                        textBox5.Focus();

                    }
                    else if (char.TryParse(textBox5.Text, out let) == true)
                    {
                        textBox1.BackColor = Color.White;
                        MessageBox.Show("*Last Name can't contain numbers/symbols*");
                        textBox5.BackColor = Color.OrangeRed;
                        textBox5.Focus();
                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "Last Name: " + textBox5.Text);
                        string update = "Update visitor Set Visitor_SurName = '" + textBox5.Text + "' Where Visitor_SurName = '" + textBox2.Text + "' ";
                        MySqlCommand cmd = new MySqlCommand(update, con);
                        MySqlDataAdapter up = new MySqlDataAdapter(cmd);
                        up.UpdateCommand = con.CreateCommand();
                        up.UpdateCommand.CommandText = update;
                        if (up.UpdateCommand.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Detail UPDATED successfully");
                        }
                    }

                }
                else if (index % 9 == 2)
                {
                    if (textBox4.Text == "")
                    {
                        textBox5.BackColor = Color.White;
                        MessageBox.Show("*Mobile number required*");
                        textBox4.BackColor = Color.OrangeRed;
                        textBox4.Focus();
                    }
                    else if (int.TryParse(textBox4.Text, out num) == false)
                    {
                        textBox5.BackColor = Color.White;
                        MessageBox.Show("*Mobile number can't contain letters/symbols*");
                        textBox4.BackColor = Color.OrangeRed;
                        textBox4.Focus();
                    }
                    else if (char.TryParse(textBox4.Text, out let) == true)
                    {
                        textBox5.BackColor = Color.White;
                        MessageBox.Show("*Mobile number can't contain letters/symbols*");
                        textBox4.BackColor = Color.OrangeRed;
                        textBox4.Focus();

                    }
                    else if (Regex.IsMatch(textBox4.Text, NZ_Mobile) == false)
                    {
                        textBox5.BackColor = Color.White;
                        MessageBox.Show("*Please enter a valid Mobile number of ten digits " +
                            "\n                     and must start with 0*");
                        textBox4.BackColor = Color.OrangeRed;
                        textBox4.Focus();

                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "Mobile Number: " + textBox4.Text);
                        string update = "Update visitor Set Visitor_Mobile = '" + textBox4.Text + "' Where Visitor_Mobile = '" + textBox2.Text + "' ";
                        MySqlCommand cmd = new MySqlCommand(update, con);
                        MySqlDataAdapter up = new MySqlDataAdapter(cmd);
                        up.UpdateCommand = con.CreateCommand();
                        up.UpdateCommand.CommandText = update;
                        if (up.UpdateCommand.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Detail UPDATED successfully");
                        }
                    }
                }
                else if (index % 9 == 3)
                {
                    if (textBox3.Text == "")
                    {

                        textBox4.BackColor = Color.White;
                        MessageBox.Show("*Email Address required*");
                        textBox3.BackColor = Color.OrangeRed;
                        textBox3.Focus();
                    }
                    else if (Regex.IsMatch(textBox3.Text, email) == false)
                    {
                        textBox4.BackColor = Color.White;
                        MessageBox.Show("*Please enter a vaild email address*");
                        textBox3.BackColor = Color.OrangeRed;
                        textBox3.Focus();
                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "Email Address: " + textBox3.Text);
                        string update = "Update visitor Set Visitor_Email = '" + textBox3.Text + "' Where Visitor_Email = '" + textBox2.Text + "' ";
                        MySqlCommand cmd = new MySqlCommand(update, con);
                        MySqlDataAdapter up = new MySqlDataAdapter(cmd);
                        up.UpdateCommand = con.CreateCommand();
                        up.UpdateCommand.CommandText = update;
                        if (up.UpdateCommand.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Detail UPDATED successfully");
                        }
                    }
                }
                else if (index % 9 == 4)
                {
                    if (dateTimePicker1.Value < DateTime.Today)
                    {
                        textBox3.BackColor = Color.White;
                        MessageBox.Show("*Please select a vaild date*");

                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "Meeting Date: " + dateTimePicker1.Value.ToShortDateString());
                    }
                }
                else if (index % 9 == 5)
                {
                    if (numericUpDown1.Value == 0)
                    {
                        textBox3.BackColor = Color.White;
                        MessageBox.Show("*Please enter the Meeting time:*");
                        numericUpDown1.BackColor = Color.OrangeRed;
                        numericUpDown2.BackColor = Color.OrangeRed;
                    }
                    else if (comboBox2.SelectedIndex == -1)
                    {
                        numericUpDown1.BackColor = Color.White;
                        numericUpDown2.BackColor = Color.White;
                        MessageBox.Show("Please select AM or PM");

                    }
                    else if (comboBox1.SelectedIndex == -1)
                    {
                        numericUpDown1.BackColor = Color.White;
                        numericUpDown2.BackColor = Color.White;
                        MessageBox.Show("*Please select a person to meet*");
                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "Meeting Time: " + numericUpDown1.Text + ":" + numericUpDown2.Text + " " + comboBox2.Text);

                    }
                }
                else if (index % 9 == 6)
                {

                    if (comboBox1.SelectedIndex == -1)
                    {
                        numericUpDown1.BackColor = Color.White;
                        numericUpDown2.BackColor = Color.White;
                        MessageBox.Show("*Please select a person to meet*");
                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "Meeting With: " + comboBox1.Text);

                    }
                }
                else if (index % 9 == 7)
                {
                    if (buttonwasclicked == false)
                    {
                        MessageBox.Show("*Please select a meeting aim*");
                    }
                    else if (button2.Text == "Select an option")
                    {
                        MessageBox.Show("*Please select a meeting aim*");
                    }
                    else
                    {
                        listBox1.Items.RemoveAt(index);
                        listBox1.Items.Insert(index, "Meeting Aim: " + button2.Text);
                        string update = "Update visitor Set Meeting_Aim = '" + button2.Text + "' Where Meeting_Aim = '" + textBox2.Text + "' ";
                        MySqlCommand cmd = new MySqlCommand(update, con);
                        MySqlDataAdapter up = new MySqlDataAdapter(cmd);
                        up.UpdateCommand = con.CreateCommand();
                        up.UpdateCommand.CommandText = update;
                        if (up.UpdateCommand.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Detail UPDATED successfully");
                        }
                    }
                }
                else
                {

                    resetFields();
                }
            }

            else
            {
                MessageBox.Show("*Please Select something to update.*");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            int index = listBox1.SelectedIndex;


            if (listBox1.SelectedIndex > -1)
            {
                textBox2.Text = listBox1.SelectedItem.ToString();
                if (index % 9 == 0)
                {
                    resetFields();
                    textBox1.BackColor = Color.LightBlue;
                    textBox1.Text = listBox1.SelectedItem.ToString();
                    textBox1.Text = textBox1.Text.Replace("First Name: ", "");
                    textBox2.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = textBox2.Text.Replace("First Name: ", "");

                }
                else if (index % 9 == 1)
                {
                    resetFields();
                    textBox5.BackColor = Color.LightBlue;
                    textBox5.Text = listBox1.SelectedItem.ToString();
                    textBox5.Text = textBox5.Text.Replace("Last Name: ", "");
                    textBox2.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = textBox2.Text.Replace("Last Name: ", "");

                }
                else if (index % 9 == 2)
                {
                    resetFields();
                    textBox4.BackColor = Color.LightBlue;
                    textBox4.Text = listBox1.SelectedItem.ToString();
                    textBox4.Text = textBox4.Text.Replace("Mobile Number: ", "");
                    textBox2.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = textBox2.Text.Replace("Mobile Number: ", "");

                }
                else if (index % 9 == 3)
                {
                    resetFields();
                    textBox3.BackColor = Color.LightBlue;
                    textBox3.Text = listBox1.SelectedItem.ToString();
                    textBox3.Text = textBox3.Text.Replace("Email Address: ", "");
                    textBox2.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = textBox2.Text.Replace("Email Address: ", "");
                }
                else if (index % 9 == 4)
                {
                    resetFields();
                    dateTimePicker1.CalendarTrailingForeColor = Color.LightBlue;
                    textBox2.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = textBox3.Text.Replace("Meeting Date: ", "");

                }
                else if (index % 9 == 5)
                {
                    resetFields();
                    comboBox2.BackColor = Color.LightBlue;
                    numericUpDown1.BackColor = Color.LightBlue;
                    numericUpDown2.BackColor = Color.LightBlue;

                }
                else if (index % 9 == 6)
                {
                    resetFields();
                    comboBox1.BackColor = Color.LightBlue;
                    comboBox1.Text = listBox1.SelectedItem.ToString();
                    comboBox1.Text = comboBox1.Text.Replace("Meeting With: ", "");
                    textBox2.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = textBox2.Text.Replace("Meeting With: ", "");

                }
                else if (index % 9 == 7)
                {
                    resetFields();
                    button2.BackColor = Color.LightBlue;
                    button2.Text = listBox1.SelectedItem.ToString();
                    button2.Text = button2.Text.Replace("Meeting Aim: ", "");
                    textBox2.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = textBox2.Text.Replace("Meeting Aim: ", "");
                }
                else
                {
                    resetFields();
                }

            }
        }
    }
}
