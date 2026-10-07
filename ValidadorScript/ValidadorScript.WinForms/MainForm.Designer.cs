namespace ValidadorScript.WinForms
{
    partial class MainForm
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
            lbTitulo = new Label();
            rtbScriptInput = new RichTextBox();
            rtbRetornoScript = new RichTextBox();
            lbTituloConexao = new Label();
            lbDatabase = new Label();
            lbInstancia = new Label();
            lbUsuario = new Label();
            btnImportar = new Button();
            btnConexao = new Button();
            btnValidar = new Button();
            btnExportar = new Button();
            ofdAbrirScript = new OpenFileDialog();
            ssStatus = new StatusStrip();
            SuspendLayout();
            // 
            // lbTitulo
            // 
            lbTitulo.AutoSize = true;
            lbTitulo.Dock = DockStyle.Top;
            lbTitulo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitulo.ImageAlign = ContentAlignment.MiddleRight;
            lbTitulo.Location = new Point(0, 0);
            lbTitulo.Name = "lbTitulo";
            lbTitulo.Size = new Size(217, 31);
            lbTitulo.TabIndex = 6;
            lbTitulo.Text = "Validador de Script";
            lbTitulo.TextAlign = ContentAlignment.TopCenter;
            // 
            // rtbScriptInput
            // 
            rtbScriptInput.AcceptsTab = true;
            rtbScriptInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbScriptInput.DetectUrls = false;
            rtbScriptInput.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbScriptInput.Location = new Point(12, 44);
            rtbScriptInput.Name = "rtbScriptInput";
            rtbScriptInput.Size = new Size(396, 306);
            rtbScriptInput.TabIndex = 0;
            rtbScriptInput.Text = "";
            rtbScriptInput.WordWrap = false;
            // 
            // rtbRetornoScript
            // 
            rtbRetornoScript.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbRetornoScript.BackColor = SystemColors.Window;
            rtbRetornoScript.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbRetornoScript.Location = new Point(12, 356);
            rtbRetornoScript.Name = "rtbRetornoScript";
            rtbRetornoScript.ReadOnly = true;
            rtbRetornoScript.Size = new Size(656, 168);
            rtbRetornoScript.TabIndex = 5;
            rtbRetornoScript.Text = "";
            // 
            // lbTituloConexao
            // 
            lbTituloConexao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbTituloConexao.AutoEllipsis = true;
            lbTituloConexao.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTituloConexao.ImageAlign = ContentAlignment.MiddleRight;
            lbTituloConexao.Location = new Point(414, 9);
            lbTituloConexao.Name = "lbTituloConexao";
            lbTituloConexao.Size = new Size(254, 31);
            lbTituloConexao.TabIndex = 7;
            lbTituloConexao.Text = "Conexão";
            lbTituloConexao.TextAlign = ContentAlignment.TopRight;
            lbTituloConexao.UseMnemonic = false;
            // 
            // lbDatabase
            // 
            lbDatabase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbDatabase.AutoEllipsis = true;
            lbDatabase.Location = new Point(414, 47);
            lbDatabase.Name = "lbDatabase";
            lbDatabase.Size = new Size(254, 20);
            lbDatabase.TabIndex = 8;
            lbDatabase.Text = "127.0.0.1";
            lbDatabase.TextAlign = ContentAlignment.TopRight;
            lbDatabase.UseMnemonic = false;
            // 
            // lbInstancia
            // 
            lbInstancia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbInstancia.AutoEllipsis = true;
            lbInstancia.ImageAlign = ContentAlignment.MiddleRight;
            lbInstancia.Location = new Point(414, 67);
            lbInstancia.Name = "lbInstancia";
            lbInstancia.RightToLeft = RightToLeft.No;
            lbInstancia.Size = new Size(254, 20);
            lbInstancia.TabIndex = 9;
            lbInstancia.Text = "Instancia_Oracle";
            lbInstancia.TextAlign = ContentAlignment.TopRight;
            // 
            // lbUsuario
            // 
            lbUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbUsuario.AutoEllipsis = true;
            lbUsuario.ImageAlign = ContentAlignment.MiddleRight;
            lbUsuario.Location = new Point(414, 87);
            lbUsuario.Name = "lbUsuario";
            lbUsuario.RightToLeft = RightToLeft.No;
            lbUsuario.Size = new Size(254, 20);
            lbUsuario.TabIndex = 10;
            lbUsuario.Text = "USUARIO_ORACLE";
            lbUsuario.TextAlign = ContentAlignment.TopRight;
            // 
            // btnImportar
            // 
            btnImportar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnImportar.Location = new Point(414, 251);
            btnImportar.Name = "btnImportar";
            btnImportar.Size = new Size(254, 29);
            btnImportar.TabIndex = 2;
            btnImportar.Text = "Importar Script";
            btnImportar.UseVisualStyleBackColor = true;
            // 
            // btnConexao
            // 
            btnConexao.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConexao.Location = new Point(414, 216);
            btnConexao.Name = "btnConexao";
            btnConexao.Size = new Size(254, 29);
            btnConexao.TabIndex = 1;
            btnConexao.Text = "Validar Conexão";
            btnConexao.UseVisualStyleBackColor = true;
            btnConexao.Click += btnConexao_Click;
            // 
            // btnValidar
            // 
            btnValidar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnValidar.Location = new Point(414, 286);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(254, 29);
            btnValidar.TabIndex = 3;
            btnValidar.Text = "Validar Script";
            btnValidar.UseVisualStyleBackColor = true;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExportar.Location = new Point(414, 321);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(254, 29);
            btnExportar.TabIndex = 4;
            btnExportar.Text = "Exportar Script";
            btnExportar.UseVisualStyleBackColor = true;
            // 
            // ofdAbrirScript
            // 
            ofdAbrirScript.Filter = "Scripts SQL|*.sql|Arquivos de Texto|*.txt|Todos os arquivos|*.*";
            // 
            // ssStatus
            // 
            ssStatus.ImageScalingSize = new Size(20, 20);
            ssStatus.Location = new Point(0, 527);
            ssStatus.Name = "ssStatus";
            ssStatus.Size = new Size(678, 22);
            ssStatus.TabIndex = 11;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(678, 549);
            Controls.Add(ssStatus);
            Controls.Add(btnExportar);
            Controls.Add(btnValidar);
            Controls.Add(btnConexao);
            Controls.Add(btnImportar);
            Controls.Add(lbUsuario);
            Controls.Add(lbInstancia);
            Controls.Add(lbDatabase);
            Controls.Add(lbTituloConexao);
            Controls.Add(rtbRetornoScript);
            Controls.Add(rtbScriptInput);
            Controls.Add(lbTitulo);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MinimumSize = new Size(700, 600);
            Name = "MainForm";
            RightToLeft = RightToLeft.No;
            Text = "Validador de Script";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTitulo;
        private RichTextBox rtbScriptInput;
        private RichTextBox richTextBox1;
        private RichTextBox rtbRetornoScript;
        private Label lbTituloConexao;
        private Label lbDatabase;
        private Label lbInstancia;
        private Label lbUsuario;
        private Button btnImportar;
        private Button btnConexao;
        private Button btnValidar;
        private Button btnExportar;
        private OpenFileDialog ofdAbrirScript;
        private StatusStrip ssStatus;
    }
}
