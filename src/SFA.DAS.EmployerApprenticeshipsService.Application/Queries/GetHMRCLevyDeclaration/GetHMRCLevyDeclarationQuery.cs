﻿using MediatR;

namespace SFA.DAS.EAS.Application.Queries.GetHMRCLevyDeclaration
{
    public class GetHMRCLevyDeclarationQuery : IAsyncRequest<GetHMRCLevyDeclarationResponse>
    {
        public string EmpRef { get; set; }
    }
}
