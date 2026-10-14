using ValidadorScript.Application.Conexao;
using ValidadorScript.Domain.Conexao;

namespace ValidadorScript.WinForms
{
    public partial class MainForm : Form
    {
        private readonly IDadosConexaoReader _dadosConexaoReader;
        private readonly ICredenciaisStore _credenciaisStore;
        private DadosConexao _dadosConexao;
        public MainForm(IDadosConexaoReader dadosConexaoReader, ICredenciaisStore credenciaisStore)
        {
            InitializeComponent();
            _dadosConexaoReader = dadosConexaoReader;
            _credenciaisStore = credenciaisStore;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnImportar.Enabled = false;
            btnExportar.Enabled = false;
            btnValidar.Enabled = false;
            rtbScriptInput.Enabled = false;
        }

        private void btnConexao_Click(object sender, EventArgs e)
        {
            try
            {
                //Busca os dados de conexão do arquivo Conexao.dat
                var dadosConexao = _dadosConexaoReader.ObterDadosConexao();

                //Preenche os dados na tela
                if (dadosConexao != null)
                {
                    _dadosConexao = dadosConexao;
                    lbTituloConexao.Text = dadosConexao.Tipo switch
                    {
                        TipoDatabase.SQLSERVER => "SQL Server",
                        TipoDatabase.ORACLE => "Oracle",
                        _ => "Conexão Desconhecida"
                    };
                    lbDatabase.Text = $"Database: {dadosConexao.Database ?? ""}";
                    lbInstancia.Text = $"Instancia: {dadosConexao.Servidor ?? ""}";
                    lbUsuario.Text = $"Usuario: {dadosConexao.Usuario ?? ""}";

                    btnValidar.Enabled = true;
                    btnImportar.Enabled = true;
                    rtbScriptInput.Enabled = true;

                }
                else
                {
                    // Se não encontrar os dados de conexão, desabilita os botões e exibe uma mensagem de erro
                    btnImportar.Enabled = false;
                    btnExportar.Enabled = false;
                    btnValidar.Enabled = false;
                    rtbScriptInput.Enabled = false;
                    throw new Exception("Dados de conexão não encontrados.");
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer algum erro ao obter os dados de conexão, desabilita os botões e exibe uma mensagem de erro
                btnImportar.Enabled = false;
                btnExportar.Enabled = false;
                btnValidar.Enabled = false;
                rtbScriptInput.Enabled = false;
                MessageBox.Show($"Erro ao obter dados de conexão: {ex.Message}",
                    "Erro no Conexão.dat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool AbrirConfiguracao()
        {
            // O dialog abre preenchido com o que já existe
            using var dialog = new ConfiguracoesForm(_credenciaisStore.Obter());

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return false;

            _credenciaisStore.Salvar(dialog.Credenciais!);
            return true;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

            if (CredenciaisDatabaseValidator.ValidarCredenciais(_credenciaisStore.Obter()).Count > 0 && !AbrirConfiguracao())
            {
                Close();
                return;
            }

            btnConexao_Click(sender, e);
        }

        private void btnConfiguracoes_Click(object sender, EventArgs e)
        {
            if (AbrirConfiguracao())
                btnConexao_Click(sender, e);  // relê a conexão com as credenciais novas
        }
    }
}
