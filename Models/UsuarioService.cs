using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public void Inserir(Usuario usr)
        {
            using(BibliotecaContext bc = new BibliotecaContext()){
                usr.Senha = Encriptacao(usr.Senha);
                bc.Add(usr);
                bc.SaveChanges();

            }
        }

        public void Atualizar(Usuario usr)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = bc.Usuarios.Find(usr.Id);

                usuario.Login = usr.Login;
                usuario.Senha = Encriptacao(usr.Senha);
                usuario.Email = usr.Email;
                usuario.Telefone = usr.Telefone;

                bc.SaveChanges();
            }
        }

        private string Encriptacao(string senha){
            MD5 md5Hasher = MD5.Create();

            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);

            byte[] hashBytes = md5Hasher.ComputeHash(senhaBytes);

            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            
            return hash;
        }

        public ICollection<Usuario> ListarTodos(){
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Usuario> query;

                query = bc.Usuarios;
                
                return query.OrderBy(u => u.Login).ToList();
            }
        }
        public Usuario ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public bool Logar(Usuario usr)
        {
            Console.WriteLine(usr.Login);
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                String login = null;

                try{
                    login = bc.Usuarios.Where(u => u.Senha == Encriptacao(usr.Senha)).Where(u => u.Login == usr.Login).Select(u => u.Login).First();

                }catch(Exception e){
                    Console.WriteLine(e);
                }

                if(login == usr.Login)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }

        public void Excluir(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = ObterPorId(id);
                bc.Remove(usuario);
                bc.SaveChanges();
            }
        }
    }
}