﻿using GarmentsSquare.BL;
using GarmentsSquare.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarmentsSquare.UserControls
{
    public partial class ucAddEmployee : UserControl
    {
        public ucAddEmployee()
        {
            InitializeComponent();
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            if(txtName.Text == "Name")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Black;
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Text = "Name";
                txtName.ForeColor = Color.Silver;
            }
        }

        private void txtSalary_Enter(object sender, EventArgs e)
        {
            if (txtSalary.Text == "Salary")
            {
                txtSalary.Text = "";
                txtSalary.ForeColor = Color.Black;
            }
        }

        private void txtSalary_Leave(object sender, EventArgs e)
        {
            if (txtSalary.Text == "")
            {
                txtSalary.Text = "Salary";
                txtSalary.ForeColor = Color.Silver;
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Email";
                txtEmail.ForeColor = Color.Silver;
            }
        }

        private void txtContact_Enter(object sender, EventArgs e)
        {
            if (txtContact.Text == "Contact No.")
            {
                txtContact.Text = "";
                txtContact.ForeColor = Color.Black;
            }
        }

        private void txtContact_Leave(object sender, EventArgs e)
        {
            if (txtContact.Text == "")
            {
                txtContact.Text = "Contact No.";
                txtContact.ForeColor = Color.Silver;
            }
        }

        private void txtCnic_Enter(object sender, EventArgs e)
        {
            if (txtCnic.Text == "CNIC (Without Dashes)")
            {
                txtCnic.Text = "";
                txtCnic.ForeColor = Color.Black;
            }
        }

        private void txtCnic_Leave(object sender, EventArgs e)
        {

            if (txtCnic.Text == "")
            {
                txtCnic.Text = "CNIC (Without Dashes)";
                txtCnic.ForeColor = Color.Silver;
            }
        }

        private void resetFields()
        {
            txtName.Text = "Name";
            txtName.ForeColor = Color.Silver;
            txtSalary.Text = "Salary";
            txtSalary.ForeColor = Color.Silver;
            txtEmail.Text = "Email";
            txtEmail.ForeColor = Color.Silver;
            txtContact.Text = "Contact No.";
            txtContact.ForeColor = Color.Silver;
            txtCnic.Text = "CNIC (Without Dashes)";
            txtCnic.ForeColor = Color.Silver;
        }

        private void btnAddEmployee_Click_1(object sender, EventArgs e)
        {
            try
            {
                string name = this.txtName.Text;
                float salary = float.Parse(this.txtSalary.Text);
                string email = this.txtEmail.Text;
                string contact = this.txtContact.Text;
                string cnic = this.txtCnic.Text;

                if (name == "Name")
                {
                    throw new Exception("Please enter the name.");
                }
                if (salary <= 0)
                {
                    throw new Exception("Please enter the Salray in Correct Formate.");
                }
                if (email == "Email")
                {
                    throw new Exception("Please enter the email.");
                }
                if (email.Contains("@gmail.com") == false)
                {
                    throw new Exception("Please enter the email correctly.");
                }
                if (contact == "Contact")
                {
                    throw new Exception("Please enter the contact.");
                }
                if (contact.Length != 11)
                {
                    throw new Exception("Please enter the contact no. correctly.");
                }
                if (contact[0] != '0' && contact[1] != '3')
                {
                    throw new Exception("Please enter the contact no. correctly.");
                }
                for (int i = 0; i < contact.Length; i++)
                {
                    if(Char.IsDigit(contact, i) == false)
                    {
                        throw new Exception("Please enter the contact no. correctly.");
                    }
                }
                if (cnic == "CNIC")
                {
                    throw new Exception("Please enter the cnic.");
                }
                if (cnic.Length != 13)
                {
                    throw new Exception("Please enter the cnic correctly.");
                }
                for (int i = 0; i < cnic.Length; i++)
                {
                    if (Char.IsDigit(cnic, i) == false)
                    {
                        throw new Exception("Please enter the cnic correctly.");
                    }
                }

                resetFields();
                Employee employee = new Employee(name, salary, email, contact, cnic);
                EmployeeDL.AddEmployee(employee);

                string username = UserDL.RandomString(8);
                string password = UserDL.CreatePassword(8);
                // adding new user credentials
                User user = new User(username, password, employee.Id, "Employee");
                UserDL.AddUserIntoList(user);
                UserDL.storeSingleObject("Users.csv", user);

                // sending email to newly added employee
                Utility.EmailSender.sendEmailActorAdded(employee.Email, employee.Name, "Employee", employee.Id, username, password);

                EmployeeDL.storeData("Employees.csv");
                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btnClearFields_Click_1(object sender, EventArgs e)
        {
            resetFields();
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        

    }
}
