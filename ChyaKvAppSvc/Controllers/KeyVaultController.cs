﻿using Azure.Identity;
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
      public async Task<KeyVaultSecret> GetSecretDetailAsync(string secName)
      {
         var kvSecClient = new SecretClient(new Uri(m_KvUrl), new DefaultAzureCredential());
         var getValue = await kvSecClient.GetSecretAsync(secName);
         return getValue;
      }

      [HttpPost]
      public async Task<KeyVaultSecret> CreateSecretAsync(string secName)
      {
         var kvSecClient = new SecretClient(new Uri(m_KvUrl), new DefaultAzureCredential());
         var setSec = await kvSecClient.SetSecretAsync(new KeyVaultSecret(secName, "New Default Secret"));
         return setSec;
      }


      [HttpDelete]
      public DeleteSecretOperation DeleteSecret(string secName)
      {
         var kvSecClient = new SecretClient(new Uri(m_KvUrl), new DefaultAzureCredential());
         var delSec =  kvSecClient.StartDeleteSecret(secName);
         return delSec;
      }
   }
}
