using Cadasvan01.Enums;
using Cadasvan01.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace Cadasvan01.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Funcao> _roleManager;

        const string _admin = "Admin";
        const string _motorista = "Motorista";

        public SeedUserRoleInitial(UserManager<Usuario> userManager, RoleManager<Funcao> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Aluno").Result) 
            {
                var role = new Funcao
                {
                    Name = "Aluno",
                    NormalizedName = "ALUNO"
                };
                var roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync(_motorista).Result)
            {
                var role = new Funcao
                {
                    Name = _motorista,
                    NormalizedName = _motorista.ToUpper()
                };
                var roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync(_admin).Result)
            {
                var role = new Funcao
                {
                    Name = _admin,
                    NormalizedName = _admin.ToUpper()
                };
                
                var roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers(string email, string senha, string role)
        {
            if (_userManager.FindByEmailAsync(email).Result == null)
            {
                var tipo = UsuarioEnum.Aluno;
                switch (role)
                {
                    case _motorista:
                        tipo = UsuarioEnum.Motorista;
                        break;
                    case _admin:
                        tipo = UsuarioEnum.Admin;
                        break;
                }

                var userAdmin = new Usuario
                {
                    NomeCompleto = email,
                    Tipo = tipo,
                    UserName = email,
                    Email = email,
                    NormalizedUserName = email.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    MotoristaId = null,
                    CidadeId = 1,
                    SecurityStamp = Guid.NewGuid().ToString()
                };


                IdentityResult result = _userManager.CreateAsync(userAdmin, senha).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(userAdmin, role).Wait();
                }
            }
            
        }

        public void SeedUsers()
        {
            SeedUsers("admin@email.com", senha: "Numpy@$#2!", role: _admin);
            SeedUsers("motorista@email.com", senha: "Numpy@$#2!", role: _motorista);
            SeedUsers("aluno@email.com", "Numpy@$#2!", "ALUNO");
        }
    }
}
