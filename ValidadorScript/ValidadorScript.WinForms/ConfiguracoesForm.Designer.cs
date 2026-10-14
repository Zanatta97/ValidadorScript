namespace ValidadorScript.WinForms
{
    partial class ConfiguracoesForm
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
            lbTitulo = new Label();
            gbOracle = new GroupBox();
            txtSenhaOracle = new TextBox();
            lbSenhaOracle = new Label();
            gbSql = new GroupBox();
            txtSenhaSQL = new TextBox();
            txtUsuarioSQL = new TextBox();
            lbSenhaSql = new Label();
            label2 = new Label();
            btnSalvar = new Button();
            btnCancelar = new Button();
            gbOracle.SuspendLayout();
            gbSql.SuspendLayout();
            SuspendLayout();
            // 
            // lbTitulo
            // 
            lbTitulo.Dock = DockStyle.Top;
            lbTitulo.Font = new Font("Segoe UI", 12F);
            lbTitulo.Location = new Point(0, 0);
            lbTitulo.Name = "lbTitulo";
            lbTitulo.Size = new Size(528, 33);
            lbTitulo.TabIndex = 0;
            lbTitulo.Text = "Configurações";
            lbTitulo.TextAlign = ContentAlignment.TopCenter;
            // 
            // gbOracle
            // 
            gbOracle.Controls.Add(txtSenhaOracle);
            gbOracle.Controls.Add(lbSenhaOracle);
            gbOracle.Location = new Point(12, 36);
            gbOracle.Name = "gbOracle";
            gbOracle.Size = new Size(250, 150);
            gbOracle.TabIndex = 1;
            gbOracle.TabStop = false;
            gbOracle.Text = "Oracle";
            // 
            // txtSenhaOracle
            // 
            txtSenhaOracle.Location = new Point(11, 46);
            txtSenhaOracle.Name = "txtSenhaOracle";
            txtSenhaOracle.Size = new Size(233, 27);
            txtSenhaOracle.TabIndex = 4;
            txtSenhaOracle.UseSystemPasswordChar = true;
            // 
            // lbSenhaOracle
            // 
            lbSenhaOracle.AutoSize = true;
            lbSenhaOracle.Location = new Point(6, 23);
            lbSenhaOracle.Name = "lbSenhaOracle";
            lbSenhaOracle.Size = new Size(168, 20);
            lbSenhaOracle.TabIndex = 3;
            lbSenhaOracle.Text = "Senha Padrão do Oracle";
            // 
            // gbSql
            // 
            gbSql.Controls.Add(txtSenhaSQL);
            gbSql.Controls.Add(txtUsuarioSQL);
            gbSql.Controls.Add(lbSenhaSql);
            gbSql.Controls.Add(label2);
            gbSql.Location = new Point(268, 36);
            gbSql.Name = "gbSql";
            gbSql.Size = new Size(250, 150);
            gbSql.TabIndex = 2;
            gbSql.TabStop = false;
            gbSql.Text = "Sql Server";
            // 
            // txtSenhaSQL
            // 
            txtSenhaSQL.Location = new Point(6, 108);
            txtSenhaSQL.Name = "txtSenhaSQL";
            txtSenhaSQL.Size = new Size(238, 27);
            txtSenhaSQL.TabIndex = 6;
            txtSenhaSQL.UseSystemPasswordChar = true;
            // 
            // txtUsuarioSQL
            // 
            txtUsuarioSQL.Location = new Point(6, 46);
            txtUsuarioSQL.Name = "txtUsuarioSQL";
            txtUsuarioSQL.Size = new Size(238, 27);
            txtUsuarioSQL.TabIndex = 5;
            // 
            // lbSenhaSql
            // 
            lbSenhaSql.AutoSize = true;
            lbSenhaSql.Location = new Point(6, 85);
            lbSenhaSql.Name = "lbSenhaSql";
            lbSenhaSql.Size = new Size(196, 20);
            lbSenhaSql.TabIndex = 5;
            lbSenhaSql.Text = "Senha Padrão do SQL Server";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 23);
            label2.Name = "label2";
            label2.Size = new Size(184, 20);
            label2.TabIndex = 4;
            label2.Text = "Usuário Padrão SQL Server";
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(418, 199);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(94, 29);
            btnSalvar.TabIndex = 3;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(318, 199);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // ConfiguracoesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(528, 240);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(gbSql);
            Controls.Add(gbOracle);
            Controls.Add(lbTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfiguracoesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configurações";
            Load += ConfiguracoesForm_Load;
            gbOracle.ResumeLayout(false);
            gbOracle.PerformLayout();
            gbSql.ResumeLayout(false);
            gbSql.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lbTitulo;
        private GroupBox gbOracle;
        private Label lbSenhaOracle;
        private GroupBox gbSql;
        private Label lbSenhaSql;
        private Label label2;
        private TextBox txtUsuarioSQL;
        private TextBox txtSenhaOracle;
        private TextBox txtSenhaSQL;
        private Button btnSalvar;
        private Button btnCancelar;
    }
}