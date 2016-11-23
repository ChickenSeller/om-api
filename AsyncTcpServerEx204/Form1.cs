using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;

namespace AsyncTcpServer
{
    public partial class FrmServer : Form
    {
        public FrmServer()
        {
            InitializeComponent();
        }

        Socket client = null;
        string Sendstr;
        ArrayList friends = new ArrayList();  //保存与客户相关的信息的列表
        TcpListener listener;                      //负责侦听的套接字
        bool IsStart = false;                      //指示是否已经启动了侦听
        //对控件进行调用的委托类型和委托方法
        //在列表中写字符串
        delegate void AppendDelegate(string str);
        AppendDelegate AppendString;
        ////在建立连接时，往下拉列表框中添加客户信息
        //delegate void AddDelegate(MyFriend frd);
        //AddDelegate Addfriend;
        ////在断开连接时，从下拉列表框中删除客户信息
        //delegate void RemoveDelegate(MyFriend frd);
        //RemoveDelegate Removefriend;
        ////在列表中写字符串的委托方法
        private void AppendMethod(string str)
        {
            rtxtboxMessage.AppendText(str);
        }
        ////往下拉列表框中添加信息的委托方法
        //private void AddMethod(MyFriend frd)
        //{
        //    lock (friends)
        //    { friends.Add(frd); }
        //    comboBoxClient.Items.Add(frd.socket.RemoteEndPoint.ToString());
        //}
        ////从下拉列表框中删除信息的委托方法
        //private void RemoveMethod(MyFriend frd)
        //{
        //    int i = friends.IndexOf(frd);
        //    comboBoxClient.Items.RemoveAt(i);
        //    lock (friends) { friends.Remove(frd); }
        //    frd.Dispose();
        //}

