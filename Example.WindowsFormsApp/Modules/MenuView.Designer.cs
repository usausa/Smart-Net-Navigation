namespace Example.WindowsFormsApp.Modules
{
    partial class MenuView
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
            this.MenuButton3 = new System.Windows.Forms.Button();
            this.MenuButton2 = new System.Windows.Forms.Button();
            this.MenuButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // MenuButton3
            //
            this.MenuButton3.Location = new System.Drawing.Point(240, 240);
            this.MenuButton3.Name = "MenuButton3";
            this.MenuButton3.Size = new System.Drawing.Size(160, 32);
            this.MenuButton3.TabIndex = 2;
            this.MenuButton3.Text = "Context sample";
            this.MenuButton3.UseVisualStyleBackColor = true;
            this.MenuButton3.Click += new System.EventHandler(this.OnMenuButton3Click);
            //
            // MenuButton2
            //
            this.MenuButton2.Location = new System.Drawing.Point(240, 176);
            this.MenuButton2.Name = "MenuButton2";
            this.MenuButton2.Size = new System.Drawing.Size(160, 32);
            this.MenuButton2.TabIndex = 1;
            this.MenuButton2.Text = "Stack sample";
            this.MenuButton2.UseVisualStyleBackColor = true;
            this.MenuButton2.Click += new System.EventHandler(this.OnMenuButton2Click);
            //
            // MenuButton1
            //
            this.MenuButton1.Location = new System.Drawing.Point(240, 112);
            this.MenuButton1.Name = "MenuButton1";
            this.MenuButton1.Size = new System.Drawing.Size(160, 32);
            this.MenuButton1.TabIndex = 0;
            this.MenuButton1.Text = "Multiple id sample";
            this.MenuButton1.UseVisualStyleBackColor = true;
            this.MenuButton1.Click += new System.EventHandler(this.OnMenuButton1Click);
            //
            // MenuView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MenuButton3);
            this.Controls.Add(this.MenuButton1);
            this.Controls.Add(this.MenuButton2);
            this.Name = "MenuView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MenuButton3;
        private System.Windows.Forms.Button MenuButton2;
        private System.Windows.Forms.Button MenuButton1;
    }
}
