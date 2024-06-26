﻿using HD_Support_API.Components;
using HD_Support_API.Models;
using HD_Support_API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HD_Support_API.Repositorios
{
    public class EmprestimoRepositorio : IEmprestimoRepositorio
    {
        private readonly BancoContext _contexto = new BancoContext();

        /*usando sql "normal"
        public EmprestimoRepositorio(BancoContext contexto)
        {
            _contexto = contexto;
        }*/
        public async Task<Emprestimos> AdicionarEmprestimo(string idPatrimonio, string email)
        {
            var verificarEquipamento = _contexto.Emprestimo.FirstOrDefault(x => x.Equipamentos.IdPatrimonio == idPatrimonio);
            var verificarFuncionario = _contexto.Emprestimo.FirstOrDefault(x => x.Usuario.Email == email && x.Equipamentos.Tipo == "Desktop" || x.Usuario.Email == email && x.Equipamentos.Tipo == "Notebook");
            if(verificarEquipamento == null && verificarFuncionario == null)
            {
                Emprestimos emprestimo = new Emprestimos();
                emprestimo.Equipamentos = _contexto.Equipamento.FirstOrDefault(x => x.IdPatrimonio == idPatrimonio);
                emprestimo.Usuario = _contexto.Usuarios.FirstOrDefault(x => x.Email == email);
                if(emprestimo.Usuario== null)
                {
                    throw new Exception("Nenhum usuário encontrado com esse email.");
                }
                emprestimo.EquipamentosId = emprestimo.Equipamentos.Id;
                emprestimo.UsuarioId = emprestimo.Usuario.Id;
                emprestimo.Equipamentos.DtEmeprestimoInicio = DateTime.UtcNow;

                await _contexto.Emprestimo.AddAsync(emprestimo);
                await _contexto.SaveChangesAsync();
                return emprestimo;
            }
            throw new Exception("Equipamento em emprestimo ou funcionario já possui um emprestimo.");
           
            
        }

        public async Task<Emprestimos> AtualizarEmprestimo(Emprestimos emprestimo, int id)
        {
            var busca = await BuscarEmprestimosPorID(id);
            var verificarEquipamento = _contexto.Emprestimo.FirstOrDefault(x => x.Equipamentos.IdPatrimonio == emprestimo.Equipamentos.IdPatrimonio);
            var verificarFuncionario = _contexto.Emprestimo.FirstOrDefault(x => x.Usuario.Email == emprestimo.Usuario.Email);
            if (busca == null)
            {
                throw new Exception($"Equipamento de Id:{emprestimo.Id} não encontrado na base de dados.");
            }
            if(verificarEquipamento == null && verificarFuncionario == null)
            {
                var equipamento = await _contexto.Equipamento.FirstOrDefaultAsync(x => x.IdPatrimonio == Convert.ToString(emprestimo.EquipamentosId));
                busca.EquipamentosId = equipamento.Id;
                busca.UsuarioId = emprestimo.UsuarioId;

                _contexto.Emprestimo.Update(busca);
                await _contexto.SaveChangesAsync();

                return busca;
            }
            if(verificarEquipamento != null || verificarFuncionario != null)
            {
                if (verificarEquipamento != null && verificarFuncionario == null)
                {
                    throw new Exception($"Não foi possivel atualizar o emprestimo, pois o equipamento com patrimônio:{emprestimo.Equipamentos.IdPatrimonio} possui um emprestimo em andamento");
                }
                if (verificarFuncionario != null && verificarEquipamento == null)
                {
                    throw new Exception($"Não foi possivel atualizar o emprestimo, pois o funcionario de E-mail:{emprestimo.Usuario.Email} possui um emprestimo cadastrado.");
                }
                
            }
            throw new Exception($"Não foi possivel atualizar o emprestimo");
        }

        public async Task<Emprestimos> BuscarEmprestimos(string idPatrimonio, string email)
        {
            var emprestimo = _contexto.Emprestimo.FirstOrDefault(
                x => x.Equipamentos.IdPatrimonio == idPatrimonio ||
                x.Usuario.Email == email);
            return emprestimo;
        }

        public async Task<Emprestimos> BuscarEmprestimosPorID(int id)
        {
            return _contexto.Emprestimo.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> ExcluirEmprestimo(int id)
        {
            var buscar = await BuscarEmprestimosPorID(id);

            if (buscar == null)
            {
                throw new Exception($"Emprestimo de Id:{id} não encontrado na base de dados.");
            }
            _contexto.Remove(buscar);
            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<List<Emprestimos>> ListarEmprestimos()
        {
            var lista =  await _contexto.Emprestimo.ToListAsync();
            if(lista == null)
            {
                throw new Exception("Ainda não temos nenhum emprestimo registrado");
            }
            for(var i = 0; i < lista.Count;i++)
            {
                var emp = lista[i];
                var equipamento = await _contexto.Equipamento.FindAsync(emp.EquipamentosId);
                var usuario = await _contexto.Usuarios.FindAsync(emp.UsuarioId);
                lista[i].Equipamentos = equipamento;
                lista[i].Usuario = usuario;
            }

            return await _contexto.Emprestimo.ToListAsync();
        }
    }
}
