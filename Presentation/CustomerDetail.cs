using DataLayer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Presentation
{
    public partial class CustomerDetail : Form
    {
        private Customer customer = null;
        private CustomerDataContext ctx = new CustomerDataContext();

        public CustomerDetail(CustomerModel cust = null)
        {
            InitializeComponent();

            if (cust == null)
            {
                customer = new Customer();
            }
            else
            {
                customer = ctx.Customers.Find(cust.CustomerId);
            }

            txtFirstname.DataBindings.Add("Text", customer, "Firstname");
            txtLastname.DataBindings.Add("Text", customer, "Lastname");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (customer.CustomerId == 0)
            {
                ctx.Database.ExecuteSqlRaw("exec AddCustomer @first, @last", new SqlParameter("@first", customer.Firstname), new SqlParameter("@last", customer.Lastname));
                this.Close();
            }
            else
            {
                ctx.Database.ExecuteSqlRaw("EXECUTE dbo.EditCustomer @id, @first, @last", new SqlParameter("@id", customer.CustomerId), new SqlParameter("@first", customer.Firstname), new SqlParameter("@last", customer.Lastname));
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
