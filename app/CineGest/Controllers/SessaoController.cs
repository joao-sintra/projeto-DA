﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CineGest.Controllers {
    internal class SessaoController {
        public static List<Sessao> GetSessoes() {

            using (var db = new CinegestContext()) {
                return db.Sessoes.Include("FilmeID").Include("SalaID").ToList();

            }
        }
        public static List<Sessao> GetSessoesHoje() {

            using (var db = new CinegestContext()) {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                return db.Sessoes.Include("FilmeID").Include("SalaID").Where(s => s.DataHora >= today && s.DataHora < tomorrow).ToList();

            }
        }

        public static List<string> GetOnlyFilmesAtivos() {

            using (var db = new CinegestContext()) {

                List<string> filmes = db.Filmes
                    .Where(filme => filme.Activo.Equals(true))
                    .Select(filme => filme.Nome).ToList();

                return filmes;
            }
        }

        public static void AdicionarSessao(string Filme, string Sala, DateTime DataHora, float Preco) {

            using (var db = new CinegestContext()) {
                var filme = db.Filmes.FirstOrDefault(x => x.Nome == Filme);
                var sala = db.Salas.FirstOrDefault(x => x.Nome == Sala);

                var sessao = new Sessao { DataHora = DataHora, Preco = Preco, FilmeID = filme, SalaID = sala };
                DateTime currentDateTime = DateTime.Now;

                List<Sessao> list = db.Sessoes
                    .Where(s => s.DataHora == DataHora)
                    .Where(s => s.SalaID.Id == sala.Id)
                    .ToList();
               

                if (list.Count > 0) {
                    MessageBox.Show("Já existe uma sessão nesta sala e com esta data e hora. \n(" + "Sala: " + sala + " | " + "DataHora: " + DataHora + ") já existe!");
                    return;
                } else if (DataHora < currentDateTime) {
                    MessageBox.Show("Não pode adicionar uma sessao com a data anterior à atual!\n" + DataHora);
                    return;
                }

                db.Sessoes.Add(sessao);
                db.SaveChanges();


            }
        }
        public static void AlterarSessao(int ID, string Filme, string Sala, DateTime DataHora, float Preco) {

            using (var db = new CinegestContext()) {

                Sessao sessao = db.Sessoes.FirstOrDefault(Sessao => Sessao.Id == ID);

                DateTime currentDateTime = DateTime.Now;

                var filme = db.Filmes.FirstOrDefault(x => x.Nome == Filme);
                var sala = db.Salas.FirstOrDefault(x => x.Nome == Sala);


                if (DataHora < currentDateTime) {
                    MessageBox.Show("Não pode alterar uma sessao com a data anterior à atual!!\n" + DataHora);
                    return;
                }

                sessao.FilmeID = filme;
                sessao.SalaID = sala;
                sessao.DataHora = DataHora;
                sessao.Preco = Preco;

                db.SaveChanges();
            }
        }
        public static void RemoverSessao(int ID) {
            using (var db = new CinegestContext()) {
                Sessao ses = db.Sessoes.FirstOrDefault(s => s.Id == ID);

                db.Sessoes.Remove(ses);

                db.SaveChanges();
            }
        }
    }

}
