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

namespace Server
{
    public partial class frmServer : Form
    {

        private const int PORT = 2023;
        private const int MAX_CONNECT = 10;
        public frmServer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            init();
            //receive();
        }
        Socket server;
        private Dictionary<string, Socket> clientNames = new Dictionary<string, Socket>();

        private void init()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, PORT);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iPEndPoint);
            server.Listen(MAX_CONNECT);

            SocketAsyncEventArgs acceptArgs = new SocketAsyncEventArgs();
            acceptArgs.Completed += AcceptCompleted;

            AcceptNextClient(acceptArgs);
        }

        private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
            AcceptNextClient(e);
        }

        private void AcceptNextClient(SocketAsyncEventArgs acceptArgs)
        {
            acceptArgs.AcceptSocket = null;

            if (!server.AcceptAsync(acceptArgs))
            {
                ProcessAccept(acceptArgs);
            }
        }



        private void ProcessAccept(SocketAsyncEventArgs acceptArgs)
        {
            try
            {
                Socket clientSocket = acceptArgs.AcceptSocket;

                byte[] receiveBuffer = new byte[10000];
                SocketAsyncEventArgs receiveArgs = new SocketAsyncEventArgs();
                receiveArgs.SetBuffer(receiveBuffer, 0, receiveBuffer.Length);
                receiveArgs.Completed += ReceiveCompleted;
                receiveArgs.UserToken = clientSocket;


                //ReceiveClientName(clientSocket, receiveArgs);

                // Gửi danh sách client hiện tại cho client mới kết nối
                //SendClientListToAllClients();

                // lưu tên client vào từ điển clientnames

                StartReceiving(receiveArgs);
            }
            catch
            {
                MessageBox.Show("ProcessAccept Error");
            }

        }

        //private void SendClientListToAllClients()
        //{



        //    string[] clientnamearray;

        //    lock (clientNames)
        //    {
        //        clientnamearray = clientNames.Keys.ToArray();
        //    }

        //    string clientliststring = "#clientlist#" + string.Join(",", clientnamearray);
        //    //MessageBox.Show(clientnamearray.Length.ToString());
        //    foreach (Socket clientsocket in clientNames.Values)
        //    {
        //        try
        //        {
        //            byte[] data = Encoding.Unicode.GetBytes(clientliststring);
        //            clientsocket.Send(data);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("error sending client list to client: " + ex.Message);
        //        }
        //    }

        //    //Thread.Sleep(5000);

        //}
        private void ReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessReceive(e);
            StartReceiving(e);
        }

        private void StartReceiving(SocketAsyncEventArgs receiveArgs)
        {
            Socket clientSocket = (Socket)receiveArgs.UserToken;

            if (!clientSocket.ReceiveAsync(receiveArgs))
            {

                ProcessReceive(receiveArgs);
            }


        }


        private void ProcessReceive(SocketAsyncEventArgs receiveArgs)
        {
            try
            {
                Socket clientSocket = (Socket)receiveArgs.UserToken;
                int bytesRead = receiveArgs.BytesTransferred;
                if (bytesRead > 0 && receiveArgs.SocketError == SocketError.Success)
                {
                    byte[] receivedData = new byte[bytesRead];
                    Array.Copy(receiveArgs.Buffer, receiveArgs.Offset, receivedData, 0, bytesRead);
                    string message = Encoding.UTF8.GetString(receivedData);
                    string targetKey = clientNames.FirstOrDefault(x => x.Value == clientSocket).Key;
                    if (message.Length > 7)
                    {
                        string messageTmp = message.Substring(0, 7);
                        if (messageTmp.Equals("#image#"))
                        {
                            byte[] receivedDataImage = new byte[bytesRead - 7];

                            Array.Copy(receiveArgs.Buffer, 7, receivedDataImage, 0, receivedDataImage.Length);
                            ImageConverter convertData = new ImageConverter();
                            Image image = (Image)convertData.ConvertFrom(receivedDataImage);
                            
                            BeginInvoke((Action)(() =>
                            {
                                Panel pn = new Panel();
                                pn = createSendImagePanel(targetKey, image, false);
                                pn.Dock = DockStyle.Top;
                                panelShowMessage.Controls.Add(pn);
                                return;
                            }));
                        }
                        else if (message.StartsWith("#CLIENT_NAME#"))
                        {
                            string clientName = message.Replace("#CLIENT_NAME#", "");
                            clientNames.Add(clientName, clientSocket);
                            clbClientConnected.Items.Add(clientName);
                        }

                        else
                        {
                            //string targetKey = clientNames.FirstOrDefault(x => x.Value == clientSocket).Key;
                            //rtbReceive.Text += targetKey + ": " + message + "\r\n";
                            BeginInvoke((Action)(() =>
                            {
                                Panel pn = new Panel();
                                pn = createSendMessagePanel(targetKey, message, false);
                                pn.Dock = DockStyle.Top;
                                panelShowMessage.Controls.Add(pn);
                            }));

                        }

                    }
                    else
                    {
                        //string targetKey = clientNames.FirstOrDefault(x => x.Value == clientSocket).Key;
                            //rtbReceive.Text += targetKey + ": " + message + "\r\n";
                            BeginInvoke((Action)(() =>
                            {
                                Panel pn = new Panel();
                                pn = createSendMessagePanel(targetKey, message, false);
                                pn.Dock = DockStyle.Top;
                                panelShowMessage.Controls.Add(pn);
                            }));
                    }
                    // Xử lý tin nhắn từ client ở đây
                   
                    // Tiếp tục nhận dữ liệu từ client
                    receiveArgs.SetBuffer(receiveArgs.Buffer, receiveArgs.Offset, receiveArgs.Buffer.Length);
                }
                else
                {
                    string targetKey = clientNames.FirstOrDefault(x => x.Value == clientSocket).Key;

                    // Thực hiện việc loại bỏ client khỏi clbClientConnected trên luồng giao diện người dùng chính
                    Invoke((Action)(() =>
                    {
                        clientNames.Remove(targetKey);
                        for (int i = clbClientConnected.Items.Count - 1; i >= 0; i--)
                        {
                            if (clbClientConnected.Items[i].ToString().ToLower().Equals(targetKey.ToLower()))
                            {
                                clbClientConnected.Items.RemoveAt(i);
                                break;
                            }
                        }
                    }));

                    clientSocket.Close();
                }
            }
            catch
            {
                MessageBox.Show("ProcessReceive Error");
            }
        }

        private bool isCheckOneListClient()
        {
            for (int i = 0; i < clbClientConnected.Items.Count; i++)
            {
                if (clbClientConnected.GetItemChecked(i))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            if (isCheckOneListClient())
            {
                if (!txtSend.Text.Equals(""))
                {
                    byte[] data = new byte[1024]; 
                    data = Encoding.UTF8.GetBytes(txtSend.Text);
                    
                    Panel pn = new Panel();
                    pn = createSendMessagePanel("Server", txtSend.Text, true);
                    pn.Dock = DockStyle.Top;
                    panelShowMessage.Controls.Add(pn);
                    

                    CheckedListBox.CheckedItemCollection checkedItems = clbClientConnected.CheckedItems;
                    foreach (object item in checkedItems)
                    {
                        string text = clbClientConnected.GetItemText(item);
                        clientNames[text].Send(data);
                    }
                    txtSend.Text = "";
                }
                else
                {
                    MessageBox.Show("Vui long nhap tin nhan muon gui");
                }
                

            }
            else
            {
                MessageBox.Show("Vui long chon client muon gui");
            }
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            if (isCheckOneListClient())
            {

                string pahtImage = getPathImageDialog();
                if (pahtImage.Equals(""))
                {
                    MessageBox.Show("Lỗi chọn ảnh");
                    return;
                }
               

                CheckedListBox.CheckedItemCollection checkedItems = clbClientConnected.CheckedItems;
                foreach (object item in checkedItems)
                {
                    string text = clbClientConnected.GetItemText(item);
                    handlerSendImage(clientNames[text],pahtImage);
                }

            }
            else
            {
                MessageBox.Show("Vui long chon client muon gui");
            }
        }


        private void handlerSendImage(Socket client,string imagePath)
        {


            // Hiển thị hộp thoại OpenFileDialog và kiểm tra kết quả


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

        private string getPathImageDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp) | *.jpg; *.jpeg; *.png; *.gif; *.bmp";
            openFileDialog.Title = "Chọn hình ảnh";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                    return openFileDialog.FileName;
            }
            return "";
        }


        private Panel createSendImagePanel(string nameSend, Image image, bool isMyMessage)
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

        private Panel createSendMessagePanel(string nameSend, string message, bool isMyMessage)
        {
            Color backgroundColor;
            if (isMyMessage)
            {
                backgroundColor = Color.LightSteelBlue;
            }
            else
            {
                backgroundColor = Color.LightCoral;

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

    }
}
