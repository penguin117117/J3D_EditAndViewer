
namespace J3DEditorAndViewer
{
    partial class MainWindow
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            glControl = new OpenTK.GLControl.GLControl();
            MainPanel = new Panel();
            GL_Panel = new Panel();
            panel1 = new Panel();
            button1 = new Button();
            textBox1 = new TextBox();
            SceneTreeView = new TreeView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            splitter1 = new Splitter();
            MainPanel.SuspendLayout();
            GL_Panel.SuspendLayout();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // glControl
            // 
            glControl.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl.APIVersion = new Version(3, 3, 0, 0);
            glControl.BackColor = Color.Black;
            glControl.Dock = DockStyle.Fill;
            glControl.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl.IsEventDriven = true;
            glControl.Location = new Point(0, 0);
            glControl.Margin = new Padding(6, 5, 6, 5);
            glControl.Name = "glControl";
            glControl.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            glControl.SharedContext = null;
            glControl.Size = new Size(718, 522);
            glControl.TabIndex = 0;
            glControl.Load += glControl_Load;
            glControl.Paint += glControl_Paint;
            glControl.KeyDown += glControl_KeyDown;
            glControl.MouseMove += glControl_MouseMove;
            glControl.MouseWheel += glControl_MouseWheel;
            glControl.Resize += glControl_Resize;
            // 
            // MainPanel
            // 
            MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MainPanel.BorderStyle = BorderStyle.Fixed3D;
            MainPanel.Controls.Add(GL_Panel);
            MainPanel.Location = new Point(211, 33);
            MainPanel.Margin = new Padding(4, 3, 4, 3);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(722, 526);
            MainPanel.TabIndex = 0;
            // 
            // GL_Panel
            // 
            GL_Panel.AutoSize = true;
            GL_Panel.Controls.Add(glControl);
            GL_Panel.Dock = DockStyle.Fill;
            GL_Panel.Location = new Point(0, 0);
            GL_Panel.Margin = new Padding(4, 3, 4, 3);
            GL_Panel.Name = "GL_Panel";
            GL_Panel.Size = new Size(718, 522);
            GL_Panel.TabIndex = 0;
            GL_Panel.Resize += GL_Panel_Resize;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(SceneTreeView);
            panel1.Location = new Point(14, 40);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 517);
            panel1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(2, 486);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(124, 25);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Left;
            textBox1.Location = new Point(0, 0);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(188, 23);
            textBox1.TabIndex = 0;
            // 
            // SceneTreeView
            // 
            SceneTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SceneTreeView.Location = new Point(0, 31);
            SceneTreeView.Margin = new Padding(4, 3, 4, 3);
            SceneTreeView.Name = "SceneTreeView";
            SceneTreeView.Size = new Size(186, 448);
            SceneTreeView.TabIndex = 1;
            SceneTreeView.AfterSelect += SceneTreeView_AfterSelect;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(206, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            fileToolStripMenuItem.Click += fileToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(103, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(206, 0);
            splitter1.Margin = new Padding(4, 3, 4, 3);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(727, 560);
            splitter1.TabIndex = 3;
            splitter1.TabStop = false;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 560);
            Controls.Add(panel1);
            Controls.Add(MainPanel);
            Controls.Add(menuStrip1);
            Controls.Add(splitter1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainWindow";
            Text = "J3D_EditAndViewer";
            Load += MainWindow_Load;
            MainPanel.ResumeLayout(false);
            MainPanel.PerformLayout();
            GL_Panel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private OpenTK.GLControl.GLControl glControl;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TreeView SceneTreeView;
        private System.Windows.Forms.Panel GL_Panel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button button1;
    }
}

