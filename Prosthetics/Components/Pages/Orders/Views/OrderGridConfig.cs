using Prosthetics.Components.Models.Grid;
using Prosthetics.Components.Pages.Orders.ViewModels;
using Prosthetics.Features.Orders;

namespace Prosthetics.Components.Pages.Orders.Views
{
    public class OrderGridConfig : GridConfig<OrderView>
    {
        public OrderGridConfig() 
            : base(
                [
                    new ColumnInfo<OrderView> { Title = "Pacjent", Property = "PatientFullName", Display = _  => _.PatientFullName },
                    new ColumnInfo<OrderView> { Title = "Typ zamówienia", Property = "Type", Display = _  => _.Type },
                    new ColumnInfo<OrderView> { Title = "Ilość dodatków", Property = "AdditionalWorksCount", Display = _  => _.AdditionalWorksCount.ToString() },
                    new ColumnInfo<OrderView> { Title = "Dodatki", Property = "AdditionalWorks", Hide = true },
                    new ColumnInfo<OrderView> { Title = "Komentarz", Property = "ShortComment", Display = _ => _.ShortComment },
                    new ColumnInfo<OrderView> { Title = "Data zlecenia", Property = "OrderDate", Display = _ => _.OrderDate },
                    new ColumnInfo<OrderView> { Title = "Data końcowa", Property = "DeadLine", Display = _  => _.DeadLine },
                    new ColumnInfo<OrderView> { Title = "Status", Property = "Status", Display = _ => _.Status, Hide = true },
                    new ColumnInfo<OrderView> { Title =  "Usuń/status", Property = "Status", Display = _ => _.Status, Template = _ => _.Actions }
                ]
            ) 
        { }
    }
}
