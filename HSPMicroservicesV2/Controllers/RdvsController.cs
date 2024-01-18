using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using HSPMicroservicesV2.ApiServices;
using HSPMicroservicesV2.Models;

namespace HSPMicroservicesV2.Controllers
{
    [Authorize]
    public class RdvsController : Controller
    {
        private readonly IRdvApiService _rdvApiService;

        public RdvsController(IRdvApiService rdvApiService)
        {
            _rdvApiService = rdvApiService ?? throw new ArgumentNullException(nameof(rdvApiService));
        }

        // Only Admin
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> OnlyAdmin()
        {
            var userInfo = await _rdvApiService.GetUserInfo();

            return View(userInfo);
        }

        // GET: Rdvs
        public async Task<IActionResult> Index()
        {
            // Section 10
            //LogTokenAndClaims();
            await LogTokenAndClaims();

            return View(await _rdvApiService.GetRdvs());
        }

        // Log Token and Claims
        public async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

        // Logout 
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

        // GET: Rdvs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }

        // GET: Rdvs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rdvs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, NomDuPatient, ObjetConsultation, JourRendezVous")] Rdv rdv)
        {
            return View();
        }

        // GET: Rdvs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        // POST: Rdvs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, NomDuPatient, ObjetConsultation, JourRendezVous")] Rdv rdv)
        {
            return View();
        }

        // GET: Rdvs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }

        // POST: Rdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return View();
        }

        private bool RdvExists(int id)
        {
            return true;
        }
    }
}
