namespace Example.WindowsFormsApp.Pages.Stack
{
    partial class Stack2Page
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
            this.PushButton = new System.Windows.Forms.Button();
            this.PopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PushButton
            // 
            this.PushButton.Location = new System.Drawing.Point(323, 178);
            this.PushButton.Name = "PushButton";
            this.PushButton.Size = new System.Drawing.Size(160, 32);
            this.PushButton.TabIndex = 11;
            this.PushButton.Text = "Push";
            this.PushButton.UseVisualStyleBackColor = true;
            // 
            // PopButton
            // 
            this.PopButton.Location = new System.Drawing.Point(157, 178);
            this.PopButton.Name = "PopButton";
            this.PopButton.Size = new System.Drawing.Size(160, 32);
            this.PopButton.TabIndex = 10;
            this.PopButton.Text = "Pop";
            this.PopButton.UseVisualStyleBackColor = true;
            // 
            // Stack2Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PushButton);
            this.Controls.Add(this.PopButton);
            this.Name = "Stack2Page";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PushButton;
        private System.Windows.Forms.Button PopButton;
    }
}
