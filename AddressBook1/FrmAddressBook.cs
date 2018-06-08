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


namespace AddressBook1
   
{
    public partial class FrmAddressBook : Form
    {
        Address p = new Address();
        AddressBook1Controller controller = new AddressBook1Controller();
         Address address = new Address();
        public FrmAddressBook()
        {
            InitializeComponent();
        }

        public void FrmAddressBook_Load(object sender, EventArgs e)
        {
            List<Address> listData = controller.ListData;
            foreach (Address item in listData)
            {
                dgvData.Rows.Add(item.Nama,item.Alamat,item.Kota,item.NoHp,item.TanggalLahir,item.Email);
            }
            this.lblBanyakRecordData.Text = $"{this.dgvData.Rows.Count.ToString("n0")} Record data."; 
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            FrmTambahData tambah = new FrmTambahData(true);
            tambah.Run(tambah);
            tambah.Loads(p);
            if (tambah.txtNama.Text != null )
            {
                dgvData.Rows.Add(p.Nama, p.Alamat, p.Kota, p.NoHp, p.TanggalLahir, p.Email);
            }
            this.lblBanyakRecordData.Text = $"{this.dgvData.Rows.Count.ToString("n0")} Record data.";
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        { 
            FrmTambahData edit = new FrmTambahData(false);
            controller.Edit(dgvData,address);
            edit.Moves(address);
            edit.Run(edit);
            dgvData.Rows.Clear();
            FrmAddressBook_Load(null, null);
        
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            controller.Edit(dgvData,address);
            controller.Delete(address,dgvData);
            int rowindex = dgvData.CurrentCell.RowIndex;
            dgvData.Rows.RemoveAt(rowindex);
            this.lblBanyakRecordData.Text = $"{this.dgvData.Rows.Count.ToString("n0")} Record data.";
            
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            FrmTambahData frm = new FrmTambahData(true);
            frm.Loads(p);
           /* bool txtnama,txtalamat,txtkota,txtnohp,txtemail,txttgllahir;
            controller.Filter(dgvData,p);

            dgvData.Rows.Clear();
            string[] line = File.ReadAllLines("addressbook.csv");
            txtnama = txtalamat = txtkota = txtemail = txtnohp = txttgllahir = false;
                    if (txtNama.Text.Trim() == "")
                        txtnama = true;
                    if (txtAlamat.Text.Trim() == "")
                        txtalamat = true;
                    if (txtKota.Text.Trim() == "")
                        txtkota = true;
                    if (txtNoHp.Text.Trim() == "")
                        txtnohp = true;
                    if (txtTglLahir.Text.Trim() == "")
                        txttgllahir = true;
                    if (txtEmail.Text.Trim() == "")
                        txtemail = true;
                foreach (string item in line)
                {
                   
                    string[] arrItem = item.Split(';');
                    if (txtnama && txtkota && txtalamat && txtnohp && txttgllahir && txtemail)
                    {
                        FrmAddressBook_Load(null, null);
                    break;
                    }
                    else if ((arrItem[0].Contains(txtNama.Text) || txtnama) && (arrItem[1].Contains(txtAlamat.Text) || txtalamat) && (arrItem[2].Contains(txtKota.Text) || txtkota) &&
                        (arrItem[3].Contains(txtNoHp.Text) || txtnohp) && (arrItem[4].Contains(txtTglLahir.Text) || txttgllahir) && (arrItem[5].Contains(txtEmail.Text) || txtemail))
                    {
                        dgvData.Rows.Add(new string[] { arrItem[0], arrItem[1], arrItem[2], arrItem[3], arrItem[4], arrItem[5] });
                    }
                    else
                {
                  
                }



                }
                */
            
    
        }
 
        private void txtNoHp_KeyPress(object sender, KeyPressEventArgs e)
        {FrmTambahData frm = new FrmTambahData(true);
           
            frm.txtNoHp_KeyPress(sender,e);

        }
    }
    }

