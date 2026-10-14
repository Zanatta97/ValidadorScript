using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ValidadorScript.Application.Conexao;
using ValidadorScript.Domain.Conexao;

namespace ValidadorScript.WinForms
{
    public partial class ConfiguracoesForm : Form
    {
        private readonly Credenciais _credenciaisAtuais;

        public Credenciais? Credenciais { get; private set; }

        public ConfiguracoesForm(Credenciais credenciais)
        {
            InitializeComponent();
            _credenciaisAtuais = credenciais;
        }

        private void ConfiguracoesForm_Load(object sender, EventArgs e)
        {
            txtSenhaOracle.Text = _credenciaisAtuais.SenhaOracle;
            txtUsuarioSQL.Text = _credenciaisAtuais.UsuarioSqlServer;
            txtSenhaSQL.Text = _credenciaisAtuais.SenhaSqlServer;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var credenciais = new Credenciais(
            txtSenhaOracle.Text,
            txtUsuarioSQL.Text.Trim(),
            txtSenhaSQL.Text);

            var erros = CredenciaisDatabaseValidator.ValidarCredenciais(credenciais);
            if (erros.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, erros), "Configurações",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Credenciais = credenciais;
            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

}
