namespace Flowra.Backend.Application.SystemMessages
{
    public static class ResponseMessages
    {
        public static class Common
        {
            public const string Error = "Common_Error"; // Beklenmedik bir hata oluştu.
            public const string ValidationFailed = "Common_ValidationFailed"; // Girdiğiniz verilerde doğrulama hatası var.
            public const string Unauthorized = "Common_Unauthorized"; // Bu işlem için oturum açmanız gerekiyor.
            public const string Forbidden = "Common_Forbidden"; // Bu işlemi yapmaya yetkiniz yok.
            public const string NotFound = "Common_NotFound"; // İstenen kaynak bulunamadı.
            public const string OperationSuccess = "Common_OperationSuccess"; // İşlem başarıyla tamamlandı.
            public const string OperationFailed = "Common_OperationFailed"; // İşlem gerçekleştirilemedi.
            public const string MaintenanceMode = "Common_MaintenanceMode"; // Sistem şu anda bakım modunda.
            public const string TooManyRequests = "Common_TooManyRequests"; // Çok fazla istek gönderdiniz, lütfen bekleyin.
        }

        public static class User
        {
            // GET Operations
            public const string GetById_NotFound = "User_GetById_NotFound"; // GetById metodunda ID bulunamadı.
            public const string GetByEmail_NotFound = "User_GetByEmail_NotFound"; // GetByEmail metodunda mail bulunamadı.

            // CREATE Operations
            public const string Create_EmailAlreadyExists = "User_Create_EmailAlreadyExists"; // Create işleminde email çakışması.
            public const string Create_Restored = "User_Create_Restored"; // Silinmişti, geri getirildi.
            public const string Create_UserNameTaken = "User_Create_UserNameTaken"; // Create işleminde kullanıcı adı çakışması.
            public const string Create_IdentityFailed = "User_Create_IdentityFailed"; // UserManager.CreateAsync başarısız oldu.
            public const string Create_PasswordAssignFailed = "User_Create_PasswordAssignFailed"; // Kullanıcı oluştu ama şifre atanamadı.
            public const string Create_MailSendFailed = "User_Create_MailSendFailed"; // Kullanıcı oluştu ama hoşgeldin maili atılamadı.

            // UPDATE Operations
            public const string Update_NotFound = "User_Update_NotFound"; // Update edilecek kullanıcı bulunamadı.
            public const string Update_IdentityFailed = "User_Update_IdentityFailed"; // UserManager.UpdateAsync başarısız oldu.
            public const string Update_EmailAlreadyExists = "User_Update_EmailAlreadyExists"; //  Update sırasında email çakışması.
            public const string Update_UserNameTaken = "User_Update_UserNameTaken"; //  Update sırasında username çakışması.

            // DELETE Operations
            public const string Delete_NotFound = "User_Delete_NotFound"; // Silinecek kullanıcı bulunamadı.
            public const string Delete_IdentityFailed = "User_Delete_IdentityFailed"; // Silme (Pasife çekme) işlemi Identity tarafında başarısız.

            // LOCK/UNLOCK Operations
            public const string Lock_NotFound = "User_Lock_NotFound"; // Kilitlenecek kullanıcı bulunamadı.
            public const string Lock_EnableFailed = "User_Lock_EnableFailed"; // Lockout özelliği aktif edilirken hata.
            public const string Lock_DateSetFailed = "User_Lock_DateSetFailed"; // Kilit süresi atanırken hata.
            public const string Lock_UpdateFailed = "User_Lock_UpdateFailed"; // Kilit sonrası update işlemi başarısız.

            public const string Unlock_NotFound = "User_Unlock_NotFound"; // Kilidi açılacak kullanıcı bulunamadı.
            public const string Unlock_DateResetFailed = "User_Unlock_DateResetFailed"; // Kilit tarihi sıfırlanırken hata.
            public const string Unlock_AccessCountResetFailed = "User_Unlock_AccessCountResetFailed"; //  Hatalı giriş sayacı sıfırlanamadı.
            public const string Unlock_DisableFailed = "User_Unlock_DisableFailed"; // Lockout özelliği kapatılırken hata.
            public const string Unlock_UpdateFailed = "User_Unlock_UpdateFailed"; // Kilit açma sonrası update başarısız.

