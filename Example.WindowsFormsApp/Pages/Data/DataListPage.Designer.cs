namespace Example.WindowsFormsApp.Pages.Data
{
    partial class DataListPage
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.EditButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EditButton
            // 
            this.EditButton.Location = new System.Drawing.Point(323, 178);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(160, 32);
            this.EditButton.TabIndex = 3;
            this.EditButton.Text = "編集";
            this.EditButton.UseVisualStyleBackColor = true;
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(157, 178);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(160, 32);
            this.NewButton.TabIndex = 2;
            this.NewButton.Text = "新規作成";
            this.NewButton.UseVisualStyleBackColor = true;
            // 
            // DataListPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.NewButton);
            this.Name = "DataListPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button NewButton;
    }
}
