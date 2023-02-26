using Api_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api_.Data;
using System.Web;

namespace Api_.Controllers
{
    public class PagamentosController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Fatura> GetFaturas()
        {
            return PagamentoDao.ListarFaturas() ;
        }

        public HttpResponseMessage PostFatura(Fatura fatura)
        {
            string mensagem = "";
            StatusPagamento status = PagamentoDao.IncluirFatura(fatura);
            if(status != StatusPagamento.PEDIDO_OK)
            {
                
                switch (status)
                {
                    case StatusPagamento.CARTAO_INEXISTENTE:
                        mensagem = "O cartão não existe";
                        break;

                    case StatusPagamento.PEDIDO_JA_PAGO:
                        mensagem = "Esse pedido já está pago!";
                        break;

                    case StatusPagamento.SALDO_INDISPONIVEL:
                        mensagem = "Sem saldo!";
                        break;
                }

                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no Servidor!!"),
                    ReasonPhrase = mensagem
            };

                throw new HttpResponseException(erro);
            }
            else
            {
                var resposta = Request.CreateResponse<Fatura>(HttpStatusCode.Created, fatura);
                string uri = Url.Link("DefaultApi", new { id = fatura.Id });
                return resposta;
            }
        }

    }
}