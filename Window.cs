using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using RaceGame.Delegates;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace RaceGame
{
    public partial class Window : Form
    {
        public Timer GameTimer;
        public Bitmap BackBuffer;
        public List<GameTask> GameTasks;
        public List<DrawInfo> DrawInfos;
        private RadioButton Tank2;
        private RadioButton Jackass2;
        private RadioButton LAPV2;
        private RadioButton Horsepower2;
        private RadioButton Motorfiets2;
        private RadioButton Motorfiets1;
        private RadioButton Horsepower1;
        private RadioButton LAPV1;
        private RadioButton Jackass1;
        private RadioButton Tank1;
        private Panel panel2;
        private Panel panel1;
        private Button startgame;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        public Label Player1PitCount;
        private Label label10;
        public Label Player1LapCount;
        private Label label13;
        private Label label14;
        private Label label16;
        public Label Player2PitCount;
        private Label label18;
        public Label Player2LapCount;
        public ProgressBar Player1Fuel;
        public ProgressBar Player1Health;
        public ProgressBar Player1Speed;
        public ProgressBar Player2Fuel;
        public ProgressBar Player2Health;
        public ProgressBar Player2Speed;
        public Game currentGame;
        public bool showMenu = true;
        public bool disableInput = true;
        public int TotalLaps = 5;
        Enums.VehicleType player1Vehicle;
        public Button button1;
        private Label label7;
        private Label label9;
        private Label label11;
        private Label label12;
        private Panel panel3;
        Enums.VehicleType player2Vehicle;

        /// <summary>
        /// Initialiseert de window. Override de standaard WM_PAINT message van de messageloop, 
        /// en start het drawproces van de game.
        /// </summary>
        public Window()
        {
            InitializeComponent();
            GameTasks = new List<GameTask>();
            DrawInfos = new List<DrawInfo>();
            Base.drawInfos = DrawInfos;
            Base.gameTasks = GameTasks;

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            GameTimer = new Timer();
            GameTimer.Interval = 16;
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            GameTimer.Start();
            Load += new EventHandler(CreateBackBuffer);
            Paint += new PaintEventHandler(PaintBackbuffer);
            toggleVisibility();
            if (Base.drawInfos == null || Base.drawInfos.Count != 0)
            {
                Base.drawInfos = new List<DrawInfo>();
            }
            Base.drawInfos.Add(new DrawInfo(Bitmaps.Other.Background, 1008 / 2, 792 / 2, 1008, 792, -90));
        }

        /// <summary>
        /// Regelt de zichtbaarheid van het hoofdmenu
        /// </summary>
        void toggleVisibility()
        {
            if (showMenu)
            {
                this.label12.Visible = true;
                this.panel3.Visible = true;
                this.panel2.Visible = true;
                this.panel1.Visible = true;
                this.startgame.Visible = true;
                this.numericUpDown1.Visible = true;
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.label3.Visible = false;
                this.label4.Visible = false;
                this.label5.Visible = false;
                this.label6.Visible = false;
                this.label8.Visible = false;
                this.Player1PitCount.Visible = false;
                this.label10.Visible = false;
                this.Player1LapCount.Visible = false;
                this.label13.Visible = false;
                this.label14.Visible = false;
                this.label16.Visible = false;
                this.Player2PitCount.Visible = false;
                this.label18.Visible = false;
                this.Player2LapCount.Visible = false;
                this.Player1Fuel.Visible = false;
                this.Player1Health.Visible = false;
                this.Player1Speed.Visible = false;
                this.Player2Fuel.Visible = false;
                this.Player2Health.Visible = false;
                this.Player2Speed.Visible = false;
    }
            else
            {
                this.label12.Visible = false;
                this.panel3.Visible = false;
                this.panel2.Visible = false;
                this.panel1.Visible = false;
                this.startgame.Visible = false;
                this.numericUpDown1.Visible = false;
                this.label1.Visible = true;
                this.label2.Visible = true;
                this.label3.Visible = true;
                this.label4.Visible = true;
                this.label5.Visible = true;
                this.label6.Visible = true;
                this.label8.Visible = true;
                this.Player1PitCount.Visible = true;
                this.label10.Visible = true;
                this.Player1LapCount.Visible = true;
                this.label13.Visible = true;
                this.label14.Visible = true;
                this.label16.Visible = true;
                this.Player2PitCount.Visible = true;
                this.label18.Visible = true;
                this.Player2LapCount.Visible = true;
                this.Player1Fuel.Visible = true;
                this.Player1Health.Visible = true;
                this.Player1Speed.Visible = true;
                this.Player2Fuel.Visible = true;
                this.Player2Health.Visible = true;
                this.Player2Speed.Visible = true;
            }
        }

        /// <summary>
        /// Initializer van de Form Controls.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.Tank2 = new System.Windows.Forms.RadioButton();
            this.Jackass2 = new System.Windows.Forms.RadioButton();
            this.LAPV2 = new System.Windows.Forms.RadioButton();
            this.Horsepower2 = new System.Windows.Forms.RadioButton();
            this.Motorfiets2 = new System.Windows.Forms.RadioButton();
            this.Motorfiets1 = new System.Windows.Forms.RadioButton();
            this.Horsepower1 = new System.Windows.Forms.RadioButton();
            this.LAPV1 = new System.Windows.Forms.RadioButton();
            this.Jackass1 = new System.Windows.Forms.RadioButton();
            this.Tank1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startgame = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Player1PitCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Player1LapCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Player2PitCount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.Player2LapCount = new System.Windows.Forms.Label();
            this.Player1Fuel = new System.Windows.Forms.ProgressBar();
            this.Player1Health = new System.Windows.Forms.ProgressBar();
            this.Player1Speed = new System.Windows.Forms.ProgressBar();
            this.Player2Fuel = new System.Windows.Forms.ProgressBar();
            this.Player2Health = new System.Windows.Forms.ProgressBar();
            this.Player2Speed = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tank2
            // 
            this.Tank2.AutoSize = true;
            this.Tank2.Checked = true;
            this.Tank2.ForeColor = System.Drawing.Color.White;
            this.Tank2.Location = new System.Drawing.Point(10, 10);
            this.Tank2.Name = "Tank2";
            this.Tank2.Size = new System.Drawing.Size(50, 17);
            this.Tank2.TabIndex = 3;
            this.Tank2.TabStop = true;
            this.Tank2.Text = "Tank";
            this.Tank2.UseVisualStyleBackColor = true;
            this.Tank2.CheckedChanged += new System.EventHandler(this.Tank2_CheckedChanged);
            // 
            // Jackass2
            // 
            this.Jackass2.AutoSize = true;
            this.Jackass2.ForeColor = System.Drawing.Color.White;
            this.Jackass2.Location = new System.Drawing.Point(10, 33);
            this.Jackass2.Name = "Jackass2";
            this.Jackass2.Size = new System.Drawing.Size(91, 17);
            this.Jackass2.TabIndex = 4;
            this.Jackass2.Text = "Shopping cart";
            this.Jackass2.UseVisualStyleBackColor = true;
            this.Jackass2.CheckedChanged += new System.EventHandler(this.Jackass2_CheckedChanged);
            // 
            // LAPV2
            // 
            this.LAPV2.AutoSize = true;
            this.LAPV2.ForeColor = System.Drawing.Color.White;
            this.LAPV2.Location = new System.Drawing.Point(10, 56);
            this.LAPV2.Name = "LAPV2";
            this.LAPV2.Size = new System.Drawing.Size(52, 17);
            this.LAPV2.TabIndex = 5;
            this.LAPV2.Text = "LAPV";
            this.LAPV2.UseVisualStyleBackColor = true;
            this.LAPV2.CheckedChanged += new System.EventHandler(this.LAPV2_CheckedChanged);
            // 
            // Horsepower2
            // 
            this.Horsepower2.AutoSize = true;
            this.Horsepower2.ForeColor = System.Drawing.Color.White;
            this.Horsepower2.Location = new System.Drawing.Point(10, 79);
            this.Horsepower2.Name = "Horsepower2";
            this.Horsepower2.Size = new System.Drawing.Size(82, 17);
            this.Horsepower2.TabIndex = 6;
            this.Horsepower2.Text = "Horsepower";
            this.Horsepower2.UseVisualStyleBackColor = true;
            this.Horsepower2.CheckedChanged += new System.EventHandler(this.Horsepower2_CheckedChanged);
            // 
            // Motorfiets2
            // 
            this.Motorfiets2.AutoSize = true;
            this.Motorfiets2.ForeColor = System.Drawing.Color.White;
            this.Motorfiets2.Location = new System.Drawing.Point(10, 102);
            this.Motorfiets2.Name = "Motorfiets2";
            this.Motorfiets2.Size = new System.Drawing.Size(71, 17);
            this.Motorfiets2.TabIndex = 7;
            this.Motorfiets2.Text = "Motorfiets";
            this.Motorfiets2.UseVisualStyleBackColor = true;
            this.Motorfiets2.CheckedChanged += new System.EventHandler(this.Motorfiets2_CheckedChanged);
            // 
            // Motorfiets1
            // 
            this.Motorfiets1.AutoSize = true;
            this.Motorfiets1.ForeColor = System.Drawing.Color.White;
            this.Motorfiets1.Location = new System.Drawing.Point(10, 102);
            this.Motorfiets1.Name = "Motorfiets1";
            this.Motorfiets1.Size = new System.Drawing.Size(71, 17);
            this.Motorfiets1.TabIndex = 12;
            this.Motorfiets1.Text = "Motorfiets";
            this.Motorfiets1.UseVisualStyleBackColor = true;
            this.Motorfiets1.CheckedChanged += new System.EventHandler(this.Motorfiets1_CheckedChanged);
            // 
            // Horsepower1
            // 
            this.Horsepower1.AutoSize = true;
            this.Horsepower1.ForeColor = System.Drawing.Color.White;
            this.Horsepower1.Location = new System.Drawing.Point(10, 79);
            this.Horsepower1.Name = "Horsepower1";
            this.Horsepower1.Size = new System.Drawing.Size(82, 17);
            this.Horsepower1.TabIndex = 11;
            this.Horsepower1.Text = "Horsepower";
            this.Horsepower1.UseVisualStyleBackColor = true;
            this.Horsepower1.CheckedChanged += new System.EventHandler(this.Horsepower1_CheckedChanged);
            // 
            // LAPV1
            // 
            this.LAPV1.AutoSize = true;
            this.LAPV1.ForeColor = System.Drawing.Color.White;
            this.LAPV1.Location = new System.Drawing.Point(10, 56);
            this.LAPV1.Name = "LAPV1";
            this.LAPV1.Size = new System.Drawing.Size(52, 17);
            this.LAPV1.TabIndex = 10;
            this.LAPV1.Text = "LAPV";
            this.LAPV1.UseVisualStyleBackColor = true;
            this.LAPV1.CheckedChanged += new System.EventHandler(this.LAPV1_CheckedChanged);
            // 
            // Jackass1
            // 
            this.Jackass1.AutoSize = true;
            this.Jackass1.ForeColor = System.Drawing.Color.White;
            this.Jackass1.Location = new System.Drawing.Point(10, 33);
            this.Jackass1.Name = "Jackass1";
            this.Jackass1.Size = new System.Drawing.Size(91, 17);
            this.Jackass1.TabIndex = 9;
            this.Jackass1.Text = "Shopping cart";
            this.Jackass1.UseVisualStyleBackColor = true;
            this.Jackass1.CheckedChanged += new System.EventHandler(this.Jackass1_CheckedChanged);
            // 
            // Tank1
            // 
            this.Tank1.AutoSize = true;
            this.Tank1.Checked = true;
            this.Tank1.ForeColor = System.Drawing.Color.White;
            this.Tank1.Location = new System.Drawing.Point(10, 10);
            this.Tank1.Name = "Tank1";
            this.Tank1.Size = new System.Drawing.Size(50, 17);
            this.Tank1.TabIndex = 8;
            this.Tank1.TabStop = true;
            this.Tank1.Text = "Tank";
            this.Tank1.UseVisualStyleBackColor = true;
            this.Tank1.CheckedChanged += new System.EventHandler(this.Tank1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LAPV2);
            this.panel2.Controls.Add(this.Tank2);
            this.panel2.Controls.Add(this.Jackass2);
            this.panel2.Controls.Add(this.Horsepower2);
            this.panel2.Controls.Add(this.Motorfiets2);
            this.panel2.Location = new System.Drawing.Point(409, 26);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(100, 133);
            this.panel2.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Jackass1);
            this.panel1.Controls.Add(this.Tank1);
            this.panel1.Controls.Add(this.Motorfiets1);
            this.panel1.Controls.Add(this.LAPV1);
            this.panel1.Controls.Add(this.Horsepower1);
            this.panel1.Location = new System.Drawing.Point(8, 26);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(100, 133);
            this.panel1.TabIndex = 14;
            // 
            // startgame
            // 
            this.startgame.Location = new System.Drawing.Point(114, 45);
            this.startgame.Name = "startgame";
            this.startgame.Size = new System.Drawing.Size(289, 114);
            this.startgame.TabIndex = 15;
            this.startgame.Text = "Start Game";
            this.startgame.UseVisualStyleBackColor = true;
            this.startgame.Click += new System.EventHandler(this.startgame_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(266, 8);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Turquoise;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(437, 680);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "PLAYER 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(942, 680);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "PLAYER 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(0, 680);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fuel:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(504, 680);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Fuel:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(0, 693);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Health:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(0, 706);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Speed:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.ForeColor = System.Drawing.Color.Yellow;
            this.label8.Location = new System.Drawing.Point(0, 732);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Pit stops:";
            // 
            // Player1PitCount
            // 
            this.Player1PitCount.AutoSize = true;
            this.Player1PitCount.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player1PitCount.ForeColor = System.Drawing.Color.White;
            this.Player1PitCount.Location = new System.Drawing.Point(56, 732);
            this.Player1PitCount.Name = "Player1PitCount";
            this.Player1PitCount.Size = new System.Drawing.Size(25, 13);
            this.Player1PitCount.TabIndex = 10;
            this.Player1PitCount.Text = "<2>";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.ForeColor = System.Drawing.Color.Yellow;
            this.label10.Location = new System.Drawing.Point(232, 732);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Laps:";
            // 
            // Player1LapCount
            // 
            this.Player1LapCount.AutoSize = true;
            this.Player1LapCount.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player1LapCount.ForeColor = System.Drawing.Color.White;
            this.Player1LapCount.Location = new System.Drawing.Point(270, 732);
            this.Player1LapCount.Name = "Player1LapCount";
            this.Player1LapCount.Size = new System.Drawing.Size(36, 13);
            this.Player1LapCount.TabIndex = 12;
            this.Player1LapCount.Text = "<2/5>";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label13.ForeColor = System.Drawing.Color.Yellow;
            this.label13.Location = new System.Drawing.Point(504, 693);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Health:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label14.ForeColor = System.Drawing.Color.Yellow;
            this.label14.Location = new System.Drawing.Point(504, 706);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Speed:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label16.ForeColor = System.Drawing.Color.Yellow;
            this.label16.Location = new System.Drawing.Point(504, 732);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 17;
            this.label16.Text = "Pit stops:";
            // 
            // Player2PitCount
            // 
            this.Player2PitCount.AutoSize = true;
            this.Player2PitCount.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player2PitCount.ForeColor = System.Drawing.Color.White;
            this.Player2PitCount.Location = new System.Drawing.Point(560, 732);
            this.Player2PitCount.Name = "Player2PitCount";
            this.Player2PitCount.Size = new System.Drawing.Size(25, 13);
            this.Player2PitCount.TabIndex = 18;
            this.Player2PitCount.Text = "<3>";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label18.ForeColor = System.Drawing.Color.Yellow;
            this.label18.Location = new System.Drawing.Point(736, 732);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "Laps:";
            // 
            // Player2LapCount
            // 
            this.Player2LapCount.AutoSize = true;
            this.Player2LapCount.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Player2LapCount.ForeColor = System.Drawing.Color.White;
            this.Player2LapCount.Location = new System.Drawing.Point(774, 732);
            this.Player2LapCount.Name = "Player2LapCount";
            this.Player2LapCount.Size = new System.Drawing.Size(36, 13);
            this.Player2LapCount.TabIndex = 20;
            this.Player2LapCount.Text = "<2/5>";
            // 
            // Player1Fuel
            // 
            this.Player1Fuel.BackColor = System.Drawing.Color.Red;
            this.Player1Fuel.ForeColor = System.Drawing.Color.Gold;
            this.Player1Fuel.Location = new System.Drawing.Point(60, 680);
            this.Player1Fuel.Name = "Player1Fuel";
            this.Player1Fuel.Size = new System.Drawing.Size(300, 13);
            this.Player1Fuel.Step = 1;
            this.Player1Fuel.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Player1Fuel.TabIndex = 22;
            // 
            // Player1Health
            // 
            this.Player1Health.BackColor = System.Drawing.Color.Red;
            this.Player1Health.ForeColor = System.Drawing.Color.Lime;
            this.Player1Health.Location = new System.Drawing.Point(60, 693);
            this.Player1Health.Name = "Player1Health";
            this.Player1Health.Size = new System.Drawing.Size(300, 13);
            this.Player1Health.Step = 1;
            this.Player1Health.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Player1Health.TabIndex = 23;
            // 
            // Player1Speed
            // 
            this.Player1Speed.BackColor = System.Drawing.Color.Red;
            this.Player1Speed.ForeColor = System.Drawing.Color.Lime;
            this.Player1Speed.Location = new System.Drawing.Point(60, 706);
            this.Player1Speed.Name = "Player1Speed";
            this.Player1Speed.Size = new System.Drawing.Size(300, 13);
            this.Player1Speed.Step = 1;
            this.Player1Speed.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Player1Speed.TabIndex = 24;
            // 
            // Player2Fuel
            // 
            this.Player2Fuel.BackColor = System.Drawing.Color.Red;
            this.Player2Fuel.ForeColor = System.Drawing.Color.Yellow;
            this.Player2Fuel.Location = new System.Drawing.Point(563, 680);
            this.Player2Fuel.Name = "Player2Fuel";
            this.Player2Fuel.Size = new System.Drawing.Size(300, 13);
            this.Player2Fuel.Step = 1;
            this.Player2Fuel.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Player2Fuel.TabIndex = 26;
            // 
            // Player2Health
            // 
            this.Player2Health.BackColor = System.Drawing.Color.Red;
            this.Player2Health.ForeColor = System.Drawing.Color.Lime;
            this.Player2Health.Location = new System.Drawing.Point(563, 693);
            this.Player2Health.Name = "Player2Health";
            this.Player2Health.Size = new System.Drawing.Size(300, 13);
            this.Player2Health.Step = 1;
            this.Player2Health.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Player2Health.TabIndex = 27;
            // 
            // Player2Speed
            // 
            this.Player2Speed.BackColor = System.Drawing.Color.Red;
            this.Player2Speed.ForeColor = System.Drawing.Color.Lime;
            this.Player2Speed.Location = new System.Drawing.Point(563, 706);
            this.Player2Speed.Name = "Player2Speed";
            this.Player2Speed.Size = new System.Drawing.Size(300, 13);
            this.Player2Speed.Step = 1;
            this.Player2Speed.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Player2Speed.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 70);
            this.button1.TabIndex = 29;
            this.button1.Text = "Back to main menu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Snow;
            this.label7.Location = new System.Drawing.Point(183, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Amount of laps";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(15, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Vehicle player 1:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(416, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Vehicle player 2:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Snow;
            this.label12.Location = new System.Drawing.Point(396, 498);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(219, 63);
            this.label12.TabIndex = 33;
            this.label12.Text = "K.A.R.S";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Controls.Add(this.startgame);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(253, 564);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(515, 181);
            this.panel3.TabIndex = 34;
            // 
            // Window
            // 
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Player2Speed);
            this.Controls.Add(this.Player2Health);
            this.Controls.Add(this.Player2Fuel);
            this.Controls.Add(this.Player1Speed);
            this.Controls.Add(this.Player1Health);
            this.Controls.Add(this.Player1Fuel);
            this.Controls.Add(this.Player2LapCount);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.Player2PitCount);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Player1LapCount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Player1PitCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 786);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "Window";
            this.ShowIcon = false;
            this.Text = "K.A.R.S";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Event dat uitgevoerd wordt als de GameTimer een tick maakt.
        /// Voert de delegates inde GameTasks lijst uit, hierna wordt Draw() gecalled om alles op het scherm te tekenen.
        /// </summary>
        void GameTimer_Tick(object sender, EventArgs e)
        {
            //Alle game tasks invoken
            for (int i = 0; i < GameTasks.Count; i++)
            {
                GameTasks[i].Invoke();
            }
            Draw();
        }
        /// <summary>
        /// De werkelijke draw loop. Samen in combi met de GameTimer_Tick() de gehele gameloop.  Door gebruik van een lijst van delegates en lijst van 'DrawInfo's' hoef je hier als programmeur totaal niet aan te denken.
        /// Voorkomt ook dat de gameloop vol wordt gestouwd met code. Deze vult de Backbuffer met informatie die in het volgende frame moeten.
        /// </summary>
        void Draw()
        {
            if (BackBuffer != null)
            {
                using (var buffer = Graphics.FromImage(BackBuffer))
                {
                    buffer.Clear(Color.Black);
                    for (int i = 0; i < DrawInfos.Count; i++)
                    {
                        DrawInfo DR = DrawInfos[i];

                        PointF rotatePoint = new PointF(DR.x, DR.y);
                        Matrix myMatrix = new Matrix();
                        myMatrix.RotateAt(DR.angle + 90, rotatePoint, MatrixOrder.Append);
                        buffer.Transform = myMatrix;
                        buffer.DrawImage(DR.bitmapdata, DR.x - DR.width / 2, DR.y - DR.height / 2, DR.width, DR.height);

                        if (DR.AutoRemove)
                        {
                            if (DR.Frames != 0)
                            {
                                DR.LowerFrameCount();
                            }
                            else
                            {
                                DrawInfos.RemoveAt(i);
                            }
                        }
                    }
                }
                Invalidate();
            }
        }
        /// <summary>
        /// Creëert de backbuffer en regelt het disposen ervan. Aangezien het een COM element is en teruggeven moet worden aan de Garbage Collector.
        /// Wordt gecallt wanneer het Form opent.
        /// </summary>
        void CreateBackBuffer(object sender, EventArgs e)
        {
            if (BackBuffer != null)
            {
                BackBuffer.Dispose();
            }
            BackBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        /// <summary>
        /// Override van het WM_PAINT message, en swapped de backbuffer met wat er op het scherm staat.
        /// </summary>
        void PaintBackbuffer(object sender, PaintEventArgs e)
        {
            if (BackBuffer != null)
            {
                e.Graphics.DrawImageUnscaled(BackBuffer, new System.Drawing.Point(0, 0));
            }
        }

        /// <summary>
        /// Reset en creëert een nieuwe game.
        /// </summary>
        void CreateGame()
        {
            Base.currentGame = null;
            currentGame = null;
            Base.currentGame = new Game(player1Vehicle, player2Vehicle);
            Progressbars.TotalLaps = TotalLaps;
            currentGame = Base.currentGame;
        }

        /// <summary>
        /// Deze eventhandler verzorgt de input wanneer een toets ingedrukt wordt.
        /// </summary>
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (!disableInput)
            {
                //CheckKeysDown();
                e.Handled = true;
                e.SuppressKeyPress = true;
                switch (e.KeyCode)
                {
                    case Keys.W:
                        Base.currentGame.player1.vehicle.throttle = true;
                        break;
                    case Keys.S:
                        Base.currentGame.player1.vehicle.brake = true;
                        break;
                    case Keys.A:
                        Base.currentGame.player1.vehicle.turning = "left";
                        break;
                    case Keys.D:
                        Base.currentGame.player1.vehicle.turning = "right";
                        break;

                    case Keys.Q:
                        if (Base.currentGame.player1.vehicle.weapon != null)
                            Base.currentGame.player1.vehicle.weapon.turning = "left";
                        break;
                    case Keys.E:
                        if (Base.currentGame.player1.vehicle.weapon != null)
                            Base.currentGame.player1.vehicle.weapon.turning = "right";
                        break;
                    case Keys.D2:
                        if (Base.currentGame.player1.vehicle.weapon != null)
                        {
                            Base.currentGame.player1.vehicle.shooting = true;
                            if (Base.currentGame.player1.vehicleType == Enums.VehicleType.HorsePower && Base.currentGame.player1.SpecBool == false && Base.currentGame.player1.vehicle.weapon.weaponReloading == 0)
                            {
                                AudioFiles.Flame.PlayLooping();
                                Base.currentGame.player1.SpecBool = true;
                            }
                        }
                        break;


                    //P2
                    case Keys.NumPad8:
                        Base.currentGame.player2.vehicle.throttle = true;
                        break;
                    case Keys.NumPad5:
                        Base.currentGame.player2.vehicle.brake = true;
                        break;
                    case Keys.NumPad4:
                        Base.currentGame.player2.vehicle.turning = "left";
                        break;
                    case Keys.NumPad6:
                        Base.currentGame.player2.vehicle.turning = "right";
                        break;

                    case Keys.NumPad7:
                        if (Base.currentGame.player2.vehicle.weapon != null)
                            Base.currentGame.player2.vehicle.weapon.turning = "left";
                        break;
                    case Keys.NumPad9:
                        if (Base.currentGame.player2.vehicle.weapon != null)
                            Base.currentGame.player2.vehicle.weapon.turning = "right";
                        break;
                    case Keys.Divide:
                        if (Base.currentGame.player2.vehicle.weapon != null)
                        {
                            Base.currentGame.player2.vehicle.shooting = true;
                            if (Base.currentGame.player2.vehicleType == Enums.VehicleType.HorsePower && Base.currentGame.player2.SpecBool == false && Base.currentGame.player2.vehicle.weapon.weaponReloading == 0)
                            {
                                AudioFiles.Flame.PlayLooping();
                                Base.currentGame.player2.SpecBool = true;
                            }
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// Deze eventhandler wordt uitgevoerd wanneer een toets wordt losgelaten.
        /// </summary>
        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            if (!disableInput) { 
            e.Handled = true;
            e.SuppressKeyPress = true;
                switch (e.KeyCode)
                {
                    case Keys.W:
                        Base.currentGame.player1.vehicle.throttle = false;
                        break;
                    case Keys.S:
                        Base.currentGame.player1.vehicle.brake = false;
                        break;
                    case Keys.A:
                        Base.currentGame.player1.vehicle.turning = "false";
                        break;
                    case Keys.D:
                        Base.currentGame.player1.vehicle.turning = "false";
                        break;

                    case Keys.Q:
                        if (Base.currentGame.player1.vehicle.weapon != null)
                            Base.currentGame.player1.vehicle.weapon.turning = "false";
                        break;
                    case Keys.E:
                        if (Base.currentGame.player1.vehicle.weapon != null)
                            Base.currentGame.player1.vehicle.weapon.turning = "false";
                        break;
                    case Keys.D2:
                        if (Base.currentGame.player1.vehicle.weapon != null)
                        {
                            Base.currentGame.player1.vehicle.shooting = false;
                            if (Base.currentGame.player1.vehicleType == Enums.VehicleType.HorsePower)
                            {
                                AudioFiles.Flame.Stop();
                                Base.currentGame.player1.SpecBool = false;
                            }
                        }
                        break;


                    //P2
                    case Keys.NumPad8:
                        Base.currentGame.player2.vehicle.throttle = false;
                        break;
                    case Keys.NumPad5:
                        Base.currentGame.player2.vehicle.brake = false;
                        break;
                    case Keys.NumPad4:
                        Base.currentGame.player2.vehicle.turning = "false";
                        break;
                    case Keys.NumPad6:
                        Base.currentGame.player2.vehicle.turning = "false";
                        break;

                    case Keys.NumPad7:
                        if (Base.currentGame.player2.vehicle.weapon != null)
                        {
                            Base.currentGame.player2.vehicle.weapon.turning = "false";
                            if (Base.currentGame.player2.vehicleType == Enums.VehicleType.HorsePower)
                            {
                                AudioFiles.Flame.Stop();
                                Base.currentGame.player2.SpecBool = false;
                            }
                        }
                        break;
                    case Keys.NumPad9:
                        if (Base.currentGame.player2.vehicle.weapon != null)
                            Base.currentGame.player2.vehicle.weapon.turning = "false";
                        break;
                    case Keys.Divide:
                        if (Base.currentGame.player2.vehicle.weapon != null)
                            Base.currentGame.player2.vehicle.shooting = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Bevat de code voor de radiobuttons om hun keuzes te bevestigen.
        /// </summary>
        #region RadioButtons
        private void Tank1_CheckedChanged(object sender, EventArgs e)
        {
            player1Vehicle = Enums.VehicleType.Tank;
        }

        private void Jackass1_CheckedChanged(object sender, EventArgs e)
        {
            player1Vehicle = Enums.VehicleType.Jackass;
        }

        private void LAPV1_CheckedChanged(object sender, EventArgs e)
        {
            player1Vehicle = Enums.VehicleType.LAPV;
        }

        private void Horsepower1_CheckedChanged(object sender, EventArgs e)
        {
            player1Vehicle = Enums.VehicleType.HorsePower;
        }

        private void Motorfiets1_CheckedChanged(object sender, EventArgs e)
        {
            player1Vehicle = Enums.VehicleType.Motorfiets;
        }

        private void Tank2_CheckedChanged(object sender, EventArgs e)
        {
            player2Vehicle = Enums.VehicleType.Tank;
        }

        private void Jackass2_CheckedChanged(object sender, EventArgs e)
        {
            player2Vehicle = Enums.VehicleType.Jackass;
        }

        private void LAPV2_CheckedChanged(object sender, EventArgs e)
        {
            player2Vehicle = Enums.VehicleType.LAPV;
        }

        private void Horsepower2_CheckedChanged(object sender, EventArgs e)
        {
            player2Vehicle = Enums.VehicleType.HorsePower;
        }

        private void Motorfiets2_CheckedChanged(object sender, EventArgs e)
        {
            player2Vehicle = Enums.VehicleType.Motorfiets;
        }
        #endregion

        /// <summary>
        /// Deze method initialiseert een game met de gekozen voertuigen voor elke speler.
        /// </summary>
        private void startgame_Click(object sender, EventArgs e)
        {
            AudioFiles.Motor.Play();
            showMenu = false;
            disableInput = false;
            toggleVisibility();
            Base.currentGame = null;
            Base.gameTasks = null;
            Base.drawInfos = null;
            GameTasks = new List<GameTask>();
            DrawInfos = new List<DrawInfo>();
            Base.drawInfos = DrawInfos;
            Base.gameTasks = GameTasks;
            CreateGame();
            Pitstop P = new Pitstop();
            Progressbars.Initialize();
            Base.gameTasks.Add(Progressbars.Check);
            Base.gameTasks.Add(Base.currentGame.CheckHealth);
        }

        /// <summary>
        /// Check de numericUpDown en stelt het aantal laps in.
        /// </summary>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            TotalLaps = (int)(numericUpDown1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            showMenu = true;
            toggleVisibility();
            Base.drawInfos.Add(new DrawInfo(Bitmaps.Other.Background, 1008/2, 792/ 2, 1008, 792, -90));
        }
    }
}