﻿namespace AD_Manager.Layers.Model
{
    enum ADSTATUSE {
        SCRIPT = 0x0001, // 1
        ACCOUNTDISABLE = 0x0002, // 2
        HOMEDIR_REQUIRED = 0x0008, // 8
        LOCKOUT = 0x0010, // 16
        PASSWD_NOTREQD = 0x0020, // 32
        PASSWD_CANT_CHANGE = 0x0040, // 64
        ENCRYPTED_TEXT_PWD_ALLOWED = 0x0080, // 128
        TEMP_DUPLICATE_ACCOUNT = 0x0100, // 256
        NORMAL_ACCOUNT = 0x0200, // 512
        INTERDOMAIN_TRUST_ACCOUNT = 0x0800, // 2048
        WORKSTATION_TRUST_ACCOUNT = 0x1000, // 4096
        SERVER_TRUST_ACCOUNT = 0x2000, // 8192
        DONT_EXPIRE_PASSWORD = 0x10000, // 65536
        MNS_LOGON_ACCOUNT = 0x20000, // 131072
        SMARTCARD_REQUIRED = 0x40000, // 262144
        TRUSTED_FOR_DELEGATION = 0x80000, // 524288
        NOT_DELEGATED = 0x100000, // 1048576
        USE_DES_KEY_ONLY = 0x200000, // 2097152
        DONT_REQ_PREAUTH = 0x400000, // 4194304
        PASSWORD_EXPIRED = 0x800000, // 8388608
        TRUSTED_TO_AUTH_FOR_DELEGATION = 0x1000000, // 16777216
        PARTIAL_SECRETS_ACCOUNT = 0x04000000, // 67108864
    }

    public class User
    {
        public string UserName { get; set; }
        public string password { get; set; }
        public string? Captcha { get; set; }
        public string _token { get; set; }
        public string expiresIn { get; set; }
        //[JsonIgnore]
        public string Role { get; set; }
        public bool GTUser { get; set; }
        public string[] Permitions { get; set; }

    }
}