        private void FrmServer_Load(object sender, EventArgs e)
        {
            //实例化委托对象，与委托方法关联
            AppendString = new AppendDelegate(AppendMethod);
            //Addfriend = new AddDelegate(AddMethod);
            //Removefriend = new RemoveDelegate(RemoveMethod);
            IncomingHandler x = new IncomingHandler();
            x.TransferVoiceMenu("1", "22");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //服务器已经其中侦听，则返回
            if (IsStart) return;
            //服务器启动侦听
            try
            {
                IPEndPoint localep = new IPEndPoint(Dns.GetHostByName(Dns.GetHostName()).AddressList[0], int.Parse(txtport.Text));
                //IPEndPoint localep = new IPEndPoint(IPAddress.Parse("192.168.130.72"), 8077);
                listener = new TcpListener(localep);
                listener.Start(10);
            }
            catch
            {
                rtxtboxMessage.Invoke(AppendString, "端口错误，请修改后重试！\n");
                return;
            }
            IsStart = true;
            rtxtboxMessage.Invoke(AppendString, string.Format("服务器已经启动侦听！端点为：{0}。\n", listener.LocalEndpoint.ToString()));
            //接受连接请求的异步调用
            //AsyncCallback callback = new AsyncCallback(AcceptCallback);
            listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), listener);
            this.btnStart.Enabled = false;
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                //完成异步接受连接请求的异步调用
                //将连接信息添加到列表和下拉列表框中
                Socket handle = listener.EndAcceptSocket(ar);
                MyFriend frd = new MyFriend(handle);
                //comboBoxClient.Invoke(Addfriend, frd);
                //AsyncCallback callback;
                //继续调用异步方法接收连接请求
                if (IsStart)
                {
                    //callback = new AsyncCallback(AcceptCallback);
                    listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), listener);
                }
                //开始在连接上进行异步的数据接收
                frd.ClearBuffer();
               // callback = new AsyncCallback(ReceiveCallback);
                frd.socket.BeginReceive(frd.Rcvbuffer, 0, frd.Rcvbuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), frd);
            }
            catch
            {
                //listBox1.Invoke(AppendString,exception
                //在调用EndAcceptSocket方法时可能引发异常
                //——套接字Listener被关闭，则设置为未启动侦听状态
                IsStart = false;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            MyFriend frd = (MyFriend)ar.AsyncState;
            try
            {
                int i = frd.socket.EndReceive(ar);

                if (i == 0)
                {
                    //comboBoxClient.Invoke(Removefriend, frd);
                    return;
                }
                else
                {
                    string data = Encoding.UTF8.GetString(frd.Rcvbuffer, 0, i);
                    data = string.Format("From[{0}]:\n{1}", frd.socket.RemoteEndPoint.ToString(), data);
                    rtxtboxMessage.Invoke(AppendString, data);

                    frd.ClearBuffer();
                    //AsyncCallback callback = new AsyncCallback(ReceiveCallback);
                    frd.socket.BeginReceive(frd.Rcvbuffer, 0, frd.Rcvbuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), frd);
                }
            }
            catch //(Exception ex)
            {
                //comboBoxClient.Invoke(Removefriend, frd);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!IsStart) return;
            listener.Stop();
            IsStart = false;
            rtxtboxMessage.Invoke(AppendString, "已经结束了服务器的侦听！\n");
            this.btnStart.Enabled = true;
        }

        private void btnSendMes_Click(object sender, EventArgs e)
        {
            if (txtBoxMessag.Text.Trim() == "")
            {
                rtxtboxMessage.AppendText("不能发送空字符串！\n");
                txtBoxMessag.Focus();
                return;
            }
            if (txtaddress.Text.Trim() == "" || !txtaddress.Text.Trim().Contains(':'))
            {
                rtxtboxMessage.AppendText("请输入正确的发送地址！\n");
                return;
            }
            Sendstr = txtBoxMessag.Text.Trim();
            Connet(txtaddress.Text.Trim());

        }
        private byte[] BuildMsg(string send)
        {
            string sendString = @"POST /xml HTTP/1.0";
            sendString += string.Format("\nContent-Type:text/html\nContent-Length: {0}\n\n{1}", send.Length, send);
            return Encoding.UTF8.GetBytes(sendString);
        }
        public void Connet(string addr)
        {
            try
            {
                IPEndPoint remoteip = new IPEndPoint(IPAddress.Parse(addr.Split(':')[0]), int.Parse(addr.Split(':')[1]));
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(remoteip, new AsyncCallback(ConnectCallback), client);
            }
            catch
            { //服务器异常
                rtxtboxMessage.Invoke(AppendString, "发送失败！\n");
            }
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                client.EndConnect(ar);
                SendData();
            }
            catch
            {//服务器异常
                rtxtboxMessage.Invoke(AppendString, "发送失败！\n");
            }
        }

        public void SendData()
        {
            try
            {
                //byte[] msg = Encoding.UTF8.GetBytes(Sendstr);
                byte[] msg = new byte[1024];
                msg = BuildMsg(Sendstr);
                client.Send(msg, SocketFlags.None);
                rtxtboxMessage.Invoke(AppendString, string.Format("Sendto[{0}]：\n{1}\n", client.RemoteEndPoint, Encoding.UTF8.GetString(msg)));
                MyFriend frd = new MyFriend(client);
                frd.ClearBuffer();
                frd.socket.BeginReceive(frd.Rcvbuffer, 0, frd.Rcvbuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), frd);
                Thread.Sleep(3000);
            }
            catch
            {
                rtxtboxMessage.Invoke(AppendString, "发送失败！\n");
            }
            finally
            {
                SocketClose();
            }
        }

        private void SocketClose()
        {
            if (client == null) return;
            if (!client.Connected) return;
            rtxtboxMessage.Invoke(AppendString, string.Format("Close[{0}]\n", client.RemoteEndPoint));
            client.Shutdown(SocketShutdown.Both);
            client.Close(1000);
            client = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.rtxtboxMessage.Clear();
        }
    }
}
