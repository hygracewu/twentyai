﻿namespace TwentyAI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ooo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchMethod = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.finishLabel = new System.Windows.Forms.Label();
            this.currentDepth = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ooo
            // 
            this.ooo.AutoSize = true;
            this.ooo.Font = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ooo.Location = new System.Drawing.Point(48, 146);
            this.ooo.Name = "ooo";
            this.ooo.Size = new System.Drawing.Size(49, 20);
            this.ooo.TabIndex = 7;
            this.ooo.Text = "None";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Current State";
            // 
            // searchMethod
            // 
            this.searchMethod.AutoSize = true;
            this.searchMethod.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchMethod.Location = new System.Drawing.Point(47, 33);
            this.searchMethod.Name = "searchMethod";
            this.searchMethod.Size = new System.Drawing.Size(147, 24);
            this.searchMethod.TabIndex = 10;
            this.searchMethod.Text = "Search Method";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("新細明體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "AStar",
            "BFS",
            "DFS"});
            this.comboBox1.Location = new System.Drawing.Point(56, 60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 30);
            this.comboBox1.TabIndex = 11;
            // 
            // finishLabel
            // 
            this.finishLabel.AutoSize = true;
            this.finishLabel.Location = new System.Drawing.Point(242, 67);
            this.finishLabel.Name = "finishLabel";
            this.finishLabel.Size = new System.Drawing.Size(96, 18);
            this.finishLabel.TabIndex = 12;
            this.finishLabel.Text = "Not Finished";
            // 
            // currentDepth
            // 
            this.currentDepth.AutoSize = true;
            this.currentDepth.Location = new System.Drawing.Point(245, 38);
            this.currentDepth.Name = "currentDepth";
            this.currentDepth.Size = new System.Drawing.Size(16, 18);
            this.currentDepth.TabIndex = 13;
            this.currentDepth.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 516);
            this.Controls.Add(this.currentDepth);
            this.Controls.Add(this.finishLabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.searchMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ooo);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TwentySolver";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ooo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label searchMethod;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label finishLabel;
        private System.Windows.Forms.Label currentDepth;
    }
}

