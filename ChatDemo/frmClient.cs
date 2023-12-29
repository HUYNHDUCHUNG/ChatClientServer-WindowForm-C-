using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatDemo
{
    public partial class frmClient : Form
    {

        private const string IP_ADDRESS = "127.0.0.1";
        private const int PORT = 2023;
        Socket clientSocket;
        private byte[] buffers;
        //private int oldLocationYPanel = 0;
        public frmClient()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //init();
            btnSend.Enabled = false;
            buffers = new byte[4000];
        }

        private void ConnectToServer()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress serverIP = IPAddress.Parse(IP_ADDRESS); 
                IPEndPoint serverEndPoint = new IPEndPoint(serverIP, PORT);

                clientSocket.BeginConnect(serverEndPoint, ConnectCallback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to server: " + ex.Message);
            }
        }
        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                clientSocket.EndConnect(result);

                // Gửi dữ liệu tới server sau khi kết nối thành công
                string message = "#CLIENT_NAME#" +  txtNameClient.Text;
                byte[] sendData = Encoding.UTF8.GetBytes(message);
                clientSocket.BeginSend(sendData, 0, sendData.Length, SocketFlags.None, SendCallback, null);

                // Bắt đầu nhận dữ liệu từ server sau khi gửi dữ liệu
                clientSocket.BeginReceive(buffers, 0, buffers.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi kết nối tới server
                Console.WriteLine("Error connecting to server: " + ex.Message);
            }
        }

        private void SendCallback(IAsyncResult result)
        {
            try
            {
                clientSocket.EndSend(result);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi gửi dữ liệu tới server
                Console.WriteLine("Error sending data to server: " + ex.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int bytesRead = clientSocket.EndReceive(result);



                if (bytesRead > 0)
                {
                    // Xử lý dữ liệu nhận được từ server
                    string receivedData = Encoding.UTF8.GetString(buffers, 0, bytesRead);
                    if (receivedData.Length > 7)
                    {
                        string receivedDataTmp = receivedData.Substring(0, 7);
                        if (receivedDataTmp.Equals("#image#") || receivedData.Length < 7)
                        {
                            byte[] receivedDataImage = new byte[bytesRead - 7];

                            Array.Copy(buffers, 7, receivedDataImage, 0, receivedDataImage.Length);
                            string s = Encoding.UTF8.GetString(buffers, 0, receivedDataImage.Length);
                            ImageConverter convertData = new ImageConverter();
                            Image image = (Image)convertData.ConvertFrom(receivedDataImage);
                            BeginInvoke((Action)(() =>
                            {
                                //MemoryStream ms = new MemoryStream(receivedDataImage);
                                //Image bmp = Image.FromStream(ms);
                                //pictureBox1.Image = bmp;
                                
                                Panel pn = new Panel();
                                pn = createSendImagePanel("Server", image, false);
                                pn.Dock = DockStyle.Top;
                                panelShowMessage.Controls.Add(pn);
                                return;
                            }));
                            
                        }
                        else
                        {

                            BeginInvoke((Action)(() =>
                            {
                                Panel pn = new Panel();
                                pn = createSendMessagePanel("Server", receivedData, false);
                                pn.Dock = DockStyle.Top;
                                panelShowMessage.Controls.Add(pn);
                            }));
                        }
                    }
                    else
                    {
                        BeginInvoke((Action)(() =>
                        {
                            Panel pn = new Panel();
                            pn = createSendMessagePanel("Server", receivedData, false);
                            pn.Dock = DockStyle.Top;
                            panelShowMessage.Controls.Add(pn);
                        }));
                    }
                    
                    
                   


                    // Tiếp tục nhận dữ liệu từ server
                    clientSocket.BeginReceive(buffers, 0, buffers.Length, SocketFlags.None, ReceiveCallback, null);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi nhận dữ liệu từ server
                MessageBox.Show("Error receiving data from server: " + ex.Message);
                Console.WriteLine("Error receiving data from server: " + ex.Message);
            }
        }

    


        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            if (txtNameClient.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập tên client");
            }
            else
            {
                ConnectToServer();
                btnConnectServer.Enabled = false;
                btnSend.Enabled = true;
                txtNameClient.ReadOnly = true;
            }
           
        }


        private Panel createSendImagePanel(string nameSend,Image image, bool isMyMessage)
        {
            Color backgroundColor;
            if (isMyMessage)
            {
                backgroundColor = Color.LightBlue;
            }
            else
            {
                backgroundColor = Color.LightGreen;

            }


            int MAX_WITH = panelShowMessage.Width;
            Panel panel = new Panel();
            panel.AutoSize = true;
            panel.Width = MAX_WITH;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = backgroundColor;


            Label nameClientLabel = new Label();
            nameClientLabel.Text = nameSend;
            nameClientLabel.AutoSize = true;
            int x = nameClientLabel.Location.X;
            int y = nameClientLabel.Location.Y;
            Point p = new Point(x, y + nameClientLabel.Height);

            PictureBox picture = new PictureBox();
            picture.Height = 70;
            picture.Width = 70;
            picture.Location = p;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            //picture.ImageLocation = "duong_dan_anh";
            picture.Image = image;



            Label timeLabel = new Label();
            timeLabel.Text = "Time: " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            timeLabel.AutoSize = true;
            timeLabel.BackColor = backgroundColor;
            Point p1 = new Point(x, y + picture.Height + nameClientLabel.Height);
            timeLabel.Location = p1;

            panel.Controls.Add(nameClientLabel);
            panel.Controls.Add(picture);
            panel.Controls.Add(timeLabel);
            return panel;
        }

        private Panel createSendMessagePanel(string nameSend, string message,bool isMyMessage)
        {
            Color backgroundColor;
            if (isMyMessage)
            {
                backgroundColor = Color.LightBlue;
            }
            else
            {
                backgroundColor = Color.LightGreen;

            }


            int MAX_WITH = panelShowMessage.Width;
            Panel panel = new Panel();
            panel.AutoSize = true;
            panel.Width = MAX_WITH;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = backgroundColor;


            Label nameClientLabel = new Label();
            nameClientLabel.Text = nameSend;
            nameClientLabel.AutoSize = true;
            int x = nameClientLabel.Location.X;
            int y = nameClientLabel.Location.Y;
            Point p = new Point(x, y + nameClientLabel.Height);
            
            TextBox messageTextBox = new TextBox();
            messageTextBox.Text = "Message: " + message;
            messageTextBox.AutoSize = true;
            messageTextBox.Height = 70;
            //messageTextBox.ScrollBars = ScrollBars.Vertical;
            messageTextBox.Width = MAX_WITH;
            messageTextBox.Multiline = true;
            messageTextBox.ReadOnly = true;
            messageTextBox.BorderStyle = BorderStyle.None;
            messageTextBox.BackColor = backgroundColor;

            messageTextBox.Location = p;
        

           

            Label timeLabel = new Label();
            timeLabel.Text = "Time: " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            timeLabel.AutoSize = true;
            timeLabel.BackColor = backgroundColor;
            Point p1 = new Point(x, y + messageTextBox.Height + nameClientLabel.Height);
            timeLabel.Location = p1;

            if (isMyMessage)
            {
                Button btnRecall = new Button();
                Point p2 = new Point(MAX_WITH - btnRecall.Width, y + messageTextBox.Height + nameClientLabel.Height);
                btnRecall.Location = p2;
                btnRecall.Text = "Thu hồi";
                btnRecall.AutoSize = true;
                //btnRecall.Dock = DockStyle.Right;
                btnRecall.Click += (sender, e) =>
                {
                    // Xóa panel khi người dùng nhấp vào nút "Thu hồi"
                    panelShowMessage.Controls.Remove(panel);
                };

                panel.Controls.Add(btnRecall);
            }
            panel.Controls.Add(nameClientLabel);
            panel.Controls.Add(messageTextBox);
            panel.Controls.Add(timeLabel);
            return panel;
        }


        private void btnSend_Click(object sender, EventArgs e)
        {

            if (txtSend.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập tin nhắn muốn gửi");
            }
            else
            {
                byte[] data1 = new byte[1024];
                data1 = Encoding.UTF8.GetBytes(txtSend.Text);
                clientSocket.Send(data1);
                Panel pn = new Panel();
                pn = createSendMessagePanel("Me", txtSend.Text, true);
                pn.Dock = DockStyle.Top;
                panelShowMessage.Controls.Add(pn);
                txtSend.Text = "";
            }
            
        }
        private void btnSendImage_Click(object sender, EventArgs e)
        {
            handlerSendImage(clientSocket);
        }

        private void handlerSendImage(Socket client)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp) | *.jpg; *.jpeg; *.png; *.gif; *.bmp";
            openFileDialog.Title = "Chọn hình ảnh";

            // Hiển thị hộp thoại OpenFileDialog và kiểm tra kết quả
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn tệp hình ảnh đã chọn
                string imagePath = openFileDialog.FileName;

                // Tạo một đối tượng Bitmap từ đường dẫn hình ảnh
                Bitmap bmp = new Bitmap(imagePath);



                // Chuyển đổi đối tượng Bitmap thành một mảng byte
                byte[] bmpBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    bmpBytes = ms.ToArray();
                }

                // Gửi dữ liệu hình ảnh
                byte[] typeData = Encoding.UTF8.GetBytes("#image#");
                byte[] result = new byte[typeData.Length + bmpBytes.Length];
                Array.Copy(typeData, result, typeData.Length);
                Array.Copy(bmpBytes, 0, result, typeData.Length, bmpBytes.Length);
                client.Send(result);

                // Giải phóng tài nguyên
                bmp.Dispose();
            }
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng ứng dụng?", "Xác nhận đóng ứng dụng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    byte[] data1 = new byte[1024];
                    //data1 = Encoding.UTF8.GetBytes("#closing_form#" + txtNameClient.Text);
                    data1 = Encoding.UTF8.GetBytes(txtNameClient.Text + " Disconnected");
                    clientSocket.Send(data1);

                    clientSocket.Close();
                    e.Cancel = false;
                }
            }
        }

        
    }
}
