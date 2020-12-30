using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario novoUsuario)
        {
            UsuarioService usuarioService = new UsuarioService();

            if(novoUsuario.Id == 0)
            {
                usuarioService.Inserir(novoUsuario);
            }else
            {
                usuarioService.Atualizar(novoUsuario);
            }   
            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
            UsuarioService usuarioService = new UsuarioService();
            return View(usuarioService.ListarTodos());
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            UsuarioService usuarioService= new UsuarioService();
            Usuario usr = usuarioService.ObterPorId(id);
            return View(usr);
        }

        public IActionResult Excluir(int id)
        {
            Autenticacao.CheckLogin(this);
            Console.WriteLine(id);
            UsuarioService usuarioService = new UsuarioService();
            usuarioService.Excluir(id);
            return View();
        }
    }
}