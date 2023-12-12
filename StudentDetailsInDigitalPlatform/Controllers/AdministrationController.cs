using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentDetailsInDigitalPlatform.ViewModels;

namespace StudentDetailsInDigitalPlatform.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                            UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
           if(ModelState.IsValid)
            {
                var role = new IdentityRole { Name = model.RoleName };
                if (role != null)
                {    
                    await roleManager.CreateAsync(role);
                    return RedirectToAction("ListRole");
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }



        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            EditRoleViewModel er = new EditRoleViewModel();
            if (role != null)
            {
                er.Id = role.Id;
                er.RoleName = role.Name;

                foreach (var user in  userManager.Users)
                {
                    if(user != null)
                    {
                        ManageUserViewModel mu = new ManageUserViewModel();
                        mu.UserId = user.Id;
                        mu.UserName = user.UserName;
                        if (await userManager.IsInRoleAsync(user, role.Name))
                        {
                            mu.IsSelected= true;
                        }
                        else
                        {
                            mu.IsSelected= false;
                        }
                        er.Users.Add(mu);
                    }
                   
                } 
            }
            return View(er);
        }



        [HttpPost]
        public async Task<IActionResult> EditRole(string id ,EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    role.Name = model.RoleName;
                    await roleManager.UpdateAsync(role);
                    foreach (var user in model.Users)
                    {
                        if (user != null){
                            if (user.IsSelected)
                            {
                                var u = await userManager.FindByIdAsync(user.UserId);
                               await  userManager.AddToRoleAsync(u,role.Name);

                            }
                            else
                            {
                                var u = await userManager.FindByIdAsync(user.UserId);
                                await userManager.RemoveFromRoleAsync(u, role.Name);
                            }
                        }
                    }
                    return RedirectToAction("ListRole");
                }
                else
                {
                    ViewBag.ErrorTitle = $"No Role with id {id}";
                    return View("Error");
                }
            }
            return View(model);
        }




        
    }
}
