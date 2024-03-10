using Mapster;

namespace Prosthetics.ApiModels
{
    public class EditedAdditionalCountWorkDto : IRegister
    {
        public int AdditionalWorkId { get; set; }
        public int Count { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdditionalWorkCountDto, EditedAdditionalCountWorkDto>()
                .Map(dest => dest.Count, src => int.Parse(src.Count))
                .Map(dest => dest.AdditionalWorkId, src => src.Id);
        }
    }
}
