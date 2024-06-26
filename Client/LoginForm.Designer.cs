﻿using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbUsername = new TextBox();
            tbPassword = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnLogin = new Button();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            lblSignUp = new Label();
            label6 = new Label();
            tbPort = new TextBox();
            label7 = new Label();
            tbServerIP = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tbUsername
            // 
            tbUsername.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            tbUsername.Location = new Point(402, 383);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(216, 32);
            tbUsername.TabIndex = 2;
            // 
            // tbPassword
            // 
            tbPassword.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            tbPassword.Location = new Point(402, 435);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(216, 32);
            tbPassword.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Image = Properties.Resources.form_background;
            label1.Location = new Point(305, 386);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 2;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Image = Properties.Resources.form_background;
            label2.Location = new Point(305, 438);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Image = Properties.Resources.form_background;
            label3.Location = new Point(402, 232);
            label3.Name = "label3";
            label3.Size = new Size(216, 30);
            label3.TabIndex = 4;
            label3.Text = "Chat with your friend";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = SystemColors.Highlight;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnLogin.Location = new Point(402, 488);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(216, 40);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.chat3_png;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(450, 124);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(131, 105);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(484, 470);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 7;
            // 
            // lblSignUp
            // 
            lblSignUp.AutoSize = true;
            lblSignUp.Cursor = Cursors.Hand;
            lblSignUp.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point);
            lblSignUp.Image = Properties.Resources.form_background;
            lblSignUp.Location = new Point(392, 578);
            lblSignUp.Name = "lblSignUp";
            lblSignUp.Size = new Size(231, 21);
            lblSignUp.TabIndex = 5;
            lblSignUp.Text = "Create an account if you're new";
            lblSignUp.Click += lblSignUp_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Image = Properties.Resources.form_background;
            label6.Location = new Point(305, 332);
            label6.Name = "label6";
            label6.Size = new Size(46, 25);
            label6.TabIndex = 11;
            label6.Text = "Port";
            // 
            // tbPort
            // 
            tbPort.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            tbPort.Location = new Point(402, 329);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(216, 32);
            tbPort.TabIndex = 1;
            tbPort.Text = "11000";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Image = Properties.Resources.form_background;
            label7.Location = new Point(305, 278);
            label7.Name = "label7";
            label7.Size = new Size(86, 25);
            label7.TabIndex = 13;
            label7.Text = "Server IP";
            // 
            // tbServerIP
            // 
            tbServerIP.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            tbServerIP.Location = new Point(402, 275);
            tbServerIP.Name = "tbServerIP";
            tbServerIP.Size = new Size(216, 32);
            tbServerIP.TabIndex = 0;
            tbServerIP.Text = "127.0.0.1";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.form_background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(984, 661);
            Controls.Add(label7);
            Controls.Add(tbServerIP);
            Controls.Add(label6);
            Controls.Add(tbPort);
            Controls.Add(lblSignUp);
            Controls.Add(label4);
            Controls.Add(pictureBox1);
            Controls.Add(btnLogin);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbPassword);
            Controls.Add(tbUsername);
            MaximumSize = new Size(1000, 700);
            MinimumSize = new Size(1000, 700);
            Name = "LoginForm";
            Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbUsername;
        private TextBox tbPassword;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnLogin;
        private PictureBox pictureBox1;
        private Label label4;
        private Label lblSignUp;
        private Label label6;
        private TextBox tbPort;
        private Label label7;
        private TextBox tbServerIP;
    }
}
