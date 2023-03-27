using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTAPP.DAL.Model;
using MTAPP.DAL.Repository;
using MTAPP.Model;

namespace MTAppWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IGenericRepository<Organization> _organizationRepository;

        public OrganizationController(IGenericRepository<Organization> organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        [HttpPost]
        public IActionResult GetOrganizations()
        {
            var orgdetails = _organizationRepository.GetAll().Select(x => ConvertToModel(x));
            return Ok(orgdetails);
        }

        [HttpPost]
        public IActionResult GetOrganizationById(int orgid)
        {
            var orgdetail = _organizationRepository.Get().Where(x => x.orgid == orgid)?.FirstOrDefault();
            return Ok(ConvertToModel(orgdetail));
        }

        [HttpPost]
        public IActionResult SaveOrganization(OrganizationModel organization)
        {
            var orgdetails = _organizationRepository.Get().Where(x => x.orgname.Equals(organization.orgname))?.SingleOrDefault();
            if (orgdetails != null && organization.orgid == 0)
            {
                throw new Exception("OrgName already exists.");
            }
            var dbOrgModel = ConvertToDBModel(organization);
            if (organization.orgid == 0)
                _organizationRepository.Insert(dbOrgModel);
            else
                _organizationRepository.Update(dbOrgModel);
            return Ok(ConvertToModel(dbOrgModel));
        }

        [HttpPost]
        public void DeleteOrganization(int orgid)
        {
            var orgdetails = _organizationRepository.Get().Where(x => x.orgid == orgid)?.FirstOrDefault();
            if (orgdetails != null)
                _organizationRepository.Delete(orgdetails);
        }

        private OrganizationModel ConvertToModel(Organization organization) =>
            new OrganizationModel()
            {
                orgid =  organization.orgid,
                orgname = organization.orgname,
                orgcode = organization.orgcode,
                isactive = organization.isactive,
                createddate = organization.createddate
            };

        private Organization ConvertToDBModel(OrganizationModel organization) =>
            new Organization()
            {
                orgid = organization.orgid,
                orgname = organization.orgname,
                orgcode = organization.orgcode,
                isactive = organization.isactive,
                createddate = organization.createddate
            };
    }
}
