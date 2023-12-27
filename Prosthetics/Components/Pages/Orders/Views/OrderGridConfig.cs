using Prosthetics.Components.Models.Grid;
using Prosthetics.Features.Orders;

namespace Prosthetics.Components.Pages.Orders.Views
{
    public class OrderGridConfig : GridConfig<OrderDto>
    {
        public OrderGridConfig() 
            : base(
                [
                    new ColumnInfo<OrderDto> { Title = "Pacjent", Property = "PatientFullName", Display = _  => _.PatientFullName },
                    new ColumnInfo<OrderDto> { Title = "Typ zamówienia", Property = "Type", Display = _  => _.Type },
                    new ColumnInfo<OrderDto> { Title = "Ilość dodatków", Property = "AdditionalWorksCount", Display = _  => _.AdditionalWorksCount.ToString() },
                    new ColumnInfo<OrderDto> { Title = "Dodatki", Property = "AdditionalWorks", Hide = true },
                    new ColumnInfo<OrderDto> { Title = "Komentarz", Property = "ShortComment", Display = _ => _.ShortComment },
                    new ColumnInfo<OrderDto> { Title = "Data zlecenia", Property = "OrderDate", Display = _ => _.OrderDate },
                    new ColumnInfo<OrderDto> { Title = "Data końcowa", Property = "DeadLine", Display = _  => _.DeadLine },
                    new ColumnInfo<OrderDto> { Title = "Status", Property = "Status", Display = _ => _.Status }
                ]
            ) 
        { }
    }
}
