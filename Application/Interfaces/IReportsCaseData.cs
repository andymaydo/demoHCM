using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IReportsCaseData
    {
        Task<List<CaseModel>> CaseDetail(CaseFilterBase caseFilter, int? ContactID);

        Task<List<CaseModel>> CaseByStatus(CaseFilterBase caseFilter, int? ContactID);

        Task<List<CaseModel>> CaseByResult(CaseFilterBase caseFilter, int? ContactID);
        Task<List<CaseModel>> AliasCount(CaseFilterBase caseFilter, int? ContactID);

        Task<List<CaseModel>> AliasDetail(CaseFilterBase caseFilter, int? ContactID);
    }
}