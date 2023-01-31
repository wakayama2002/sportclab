using System;
using System.Windows.Forms;

namespace SportsClub
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }
        private void AddData_Click(object sender, EventArgs e)
        {
            this.Visible = false;//今開いている画面を非表示
            Form2 form2 = new Form2();
            form2.Show();//From2の画面を開く

        }

        private void DataViewClick(object sender, EventArgs e)
        {

            this.Visible = false;//今開いている画面を非表示
            SearchData dataSerach = new SearchData();
            dataSerach.Show();//Form３の画面を開く
        }


        private void DeleteClick(object sender, EventArgs e)
        {
            this.Visible = false;//今開いている画面を非表示
            Update update = new Update();
            update.Show();//From2の画面を開く
        }

       
    }
}