            // REACTIVATE Operations
            public const string Reactivate_NotFound = "User_Reactivate_NotFound"; // Aktif edilecek kullanıcı bulunamadı.
            public const string Reactivate_AlreadyActive = "User_Reactivate_AlreadyActive"; // Kullanıcı zaten aktif durumda.
            public const string Reactivate_UpdateFailed = "User_Reactivate_UpdateFailed"; // Aktifleştirme update işlemi başarısız.

            // GENERAL Success
            public const string Created = "User_Created";
            public const string Updated = "User_Updated";
            public const string Deleted = "User_Deleted";
            public const string Locked = "User_Locked";
            public const string Unlocked = "User_Unlocked";
            public const string Reactivated = "User_Reactivated";
            public const string Listed = "User_Listed";
            public const string DetailLoaded = "User_DetailLoaded";

        }
        public static class Role
        {
            // GET Operations
            public const string GetById_NotFound = "Role_GetById_NotFound"; // ID ile rol sorgularken kayıt bulunamadı.

            // CREATE Operations
            public const string Create_AlreadyExists = "Role_Create_AlreadyExists"; // Oluşturulmak istenen rol adı zaten mevcut.
            public const string Create_Restored = "Role_Create_Restored"; // YENİ: Silinmişti, geri getirildi.
            public const string Create_IdentityFailed = "Role_Create_IdentityFailed"; // Rol oluşturulurken Identity hatası (DB vs).

            // UPDATE Operations
            public const string Update_NotFound = "Role_Update_NotFound"; // Güncellenecek rol bulunamadı.
            public const string Update_NameAlreadyExists = "Role_Update_NameAlreadyExists"; // Yeni verilmek istenen isim başka bir rolde var.
            public const string Update_IdentityFailed = "Role_Update_IdentityFailed"; // Güncelleme işlemi Identity tarafında başarısız.

            // DELETE Operations
            public const string Delete_NotFound = "Role_Delete_NotFound"; // Silinecek rol bulunamadı.
            public const string Delete_SystemRole = "Role_Delete_SystemRole"; // (Opsiyonel) Admin gibi sistem rolleri silinemez kontrolü için.
            public const string Delete_IdentityFailed = "Role_Delete_IdentityFailed"; // Silme (soft delete) işlemi başarısız.

            // Success Messages
            public const string Listed = "Role_Listed";
            public const string Created = "Role_Created";
            public const string Updated = "Role_Updated";
            public const string Deleted = "Role_Deleted";
            public const string DetailLoaded = "Role_DetailLoaded";
        }

        public static class UserRole
        {
            // GET Operations
            public const string Listed = "UserRole_Listed";

            // ASSIGN Operations
            public const string Assign_UserNotFound = "UserRole_Assign_UserNotFound"; // Rol atanırken kullanıcı bulunamadı.
            public const string Assign_RoleNotFound = "UserRole_Assign_RoleNotFound"; // Kullanıcıya atanacak rol bulunamadı.
            public const string Assign_IdentityFailed = "UserRole_Assign_IdentityFailed"; // Atama işlemi Identity tarafında başarısız.
            // VALIDATION (Business Logic)
            public const string AlreadyAssigned = "UserRole_AlreadyAssigned"; // Kullanıcı zaten bu role sahip.
            public const string NotAssigned = "UserRole_NotAssigned";         // Kullanıcı bu role sahip değil (silinmek istenen rol yok).

            // REMOVE Operations
            public const string Remove_UserNotFound = "UserRole_Remove_UserNotFound"; // Rol kaldırılırken kullanıcı bulunamadı.
            public const string Remove_RoleNotFound = "UserRole_Remove_RoleNotFound"; // Kaldırılacak rol bulunamadı.
            public const string Remove_IdentityFailed = "UserRole_Remove_IdentityFailed"; // Kaldırma işlemi Identity tarafında başarısız.

            // Success Messages
            public const string RoleAssigned = "UserRole_RoleAssigned";
            public const string RoleRemoved = "UserRole_RoleRemoved";
        }


        public static class Auth
        {
            public const string Unauthorized = "Auth.Unauthorized"; // Yetkilendirme başarısız, geçersiz veya süresi dolmuş token.
            // REGISTER Operations
            public const string Register_EmailConflict = "Auth_Register_EmailConflict"; // E-posta zaten kayıtlı.
            public const string Register_UsernameConflict = "Auth_Register_UsernameConflict"; // Kullanıcı adı zaten alınmış.
            public const string Register_IdentityFailed = "Auth_Register_IdentityFailed"; // Kayıt oluşturulurken Identity hatası.
            public const string Register_Success = "Auth_Register_Success"; // Kayıt başarılı.
            public const string Register_RoleAssignFailed = "Auth_Register_RoleAssignFailed"; // "Kayıt sırasında varsayılan rol atanamadı."
            public const string Register_MailSendFailed = "Auth_Register_MailSendFailed";     // "Doğrulama maili gönderilemediği için kayıt işlemi iptal edildi."

