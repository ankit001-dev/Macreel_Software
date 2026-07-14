namespace Macreel_Software.Contracts.DTOs.MyPinDtos
{
    public class EndMyDayRequestDto
    {
        public int UserId { get; set; }
        public string ActivityKey { get; set; }
        public object ActivityValue { get; set; }
        public DateTime LogoutTime { get; set; }
        public string TotalWorkingHrs { get; set; }
        public string LateReason { get; set; }
        public bool isEndMyDay { get; set; }
        public Guid? public_id { get; set; }
    }
}
