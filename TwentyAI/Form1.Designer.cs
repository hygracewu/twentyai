namespace TwentyAI
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblA = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.lblG = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCoordX = new System.Windows.Forms.Label();
            this.lblCoordY = new System.Windows.Forms.Label();
            this.ooo = new System.Windows.Forms.Label();
            this.targetNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startX = new System.Windows.Forms.TextBox();
            this.startY = new System.Windows.Forms.TextBox();
            this.destX = new System.Windows.Forms.TextBox();
            this.destY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textMovable = new System.Windows.Forms.Label();
            this.sn = new System.Windows.Forms.Label();
            this.dn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(344, 55);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(40, 18);
            this.lblA.TabIndex = 0;
            this.lblA.Text = "A = ";
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.Location = new System.Drawing.Point(344, 90);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(39, 18);
            this.lblR.TabIndex = 1;
            this.lblR.Text = "R = ";
            // 
            // lblG
            // 
            this.lblG.AutoSize = true;
            this.lblG.Location = new System.Drawing.Point(344, 129);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(35, 18);
            this.lblG.TabIndex = 2;
            this.lblG.Text = "G =";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(344, 171);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(34, 18);
            this.lblB.TabIndex = 3;
            this.lblB.Text = "B =";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(169, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 133);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblCoordX
            // 
            this.lblCoordX.AutoSize = true;
            this.lblCoordX.Location = new System.Drawing.Point(44, 56);
            this.lblCoordX.Name = "lblCoordX";
            this.lblCoordX.Size = new System.Drawing.Size(40, 18);
            this.lblCoordX.TabIndex = 5;
            this.lblCoordX.Text = "X = ";
            // 
            // lblCoordY
            // 
            this.lblCoordY.AutoSize = true;
            this.lblCoordY.Location = new System.Drawing.Point(44, 90);
            this.lblCoordY.Name = "lblCoordY";
            this.lblCoordY.Size = new System.Drawing.Size(40, 18);
            this.lblCoordY.TabIndex = 6;
            this.lblCoordY.Text = "Y = ";
            // 
            // ooo
            // 
            this.ooo.AutoSize = true;
            this.ooo.Location = new System.Drawing.Point(44, 377);
            this.ooo.Name = "ooo";
            this.ooo.Size = new System.Drawing.Size(13, 18);
            this.ooo.TabIndex = 7;
            this.ooo.Text = " ";
            // 
            // targetNum
            // 
            this.targetNum.AutoSize = true;
            this.targetNum.Location = new System.Drawing.Point(344, 254);
            this.targetNum.Name = "targetNum";
            this.targetNum.Size = new System.Drawing.Size(61, 18);
            this.targetNum.TabIndex = 8;
            this.targetNum.Text = "Num = ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 343);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "Current State";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Color";
            // 
            // startX
            // 
            this.startX.Location = new System.Drawing.Point(48, 254);
            this.startX.Name = "startX";
            this.startX.Size = new System.Drawing.Size(37, 29);
            this.startX.TabIndex = 11;
            this.startX.Text = "0";
            // 
            // startY
            // 
            this.startY.Location = new System.Drawing.Point(48, 282);
            this.startY.Name = "startY";
            this.startY.Size = new System.Drawing.Size(37, 29);
            this.startY.TabIndex = 12;
            this.startY.Text = "0";
            // 
            // destX
            // 
            this.destX.Location = new System.Drawing.Point(107, 254);
            this.destX.Name = "destX";
            this.destX.Size = new System.Drawing.Size(37, 29);
            this.destX.TabIndex = 13;
            this.destX.Text = "0";
            // 
            // destY
            // 
            this.destY.Location = new System.Drawing.Point(107, 282);
            this.destY.Name = "destY";
            this.destY.Size = new System.Drawing.Size(37, 29);
            this.destY.TabIndex = 14;
            this.destY.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "start";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "dest";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 18);
            this.label5.TabIndex = 17;
            this.label5.Text = "movable";
            // 
            // textMovable
            // 
            this.textMovable.AutoSize = true;
            this.textMovable.Location = new System.Drawing.Point(172, 254);
            this.textMovable.Name = "textMovable";
            this.textMovable.Size = new System.Drawing.Size(44, 18);
            this.textMovable.TabIndex = 18;
            this.textMovable.Text = "None";
            // 
            // sn
            // 
            this.sn.AutoSize = true;
            this.sn.Location = new System.Drawing.Point(48, 314);
            this.sn.Name = "sn";
            this.sn.Size = new System.Drawing.Size(16, 18);
            this.sn.TabIndex = 19;
            this.sn.Text = "0";
            // 
            // dn
            // 
            this.dn.AutoSize = true;
            this.dn.Location = new System.Drawing.Point(108, 314);
            this.dn.Name = "dn";
            this.dn.Size = new System.Drawing.Size(16, 18);
            this.dn.TabIndex = 20;
            this.dn.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 668);
            this.Controls.Add(this.dn);
            this.Controls.Add(this.sn);
            this.Controls.Add(this.textMovable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.destY);
            this.Controls.Add(this.destX);
            this.Controls.Add(this.startY);
            this.Controls.Add(this.startX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetNum);
            this.Controls.Add(this.ooo);
            this.Controls.Add(this.lblCoordY);
            this.Controls.Add(this.lblCoordX);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblG);
            this.Controls.Add(this.lblR);
            this.Controls.Add(this.lblA);
            this.Name = "Form1";
            this.Text = "TwentySolver";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.Label lblG;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCoordX;
        private System.Windows.Forms.Label lblCoordY;
        private System.Windows.Forms.Label ooo;
        private System.Windows.Forms.Label targetNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startX;
        private System.Windows.Forms.TextBox startY;
        private System.Windows.Forms.TextBox destX;
        private System.Windows.Forms.TextBox destY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label textMovable;
        private System.Windows.Forms.Label sn;
        private System.Windows.Forms.Label dn;
    }
}