            // LOGIN Operations
            public const string Login_UserNotFound = "Auth_Login_UserNotFound"; // Kullanıcı bulunamadı (veya genel hata).
            public const string Login_InvalidCredentials = "Auth_Login_InvalidCredentials"; // Şifre yanlış.
            public const string Login_Locked = "Auth_Login_Locked"; // Hesap kilitli.
            public const string Login_Inactive = "Auth_Login_Inactive"; // Hesap pasif.
            public const string Login_EmailNotConfirmed = "Auth_Login_EmailNotConfirmed"; // Mail doğrulanmamış.
            public const string Login_PasswordResetRequired = "Auth_Login_PasswordResetRequired"; // Şifre sıfırlama gerekli (ilk giriş).
            public const string Login_Success = "Auth_Login_Success"; // Giriş başarılı.

            // REFRESH TOKEN Operations
            public const string Refresh_InvalidToken = "Auth_Refresh_InvalidToken"; // Token geçersiz.
            public const string Refresh_UserNotFound = "Auth_Refresh_UserNotFound"; // Token sahibine ulaşılamadı.
            public const string Refresh_Failed = "Auth_Refresh_Failed"; // Yenileme işlemi başarısız.
            public const string Refresh_Success = "Auth_Refresh_Success"; // Token yenilendi.

            // PASSWORD Operations (Reset & Change)
            public const string ForgotPass_Sent = "Auth_ForgotPass_Sent"; // Şifremi unuttum maili gönderildi.

            public const string ResetPass_UserNotFound = "Auth_ResetPass_UserNotFound"; // Resetlenecek kullanıcı yok.
            public const string ResetPass_Inactive = "Auth_ResetPass_Inactive"; // Pasif kullanıcı şifre sıfırlayamaz.
            public const string ResetPass_EmailNotConfirmed = "Auth_ResetPass_EmailNotConfirmed"; // Maili onaylanmamış kullanıcı şifre sıfırlayamaz.
            public const string ResetPass_Failed = "Auth_ResetPass_Failed"; // Reset işlemi başarısız (Token süresi vb.).
            public const string ResetPass_Success = "Auth_ResetPass_Success"; // Şifre başarıyla sıfırlandı.

            public const string ChangePass_UserNotFound = "Auth_ChangePass_UserNotFound"; // Kullanıcı bulunamadı.
            public const string ChangePass_WrongOldPassword = "Auth_ChangePass_WrongOldPassword"; // Eski şifre yanlış.
            public const string ChangePass_Failed = "Auth_ChangePass_Failed"; // Değiştirme işlemi başarısız.
            public const string ChangePass_Success = "Auth_ChangePass_Success"; // Şifre değişti.

            // EMAIL CONFIRMATION Operations
            public const string ConfirmEmail_UserNotFound = "Auth_ConfirmEmail_UserNotFound"; // Kullanıcı yok.
            public const string ConfirmEmail_Inactive = "Auth_ConfirmEmail_Inactive"; // Pasif kullanıcı.
            public const string ConfirmEmail_AlreadyConfirmed = "Auth_ConfirmEmail_AlreadyConfirmed"; // Zaten doğrulanmış.
            public const string ConfirmEmail_Failed = "Auth_ConfirmEmail_Failed"; // Doğrulama başarısız (Token geçersiz).
            public const string ConfirmEmail_Success = "Auth_ConfirmEmail_Success"; // Mail doğrulandı.

            public const string ResendEmail_RateLimit = "Auth_ResendEmail_RateLimit"; // Çok sık istek atıldı.
            public const string ResendEmail_Sent = "Auth_ResendEmail_Sent"; // Tekrar gönderildi.

            // LOGOUT & OTHERS
            public const string Logout_Success = "Auth_Logout_Success";
            public const string Token_InvalidClaim = "Auth_Token_InvalidClaim"; // Token içinden ID okunamadı.
        }

        public static class Permission
        {
            // GET Operations
            public const string Listed = "Permission_Listed";           // Listeleme başarılı
            public const string DetailLoaded = "Permission_DetailLoaded"; // Tekil kayıt getirme başarılı

