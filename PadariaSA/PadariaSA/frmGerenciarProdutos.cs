using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PadariaSA
{
    public partial class frmGerenciarProdutos : Form
    {
        public frmGerenciarProdutos()
        {
            InitializeComponent();
            desativarCampos();
        }
        public bool novo = false, alterar = false, excluir = false;



        private void btnNovo_Click(object sender, EventArgs e)
        {
            ativarCampos();
            btnGravar.Enabled = true;
            btnCancelar.Enabled = true;
            novo = true;
        }

        public void desativarCampos()
        {
            txtCodigo.Enabled = false;
            txtDescrProd.Enabled = false;
            txtPrecoVenda.Enabled = false;
            txtPrecoCompra.Enabled = false;
            txtEstAtualLoja.Enabled = false;


            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnGravar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        public void ativarCampos()
        {
            txtCodigo.Enabled = false;
            txtDescrProd.Enabled = true;
            txtPrecoVenda.Enabled = true;
            txtPrecoCompra.Enabled = true;
            txtEstAtualLoja.Enabled = true;
            txtDescrProd.Focus();
        }

        public void limparCampos()
        {
            txtCodigo.Text = "";
            txtDescrProd.Text = "";
            txtPrecoVenda.Text = "";
            txtPrecoCompra.Text = "";
            txtEstAtualLoja.Text = "";

            ltbListagemProdutos.Items.Clear();
            rdbCodigo.Checked = false;
            rdbNome.Checked = false;

            desativarCampos();


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (rdbCodigo.Checked == true)
            {
                pesquisarPorCodigo();

            }
            if (rdbNome.Checked == true)
            {
                pesquisarPorNome();

            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            alterarProdutos();

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (novo == true)
            {
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                btnGravar.Enabled = true;
                if (txtDescrProd.Text.Equals("") || txtPrecoVenda.Text.Equals("") || txtPrecoCompra.Text.Equals("") || txtEstAtualLoja.Text.Equals(""))
                {
                    MessageBox.Show("Favor preencher todos os campos!!!", "Padaria-SA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    cadastrarProdutos();
                    limparCampos();
                    txtDescricao.Focus();
                }
            }
        }

        //Criando o método para cadastrar produtos
        public void cadastrarProdutos()
        {

            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "insert into tbProdutos(descricao,precoVenda,precoCompra,estoqueAtual)" +
                "values(@descricao,@precoVenda,@precoCompra,@estoqueAtual); ";
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();

            comm.Parameters.Add("@descricao", MySqlDbType.VarChar, 200).Value = txtDescrProd.Text;
            comm.Parameters.Add("@precoVenda", MySqlDbType.Decimal, 18).Value = txtPrecoVenda.Text;
            comm.Parameters.Add("@precoCompra", MySqlDbType.Decimal, 18).Value = txtPrecoCompra.Text;
            comm.Parameters.Add("@estoqueAtual", MySqlDbType.Decimal, 18).Value = txtEstAtualLoja.Text;

            try
            {
                comm.Connection = Conexao.obterConexao();

                int result = comm.ExecuteNonQuery();

                if (result == 1)
                {
                    MessageBox.Show("Produto Cadastrado!!!", "Padaria-SA",
                        MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    Conexao.fecharConexao();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar o produto!!!", "Padaria-SA",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    Conexao.fecharConexao();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao conectar com o banco de dados!!!", "Padaria-SA",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                Conexao.fecharConexao();
            }
        }

        //Método para alterar produtos

        public void alterarProdutos()
        {

            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "update tbProdutos set descricao = @descricao, precoVenda = @precoVenda, precoCompra = @precoCompra, estoqueAtual =@estoqueAtual   where codProd = " + txtCodigo.Text;
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();

            comm.Parameters.Add("@descricao", MySqlDbType.VarChar, 200).Value = txtDescrProd.Text;
            comm.Parameters.Add("@precoVenda", MySqlDbType.Decimal, 18).Value = txtPrecoVenda.Text;
            comm.Parameters.Add("@precoCompra", MySqlDbType.Decimal, 18).Value = txtPrecoCompra.Text;
            comm.Parameters.Add("@estoqueAtual", MySqlDbType.Decimal, 18).Value = txtEstAtualLoja.Text;

            try
            {
                comm.Connection = Conexao.obterConexao();

                int result = comm.ExecuteNonQuery();

                if (result == 1)
                {
                    MessageBox.Show("Produto alterado!!!", "Padaria-SA",
                        MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    Conexao.fecharConexao();
                    limparCampos();
                    ltbListagemProdutos.Items.Clear();
                    rdbNome.Checked = false;
                    rdbCodigo.Checked = false;
                }
                else
                {
                    MessageBox.Show("Erro ao alterar o produto!!!", "Padaria-SA",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    Conexao.fecharConexao();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao conectar com o banco de dados!!!", "Padaria-SA",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                Conexao.fecharConexao();
            }
        }

        private void ltbListagemProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ativar os botões alterar e excluir
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
            btnNovo.Enabled = false;

            //ativar os campos
            txtDescrProd.Enabled = true;
            txtPrecoVenda.Enabled = true;
            txtPrecoCompra.Enabled = true;
            txtEstAtualLoja.Enabled = true;

            txtDescrProd.Focus();

            txtDescrProd.Text = ltbListagemProdutos.SelectedItem.ToString();

            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from tbProdutos where descricao like '%" + txtDescrProd.Text + "%'";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();


            MySqlDataReader DR;

            DR = comm.ExecuteReader();

            DR.Read();

            txtCodigo.Text = Convert.ToString(DR.GetInt32(0));
            txtDescrProd.Text = DR.GetString(1);
            txtPrecoVenda.Text = DR.GetString(2);
            txtPrecoCompra.Text = DR.GetString(3);
            txtEstAtualLoja.Text = DR.GetString(4);

            Conexao.fecharConexao();

        }

        //Criando o método pesquisar por nome
        public void pesquisarPorNome()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from tbProdutos where descricao like '%" + txtDescricao.Text + "%'";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();


            MySqlDataReader DR;

            DR = comm.ExecuteReader();

            ltbListagemProdutos.Items.Clear();

            while (DR.Read())
            {
                ltbListagemProdutos.Items.Add(DR.GetString(1));
            }
        }

        //Criando o método pesquisar por código
        public void pesquisarPorCodigo()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from tbProdutos where codProd = " + txtDescricao.Text;
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;

            DR = comm.ExecuteReader();
            DR.Read();

            ltbListagemProdutos.Items.Add(DR.GetInt32(0) + " - " + DR.GetString(1));

            Conexao.fecharConexao();
        }

    }
}