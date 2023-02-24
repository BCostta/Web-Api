using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api_.Models
{
    public class PagamentosContext :DbContext
    {


        public PagamentosContext():

            base("PagamentoConnection")
        {
        }

        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cartao>().ToTable("TBcartao");
            modelBuilder.Entity<Cartao>().Property(P => P.NumeroCartao).IsRequired().HasMaxLength(16);
            modelBuilder.Entity<Cartao>().Property(P => P.Limite).IsRequired();

            modelBuilder.Entity<Fatura>().ToTable("TBfatura");
            modelBuilder.Entity<Fatura>().Property(P => P.NumeroCartao).IsRequired().HasMaxLength(16);
            modelBuilder.Entity<Fatura>().Property(P => P.Valor).IsRequired();
            modelBuilder.Entity<Fatura>().Property(P => P.NumeroPedido).IsRequired();
        }






    }
}