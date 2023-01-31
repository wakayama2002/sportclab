using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class AddData : Form
    {
        public AddData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// このボタンが押された場合新規テーブルを作成する
        /// </summary>
        private void AddTableClick(object sender, EventArgs e)
        {
            #region テーブル作成
            using (var con = new SQLiteConnection("Data Source=member.db"))
            {
                //テーブルの作成と設定
                con.Open();
                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText =
                        "create table m_product(CD INTEGER PRIMARY KEY AUTOINCREMENT , Name TEXT , Born TEXT , Address TEXT , Telephone INTEGER )";
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            #endregion
        }
        /// <summary>
        /// このボタンが押された場合最終確認してデータを追加する
        /// </summary>
        private void NewAddDataClick(object sender, EventArgs e)
        {
            #region 最終確認
            //登録する前に最終確認をする
            DialogResult YesOrNo = MessageBox.Show("登録してよろしいですか", "最終確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);
            #endregion

            //Yesを押された場合登録する
            if (YesOrNo == DialogResult.Yes)
            {
                using (var con = new SQLiteConnection("Data Source=member.db"))
                {
                    con.Open();
                    #region データ設定と追加
                    using (SQLiteTransaction trans = con.BeginTransaction())
                    {
                        SQLiteCommand cmd = con.CreateCommand();
                        //インサート
                        cmd.CommandText = "INSERT INTO m_product(Name,Born,Address,Telephone) VALUES (@Name , @Born , @Address , @Telephone)";
                        //パラメーターセット
                        cmd.Parameters.Add("Name", System.Data.DbType.String);
                        cmd.Parameters.Add("Born", System.Data.DbType.String);
                        cmd.Parameters.Add("Address", System.Data.DbType.String);
                        cmd.Parameters.Add("Telephone", System.Data.DbType.Int64);
                        //データ追加
                        cmd.Parameters["Name"].Value = NameBox.Text;
                        cmd.Parameters["Born"].Value = BornBox.Text;
                        cmd.Parameters["Address"].Value = AddressBox.Text;
                        cmd.Parameters["Telephone"].Value = TelephoneBox.Text;


                        cmd.ExecuteNonQuery();

                        trans.Commit();
                    }
                    #endregion
                    con.Close();
                }
                //データ検索画面に遷移する
                SearchData searchData =new SearchData();
                searchData.Show();
                this.Visible = false;

            }
        }
        /// <summary>
        /// このボタンが押された場合この画面を非表示にしてStartに遷移する
        /// </summary>
        private void BackButton_Click(object sender, EventArgs e)
        {
            Start home = new Start();
            home.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 数字だけ入力可能を設定してプロパティプロパティから電話番号と生年月日に適用する
        /// </summary>
        private void NumberBoxKeyPress(object sender, KeyPressEventArgs InputNumber)
        {
            //バックスペースが押された時は有効（Deleteキーも有効）
            if (InputNumber.KeyChar == '\b')
            {
                return;
            }

            //数値0～9以外が押された時はキャンセルする
            if ((InputNumber.KeyChar < '0' || '9' < InputNumber.KeyChar))
            {
                InputNumber.Handled = true;
            }
        }

    }
    
}
