namespace DutchTreat.Data.Entities
{
  public class OrderItem
  {
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    //public Order Order { get; set; }        //This cases self referencing loop. To handle this web have to add some code in startup.cs
                                            //i.e. remove self referencing loop from 
  }
}