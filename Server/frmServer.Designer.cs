
namespace Server
{
    partial class frmServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSend = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clbClientConnected = new System.Windows.Forms.CheckedListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSendImage = new System.Windows.Forms.Button();
            this.panelShowMessage = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(0, 12);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(320, 41);
            this.txtSend.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clbClientConnected);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 489);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List Client Connected";
            // 
            // clbClientConnected
            // 
            this.clbClientConnected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbClientConnected.FormattingEnabled = true;
            this.clbClientConnected.Location = new System.Drawing.Point(3, 16);
            this.clbClientConnected.Name = "clbClientConnected";
            this.clbClientConnected.Size = new System.Drawing.Size(194, 470);
            this.clbClientConnected.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(394, 13);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 37);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click_1);
            // 
            // btnSendImage
            // 
            this.btnSendImage.Location = new System.Drawing.Point(326, 13);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(62, 36);
            this.btnSendImage.TabIndex = 8;
            this.btnSendImage.Text = "Image";
            this.btnSendImage.UseVisualStyleBackColor = true;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // panelShowMessage
            // 
            this.panelShowMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowMessage.BackColor = System.Drawing.Color.Plum;
            this.panelShowMessage.Location = new System.Drawing.Point(203, 0);
            this.panelShowMessage.Name = "panelShowMessage";
            this.panelShowMessage.Size = new System.Drawing.Size(589, 415);
            this.panelShowMessage.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtSend);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.btnSendImage);
            this.panel1.Location = new System.Drawing.Point(203, 421);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 65);
            this.panel1.TabIndex = 10;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(795, 489);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelShowMessage);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmServer";
            this.Text = "Server";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox clbClientConnected;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.Panel panelShowMessage;
        private System.Windows.Forms.Panel panel1;
    }
}

