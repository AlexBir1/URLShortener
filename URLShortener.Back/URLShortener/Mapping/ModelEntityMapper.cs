using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.Models;

namespace URLShortener.Mapping
{
    public class ModelEntityMapper : Profile
    {
        public ModelEntityMapper()
        {
            CreateMap<AboutContent, AboutContentModel>();
            CreateMap<AboutContentModel, AboutContent>();

            CreateMap<Setting, SettingModel>();
            CreateMap<SettingModel, Setting>();

            CreateMap<SignUpModel, Account>()
                .ForMember(x => x.UserName, f => f.MapFrom(x => x.Username));
            CreateMap<Account, SignUpModel>()
                .ForMember(x => x.Username, f => f.MapFrom(x => x.UserName));

            CreateMap<SignInModel, Account>()
                .ForMember(x => x.UserName, f => f.MapFrom(x => x.Login));
            CreateMap<Account, SignInModel>()
                .ForMember(x => x.Login, f => f.MapFrom(x => x.UserName));

            CreateMap<Account, AccountModel>()
                .ForMember(x => x.Username, f => f.MapFrom(x => x.UserName));
            CreateMap<AccountModel, Account>()
                .ForMember(x => x.UserName, f => f.MapFrom(x => x.Username));

            CreateMap<UpdateAccountModel, Account>()
                .ForMember(x => x.UserName, f => f.MapFrom(x => x.Username));
            CreateMap<Account, UpdateAccountModel > ()
                .ForMember(x => x.Username, f => f.MapFrom(x => x.UserName));

            CreateMap<UpdateAccountModel, ChangePasswordProperties>()
                .ForMember(x => x.OldPassword, f => f.MapFrom(x => x.OldPassword))
                .ForMember(x => x.NewPassword, f => f.MapFrom(x => x.NewPassword))
                .ForMember(x => x.ConfirmNewPassword, f => f.MapFrom(x => x.ConfirmNewPassword));

            CreateMap<ChangePasswordProperties, UpdateAccountModel>()
                .ForMember(x => x.OldPassword, f => f.MapFrom(x => x.OldPassword))
                .ForMember(x => x.NewPassword, f => f.MapFrom(x => x.NewPassword))
                .ForMember(x => x.ConfirmNewPassword, f => f.MapFrom(x => x.ConfirmNewPassword));

            CreateMap<ShortURL, ShortURLModel>()
                .ForPath(x => x.CreatedBy, f => f.MapFrom(x => x.Info.CreatedBy));
            CreateMap<ShortURLModel, ShortURL>()
                .ForPath(x => x.Info.CreatedBy, f => f.MapFrom(x => x.CreatedBy));

            CreateMap<ShortURLInfo, ShortURLInfoModel>()
                .ForPath(x => x.Url, f => f.MapFrom(x => x.ShortURL.Url))
                .ForPath(x => x.Origin, f => f.MapFrom(x => x.ShortURL.Origin))
                .ForPath(x => x.CreatedByUserId, f => f.MapFrom(x => x.ShortURL.CreatedByUserId));
            CreateMap<ShortURLInfoModel, ShortURLInfo>()
                .ForPath(x => x.ShortURL.Url, f => f.MapFrom(x => x.Url))
                .ForPath(x => x.ShortURL.Origin, f => f.MapFrom(x => x.Origin))
                .ForPath(x => x.ShortURL.CreatedByUserId, f => f.MapFrom(x => x.CreatedByUserId));

            CreateMap<ShortURL, ShortURLInfoModel>()
                .ForPath(d => d.CreatedBy, s => s.MapFrom(x => x.Info.CreatedBy))
                .ForPath(d => d.CreationDate, s => s.MapFrom(x => x.Info.CreationDate));

            CreateMap<ShortURLInfoModel, ShortURL>()
                .ForPath(d => d.Info.CreatedBy, s => s.MapFrom(x => x.CreatedBy))
                .ForPath(d => d.Info.CreationDate, s => s.MapFrom(x => x.CreationDate));
        }
    }
}
