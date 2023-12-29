
namespace ChatDemo
{
    partial class frmClient
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
            this.btnSend = new System.Windows.Forms.Button();
            this.txtNameClient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnectServer = new System.Windows.Forms.Button();
            this.panelShowMessage = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSendImage = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(376, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(91, 31);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtNameClient
            // 
            this.txtNameClient.Location = new System.Drawing.Point(41, 11);
            this.txtNameClient.Name = "txtNameClient";
            this.txtNameClient.Size = new System.Drawing.Size(104, 20);
            this.txtNameClient.TabIndex = 7;
            this.txtNameClient.Text = "Client";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tên:";
            // 
            // btnConnectServer
            // 
            this.btnConnectServer.Location = new System.Drawing.Point(41, 37);
            this.btnConnectServer.Name = "btnConnectServer";
            this.btnConnectServer.Size = new System.Drawing.Size(104, 23);
            this.btnConnectServer.TabIndex = 9;
            this.btnConnectServer.Text = "Connect Server";
            this.btnConnectServer.UseVisualStyleBackColor = true;
            this.btnConnectServer.Click += new System.EventHandler(this.btnConnectServer_Click);
            // 
            // panelShowMessage
            // 
            this.panelShowMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowMessage.AutoScroll = true;
            this.panelShowMessage.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panelShowMessage.Location = new System.Drawing.Point(190, 2);
            this.panelShowMessage.Name = "panelShowMessage";
            this.panelShowMessage.Size = new System.Drawing.Size(469, 352);
            this.panelShowMessage.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNameClient);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnConnectServer);
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 424);
            this.panel1.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Plum;
            this.panel2.Controls.Add(this.btnSendImage);
            this.panel2.Controls.Add(this.txtSend);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Location = new System.Drawing.Point(189, 354);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(470, 75);
            this.panel2.TabIndex = 13;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(3, 6);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(367, 20);
            this.txtSend.TabIndex = 6;
            // 
            // btnSendImage
            // 
            this.btnSendImage.Location = new System.Drawing.Point(376, 43);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(91, 24);
            this.btnSendImage.TabIndex = 7;
            this.btnSendImage.Text = "Image";
            this.btnSendImage.UseVisualStyleBackColor = true;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // frmClient
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(660, 432);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelShowMessage);
            this.Name = "frmClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmClient_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtNameClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnectServer;
        private System.Windows.Forms.Panel panelShowMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSendImage;
    }
}

