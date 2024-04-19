﻿using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        private TcpClient tcpClient;
        private string username;
        private bool isClientRunning;
        private StreamWriter streamWriter;
        private StreamReader streamReader;
        private delegate void SafeCallDelegate(string text);

        public ClientForm(TcpClient tcpClient, string username)
        {
            this.tcpClient = tcpClient;
            this.username = username;
            isClientRunning = false;
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            streamWriter = new StreamWriter(tcpClient.GetStream());
            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter.AutoFlush = true;

            this.Text = $"{username}'s ClientForm";

            isClientRunning = true;
            Thread clientThread = new Thread(() => receiveFromServer());
            clientThread.Start();
            clientThread.IsBackground = true;
        }

        private void receiveFromServer() // always running
        {
            while (isClientRunning)
            {
                string msgFromServer = streamReader.ReadLine();

                // receive the online usernames and groupnames then update it to DataGridView
                if (msgFromServer == "<UaG_Name>")
                {
                    string formatMsg = streamReader.ReadLine();
                    string usernames = formatMsg.Split(',')[0];
                    string groupnames = formatMsg.Split(',')[1];

                    foreach (string user in usernames.Split('|'))
                    {
                        UpdateOnlineUserDataGridViewThreadSafe(user);
                    }
                    foreach (string group in groupnames.Split('|'))
                    {
                        UpdateGroupDataGridViewThreadSafe(group);
                    }
                }

                // receive messages from other clients
                if (msgFromServer == "<Message>")
                {
                    string senderAndMsg = streamReader.ReadLine();
                    // splitString[0] is sender's username, splitString[1] is username or group's name, splitStrin[2] is message
                    string[] splitString = senderAndMsg.Split('|');

                    // update the received message to the RichTextBox
                    AppendRichTextBox(splitString[0], splitString[1], splitString[2], "");

                    continue;
                }

                // receive images from other clients
                if (msgFromServer == "<Image>")
                {
                    // maximum size of image is 524288 bytes
                    byte[] bytes = new byte[524288];

                    string sender = streamReader.ReadLine();

                    streamReader.BaseStream.Read(bytes, 0, bytes.Length);

                    // create a new ImageViewForm to display the picture
                    new Thread(() => Application.Run(new ImageViewForm(bytes, username))).Start();

                    // update the received message to the RichTextBox
                    AppendRichTextBox(sender, username, "Sent you a picture.", "");

                    continue;
                }

                // update the online user data grid view if a user logged in or signed up
                if (msgFromServer == "<User_Onl>")
                {
                    string onlineUsername = streamReader.ReadLine();
                    UpdateOnlineUserDataGridViewThreadSafe(onlineUsername);
                    continue;
                }

                // update the online user data grid view if a user logged out
                if (msgFromServer == "<User_Off>")
                {
                    string offlineUsername = streamReader.ReadLine();
                    UpdateOfflineUserDataGridViewThreadSafe(offlineUsername);
                    continue;
                }

                // update the group data grid view if a group is created
                if (msgFromServer == "<Group_Created>")
                {
                    string groupName = streamReader.ReadLine();
                    UpdateGroupDataGridViewThreadSafe(groupName);
                    continue;
                }

                // 
                if (msgFromServer == "<Group_Exists>")
                {
                    MessageBox.Show("Group existed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                // username or group's name doesn't exist!
                if (msgFromServer == "<UoG_Not_Exist>")
                {
                    MessageBox.Show("Username or group's name doesn't exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            streamWriter.WriteLine("<Logout>");

            isClientRunning = false;

            new Thread(() => Application.Run(new LoginForm())).Start();
            this.Close();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            // exceptions catching
            if (msgToSend.Text == "")
            {
                MessageBox.Show("Empty Fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tbReceiver.Text == username)
            {
                MessageBox.Show("Cannot send message to yourself!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // implementation
            streamWriter.WriteLine("<Message>");
            streamWriter.WriteLine($"{username}|{tbReceiver.Text}|{msgToSend.Text}");

            // update the sent message to RichTextBox
            AppendRichTextBox(username, tbReceiver.Text, msgToSend.Text, "");

            msgToSend.Text = "";
        }

        private void msgToSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (sendBtn.Enabled)
                {
                    sendBtn_Click(sender, e);
                }
            }
        }

        private void imageBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // convert image from OFD to byte array (image file to base64String)
                Image image = Image.FromFile(ofd.FileName);
                var ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                byte[] bytes = ms.ToArray();

                // send the signal message and byte array to server
                streamWriter.WriteLine("<Image>");
                streamWriter.WriteLine($"{username}|{tbReceiver.Text}");
                streamWriter.BaseStream.Write(bytes, 0, bytes.Length);
            }
        }

        private void emojiCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            msgToSend.Text += emojiCB.Text;
            emojiCB.SelectedIndex = -1;
        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            new Thread(() => Application.Run(new CreateGroupForm(tcpClient, username))).Start();
        }

        // the method use to format the message then update it to RichTextBox
        // source: https://github.com/trinhvinhphuc/Chat-app/blob/master/Chat-app%20Client/ChatBox.cs
        private void AppendRichTextBox(string sender, string receiver, string message, string link)
        {
            statusAndMsg.BeginInvoke(new MethodInvoker(() =>
            {
                Font currentFont = statusAndMsg.SelectionFont;

                //Username
                statusAndMsg.SelectionStart = statusAndMsg.TextLength;
                statusAndMsg.SelectionLength = 0;
                statusAndMsg.SelectionColor = Color.Red;
                statusAndMsg.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Bold);
                statusAndMsg.AppendText(sender + "<" + receiver + ">");
                statusAndMsg.SelectionColor = statusAndMsg.ForeColor;

                statusAndMsg.AppendText(": ");

                //Message
                statusAndMsg.SelectionStart = statusAndMsg.TextLength;
                statusAndMsg.SelectionLength = 0;
                statusAndMsg.SelectionColor = Color.Green;
                statusAndMsg.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Regular);
                statusAndMsg.AppendText(message);
                statusAndMsg.SelectionColor = statusAndMsg.ForeColor;

                statusAndMsg.AppendText(" ");

                //link
                statusAndMsg.SelectionStart = statusAndMsg.TextLength;
                statusAndMsg.SelectionLength = 0;
                statusAndMsg.SelectionColor = Color.Blue;
                statusAndMsg.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Underline);
                statusAndMsg.AppendText(link);
                statusAndMsg.SelectionColor = statusAndMsg.ForeColor;


                statusAndMsg.SelectionStart = statusAndMsg.GetFirstCharIndexOfCurrentLine();
                statusAndMsg.SelectionLength = 0;

                if (sender == this.username)
                {
                    statusAndMsg.SelectionAlignment = HorizontalAlignment.Right;
                }
                else statusAndMsg.SelectionAlignment = HorizontalAlignment.Left;

                statusAndMsg.AppendText(Environment.NewLine);
            }));
        }

        #region UpdateThreadSafe

        private void UpdateOnlineUserDataGridViewThreadSafe(string text)
        {
            if (statusAndMsg.InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateOnlineUserDataGridViewThreadSafe);
                dgvUser.Invoke(d, new object[] { text });
            }
            else
            {
                dgvUser.Rows.Add(text);
            }
        }

        private void UpdateOfflineUserDataGridViewThreadSafe(string text)
        {
            if (statusAndMsg.InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateOfflineUserDataGridViewThreadSafe);
                dgvUser.Invoke(d, new object[] { text });
            }
            else
            {
                foreach (DataGridViewRow row in dgvUser.Rows)
                {
                    if (row.Cells[0].Value.ToString() == text)
                    {
                        dgvUser.Rows.Remove(row);
                        break;
                    }
                }
            }
        }

        private void UpdateGroupDataGridViewThreadSafe(string text)
        {
            if (statusAndMsg.InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateGroupDataGridViewThreadSafe);
                dgvGroup.Invoke(d, new object[] { text });
            }
            else
            {
                dgvGroup.Rows.Add(text);
            }
        }

        #endregion
    }
}