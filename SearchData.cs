using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class SearchData : Form
    {
        public SearchData()
        {
            InitializeComponent();
        }
        #region 1つがチェックされると他のチェックが外れる
        private void NumberViewCheckedChanged(object sender, EventArgs e)
        {
            if (this.NumberView.Checked)
            {
                TelephoneView.Checked = false;
                AllView.Checked = false;
            }
        }

        private void TelephoneViewCheckedChanged(object sender, EventArgs e)
        {
            if (this.TelephoneView.Checked)
            {
                NumberView.Checked = false;
                AllView.Checked = false;
            }
        }
        private void AllViewCheckedChanged(object sender, EventArgs e)
        {
            if(this.AllView.Checked)
            {
                NumberView.Checked = false;
                TelephoneView.Checked = false;
            }
        }
        #endregion
        /// <summary>
        /// チェックされた方を参照して検索する
        /// </summary>
        private void SsearchClick(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=member.db"))
            {
                if (NumberView.Checked == true)
                {
                    con.Open();
                    //DataTableを生成します。
                    var dataTable = new DataTable();
                    var adapter = new SQLiteDataAdapter("SELECT * FROM m_product WHERE CD =" + NumberBox.Text, con);
                    adapter.Fill(dataTable);
                    DataShow.DataSource = dataTable;//データベースの表示
                    con.Close();
                }
                if (TelephoneView.Checked == true)
                {
                    con.Open();
                    var adapter = new SQLiteDataAdapter("SELECT * FROM m_product WHERE Telephone=" + TelephoneBox.Text, con);
                    //DataTableを生成します。
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    DataShow.DataSource = dataTable;//データベースの表示

                    con.Close();
                }
                if (AllView.Checked == true)
                {
                    con.Open();
                    //DataTableを生成します。
                    var dataTable = new DataTable();
                    var adapter = new SQLiteDataAdapter("SELECT * FROM m_product",con);
                    adapter.Fill(dataTable);
                    DataShow.DataSource = dataTable;//データベースの表示
                    con.Close();
                }
            }
        }
        /// <summary>
        /// スタート画面に戻る
        /// </summary>
        private void BackButton_Click(object sender, EventArgs e)
        {
            Start home = new Start();
            home.Show();
            this.Visible = false;
        }


    }
}
