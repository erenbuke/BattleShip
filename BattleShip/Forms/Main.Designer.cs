
using System.IO;

namespace BattleShip.Forms
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ready = new System.Windows.Forms.Button();
            this.chat = new System.Windows.Forms.RichTextBox();
            this.send = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chattext = new System.Windows.Forms.TextBox();
            this.you = new System.Windows.Forms.Panel();
            this.enemy = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ready);
            this.panel1.Controls.Add(this.chat);
            this.panel1.Controls.Add(this.send);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chattext);
            this.panel1.Controls.Add(this.you);
            this.panel1.Controls.Add(this.enemy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 530);
            this.panel1.TabIndex = 0;
            // 
            // ready
            // 
            this.ready.Location = new System.Drawing.Point(112, 410);
            this.ready.Name = "ready";
            this.ready.Size = new System.Drawing.Size(130, 50);
            this.ready.TabIndex = 10;
            this.ready.Text = "HAZIR";
            this.ready.UseVisualStyleBackColor = true;
            // 
            // chat
            // 
            this.chat.Location = new System.Drawing.Point(408, 374);
            this.chat.Name = "chat";
            this.chat.ReadOnly = true;
            this.chat.Size = new System.Drawing.Size(330, 118);
            this.chat.TabIndex = 9;
            this.chat.Text = "";
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(663, 496);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 8;
            this.send.Text = "Gönder";
            this.send.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(405, 358);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Chat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enemy";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "You";
            // 
            // chattext
            // 
            this.chattext.Location = new System.Drawing.Point(408, 498);
            this.chattext.Name = "chattext";
            this.chattext.Size = new System.Drawing.Size(249, 20);
            this.chattext.TabIndex = 4;
            // 
            // you
            // 
            this.you.Location = new System.Drawing.Point(12, 25);
            this.you.Name = "you";
            this.you.Size = new System.Drawing.Size(330, 330);
            this.you.TabIndex = 2;
            // 
            // enemy
            // 
            this.enemy.Location = new System.Drawing.Point(408, 25);
            this.enemy.Name = "enemy";
            this.enemy.Size = new System.Drawing.Size(330, 330);
            this.enemy.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 530);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Main";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox chattext;
        private System.Windows.Forms.Panel you;
        private System.Windows.Forms.Panel enemy;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox chat;
        private System.Windows.Forms.Button ready;
    }
}