using Mapster;
using Prosthetics.Components.Layout.Abstractions;
using Microsoft.AspNetCore.Components;
using Prosthetics.Features.Doctors;

namespace Prosthetics.Components.Pages.Doctors.Models
{
    public class DoctorViewDto : DoctorDto, IGridData
    {
        public RenderFragment? Actions { get; init; }
        public string? ClassRow { get; set; }
    }
}
