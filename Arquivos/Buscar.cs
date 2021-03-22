using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manddoxx
{
    public class Buscar
    {
        static public void Mensagem(string Mensagem, string titulo, MessageBoxButtons botao, MessageBoxIcon icone)
        {
            MessageBox.Show(Mensagem, titulo, botao, icone);
        }
        static public string FormatCEP(string sender)
        {

            string response = sender.Trim();
            if (response.Length == 8)
            {
                response = response.Insert(5, "-");
            }
            return response;
        }
        static public void buscarCEP(TextBox txtCEP, TextBox txtNumeroCASA, TextBox txtEstado, TextBox txtCidade, TextBox txtBairro, TextBox txtEndereco)
        {
            txtCEP.Text = FormatCEP(txtCEP.Text);
            try
            {

                string xml = "https://viacep.com.br/ws/@/xml".Replace("@", txtCEP.Text);
                DataSet ds = new DataSet();
                ds.ReadXml(xml);
                    txtEstado.Text = ds.Tables[0].Rows[0]["uf"].ToString();
                    txtCidade.Text = ds.Tables[0].Rows[0]["localidade"].ToString();
                    txtBairro.Text = ds.Tables[0].Rows[0]["bairro"].ToString();
                    txtEndereco.Text = ds.Tables[0].Rows[0]["logradouro"].ToString();
                    txtCEP.Text = ds.Tables[0].Rows[0]["cep"].ToString();
                    txtNumeroCASA.Focus();
                    txtNumeroCASA.Select();
                
            }
            catch (Exception ex)
            {
                try
                {
                    string xml = "http://cep.republicavirtual.com.br/web_cep.php?cep=@&formato=xml".Replace("@", txtCEP.Text);
                    DataSet ds = new DataSet();
                    ds.ReadXml(xml);
                    txtEstado.Text = ds.Tables[0].Rows[0]["uf"].ToString();
                    txtCidade.Text = ds.Tables[0].Rows[0]["cidade"].ToString();
                    txtBairro.Text = ds.Tables[0].Rows[0]["bairro"].ToString();
                    txtEndereco.Text = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString()+" "+ds.Tables[0].Rows[0]["logradouro"].ToString();
                    txtCEP.Text = ds.Tables[0].Rows[0][8].ToString();
                    
                }
                catch(Exception)
                {

                }
                Mensagem("CEP unico, inserir manualmente os outros campos", "CEP Unico", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumeroCASA.Focus();
                txtNumeroCASA.Select();

            }
        }
    }
}
