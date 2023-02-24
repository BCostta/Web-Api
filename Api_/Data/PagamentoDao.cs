using Api_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Api_.Data
{
    public class PagamentoDao
    {

        public static IEnumerable<Fatura> ListarFaturas()
        {

            using (var pagamentocontext = new PagamentosContext())
            {
                return pagamentocontext.Faturas.ToList();
            }

        }


        public static StatusPagamento IncluirFatura(Fatura fatura)
        {
            using (var pagamentocontext = new PagamentosContext())
            {

                var cartao = pagamentocontext.Cartoes.FirstOrDefault(p => p.NumeroCartao.Equals(fatura.NumeroCartao));
                if(cartao == null)
                {
                    return StatusPagamento.CARTAO_INEXISTENTE;
                }

                var faturacartao = pagamentocontext.Faturas.FirstOrDefault(p => p.NumeroPedido.Equals(fatura.NumeroPedido));
                if(faturacartao != null)
                {
                    return StatusPagamento.PEDIDO_JA_PAGO;
                }

                double total = fatura.Valor;
                var pagamento = pagamentocontext.Faturas.Where(s => s.NumeroCartao.Equals(fatura.NumeroCartao));

                if(pagamento.Count() > 0)
                {
                    total += pagamento.Sum(s => s.Valor);
                }

                if (total > cartao.Limite)
                {
                    return StatusPagamento.SALDO_INDISPONIVEL;
                }

                return StatusPagamento.PEDIDO_OK ;

            }

            
        }



    }
}