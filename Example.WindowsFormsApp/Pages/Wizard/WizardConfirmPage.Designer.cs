namespace Example.WindowsFormsApp.Pages.Wizard
{
    partial class WizardConfirmPage
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
            this.Data2Label = new System.Windows.Forms.Label();
            this.Data1Label = new System.Windows.Forms.Label();
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Data2Label
            // 
            this.Data2Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Data2Label.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Data2Label.Location = new System.Drawing.Point(160, 170);
            this.Data2Label.Name = "Data2Label";
            this.Data2Label.Size = new System.Drawing.Size(320, 32);
            this.Data2Label.TabIndex = 17;
            // 
            // Data1Label
            // 
            this.Data1Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Data1Label.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Data1Label.Location = new System.Drawing.Point(160, 135);
            this.Data1Label.Name = "Data1Label";
            this.Data1Label.Size = new System.Drawing.Size(320, 32);
            this.Data1Label.TabIndex = 16;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(323, 222);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(160, 32);
            this.NextButton.TabIndex = 15;
            this.NextButton.Text = "完了";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // PrevButton
            // 
            this.PrevButton.Location = new System.Drawing.Point(157, 222);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(160, 32);
            this.PrevButton.TabIndex = 14;
            this.PrevButton.Text = "前へ";
            this.PrevButton.UseVisualStyleBackColor = true;
            // 
            // WizardConfirmPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Data2Label);
            this.Controls.Add(this.Data1Label);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PrevButton);
            this.Name = "WizardConfirmPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Data2Label;
        private System.Windows.Forms.Label Data1Label;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
    }
}
