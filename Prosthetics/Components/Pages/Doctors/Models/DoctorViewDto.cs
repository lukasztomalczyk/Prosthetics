using Microsoft.AspNetCore.Components;
using Prosthetics.Components.Layout.Abstractions;
using Prosthetics.ApiModels;

namespace Prosthetics.Components.Pages.Doctors.Models
{
    public class DoctorViewDto : DoctorDto, IGridData
    {
        public RenderFragment? Actions { get; init; }
        public string? ClassRow { get; set; }
    }
}
