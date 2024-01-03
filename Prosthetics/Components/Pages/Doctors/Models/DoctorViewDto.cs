using Mapster;
using Microsoft.AspNetCore.Components;
using Prosthetics.Features.Doctors;

namespace Prosthetics.Components.Pages.Doctors.Models
{
    public class DoctorViewDto : DoctorDto
    {
        public RenderFragment? Actions { get; init; }
    }
}
