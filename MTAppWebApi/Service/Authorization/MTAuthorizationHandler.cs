using Microsoft.AspNetCore.Authorization;
using MTAPP.DAL;
using MTAPP.DAL.Model;
using MTAPP.DAL.Repository;

namespace MTAppWebApi.Service.Authorization
{
    public class MTAuthorizationHandler : AuthorizationHandler<MTAuthorizationRequirement>
    {
        private readonly IGenericRepository<AppRoleRoute> _routeRoleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MTAuthorizationHandler(IGenericRepository<AppRoleRoute> routeRoleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _routeRoleRepository = routeRoleRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MTAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                bool isRouteAllowed = false;
                var roleid = context.User.Claims.First(x => x.Type == "roleid").Value;
                if (!string.IsNullOrEmpty(roleid))
                {
                    var routes = _routeRoleRepository.Get().Where(x => x.roleid == short.Parse(roleid)).Select(x => x.route).AsEnumerable();
                    var routepath = _httpContextAccessor.HttpContext.Request.Path.ToString().ToLower();
                    if (routes.Any(x => x.routepath.Equals("*") || routepath.Contains(x.routepath)))
                        isRouteAllowed = true;
                    else
                        isRouteAllowed = false;
                    if (routes.Any(x => x.isexclude && routepath.Contains(x.routepath)))
                        isRouteAllowed = false;
                    if (isRouteAllowed)
                        context.Succeed(requirement);
                }                
            }
            return Task.CompletedTask;
        }
    }
}
