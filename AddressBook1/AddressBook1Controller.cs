using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace AddressBook1
{
    public class AddressBook1Controller
    {
        
        public List<Address> ListData { get; set; }
        Address address = new Address();
         Address p = new Address();
        public  AddressBook1Controller()
        {
            ListData = new List<Address>();
            try
            {
                if (File.Exists(Properties.Settings.Default.NamaFile))
                {
                    string[] fileContent = File.ReadAllLines(Properties.Settings.Default.NamaFile);
                    foreach (string item in fileContent)
                    {
                        string[] arrItem = item.Split(';');
                        ListData.Add(new Address
                        {
                            Nama = arrItem[0].Trim(),
                            Alamat = arrItem[1].Trim(),
                            Kota = arrItem[2].Trim(),
                            NoHp = arrItem[3].Trim(),
                            TanggalLahir = Convert.ToDateTime(arrItem[4]),
                            Email = arrItem[5].Trim()
                        });
                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;

            }

        }
       
        public void AddItem(bool _addMode, bool _result, Address p,Address address)
        { 
            try
            {//simpan data ke file
                if (_addMode) //add new item
                {
                    using (FileStream fs = new FileStream("addressbook.csv", FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                           
                            writer.WriteLine($"{p.Nama.Trim()};{p.Alamat.Trim()};{p.Kota.Trim()};{p.NoHp.Trim()};{p.TanggalLahir.ToString()};{p.Email.Trim()}");
                             
               
                        }
                    }


                }

                else //edit data
                { 
                    string[] line = File.ReadAllLines("addressbook.csv");

                    using (FileStream fs = new FileStream("addressbook.csv", FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] == $"{address.Nama};{address.Alamat};{address.Kota};{address.NoHp};{address.TanggalLahir};{address.Email}")
                                {
                                    writer.WriteLine($"{p.Nama};{p.Alamat};{p.Kota};{p.NoHp};{p.TanggalLahir};{p.Email}");

                                }
                                else
                                {
                                    writer.WriteLine(line[i]);

                                }
                            }
                        }
                    }
                 /*   string[] fileContent = File.ReadAllLines("addressbook.csv");
                    using (FileStream fs = new FileStream("addressbook.csv", FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            foreach(string line in fileContent)
                            {
                                string[]arrline=line.Split(';');
                                if (arrline[0] == address.Nama && arrline[1] == address.Alamat && arrline[2] == address.Kota && arrline[3] == address.NoHp && Convert.ToDateTime(arrline[4]).Date == address.TanggalLahir.Date && arrline[5] == address.Email)
                                {
                                    
                                    writer.WriteLine($"{p.Nama.Trim()};{p.Alamat.Trim()};{p.Kota.Trim()};{p.NoHp.Trim()};{p.TanggalLahir.ToString()};{p.Email.Trim()}");


                                }
                                else
                                {

                                    writer.WriteLine($"{arrline[0]};{arrline[1]};{arrline[2]};{arrline[3]};{arrline[4]};{arrline[5]}");
                                }
                            }
                        }
                    }*/
                }
               
                _result = true;
                
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "addressbook", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        public void Delete(Address address, DataGridView dgvdata)
        {
            string[] line = File.ReadAllLines("addressbook.csv");
            using (FileStream fs = new FileStream("addressbook.csv", FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] != $"{address.Nama};{address.Alamat};{address.Kota};{address.NoHp};{address.TanggalLahir};{address.Email}")
                        {
                            writer.WriteLine(line[i]);
                        }
                        else
                        {

                        }
                      
                    }
                }
            }
            
            }
        public void Edit(DataGridView dgvdata,Address address)
        {
            address.Nama = dgvdata.CurrentRow.Cells[0].Value.ToString();
            address.Alamat = dgvdata.CurrentRow.Cells[1].Value.ToString();
            address.Kota = dgvdata.CurrentRow.Cells[2].Value.ToString();
            address.NoHp = dgvdata.CurrentRow.Cells[3].Value.ToString();
            address.TanggalLahir = Convert.ToDateTime(dgvdata.CurrentRow.Cells[4].Value.ToString());
            address.Email = dgvdata.CurrentRow.Cells[5].Value.ToString();

        
        }
     
       public void Filter(DataGridView dgvData,Address p)
        {
            bool txtnama, txtalamat, txtkota, txtnohp, txtemail, txttgllahir;
            dgvData.Rows.Clear();
            string[] line = File.ReadAllLines("addressbook.csv");
            txtnama = txtalamat = txtkota = txtemail = txtnohp = txttgllahir = false;
            if (p.Nama == "")
                txtnama = true;
            if (p.Alamat == "")
                txtalamat = true;
            if (p.Kota == "")
                txtkota = true;
            if (p.NoHp == "")
                txtnohp = true;
            if (p.TanggalLahir.ToString() == "")
                txttgllahir = true;
            if (p.Email == "")
                txtemail = true;
            foreach (string item in line)
            {
                
                string[] arrItem = item.Split(';');
                if (txtnama && txtkota && txtalamat && txtnohp && txttgllahir && txtemail)
                {
                    break;
                }
                else if ((arrItem[0].Contains(p.Nama) || txtnama) && (arrItem[1].Contains(p.Alamat) || txtalamat) && (arrItem[2].Contains(p.Kota) || txtkota) &&
                    (arrItem[3].Contains(p.NoHp) || txtnohp) && (arrItem[4].Contains(p.TanggalLahir.ToString()) || txttgllahir) && (arrItem[5].Contains(p.Email) || txtemail))
                {
                    dgvData.Rows.Add(new string[] { arrItem[0], arrItem[1], arrItem[2], arrItem[3], arrItem[4], arrItem[5] });
                }
                else
                {

                }



            }

        }
    }
}
        /* public void txtNoHp(object sender, KeyPressEventArgs e)
         {
             if (char.IsDigit(e.KeyChar))
             {
                 e.Handled = false;
             }
         }
        */

  