            // CREATE Operations
            public const string Created = "Permission_Created";
            public const string AlreadyExists = "Permission_Create_AlreadyExists"; // Bu izin kodu zaten var

            // UPDATE Operations
            public const string Updated = "Permission_Updated";
            public const string Update_NotFound = "Permission_Update_NotFound"; // Güncellenecek kayıt bulunamadı
            public const string CodeAlreadyExists = "Permission_Update_CodeAlreadyExists"; // Bu kod başka bir izinde kullanılıyor

            // DELETE Operations
            public const string Deleted = "Permission_Deleted";
            public const string Delete_NotFound = "Permission_Delete_NotFound"; // Silinecek kayıt bulunamadı
        }

        public static class PermissionAssignment
        {
            // VALIDATIONS
            public const string PermissionCodeRequired = "PermissionAssignment_CodeRequired";

            // NOT FOUND ERRORS (Bu işlem bağlamına özel)
            public const string RoleNotFound = "PermissionAssignment_RoleNotFound";             // Rol bulunamadı
            public const string UserNotFound = "PermissionAssignment_UserNotFound";             // Kullanıcı bulunamadı
            public const string PermissionNotFound = "PermissionAssignment_PermissionNotFound"; // Tanımlı böyle bir izin kodu yok

            // SYSTEM ERRORS (Identity işlemleri başarısız olursa)
            public const string AssignFailed = "PermissionAssignment_AssignFailed"; // Atama işlemi veritabanına yazılamadı
            public const string RemoveFailed = "PermissionAssignment_RemoveFailed"; // Silme işlemi veritabanına yazılamadı

            // SUCCESS MESSAGES
            public const string AssignedToRole = "PermissionAssignment_AssignedToRole";
            public const string RemovedFromRole = "PermissionAssignment_RemovedFromRole";
            public const string AssignedToUser = "PermissionAssignment_AssignedToUser";
            public const string RemovedFromUser = "PermissionAssignment_RemovedFromUser";
            public const string UserPermissionsListed = "PermissionAssignment_UserPermissionsListed";
            public const string RolePermissionsListed = "PermissionAssignment_RolePermissionsListed";
        }


        public static class Token
        {
            // ACCESS TOKEN Mesajları
            public const string Invalid = "Token_Invalid";                  // Geçersiz token formatı/imzası
            public const string Expired = "Token_Expired";                  // Süresi dolmuş
            public const string ValidationFailed = "Token_ValidationFailed"; // Doğrulama genel hatası

            // REFRESH TOKEN Mesajları
            public const string RefreshTokenNotFound = "Token_RefreshTokenNotFound"; // DB'de bulunamadı
            public const string RefreshTokenInvalid = "Token_RefreshTokenInvalid";   // Hatalı/Eşleşmeyen token
            public const string RefreshTokenExpired = "Token_RefreshTokenExpired";   // Süresi dolmuş veya Pasif (IsActive=false)
            public const string SecurityAlert = "Security_Token_Reuse_Detected";
            // GENEL
            public const string Generated = "Token_Generated";
            public const string Revoked = "Token_Revoked";
        }

        public static class Email
        {
            public const string SentSuccess = "Email_SentSuccess"; // E-posta başarıyla gönderildi.
            // GENEL Hatalar
            public const string SmtpFailed = "Email_SmtpFailed"; // SMTP kaynaklı hata
            public const string SendingFailed = "Email_SendingFailed";          // Genel gönderim hatası
            public const string InvalidAddress = "Email_InvalidAddress";        // Geçersiz e-posta formatı

            // GÖNDERİM BAŞARI Mesajları (Senaryo Bazlı)
            public const string ConfirmationSent = "Email_ConfirmationSent";    // Doğrulama linki gönderildi
            public const string PasswordResetSent = "Email_PasswordResetSent";  // Şifre sıfırlama linki gönderildi
            public const string CredentialsSent = "Email_CredentialsSent";      // Geçici şifre/Kullanıcı bilgileri gönderildi

            // DOĞRULAMA SONUÇLARI
            public const string VerifiedSuccess = "Email_VerifiedSuccess";      // Doğrulama başarılı
            public const string AlreadyVerified = "Email_AlreadyVerified";      // Zaten doğrulanmış
            public const string VerificationFailed = "Email_VerificationFailed";// Doğrulama başarısız (Token hatalı vs.)
        }
    }
}
