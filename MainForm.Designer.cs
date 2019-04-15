namespace NLTestApp
{
    partial class MainForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.tsbtnReload = new System.Windows.Forms.ToolStripButton();
            this.tsbtnImport = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExport = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imgUploadingAnim = new System.Windows.Forms.PictureBox();
            this.marsApplicationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolbar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgUploadingAnim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marsApplicationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 35);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(686, 415);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.dataGridView.EnabledChanged += new System.EventHandler(this.DataGridView_EnabledChanged);
            // 
            // toolbar
            // 
            this.toolbar.AutoSize = false;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnReload,
            this.tsbtnImport,
            this.tsbtnExport});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolbar.ShowItemToolTips = false;
            this.toolbar.Size = new System.Drawing.Size(686, 35);
            this.toolbar.TabIndex = 3;
            this.toolbar.Text = "toolStrip1";
            // 
            // tsbtnReload
            // 
            this.tsbtnReload.Image = global::NLTestApp.Properties.Resources.refresh_24;
            this.tsbtnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnReload.Name = "tsbtnReload";
            this.tsbtnReload.Size = new System.Drawing.Size(133, 32);
            this.tsbtnReload.Text = "Обновить данные";
            this.tsbtnReload.Click += new System.EventHandler(this.TsbtnReload_Click);
            // 
            // tsbtnImport
            // 
            this.tsbtnImport.Image = global::NLTestApp.Properties.Resources.import_24;
            this.tsbtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnImport.Name = "tsbtnImport";
            this.tsbtnImport.Size = new System.Drawing.Size(123, 32);
            this.tsbtnImport.Text = "Импортировать";
            this.tsbtnImport.Click += new System.EventHandler(this.TsbtnImport_Click);
            // 
            // tsbtnExport
            // 
            this.tsbtnExport.Image = global::NLTestApp.Properties.Resources.upload_24;
            this.tsbtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExport.Name = "tsbtnExport";
            this.tsbtnExport.Size = new System.Drawing.Size(124, 32);
            this.tsbtnExport.Text = "Экспортировать";
            this.tsbtnExport.Click += new System.EventHandler(this.TsbtnExport_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Controls.Add(this.imgUploadingAnim, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 412);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(686, 38);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // imgUploadingAnim
            // 
            this.imgUploadingAnim.Image = global::NLTestApp.Properties.Resources.import_24_anim;
            this.imgUploadingAnim.Location = new System.Drawing.Point(624, 3);
            this.imgUploadingAnim.Name = "imgUploadingAnim";
            this.imgUploadingAnim.Size = new System.Drawing.Size(59, 32);
            this.imgUploadingAnim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgUploadingAnim.TabIndex = 0;
            this.imgUploadingAnim.TabStop = false;
            this.imgUploadingAnim.Visible = false;
            // 
            // marsApplicationBindingSource
            // 
            this.marsApplicationBindingSource.DataSource = typeof(NLTestApp.MarsApplicant);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolbar);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заявки для полёта на Марс";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgUploadingAnim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marsApplicationBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource marsApplicationBindingSource;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton tsbtnReload;
        private System.Windows.Forms.ToolStripButton tsbtnImport;
        private System.Windows.Forms.ToolStripButton tsbtnExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox imgUploadingAnim;
    }
}

