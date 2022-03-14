using AutoMapper;
using PollBack.Core.Models;
using PollBack.Web.ViewModels;

namespace PollBack.Web.AutoMapper
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<SignInResponse, SignInResponseViewModel>();
        }
    }
}
