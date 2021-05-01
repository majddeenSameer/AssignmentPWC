namespace Domain.Lookups
{
    public class RequestStatus : LookupBase
    {
        public RequestStatus()
        {
        }

        public static RequestStatus Draft = new RequestStatus(1);
        public static RequestStatus Submitted = new RequestStatus(2);
        public static RequestStatus Approved = new RequestStatus(3);
        public static RequestStatus Rejected = new RequestStatus(4);

        public RequestStatus(long id):base(id)
        {
        }
    }
}
