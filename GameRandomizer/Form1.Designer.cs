namespace GameRandomizer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.bgwLoadGames = new System.ComponentModel.BackgroundWorker();
            this.imgListGame = new System.Windows.Forms.ImageList(this.components);
            this.clbDrives = new System.Windows.Forms.CheckedListBox();
            this.lvwGames = new System.Windows.Forms.ListView();
            this.btnReload = new System.Windows.Forms.Button();
            this.pnlGameInfo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // bgwLoadGames
            // 
            this.bgwLoadGames.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoadGames_DoWork);
            // 
            // imgListGame
            // 
            this.imgListGame.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgListGame.ImageSize = new System.Drawing.Size(92, 43);
            this.imgListGame.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // clbDrives
            // 
            this.clbDrives.CheckOnClick = true;
            this.clbDrives.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDrives.FormattingEnabled = true;
            this.clbDrives.Location = new System.Drawing.Point(12, 466);
            this.clbDrives.Name = "clbDrives";
            this.clbDrives.Size = new System.Drawing.Size(705, 84);
            this.clbDrives.TabIndex = 2;
            this.clbDrives.TabStop = false;
            // 
            // lvwGames
            // 
            this.lvwGames.AllowColumnReorder = true;
            this.lvwGames.BackColor = System.Drawing.SystemColors.Window;
            this.lvwGames.LargeImageList = this.imgListGame;
            this.lvwGames.Location = new System.Drawing.Point(13, 13);
            this.lvwGames.Name = "lvwGames";
            this.lvwGames.Size = new System.Drawing.Size(588, 447);
            this.lvwGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwGames.TabIndex = 1;
            this.lvwGames.TabStop = false;
            this.lvwGames.TileSize = new System.Drawing.Size(200, 64);
            this.lvwGames.UseCompatibleStateImageBehavior = false;
            this.lvwGames.View = System.Windows.Forms.View.Tile;
            this.lvwGames.SelectedIndexChanged += new System.EventHandler(this.lvwGames_SelectedIndexChanged);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(13, 557);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(704, 50);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // pnlGameInfo
            // 
            this.pnlGameInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.pnlGameInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlGameInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGameInfo.Location = new System.Drawing.Point(274, 13);
            this.pnlGameInfo.Name = "pnlGameInfo";
            this.pnlGameInfo.Size = new System.Drawing.Size(443, 447);
            this.pnlGameInfo.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(730, 644);
            this.Controls.Add(this.pnlGameInfo);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.clbDrives);
            this.Controls.Add(this.lvwGames);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Game Randomizer";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bgwLoadGames;
        private System.Windows.Forms.ImageList imgListGame;
        private System.Windows.Forms.CheckedListBox clbDrives;
        private System.Windows.Forms.ListView lvwGames;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Panel pnlGameInfo;
    }
}

