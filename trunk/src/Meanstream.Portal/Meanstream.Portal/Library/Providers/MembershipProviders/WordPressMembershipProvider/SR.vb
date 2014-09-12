﻿'------------------------------------------------------------------------------
' <copyright file="SR.cs" company="Microsoft">
' Copyright (c) Microsoft Corporation. All rights reserved.
' </copyright>
'------------------------------------------------------------------------------


Imports System
Namespace Meanstream.Portal.Providers.WordPressMembershipProvider
    Friend Module SR
        Sub New()
        End Sub
        Friend Function GetString(ByVal strString As String) As String
            Return strString
        End Function
        Friend Function GetString(ByVal strString As String, ByVal param1 As String) As String
            Return String.Format(strString, param1)
        End Function

        Friend Function GetString(ByVal strString As String, ByVal param1 As String, ByVal param2 As String) As String
            Return String.Format(strString, param1, param2)
        End Function
        Friend Function GetString(ByVal strString As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String) As String
            Return String.Format(strString, param1, param2, param3)
        End Function

        Friend Const Auth_rule_names_cant_contain_char As String = "Authorization rule names cannot contain the '{0}' character."
        Friend Const Connection_name_not_specified As String = "The attribute 'connectionStringName' is missing or empty."
        Friend Const Connection_string_not_found As String = "The connection name '{0}' was not found in the applications configuration or the connection string is empty."
        Friend Const Membership_AccountLockOut As String = "The user account has been locked out."
        Friend Const Membership_Custom_Password_Validation_Failure As String = "The custom password validation failed."
        Friend Const Membership_InvalidAnswer As String = "The password-answer supplied is invalid."
        Friend Const Membership_InvalidEmail As String = "The E-mail supplied is invalid."
        Friend Const Membership_InvalidPassword As String = "The password supplied is invalid. Passwords must conform to the password strength requirements configured for the default provider."
        Friend Const Membership_InvalidProviderUserKey As String = "The provider user key supplied is invalid. It must be of type System.Guid."
        Friend Const Membership_InvalidQuestion As String = "The password-question supplied is invalid. Note that the current provider configuration requires a valid password question and answer. As a result, a CreateUser overload that accepts question and answer parameters must also be used."
        Friend Const Membership_more_than_one_user_with_email As String = "More than one user has the specified e-mail address."
        Friend Const Membership_password_too_long As String = "The password is too long: it must not exceed 128 chars after encrypting."
        Friend Const Membership_PasswordRetrieval_not_supported As String = "This Membership Provider has not been configured to support password retrieval."
        Friend Const Membership_UserNotFound As String = "The user was not found."
        Friend Const Membership_WrongAnswer As String = "The password-answer supplied is wrong."
        Friend Const Membership_WrongPassword As String = "The password supplied is wrong."
        Friend Const PageIndex_bad As String = "The pageIndex must be greater than or equal to zero."
        Friend Const PageIndex_PageSize_bad As String = "The combination of pageIndex and pageSize cannot exceed the maximum value of System.Int32."
        Friend Const PageSize_bad As String = "The pageSize must be greater than zero."
        Friend Const Parameter_array_empty As String = "The array parameter '{0}' should not be empty."
        Friend Const Parameter_can_not_be_empty As String = "The parameter '{0}' must not be empty."
        Friend Const Parameter_can_not_contain_comma As String = "The parameter '{0}' must not contain commas."
        Friend Const Parameter_duplicate_array_element As String = "The array '{0}' should not contain duplicate values."
        Friend Const Parameter_too_long As String = "The parameter '{0}' is too long: it must not exceed {1} chars in length."
        Friend Const Password_does_not_match_regular_expression As String = "The parameter '{0}' does not match the regular expression specified in config file."
        Friend Const Password_need_more_non_alpha_numeric_chars As String = "Non alpha numeric characters in '{0}' needs to be greater than or equal to '{1}'."
        Friend Const Password_too_short As String = "The length of parameter '{0}' needs to be greater or equal to '{1}'."
        Friend Const PersonalizationProvider_ApplicationNameExceedMaxLength As String = "The ApplicationName cannot exceed character length {0}."
        Friend Const PersonalizationProvider_BadConnection As String = "The specified connectionStringName, '{0}', was not registered."
        Friend Const PersonalizationProvider_CantAccess As String = "A connection could not be made by the {0} personalization provider using the specified registration."
        Friend Const PersonalizationProvider_NoConnection As String = "The connectionStringName attribute must be specified when registering a personalization provider."
        Friend Const PersonalizationProvider_UnknownProp As String = "Invalid attribute '{0}', specified in the '{1}' personalization provider registration."
        Friend Const ProfileSqlProvider_description As String = "SQL profile provider."
        Friend Const Property_Had_Malformed_Url As String = "The '{0}' property had a malformed URL: {1}."
        Friend Const Provider_application_name_too_long As String = "The application name is too long."
        Friend Const Provider_bad_password_format As String = "Password format specified is invalid."
        Friend Const Provider_can_not_retrieve_hashed_password As String = "Configured settings are invalid: Hashed passwords cannot be retrieved. Either set the password format to different type, or set supportsPasswordRetrieval to false."
        Friend Const Provider_Error As String = "The Provider encountered an unknown error."
        Friend Const Provider_Not_Found As String = "Provider '{0}' was not found."
        Friend Const Provider_role_already_exists As String = "The role '{0}' already exists."
        Friend Const Provider_role_not_found As String = "The role '{0}' was not found."
        Friend Const Provider_Schema_Version_Not_Match As String = "The '{0}' requires a database schema compatible with schema version '{1}'. However, the current database schema is not compatible with this version. You may need to either install a compatible schema with aspnet_regsql.exe (available in the framework installation directory), or upgrade the provider to a newer version."
        Friend Const Provider_this_user_already_in_role As String = "The user '{0}' is already in role '{1}'."
        Friend Const Provider_this_user_not_found As String = "The user '{0}' was not found."
        Friend Const Provider_unknown_failure As String = "Stored procedure call failed."
        Friend Const Provider_unrecognized_attribute As String = "Attribute not recognized '{0}'"
        Friend Const Provider_user_not_found As String = "The user was not found in the database."
        Friend Const Role_is_not_empty As String = "This role cannot be deleted because there are users present in it."
        Friend Const RoleSqlProvider_description As String = "SQL role provider."
        Friend Const SiteMapProvider_cannot_remove_root_node As String = "Root node cannot be removed from the providers, use RemoveProvider(string providerName) instead."
        Friend Const SqlError_Connection_String As String = "An error occurred while attempting to initialize a System.Data.SqlClient.SqlConnection object. The value that was provided for the connection string may be wrong, or it may contain an invalid syntax."
        Friend Const SqlExpress_file_not_found_in_connection_string As String = "SQL Express filename was not found in the connection string."
        Friend Const SqlPersonalizationProvider_Description As String = "Personalization provider that stores data in a SQL Server database."
        Friend Const Value_must_be_boolean As String = "The value must be boolean (true or false) for property '{0}'."
        Friend Const Value_must_be_non_negative_integer As String = "The value must be a non-negative 32-bit integer for property '{0}'."
        Friend Const Value_must_be_positive_integer As String = "The value must be a positive 32-bit integer for property '{0}'."
        Friend Const Value_too_big As String = "The value '{0}' can not be greater than '{1}'."
        Friend Const XmlSiteMapProvider_cannot_add_node As String = "SiteMapNode {0} cannot be found in current provider, only nodes in the same provider can be added."
        Friend Const XmlSiteMapProvider_Cannot_Be_Inited_Twice As String = "XmlSiteMapProvider cannot be initialized twice."
        Friend Const XmlSiteMapProvider_cannot_find_provider As String = "Provider {0} cannot be found inside XmlSiteMapProvider {1}."
        Friend Const XmlSiteMapProvider_cannot_remove_node As String = "SiteMapNode {0} does not exist in provider {1}, it must be removed from provider {2}."
        Friend Const XmlSiteMapProvider_Description As String = "SiteMap provider which reads in .sitemap XML files."
        Friend Const XmlSiteMapProvider_Error_loading_Config_file As String = "The XML sitemap config file {0} could not be loaded. {1}"
        Friend Const XmlSiteMapProvider_FileName_already_in_use As String = "The sitemap config file {0} is already used by other nodes or providers."
        Friend Const XmlSiteMapProvider_FileName_does_not_exist As String = "The file {0} required by XmlSiteMapProvider does not exist."
        Friend Const XmlSiteMapProvider_Invalid_Extension As String = "The file {0} has an invalid extension, only .sitemap files are allowed in XmlSiteMapProvider."
        Friend Const XmlSiteMapProvider_invalid_GetRootNodeCore As String = "GetRootNode is returning null from Provider {0}, this method must return a non-empty sitemap node."
        Friend Const XmlSiteMapProvider_invalid_resource_key As String = "Resource key {0} is not valid, it must contain a valid class name and key pair. For example, $resources:'className','key'"
        Friend Const XmlSiteMapProvider_invalid_sitemapnode_returned As String = "Provider {0} must return a valid sitemap node."
        Friend Const XmlSiteMapProvider_missing_siteMapFile As String = "The {0} attribute must be specified on the XmlSiteMapProvider."
        Friend Const XmlSiteMapProvider_Multiple_Nodes_With_Identical_Key As String = "Multiple nodes with the same key '{0}' were found. XmlSiteMapProvider requires that sitemap nodes have unique keys."
        Friend Const XmlSiteMapProvider_Multiple_Nodes_With_Identical_Url As String = "Multiple nodes with the same URL '{0}' were found. XmlSiteMapProvider requires that sitemap nodes have unique URLs."
        Friend Const XmlSiteMapProvider_multiple_resource_definition As String = "Cannot have more than one resource binding on attribute '{0}'. Ensure that this attribute is not bound through an implicit expression, for example, {0}=""$resources:key""."
        Friend Const XmlSiteMapProvider_Not_Initialized As String = "XmlSiteMapProvider is not initialized. Call Initialize() method first."
        Friend Const XmlSiteMapProvider_Only_One_SiteMapNode_Required_At_Top As String = "Exactly one <siteMapNode> element is required directly inside the <siteMap> element."
        Friend Const XmlSiteMapProvider_Only_SiteMapNode_Allowed As String = "Only <siteMapNode> elements are allowed at this location."
        Friend Const XmlSiteMapProvider_resourceKey_cannot_be_empty As String = "Resource key cannot be empty."
        Friend Const XmlSiteMapProvider_Top_Element_Must_Be_SiteMap As String = "Top element must be siteMap."
        Friend Const PersonalizationProviderHelper_TrimmedEmptyString As String = "Input parameter '{0}' cannot be an empty string."
        Friend Const StringUtil_Trimmed_String_Exceed_Maximum_Length As String = "Trimmed string value '{0}' of input parameter '{1}' cannot exceed character length {2}."
        Friend Const MembershipSqlProvider_description As String = "SQL membership provider."
        Friend Const MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength As String = "The minRequiredNonalphanumericCharacters can not be greater than minRequiredPasswordLength."
        Friend Const PersonalizationProviderHelper_Empty_Collection As String = "Input parameter '{0}' cannot be an empty collection."
        Friend Const PersonalizationProviderHelper_Null_Or_Empty_String_Entries As String = "Input parameter '{0}' cannot contain null or empty string entries."
        Friend Const PersonalizationProviderHelper_CannotHaveCommaInString As String = "Input parameter '{0}' cannot have comma in string value '{1}'."
        Friend Const PersonalizationProviderHelper_Trimmed_Entry_Value_Exceed_Maximum_Length As String = "Trimmed entry value '{0}' of input parameter '{1}' cannot exceed character length {2}."
        Friend Const PersonalizationProviderHelper_More_Than_One_Path As String = "Input parameter '{0}' cannot contain more than one entry when '{1}' contains some entries."
        Friend Const PersonalizationProviderHelper_Negative_Integer As String = "The input parameter cannot be negative."
        Friend Const PersonalizationAdmin_UnexpectedPersonalizationProviderReturnValue As String = "The negative value '{0}' is returned when calling provider's '{1}' method. The method should return non-negative integer."
        Friend Const PersonalizationProviderHelper_Null_Entries As String = "Input parameter '{0}' cannot contain null entries."
        Friend Const PersonalizationProviderHelper_Invalid_Less_Than_Parameter As String = "Input parameter '{0}' must be greater than or equal to {1}."
        Friend Const PersonalizationProviderHelper_No_Usernames_Set_In_Shared_Scope As String = "Input parameter '{0}' cannot be provided when '{1}' is set to '{2}'."
        Friend Const Provider_this_user_already_not_in_role As String = "The user '{0}' is already not in role '{1}'."
        Friend Const Not_configured_to_support_password_resets As String = "This provider is not configured to allow password resets. To enable password reset, set enablePasswordReset to ""true"" in the configuration file."
        Friend Const Parameter_collection_empty As String = "The collection parameter '{0}' should not be empty."
        Friend Const Provider_can_not_decode_hashed_password As String = "Hashed passwords cannot be decoded."
        Friend Const DbFileName_can_not_contain_invalid_chars As String = "The database filename can not contain the following 3 characters: [ (open square brace), ] (close square brace) and ' (single quote)"
        Friend Const SQL_Services_Error_Deleting_Session_Job As String = "The attempt to remove the Session State expired sessions job from msdb did not succeed. This can occur either because the job no longer exists, or because the job was originally created with a different user account than the account that is currently performing the uninstall. You will need to manually delete the Session State expired sessions job if it still exists."
        Friend Const SQL_Services_Error_Executing_Command As String = "An error occurred during the execution of the SQL file '{0}'. The SQL error number is {1} and the SqlException message is: {2}"
        Friend Const SQL_Services_Invalid_Feature As String = "An invalid feature is requested."
        Friend Const SQL_Services_Database_Empty_Or_Space_Only_Arg As String = "The database name cannot be empty or contain only white space characters."
        Friend Const SQL_Services_Database_contains_invalid_chars As String = "The custom database name cannot contain the following three characters: single quotation mark ('), left bracket ([) or right bracket (])."
        Friend Const SQL_Services_Error_Cant_Uninstall_Nonexisting_Database As String = "Cannot uninstall the specified feature(s) because the SQL database '{0}' does not exist."
        Friend Const SQL_Services_Error_Cant_Uninstall_Nonempty_Table As String = "Cannot uninstall the specified feature(s) because the SQL table '{0}' in the database '{1}' is not empty. You must first remove all rows from the table."
        Friend Const SQL_Services_Error_missing_custom_database As String = "The database name cannot be null or empty if the session state type is SessionStateType.Custom."
        Friend Const SQL_Services_Error_Cant_use_custom_database As String = "You cannot specify the database name because it is allowed only if the session state type is SessionStateType.Custom."
        Friend Const SQL_Services_Cant_connect_sql_database As String = "Unable to connect to SQL Server database."
        Friend Const Error_parsing_sql_partition_resolver_string As String = "Error parsing the SQL connection string returned by an instance of the IPartitionResolver type '{0}': {1}"
        Friend Const Error_parsing_session_sqlConnectionString As String = "Error parsing <sessionState> sqlConnectionString attribute: {0}"
        Friend Const No_database_allowed_in_sqlConnectionString As String = "The sqlConnectionString attribute or the connection string it refers to cannot contain the connection options 'Database', 'Initial Catalog' or 'AttachDbFileName'. In order to allow this, allowCustomSqlDatabase attribute must be set to true and the application needs to be granted unrestricted SqlClientPermission. Please check with your administrator if the application does not have this permission."
        Friend Const No_database_allowed_in_sql_partition_resolver_string As String = "The SQL connection string (server='{1}', database='{2}') returned by an instance of the IPartitionResolver type '{0}' cannot contain the connection options 'Database', 'Initial Catalog' or 'AttachDbFileName'. In order to allow this, allowCustomSqlDatabase attribute must be set to true and the application needs to be granted unrestricted SqlClientPermission. Please check with your administrator if the application does not have this permission."
        Friend Const Cant_connect_sql_session_database As String = "Unable to connect to SQL Server session database."
        Friend Const Cant_connect_sql_session_database_partition_resolver As String = "Unable to connect to SQL Server session database. The connection string (server='{1}', database='{2}') was returned by an instance of the IPartitionResolver type '{0}'."
        Friend Const Login_failed_sql_session_database As String = "Failed to login to session state SQL server for user '{0}'."
        Friend Const Need_v2_SQL_Server As String = "Unable to use SQL Server because ASP.NET version 2.0 Session State is not installed on the SQL server. Please install ASP.NET Session State SQL Server version 2.0 or above."
        Friend Const Need_v2_SQL_Server_partition_resolver As String = "Unable to use SQL Server because ASP.NET version 2.0 Session State is not installed on the SQL server. Please install ASP.NET Session State SQL Server version 2.0 or above. The connection string (server='{1}', database='{2}') was returned by an instance of the IPartitionResolver type '{0}'."
        Friend Const Invalid_session_state As String = "The session state information is invalid and might be corrupted."

        Friend Const Missing_required_attribute As String = "The '{0}' attribute must be specified on the '{1}' tag."
        Friend Const Invalid_boolean_attribute As String = "The '{0}' attribute must be set to 'true' or 'false'."
        Friend Const Empty_attribute As String = "The '{0}' attribute cannot be an empty string."
        Friend Const Config_base_unrecognized_attribute As String = "Unrecognized attribute '{0}'. Note that attribute names are case-sensitive."
        Friend Const Config_base_no_child_nodes As String = "Child nodes are not allowed."
        Friend Const Unexpected_provider_attribute As String = "The attribute '{0}' is unexpected in the configuration of the '{1}' provider."
        Friend Const Only_one_connection_string_allowed As String = "SqlWebEventProvider: Specify either a connectionString or connectionStringName, not both."
        Friend Const Cannot_use_integrated_security As String = "SqlWebEventProvider: connectionString can only contain connection strings that use Sql Server authentication. Trusted Connection security is not supported."
        Friend Const Must_specify_connection_string_or_name As String = "SqlWebEventProvider: Either a connectionString or connectionStringName must be specified."
        Friend Const Invalid_max_event_details_length As String = "The value '{1}' specified for the maxEventDetailsLength attribute of the '{0}' provider is invalid. It should be between 0 and 1073741823."
        Friend Const Sql_webevent_provider_events_dropped As String = "{0} events were discarded since last notification was made at {1} because the event buffer capacity was exceeded."
        Friend Const Invalid_provider_positive_attributes As String = "The attribute '{0}' is invalid in the configuration of the '{1}' provider. The attribute must be set to a non-negative integer."
    End Module
End Namespace
