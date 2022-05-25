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

            desativarCampos();


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

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

            comm.Connection = Conexao.obterConexao();

            int result = comm.ExecuteNonQuery();

            if (result == 1)
            {
                MessageBox.Show("Valores inseridos com sucesso!!!", "Padaria-SA",
                    MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                Conexao.fecharConexao();
            }
            else
            {
                MessageBox.Show("Produto Cadastrado com sucesso!!!", "Padaria-SA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                Conexao.fecharConexao();
            }

        }
    }
}
