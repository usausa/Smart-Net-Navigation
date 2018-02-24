namespace Example.WindowsFormsApp.Modules.Edit
{
    partial class DataDetailView
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
            this.UpdateButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.NameText = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // UpdateButton
            //
            this.UpdateButton.Location = new System.Drawing.Point(320, 240);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(160, 32);
            this.UpdateButton.TabIndex = 11;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.OnUpdateButtonClick);
            //
            // PrevButton
            //
            this.PrevButton.Location = new System.Drawing.Point(160, 240);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(160, 32);
            this.PrevButton.TabIndex = 10;
            this.PrevButton.Text = "Cancel";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.OnPrevButtonClick);
            //
            // NameText
            //
            this.NameText.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NameText.Location = new System.Drawing.Point(160, 175);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(320, 34);
            this.NameText.TabIndex = 9;
            //
            // NameLabel
            //
            this.NameLabel.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NameLabel.Location = new System.Drawing.Point(155, 140);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(100, 32);
            this.NameLabel.TabIndex = 8;
            this.NameLabel.Text = "Name";
            //
            // DataDetailView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.NameText);
            this.Controls.Add(this.NameLabel);
            this.Name = "DataDetailView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.TextBox NameText;
        private System.Windows.Forms.Label NameLabel;
    }
}
