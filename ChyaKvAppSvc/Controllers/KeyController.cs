using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChyaKvAppSvc.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class KeyController : Controller
   {
      readonly ILogger<string> m_Logger;
      readonly string m_KvUri;

      public KeyController(ILogger<string> logger, IConfiguration config)
      {
         m_Logger = logger;
         m_KvUri = config.GetSection("KeyValutUrl").Value;
      }

      [HttpGet]
      public async Task<KeyVaultKey> GetKeyAsync(string name)
      {
         var keyClient = new KeyClient(new Uri(m_KvUri), new DefaultAzureCredential());

         var keyGet = await keyClient.GetKeyAsync(name);

         return keyGet;
      }

      [HttpPost]
      public async Task<KeyVaultKey> CreateKeyAsync(string name)
      {
         var keyClient = new KeyClient(new Uri(m_KvUri), new DefaultAzureCredential());

         var keyCreate = await keyClient.CreateKeyAsync(name, KeyType.Rsa, new CreateKeyOptions() { 
            Enabled = false,
         });

         return keyCreate;
      }

      [HttpDelete]
      public async Task<DeleteKeyOperation> DeleteKeyAsync(string name)
      {
         var keyClient = new KeyClient(new Uri(m_KvUri), new DefaultAzureCredential());

         var keyDel = await keyClient.StartDeleteKeyAsync(name);

         return keyDel;
      }
   }
}
