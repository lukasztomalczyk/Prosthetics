﻿using Prosthetics.Components.Models.Grid;
using Prosthetics.Components.Pages.Orders.Models;
using Prosthetics.Features.Orders;

namespace Prosthetics.Components.Pages.Orders.Views
{
    public class OrderGridConfig : GridConfig<OrderViewDto>
    {
        public OrderGridConfig() 
            : base(
                [
                    new ColumnInfo<OrderViewDto> { Title = "Pacjent", Property = "PatientFullName", Display = _  => _.PatientFullName },
                    new ColumnInfo<OrderViewDto> { Title = "Typ zamówienia", Property = "Type", Display = _  => _.Type },
                    new ColumnInfo<OrderViewDto> { Title = "Ilość dodatków", Property = "AdditionalWorksCount", Display = _  => _.AdditionalWorksCount.ToString() },
                    new ColumnInfo<OrderViewDto> { Title = "Dodatki", Property = "AdditionalWorks", Hide = true },
                    new ColumnInfo<OrderViewDto> { Title = "Komentarz", Property = "ShortComment", Display = _ => _.ShortComment },
                    new ColumnInfo<OrderViewDto> { Title = "Data zlecenia", Property = "OrderDate", Display = _ => _.OrderDate },
                    new ColumnInfo<OrderViewDto> { Title = "Data końcowa", Property = "DeadLine", Display = _  => _.DeadLine },
                    new ColumnInfo<OrderViewDto> { Title = "Status", Property = "Status", Display = _ => _.Status, Hide = true },
                    new ColumnInfo<OrderViewDto> { Title =  "Usuń/status", Property = "Status", Display = _ => _.Status, Template = _ => _.Actions }
                ]
            ) 
        { }
    }
}
