﻿using SFA.DAS.EmployerApprenticeshipsService.Domain;

namespace SFA.DAS.EmployerApprenticeshipsService.Web.Models
{
    public class InviteTeamMemberViewModel
    {
        public long AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}