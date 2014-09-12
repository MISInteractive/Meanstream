Imports Microsoft.VisualBasic

Namespace Meanstream.Portal.Core

    Public Class AppConstants
        Public Const APPLICATION As String = "/meanstream"
        Public Const APPLICATION_ROOT_PATH As String = "/"
        Public Const APPLICATION_VIRTUAL_PATH As String = "~/"

        Public Const PAGEID As String = "PageID"
        Public Const MODULEID As String = "ModuleID"
        Public Const ACTION As String = "Action"
        Public Const VERSIONID As String = "VersionID"
        Public Const PREVIEWID As String = "PreviewID"
        Public Const HTMLMODULEID As Integer = 109
        Public Const CUSTOMMODULEID As Integer = 137
        Public Const LOGGER As String = "LOGGER"
        Public Const EXTENSION As String = ""
        Public Const PREVIEW As String = "Preview"

        '******* ROLE CONSTANTS *******'
        Public Const ADMINISTRATOR As String = "Administrator"
        Public Const CONTENT_ADMINISTRATOR As String = "Content Administrator"
        Public Const SECURITY_ADMINISTRATOR As String = "Security Administrator"
        Public Const ECOMMERCE_ADMINISTRATOR As String = "Ecommerce Administrator"
        Public Const HOST As String = "Host"
        Public Const REGISTERED_USERS As String = "Registered Users"
        Public Const ALLUSERS As String = "All Users"

        '******* EMAIL MESSAGE CONSTANTS *******'
        Public Const MESSAGE_BODY_REGISTRATION As String = "MESSAGE_BODY_REGISTRATION"
        Public Const MESSAGE_SUBJECT_REGISTER As String = "MESSAGE_SUBJECT_REGISTER"
        Public Const MESSAGE_BODY_ADD_USER_TO_ROLE As String = "MESSAGE_BODY_ADD_USER_TO_ROLE"
        Public Const MESSAGE_SUBJECT_ADD_USER_TO_ROLE As String = "MESSAGE_SUBJECT_ADD_USER_TO_ROLE"
        Public Const MESSAGE_BODY_ADD_ROLE_TO_USER As String = "MESSAGE_BODY_ADD_ROLE_TO_USER"
        Public Const MESSAGE_SUBJECT_ADD_ROLE_TO_USER As String = "MESSAGE_SUBJECT_ADD_ROLE_TO_USER"
        Public Const MESSAGE_BODY_RESET_PASSWORD As String = "MESSAGE_BODY_RESET_PASSWORD"
        Public Const MESSAGE_SUBJECT_RESET_PASSWORD As String = "MESSAGE_SUBJECT_RESET_PASSWORD"
        Public Const MESSAGE_SUBJECT_FORGOT_PASSWORD As String = "MESSAGE_SUBJECT_FORGOT_PASSWORD"

        '******* SETTINGS CONSTANTS *********
        Public Const SMTP_FROM As String = "SMTP_FROM"
        Public Const BULKMAIL_FROM As String = "BULKMAIL_FROM"
        Public Const DOMAIN As String = "DOMAIN"
        Public Const SKINS_PATH As String = "Meanstream.SkinsPath"

        '******* SITE SEARCH CONSTANTS ******
        Public Const SITE_SEARCH_RESULTS_PAGE As String = "SITE_SEARCH_RESULTS_PAGE"
        'Public Const SITE_SEARCH_INDEX_NONPUBLIC_DOCUMENTS As String = "SITE_SEARCH_INDEX_NONPUBLIC_DOCUMENTS"
        'Public Const SITE_SEARCH_INDEX_DIRECTORY_STORE As String = "SITE_SEARCH_INDEX_DIRECTORY_STORE"
        Public Const SITE_SEARCH_INDEX_SCHEDULE As String = "SITE_SEARCH_INDEX_SCHEDULE"

        '******* EDITOR CONSTANTS ***********
        Public Const EDITOR_STYLESHEET_PATH As String = "Meanstream.CssStylesheetPath"
        Public Const EDITOR_DOCUMENTS_PATH As String = "Meanstream.DocumentsPath"
        Public Const EDITOR_FLASH_GALLERY_PATH As String = "Meanstream.FlashGalleryPath"
        Public Const EDITOR_IMAGE_GALLERY_PATH As String = "Meanstream.ImageGalleryPath"
        Public Const EDITOR_TEMPLATE_GALLERY_PATH As String = "Meanstream.TemplateGalleryPath"
        Public Const EDITOR_VIDEO_GALLERY_PATH As String = "Meanstream.VideoGalleryPath"

        '********* GOOGLE ANALYTICS ***********
        Public Const GOOGLE_ANALYTICS_TRACKING_ID As String = "GOOGLE_ANALYTICS_TRACKING_ID"
        Public Const GOOGLE_ANALYTICS_URL_PARAMETER As String = "GOOGLE_ANALYTICS_URL_PARAMETER"
        Public Const GOOGLE_ANALYTICS_SCRIPT As String = "GOOGLE_ANALYTICS_SCRIPT"
    End Class

End Namespace

