using AuditChecklistModule.Models;
using AuditChecklistModule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditChecklistModule.Providers
{
    public class ChecklistProvider:IChecklistProvider
    {
        private readonly IChecklistRepo checklistRepoObj;
        readonly log4net.ILog _log4net;
        public ChecklistProvider(IChecklistRepo _checklistRepoObj)
        {
            checklistRepoObj = _checklistRepoObj;
            _log4net = log4net.LogManager.GetLogger(typeof(ChecklistProvider));
        }
        List<Questions> listOfQuestions = new List<Questions>();

        public List<Questions> QuestionsProvider(string type)
        {
            /*if (!type.Contains("Internal") && !type.Contains("SOX"))
                return null;*/
            
            try
            {
                _log4net.Info(" Http GET request called" + nameof(ChecklistProvider));
                listOfQuestions = checklistRepoObj.GetQuestions(type);
                return listOfQuestions;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception " + e.Message + nameof(ChecklistProvider));
                return null;
            }
            
            
        }
    }
}
