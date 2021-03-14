using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Presentation
{
    public partial class AddressDetail : Form
    {
        private Customer customer;
        private Address address;
        private CustomerDataContext ctx = new CustomerDataContext();
        public AddressDetail(int customerID)
        {
            InitializeComponent();

            this.customer = ctx.Customers.Find(customerID);
            this.address = ctx.Addresses.Where(x => x.CustomerId == this.customer.CustomerId).FirstOrDefault();

            if (this.address == null)
            {
                this.address = new Address();
                this.address.CustomerId = customerID;
            }

            txtFirstname.DataBindings.Add("Text", this.customer, "Firstname");
            txtLastname.DataBindings.Add("Text", this.customer, "Lastname");
            txtStreet.DataBindings.Add("Text", this.address, "Street");
            txtCity.DataBindings.Add("Text", this.address, "City");
            txtState.DataBindings.Add("Text", this.address, "State");
            txtZip.DataBindings.Add("Text", this.address, "Zip");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.address.AddressId == 0)
            {
                ctx.Addresses.Add(this.address);
                ctx.SaveChanges();
                this.Close();
            }

            ctx.Entry(this.address).CurrentValues.SetValues(this.address);
            ctx.SaveChanges();
            this.Close();
        }
    }
}
