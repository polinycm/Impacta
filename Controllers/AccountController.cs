using EPT.Models;
using EPT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EPT.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        //TROCA A SENHA 
        [Authorize]
        public ActionResult AlterarSenha()
        {

            return View();
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NovaSenha);

                    if (result.Succeeded)
                    {
                        // Senha alterada com sucesso

                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        // Ocorreu um erro ao alterar a senha
                        ModelState.AddModelError("", "Ocorreu um erro ao alterar a senha.");
                    }
                }
                else
                {
                    // Usuário não encontrado
                    ModelState.AddModelError("", "Usuário não encontrado.");
                }
            }

            // Exiba a página de alteração de senha novamente com as mensagens de erro
            return View(model);
        }

        //

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return View(new LoginViewModel()
                {
                    ReturnUrl = returnUrl
                });
            }
            else 
            {
                if (User.IsInRole("Member") == true)
                {
                    return RedirectToAction("Index", "Home");

                }
                else if (User.IsInRole("Admin") == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Login");

            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl) ) 
                    {
                        return RedirectToAction("Index", "Home");
                    }
                   


                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login!");
            return View(loginVM);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }



        [Authorize(Roles = "Admin")]

        public IActionResult Register()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName  };
                var result = await _userManager.CreateAsync(user, registroVM.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }
            }

            return View(registroVM);
        }



    }
}
