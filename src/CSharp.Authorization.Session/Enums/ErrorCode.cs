namespace CSharp.Authorization.Session.Enums
{
    public enum ErrorCode
    {
        NONE = 0,
        SESSION_UNAAUTHORIZED, // 미인증세션
        SESSION_EXPIRED, // 세션 만료
        SYSTEM_EXCEPTION, // 시스템 에러
    }
}
