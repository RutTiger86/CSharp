namespace CSharp.Authorization.Session.Models
{
    public class SessionInfo
    {
        public required string SessionId { get; set; }
        public DateTime ExpriedTime { get; set; }
        public void InitExpiredTime(int minutes = 20)
        {
            this.ExpriedTime = DateTime.Now.AddMinutes(minutes);
        }
    }
}
