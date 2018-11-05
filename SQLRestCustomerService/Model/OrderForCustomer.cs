namespace SQLRestCustomerService.Model
{
    public class OrderForCustomer
    {
        public string Navn { get; set; }
        public int Kundeid { get; set; }
        public int OrdreId { get; set; }
        public OrderForCustomer(int ordreId, int kundeid, string navn)
        {
            this.OrdreId = ordreId;
            this.Kundeid = kundeid;
            this.Navn = navn;
        }

        public override string ToString()
        {
            return $"{nameof(OrdreId)}: {OrdreId}, {nameof(Kundeid)}: {Kundeid}, {nameof(Navn)}: {Navn}";
        }
    }
}