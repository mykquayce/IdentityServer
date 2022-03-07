```powershell
curl --cacert ${env:userprofile}\.aspnet\https\ca.crt --ssl-no-revoke --url https://identityserver:46274/.well-known/openid-configuration
```
```json
{
  "issuer": "https://identityserver:46274",
  "authorization_endpoint": "https://identityserver:46274/connect/authorize",
  "token_endpoint": "https://identityserver:46274/connect/token",
  "userinfo_endpoint": "https://identityserver:46274/connect/userinfo",
  "end_session_endpoint": "https://identityserver:46274/connect/endsession",
  "check_session_iframe": "https://identityserver:46274/connect/checksession",
  "revocation_endpoint": "https://identityserver:46274/connect/revocation",
  "introspection_endpoint": "https://identityserver:46274/connect/introspect",
  "device_authorization_endpoint": "https://identityserver:46274/connect/deviceauthorization",
  "frontchannel_logout_supported": true,
  "frontchannel_logout_session_supported": true,
  "backchannel_logout_supported": true,
  "backchannel_logout_session_supported": true,
  "scopes_supported": [ "api1", "offline_access" ],
  "claims_supported": [],
  "grant_types_supported": [ "authorization_code", "client_credentials", "refresh_token", "implicit", "urn:ietf:params:oauth:grant-type:device_code" ],
  "response_types_supported": [ "code", "token", "id_token", "id_token token", "code id_token", "code token", "code id_token token" ],
  "response_modes_supported": [ "form_post", "query", "fragment" ],
  "token_endpoint_auth_methods_supported": [ "client_secret_basic", "client_secret_post" ],
  "subject_types_supported": [ "public" ],
  "code_challenge_methods_supported": [ "plain", "S256" ],
  "request_parameter_supported": true
}
```
