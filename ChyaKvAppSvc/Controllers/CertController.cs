using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Certificates;
using Azure.Identity;
using System.Threading;

namespace ChyaKvAppSvc.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class CertController : Controller
   {
      readonly ILogger<string> m_Logger;
      readonly string m_KvUri;

      public CertController(ILogger<string> logger, IConfiguration config)
      {
         m_Logger = logger;
         m_KvUri = config.GetSection("KeyValutUrl").Value;
      }

      [HttpGet]
      public async Task<KeyVaultCertificateWithPolicy> GetCertification(string certName)
      {
         m_Logger.LogInformation("Get cert: " + certName);

         var certClient = new CertificateClient(new Uri(m_KvUri), new DefaultAzureCredential());
         var getCert = await certClient.GetCertificateAsync(certName);
         return getCert.Value;
      }

      [HttpPost]
      public async void CreateCertification(string certName, string issuer, string subject)
      {
         m_Logger.LogInformation($"Try to create Cert {certName}, issuer: {issuer}, subject: {subject}");

         var certClient = new CertificateClient(new Uri(m_KvUri), new DefaultAzureCredential());
         var certPolicy = new CertificatePolicy(issuer, subject);

         var cancelToken = new CancellationTokenSource().Token;
         //Long running task, async void
         var createCert = await certClient.StartCreateCertificateAsync(certName, certPolicy, false, null, cancelToken);

      }

      [HttpDelete]
      public async Task<KeyVaultCertificateWithPolicy> DeleteCertification(string certName)
      {
         m_Logger.LogInformation($"Try to delete Cert {certName}");

         var certClient = new CertificateClient(new Uri(m_KvUri), new DefaultAzureCredential());
         var createCert = await certClient.StartDeleteCertificateAsync(certName);
         return createCert.Value;
      }
   }
}
