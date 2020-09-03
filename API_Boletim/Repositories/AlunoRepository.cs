using API_Boletim.Context;
using API_Boletim.Domains;
using API_Boletim.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;

namespace API_Boletim.Repositories
{
    public class AlunoRepository : IAluno
    {
        //conectando ao banco
        BoletimContext conexao = new BoletimContext();

        //chama o objeto q pode executar os comandos do banco
        SqlCommand cmd = new SqlCommand();

        public Aluno Alterar(Aluno a)
        {
            //abre conexao
            cmd.Connection = conexao.Conectar();
            
           //colocar os parametro necesario para aplicação, podemos alterar nome, ra, idade, selecionando apenas o id
           //utilizando id como apoio
            cmd.CommandText = "UPDATE Aluno SET Nome= @nome, RA = @ra, Idade= @idade WHERE IdAluno= @id";
  
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);
            cmd.Parameters.AddWithValue("@id", a.IdAluno);

            
            cmd.ExecuteNonQuery();
            //fecha conexao
            conexao.Desconectar();

            return a;
        }

        public Aluno BuscarPorId(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();
            
            Aluno a = new Aluno();
            while (dados.Read())
            {
                a.IdAluno = Convert.ToInt32(dados.GetValue(0));
                a.Nome = dados.GetValue(1).ToString();
                a.RA = dados.GetValue(2).ToString();
                a.Idade = Convert.ToInt32(dados.GetValue(3));

            }

            conexao.Desconectar();

            return a;
        }

         public Aluno Cadastrar(Aluno a)
        {

            cmd.Connection = conexao.Conectar();

            cmd.CommandText =
                "INSERT INTO Aluno (Nome, RA, Idade) " +"VALUES" +"(@nome, @ra, @idade)";
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            cmd.ExecuteNonQuery();



            conexao.Desconectar();

            return a;
        }

        public Aluno Excluir(Aluno a)
        {
            //abre conexao
            cmd.Connection = conexao.Conectar();
            //deletar por id que é exato 
            cmd.CommandText = "DELETE FROM Aluno WHERE IdAluno= @id";
            //coloca os parametro necessarios para a aplicação
            cmd.Parameters.AddWithValue("@id", a.IdAluno);
            cmd.ExecuteNonQuery();
           
            //deletar por nome -- ideia
            // cmd.CommandText = "DELETE FROM Aluno WHERE Nome= @nome";
            //cmd.Parameters.AddWithValue("@nome", a.Nome);
            //cmd.ExecuteNonQuery();

            //fecha conexao
            conexao.Desconectar();

            return a;
        }

        public List<Aluno> LerTodos()
        {
            //abre conexao 
            cmd.Connection = conexao.Conectar();

            //consulta
            cmd.CommandText = "SELECT * FROM Aluno";

            //executa
            SqlDataReader dados = cmd.ExecuteReader();

            //lista para guardar alunos
            List<Aluno> alunos = new List<Aluno>();

            while (dados.Read())
            {
                alunos.Add(
                    new Aluno
                    {
                        IdAluno = Convert.ToInt32(dados.GetValue(0)),
                        Nome = dados.GetValue(1).ToString(),
                        RA = dados.GetValue(2).ToString(),
                        Idade = Convert.ToInt32(dados.GetValue(3)),
                    }
                    );
            }
            //laço


            //fecha conexao
            conexao.Desconectar();

          return alunos;

        }
    }
  }

