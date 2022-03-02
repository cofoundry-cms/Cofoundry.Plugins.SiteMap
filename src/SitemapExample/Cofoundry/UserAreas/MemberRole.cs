using Cofoundry.Domain;

namespace SitemapExample.Cofoundry.UserAreas
{
    public class MemberRole : IRoleDefinition
    {
        public const string MemberRoleCode = "MEM";

        public string Title { get { return "Member"; } }

        public string RoleCode { get { return MemberRoleCode; } }

        public string UserAreaCode { get { return MemberUserArea.Code; } }

        public void ConfigurePermissions(IPermissionSetBuilder builder)
        {
            builder.ApplyAnonymousRoleConfiguration();
        }
    }
}
