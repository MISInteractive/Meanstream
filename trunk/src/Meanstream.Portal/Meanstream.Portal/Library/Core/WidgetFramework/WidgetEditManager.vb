
Namespace Meanstream.Portal.Core.WidgetFramework
    Friend Class WidgetEditManager
        Inherits WidgetVersionManager

        Private _entity As WidgetEdit

        Sub New(ByRef attributeEntity As WidgetEdit)
            MyBase.New(attributeEntity)
            _entity = attributeEntity
        End Sub

        
    End Class
End Namespace

