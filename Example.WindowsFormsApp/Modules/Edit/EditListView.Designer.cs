namespace Example.WindowsFormsApp.Modules.Edit
{
    partial class DataListView
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
            this.DataListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            //
            // EditButton
            //
            this.EditButton.Location = new System.Drawing.Point(320, 240);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(160, 32);
            this.EditButton.TabIndex = 3;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.OnEditButtonClick);
            //
            // NewButton
            //
            this.NewButton.Location = new System.Drawing.Point(160, 240);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(160, 32);
            this.NewButton.TabIndex = 2;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.OnNewButtonClick);
            //
            // DataListBox
            //
            this.DataListBox.DisplayMember = "Name";
            this.DataListBox.Font = new System.Drawing.Font("MS UI Gothic", 20.25F);
            this.DataListBox.FormattingEnabled = true;
            this.DataListBox.ItemHeight = 27;
            this.DataListBox.Location = new System.Drawing.Point(160, 64);
            this.DataListBox.Name = "DataListBox";
            this.DataListBox.Size = new System.Drawing.Size(320, 166);
            this.DataListBox.TabIndex = 4;
            //
            // DataListView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DataListBox);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.NewButton);
            this.Name = "DataListView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.ListBox DataListBox;
    }
}
