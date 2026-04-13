namespace Flowra.Backend.Application.SystemMessages
{
    public static class MailMessages
    {
        public static class General
        {
            // Value'lar .resx dosyasındaki Key'lerdir.

            public const string Greeting = "Mail_General_Greeting"; // Sayın

            public const string FallbackInfo = "Mail_General_FallbackInfo"; // Eğer yukarıdaki butona tıklamakta sorun yaşıyorsanız, aşağıdaki bağlantıyı kopyalayıp tarayıcınıza yapıştırabilirsiniz:

            public const string LogoAltName = "Mail_General_LogoAltName"; // Europower Enerji
        }

        public static class Footer
        {
            public const string InfoOne = "Mail_Footer_InfoOne"; // Europower Enerji ve Otomasyon Tek. San. Tic. A.Ş.

            public const string InfoTwo = "Mail_Footer_InfoTwo"; // Saray Mah. Atom Cad. No:17, 06980 Kahramankazan / Ankara

            public const string InfoThree = "Mail_Footer_InfoThree"; // Tüm hakları saklıdır. Bu e-posta otomatik oluşturulmuştur, lütfen yanıtlamayınız.
        }

        public static class EmailConfirmation
        {
            public const string Title = "Mail_EmailConfirmation_Title"; // E-posta Doğrulama

            public const string Subject = "Mail_EmailConfirmation_Subject"; // E-posta Adresinizi Doğrulayın

            public const string Info = "Mail_EmailConfirmation_Info"; // Hesabınızın güvenliği ve işlemlerinize devam edebilmeniz için lütfen aşağıdaki butona tıklayarak e-posta adresinizi doğrulayınız.

            public const string ButtonText = "Mail_EmailConfirmation_ButtonText"; // Hesabımı Doğrula
        }

        public static class ResendConfirmation
        {
            public const string Title = "Mail_ResendConfirmation_Title"; // Doğrulama Bağlantısı

            public const string Subject = "Mail_ResendConfirmation_Subject"; // Yeni Doğrulama Bağlantınız

            public const string Info = "Mail_ResendConfirmation_Info"; // Yeni bir doğrulama bağlantısı talep ettiniz. Hesabınızı aktif etmek için lütfen aşağıdaki butonu kullanınız.

            public const string ButtonText = "Mail_ResendConfirmation_ButtonText"; // E-postayı Doğrula
        }

        public static class PasswordReset
        {
            public const string Title = "Mail_PasswordReset_Title"; // Şifre Sıfırlama

            public const string Subject = "Mail_PasswordReset_Subject"; // Şifre Sıfırlama Talebi

            public const string Info = "Mail_PasswordReset_Info"; // Hesabınız için bir şifre sıfırlama talebi aldık. Eğer bu işlemi siz yapmadıysanız bu e-postayı görmezden gelebilirsiniz. Şifrenizi yenilemek için:

            public const string ButtonText = "Mail_PasswordReset_ButtonText"; // Şifremi Sıfırla
        }

        public static class InitialPassword
        {
            public const string Title = "Mail_InitialPassword_Title"; // Hesap Oluşturuldu

            public const string Subject = "Mail_InitialPassword_Subject"; // EuroScada Hesap Bilgileriniz

            public const string Info = "Mail_InitialPassword_Info"; // EuroScada sistemine kaydınız başarıyla oluşturulmuştur. Giriş yapabilmeniz için gerekli bilgiler aşağıdadır. Lütfen giriş yaptıktan sonra şifrenizi değiştiriniz.

            public const string LabelUsername = "Mail_InitialPassword_LabelUsername"; // Kullanıcı Adı:

            public const string LabelPassword = "Mail_InitialPassword_LabelPassword"; // Geçici Şifre:

            public const string ButtonText = "Mail_InitialPassword_ButtonText"; // Sisteme Giriş Yap
        }

        public static class AdminAlert
        {
            public const string Subject = "Mail_AdminAlert_Subject"; // "SİSTEM ALARMI"
            public const string LabelSubject = "Mail_AdminAlert_LabelSubject";   // "Konu"
            public const string LabelDate = "Mail_AdminAlert_LabelDate";         // "Olay Tarihi"
            public const string LabelError = "Mail_AdminAlert_LabelError";       // "Hata Detayı / Stack Trace"
            public const string InfoMessage = "Mail_AdminAlert_InfoMessage";     // "Bu alarm, sistemde kritik bir hata oluştuğu için..."
        }



        // LOG RAPORLARI İÇİN STANDART BAŞLIKLAR
        public static class LogReport
        {
            public const string Header_Status = "Log_Header_Status";             // "DURUM" / "STATUS"
            public const string Header_ErrorDetail = "Log_Header_ErrorDetail";   // "HATA DETAYI" / "ERROR DETAIL"
            public const string Header_OriginalData = "Log_Header_OriginalData"; // "ORİJİNAL VERİ (JSON)" / "PAYLOAD (JSON)"
            public const string Header_ActionDetails = "Log_Header_ActionDetails"; // "İŞLEM DETAYLARI" / "ACTION DETAILS"
        }

        // SİSTEM GENELİ HATA MESAJLARI
        public static class SystemErrors
        {
            public const string RabbitMQ_ConsumeFailed = "Error_RabbitMQ_ConsumeFailed"; // "Sistem mesajı işlemeyi defalarca denedi ve başarısız oldu."
            public const string RabbitMQ_PublishTimeout = "Error_RabbitMQ_PublishTimeout"; // "Mesaj kuyruğa yazılamadı (Timeout). Veri kaybı riski olabilir."
            public const string RabbitMQ_ConnectionError = "Error_RabbitMQ_ConnectionError"; // "Mesaj gönderimi sırasında sistem geneli bir hata oluştu."
            public const string RabbitMQ_Publisher_Activity_Subject = "Error_RabbitMQ_Pub_Activity_Subject"; // "RabbitMQ Bağlantı Hatası (Activity Audit)"
            public const string RabbitMQ_Publisher_Data_Subject = "Error_RabbitMQ_Pub_Data_Subject"; // "RabbitMQ Bağlantı Hatası (Data Audit)"
            public const string RabbitMQ_Publisher_Generic_Subject = "Error_RabbitMQ_Pub_Generic_Subject"; // "RabbitMQ Bağlantı Hatası"
        }

        public static class ActivityAuditErrors
        {
            public const string Subject = "Error_ActivityAudit_Subject"; // "RabbitMQ Tüketim (Activity Audit) Hatası"
            public const string RetryLimitExceeded = "Error_ActivityAudit_RetryLimit"; // "Sistem mesajı işlemeyi 5 kez denedi ve başarısız oldu."
        }

        public static class DataAuditErrors
        {
            public const string Subject = "Error_DataAudit_Subject"; // "RabbitMQ Tüketim (Data Audit) Hatası"
        }
    }
}