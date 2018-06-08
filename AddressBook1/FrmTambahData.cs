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
using System.Text.RegularExpressions;

namespace AddressBook1
{
    public partial class FrmTambahData : Form
    {
         bool _result = false;

        public bool _addMode = false; // true:additem ,false: edit item
        AddressBook1Controller controller = new AddressBook1Controller();
        Address address = new Address();
        Address p = new Address();
        public void Loads(Address p)
        {
            p.Nama = this.txtNama.Text.Trim();
            p.Alamat = this.txtAlamat.Text.Trim();
            p.Kota = this.txtKota.Text.Trim();
            p.NoHp = this.txtNoHp.Text.Trim();
            p.TanggalLahir = Convert.ToDateTime(this.dtpTglLahir.Text);
            p.Email = this.txtEmail.Text.Trim();

        }
       
        public void Moves(Address address)
        {   this.txtNama.Text = address.Nama;
            this.txtAlamat.Text = address.Alamat;
            this.txtKota.Text = address.Kota;
            this.txtNoHp.Text = address.NoHp;
            this.dtpTglLahir.Text = address.TanggalLahir.ToString();
            this.txtEmail.Text = address.Email;

        }
        public bool Run(FrmTambahData form)
        {
            form.ShowDialog();
            return _result;
        }
        public FrmTambahData(bool addMode)
        {
            InitializeComponent();
            _addMode = addMode;

        }
      
        private void btnBatal_Click(object sender, EventArgs e)
        {
            txtNama.Text = null;
            this.Close();
            
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            //validasi
            if (this.txtNama.Text.Trim() == "")//jika isian nama kosong
            {
                MessageBox.Show("Sorry , nama wajib isi ...");
                this.txtNama.Focus();
            }
            else if (this.txtKota.Text.Trim() == "")
            {
                MessageBox.Show("Sorry , kota wajib isi ...");
                this.txtKota.Focus();
            }
            else if (this.txtAlamat.Text.Trim() == "")
            {
                MessageBox.Show("Sorry , alamat wajib isi ...");
                this.txtAlamat.Focus();
            }
            else if (this.txtNoHp.Text.Trim() == "")
            {
                MessageBox.Show("Sorry , no.hp wajib isi ...");
                this.txtNoHp.Focus();

            }
            else if (this.txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Sorry , email wajib isi ...");
                this.txtEmail.Focus();
            }
            else
            {
                try {
                    if (_addMode)
                    {
                        Loads(p);
                        controller.AddItem(_addMode, _result, p, address);
                        this.Close();
                    }
                    else
                    {

                        Loads(p);
                        controller.AddItem(_addMode, _result, p, address);
                        this.Close();

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                }
        

                
            
         /*  else
            {
                try
                {//simpan data ke file
                if (_addMode) //add new item
                {
                        using(FileStream fs=new FileStream("addressbook.csv", FileMode.Append, FileAccess.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(fs)) 
                            {
                                writer.WriteLine($"{txtNama.Text.Trim()};{txtAlamat.Text.Trim()};{txtKota.Text.Trim()};{txtNoHp.Text.Trim()};{dtpTglLahir.Value.ToShortDateString()};{txtEmail.Text.Trim()}");
                            }
                        }
                        
                        
                }
                else //edit data
                {
                        Address address = new Address();


                        string[] line = File.ReadAllLines("addressbook.csv");
                        using(FileStream fs=new FileStream("addressbook.csv", FileMode.Create, FileAccess.ReadWrite))
                        {
                            using(StreamWriter writer =new StreamWriter(fs))
                            {
                                for(int i = 0; i < line.Length; i++)
                                {
                                    if (line[i] ==$"{address.Nama};{address.Alamat};{address.Kota};{address.NoHp};{address.TanggalLahir};{address.Email}")
                                    {
                                        AddressBook1Controller controller = new AddressBook1Controller();
                                        List<Address> listData = controller.ListData;
                                        
                                        listData.Add(new Address
                                        {
                                            Nama = txtNama.Text,
                                            Alamat = txtAlamat.Text,
                                            Kota = txtKota.Text,
                                            NoHp = txtNoHp.Text,
                                            TanggalLahir = Convert.ToDateTime(dtpTglLahir.Text),
                                            Email = txtEmail.Text
                                        });
                                        writer.WriteLine($"{txtNama.Text.Trim()};{txtAlamat.Text.Trim()};{txtKota.Text.Trim()};{txtNoHp.Text.Trim()};{dtpTglLahir.Value.ToShortDateString()};{txtEmail.Text.Trim()}");

                                    }
                                    else
                                    {
                                        writer.WriteLine(line[i]);
                                    }
                                }
                            }
                        }

                }
                    _result = true;
                        this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                

            }
            
           */  
        }

        public void txtNoHp_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' ' || e.KeyChar == '-' || e.KeyChar == '(' || e.KeyChar == ')')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private bool EmailIsValid(string emailAddr)

        {

            string emailPattern1 = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            Regex regex = new Regex(emailPattern1);

            Match match = regex.Match(emailAddr);

            return match.Success;

        }



        public void txtEmail_Leave(object sender, EventArgs e)

        {

            if (this.txtEmail.Text.Trim() != "")

            {

                if (!EmailIsValid(this.txtEmail.Text))

                {

                    MessageBox.Show("Sorry, data email tidak valid ...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.txtEmail.Clear();

                    this.txtEmail.Focus();

                }

            }

        }
    }
    
}
