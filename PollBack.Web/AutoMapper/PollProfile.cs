using AutoMapper;
using PollBack.Core.PollAggregate;
using PollBack.Web.ViewModels;

namespace PollBack.Web.AutoMapper
{
    public class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<PollOption, PollOptionViewModel>();
            //TODO: pomyslec jeszcze/przerobic query tak zeby zwracalo tez sume oddanych glosow
            CreateMap<Poll, PollViewModel>()
                .ForMember(
                    dest => dest.TotalVotes,
                    opt => opt.MapFrom(src => src.Options.Sum(x => x.Votes))
                );
        }
    }
}
