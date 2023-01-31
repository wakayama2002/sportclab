using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        ///<summary>
        ///入力されたCDとデータベースにあるCDを照合してdataを表示する
        /// </summary>
        private void CdSearchClick(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source = member.db"))
            {
                var dataTable = new DataTable();
                var adapter = new SQLiteDataAdapter("SELECT * FROM m_product WHERE CD =" + CdNumber.Text, con);
                adapter.Fill(dataTable);
                Datashow.DataSource = dataTable;
            }
        }

        ///<summary>
        ///入力された文字数字を上書き保存する（CDは変更しない)
        /// </summary>
        private void CorrectionClick(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=member.db"))
            {
                con.Open();
                #region パラメータ再設定
                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    SQLiteCommand cmd = con.CreateCommand();
                    // インサート
                    cmd.CommandText = "UPDATE m_product SET Name = @Name, Telephone = @Telephone WHERE CD = @Cd;";
                    // パラメータセット
                    cmd.Parameters.Add("Name", System.Data.DbType.String);
                    cmd.Parameters.Add("Telephone", System.Data.DbType.Int64);
                    cmd.Parameters.Add("Address", System.Data.DbType.String);
                    cmd.Parameters.Add("Cd", System.Data.DbType.Int64);
                    // データ修正
                    cmd.Parameters["Name"].Value = NameBox.Text;
                    cmd.Parameters["Telephone"].Value = int.Parse(PhoneBox.Text);
                    cmd.Parameters["Address"].Value = AddressBox.Text;
                    cmd.Parameters["Cd"].Value = int.Parse(CdNumber.Text);
                    cmd.ExecuteNonQuery();
                    // コミット
                    trans.Commit();
                    #endregion
                con.Close();
                }
            }

        }
    }
}
