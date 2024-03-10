using Prosthetics.Components.Models.Grid;
using Prosthetics.Components.Pages.Doctors.Models;

namespace Prosthetics.Components.Pages.Doctors.DoctorsViews
{
    public class DoctorsGridConfig : GridConfig<DoctorViewDto>
    {
        public DoctorsGridConfig()
            : base(
                [
                    new ColumnInfo<DoctorViewDto> { Title = "Imię i nazwisko", Property = "FullName", Display = _ => _.FullName },
                    new ColumnInfo<DoctorViewDto> { Title = "Usuń", Property = "FullName", Template = _ => _.Actions }
                ]
            )
        { }
    }
}
