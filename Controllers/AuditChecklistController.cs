﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditChecklistModule.Models;
using AuditChecklistModule.Providers;
using AuditChecklistModule.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditChecklistModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditChecklistController : ControllerBase
    {
        private readonly IChecklistProvider checklistProviderobj;
        readonly log4net.ILog _log4net;
                 
        public AuditChecklistController(IChecklistProvider _checklistProviderobj)
        {
            checklistProviderobj = _checklistProviderobj;
            _log4net = log4net.LogManager.GetLogger(typeof(AuditChecklistController));
        }

        // GET: api/AuditChecklist
        [HttpGet("{auditType}")]
        public IActionResult GetQuestions(string auditType)
        {
            _log4net.Info("AuditChecklistController Http GET request called"+ nameof(AuditChecklistController));
            if (string.IsNullOrEmpty(auditType))
                return BadRequest("No Input");

            if (!auditType.Contains("Internal") && !auditType.Contains("SOX"))
                return Ok("Wrong Input");

            try
            {
                List<Questions> list = checklistProviderobj.QuestionsProvider(auditType);
                    return Ok(list);
            }
            catch(Exception e)
            {
                _log4net.Error("Exception " +e.Message+nameof(AuditChecklistController));
                return StatusCode(500);
            }
        }        
    }
}
