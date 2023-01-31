using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }
        /// <summary>
        /// このボタンを押された場合新規登録画面に移る
        /// </summary>
        private void AddDataClick(object sender, EventArgs e)
        {
            AddData dataAdd =  new AddData();
            dataAdd.Show();//登録画面を開く
            this.Visible = false;//この画面は非表示にする
        }

        private void ViewDataClick(object sender, EventArgs e)
        {
            SearchData search = new SearchData();
            search.Show();
            this.Visible = false;

        }

        private void UpDateDataClick(object sender, EventArgs e)
        {
            Update update = new Update();
            update.Show();
            this.Visible = false;
        }

        private void DeleteDataClick(object sender, EventArgs e)
        {
            Delete delete = new Delete();
            delete.Show();
            this.Visible = false;
        }
        /// <summary>
        /// Endを押されたら最終確認をして閉じる。Noが押された場合何もしない
        /// </summary>
        private void EndButton_Click(object sender, EventArgs e)
        {
            #region 最終確認
            //最終確認をする
            DialogResult YesOrNo =MessageBox.Show("本当に終了しますか？","最終確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);
            #endregion

            //Yesを押された場合終了する
            if (YesOrNo == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
