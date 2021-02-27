using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChyaKvAppSvc.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class KeyVaultController : Controller
   {
      readonly string m_KvUrl;

      public KeyVaultController(IConfiguration configuration)
      {
         m_KvUrl = configuration.GetSection("KeyValutUrl").Value;
      }

      [HttpGet]
      public async Task<string> GetSecretDetailAsync(string secName)
      {
         var kvSecClient = new SecretClient(new Uri(m_KvUrl), new DefaultAzureCredential());

         var secValue = await kvSecClient.GetSecretAsync(secName);

         return secValue.Value.Value;
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult CreateSecret(string secName)
      {
         try
         {
            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }

      [HttpPut]
      public ActionResult EditSecret(string secName)
      {
         return View();
      }

      [HttpDelete]
      public ActionResult Delete(int id)
      {
         return View();
      }
   }
}
