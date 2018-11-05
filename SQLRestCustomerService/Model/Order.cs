namespace SQLRestCustomerService.Model
{
    public class Order
    {
        public string Dato { get; set; }
        public int Kundeid { get; set; }
        public int Id { get; set; }
        public Order(int id, int kundeid, string dato)
        {
            this.Id = id;
            this.Kundeid = kundeid;
            this.Dato = dato;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Kundeid)}: {Kundeid}, {nameof(Dato)}: {Dato}";
        }
    }
}