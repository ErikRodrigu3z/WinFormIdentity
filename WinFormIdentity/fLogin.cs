using Microsoft.AspNetCore.Identity;
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
using WinFormIdentity.Services;

namespace WinFormIdentity
{
    public partial class fLogin : Form
    {
        private readonly ApplicationDbContext _db;
        private readonly AuthService _service;
        public fLogin(ApplicationDbContext db, AuthService service)
        {
            InitializeComponent();

            _db = db;
            _service = service;
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            try
            {
                var users = _db.AppUsers.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (await _service.Login(txtUser.Text, txtPassword.Text))
            {
                MessageBox.Show("Login Exitoso");
            }
            else
            {
                MessageBox.Show("Fallo Login");
            }
        }
    }
}
