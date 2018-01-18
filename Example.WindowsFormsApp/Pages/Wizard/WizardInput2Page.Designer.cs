namespace Example.WindowsFormsApp.Pages.Wizard
{
    partial class WizardInput2Page
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
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.Data2Text = new System.Windows.Forms.TextBox();
            this.Data2Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(324, 222);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(160, 32);
            this.NextButton.TabIndex = 11;
            this.NextButton.Text = "次へ";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // PrevButton
            // 
            this.PrevButton.Location = new System.Drawing.Point(158, 222);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(160, 32);
            this.PrevButton.TabIndex = 10;
            this.PrevButton.Text = "前へ";
            this.PrevButton.UseVisualStyleBackColor = true;
            // 
            // Data2Text
            // 
            this.Data2Text.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Data2Text.Location = new System.Drawing.Point(161, 170);
            this.Data2Text.Name = "Data2Text";
            this.Data2Text.Size = new System.Drawing.Size(320, 34);
            this.Data2Text.TabIndex = 9;
            // 
            // Data2Label
            // 
            this.Data2Label.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Data2Label.Location = new System.Drawing.Point(156, 135);
            this.Data2Label.Name = "Data2Label";
            this.Data2Label.Size = new System.Drawing.Size(100, 32);
            this.Data2Label.TabIndex = 8;
            this.Data2Label.Text = "データ2";
            // 
            // WizardInput2Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.Data2Text);
            this.Controls.Add(this.Data2Label);
            this.Name = "WizardInput2Page";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.TextBox Data2Text;
        private System.Windows.Forms.Label Data2Label;
    }
}
