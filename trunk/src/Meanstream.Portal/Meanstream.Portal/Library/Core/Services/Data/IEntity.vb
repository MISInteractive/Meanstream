﻿
Namespace Meanstream.Portal.Core.Services.Data
    Public Interface IEntity
        Property Id As Guid
        Sub Fill(ByVal reader As IDataReader)
    End Interface
End Namespace


