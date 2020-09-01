using API_Boletim.Domains;
using System.Collections.Generic;

namespace API_Boletim.Interfaces
{
    interface IAluno
    {
        Aluno Cadastrar(Aluno a);
        
        List<Aluno> LerTodos();

        Aluno BuscarPorId(int id);

        Aluno Alterar(Aluno a);

        Aluno Ecluir(Aluno a);




    }
}
