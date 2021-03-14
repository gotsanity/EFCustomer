using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class CustomersForm : Form
    {
        private CustomerDataContext ctx = new CustomerDataContext();
        private CustomerModel selectedCustomer = null;

        public CustomersForm()
        {
            InitializeComponent();

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            gridCustomer.DataSource = ctx.Customers.Include(x => x.Addresses).Select(c => new CustomerModel
            {
                CustomerId = c.CustomerId,
                AddressId = c.Addresses.FirstOrDefault().AddressId,
                Lastname = c.Lastname,
                Firstname = c.Firstname,
                Street = c.Addresses.FirstOrDefault().Street,
                City = c.Addresses.FirstOrDefault().City,
                State = c.Addresses.FirstOrDefault().State,
                Zip = c.Addresses.FirstOrDefault().Zip
            }).ToList();

            this.selectedCustomer = gridCustomer.Rows[0].DataBoundItem as CustomerModel;
        }

        private void gridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.selectedCustomer = gridCustomer.Rows[e.RowIndex].DataBoundItem as CustomerModel;
            btnEdit.Enabled = true;
            btnEditAddress.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (Form custForm = new CustomerDetail(selectedCustomer))
            {
                custForm.ShowDialog();
                RefreshGrid();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (Form custForm = new CustomerDetail())
            {
                custForm.ShowDialog();
                RefreshGrid();
            }
        }

        private void btnEditAddress_Click(object sender, EventArgs e)
        {
            using (Form addrForm = new AddressDetail(selectedCustomer.CustomerId))
            {
                addrForm.ShowDialog();
                RefreshGrid();
            }
        }
    }
}
