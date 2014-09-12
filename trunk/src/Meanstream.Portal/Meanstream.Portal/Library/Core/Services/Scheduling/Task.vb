
Namespace Meanstream.Portal.Core.Services.Scheduling
    Public MustInherit Class Task
        'Implements ITask
        Implements IEquatable(Of Task)

        'Private Sub New()

        'End Sub
        Public Sub New(ByVal id As Guid, ByVal interval As Double, ByVal startupType As StartupType)
            _id = id
            m_Interval = interval
            m_StartupType = startupType
        End Sub

        Public MustOverride Sub Execute()

#Region "Properties"
        Private _id As Guid
        Public ReadOnly Property Id() As Guid
            Get
                Return _id
            End Get
        End Property

        Public Property Type() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String = ""

        Public Property Status() As Status
            Get
                Return m_Status
            End Get
            Set(ByVal value As Status)
                m_Status = value
            End Set
        End Property
        Private m_Status As Status = Status.Stopped

        Public Property StartupType() As StartupType
            Get
                Return m_StartupType
            End Get
            Set(ByVal value As StartupType)
                m_StartupType = value
            End Set
        End Property
        Private m_StartupType As StartupType = StartupType.Disabled

        Public Property LastRunTime() As DateTime
            Get
                Return m_LastRunTime
            End Get
            Set(ByVal value As DateTime)
                m_LastRunTime = value
            End Set
        End Property
        Private m_LastRunTime As DateTime

        Public Property LastRunResult() As String
            Get
                Return m_LastRunResult
            End Get
            Set(ByVal value As String)
                m_LastRunResult = value
            End Set
        End Property
        Private m_LastRunResult As String

        Public Property NextRunTime() As DateTime
            Get
                Return m_NextRunTime
            End Get
            Set(ByVal value As DateTime)
                m_NextRunTime = value
            End Set
        End Property
        Private m_NextRunTime As DateTime

        Public Property LastRunSuccessful() As Boolean
            Get
                Return m_IsLastRunSuccessful
            End Get
            Set(ByVal value As Boolean)
                m_IsLastRunSuccessful = value
            End Set
        End Property
        Private m_IsLastRunSuccessful As Boolean = False

        Public Property Interval() As Double
            Get
                Return m_Interval
            End Get
            Set(ByVal value As Double)
                m_Interval = value
            End Set
        End Property
        Private m_Interval As Double = 0
#End Region
        'Public MustOverride ReadOnly Property Id As System.Guid Implements ITask.Id
        'Public MustOverride Property Interval As Double Implements ITask.Interval       
        'Public MustOverride Property LastRunResult As String Implements ITask.LastRunResult       
        'Public MustOverride Property LastRunSuccessful As Boolean Implements ITask.LastRunSuccessful       
        'Public MustOverride Property LastRunTime As Date Implements ITask.LastRunTime
        'Public MustOverride Property Name As String Implements ITask.Name   
        'Public MustOverride Property NextRunTime As Date Implements ITask.NextRunTime
        'Public MustOverride Property StartupType As StartupType Implements ITask.StartupType    
        'Public MustOverride Property Status As Status Implements ITask.Status

        Public Overloads Function Equals(ByVal other As Task) As Boolean Implements System.IEquatable(Of Task).Equals
            If Me.Id = other.Id Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

