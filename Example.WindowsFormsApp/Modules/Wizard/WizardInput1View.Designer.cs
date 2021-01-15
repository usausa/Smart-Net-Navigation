namespace Example.WindowsFormsApp.Modules.Wizard
{
    partial class WizardInput1View
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
            this.Data1Text = new System.Windows.Forms.TextBox();
            this.Data1Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // NextButton
            //
            this.NextButton.Location = new System.Drawing.Point(320, 240);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(160, 32);
            this.NextButton.TabIndex = 11;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.OnNextButtonClick);
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
            // Data1Text
            //
            this.Data1Text.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Data1Text.Location = new System.Drawing.Point(160, 175);
            this.Data1Text.Name = "Data1Text";
            this.Data1Text.Size = new System.Drawing.Size(320, 34);
            this.Data1Text.TabIndex = 9;
            //
            // Data1Label
            //
            this.Data1Label.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Data1Label.Location = new System.Drawing.Point(155, 140);
            this.Data1Label.Name = "Data1Label";
            this.Data1Label.Size = new System.Drawing.Size(100, 32);
            this.Data1Label.TabIndex = 8;
            this.Data1Label.Text = "Data1";
            //
            // WizardInput1View
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.Data1Text);
            this.Controls.Add(this.Data1Label);
            this.Name = "WizardInput1View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.TextBox Data1Text;
        private System.Windows.Forms.Label Data1Label;
    }
}
