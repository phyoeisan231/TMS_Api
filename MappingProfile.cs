using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TMS_Api.DBModels;
using TMS_Api.DTOs;
namespace TMS_Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
			CreateMap<UserForRegistrationDto, IdentityUser>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<TruckType, TruckTypeDto>().ReverseMap();
            CreateMap<TransporterType, TransporterTypeDto>().ReverseMap();
            CreateMap<Transporter, TransporterDto>().ReverseMap();
            CreateMap<Gate, GateDto>().ReverseMap();
            CreateMap<Truck, TruckDto>().ReverseMap();
            CreateMap<Trailer, TrailerDto>().ReverseMap();
            CreateMap<Driver, DriverDto>().ReverseMap();
            CreateMap<Yard, YardDto>().ReverseMap();
            CreateMap<WeightBridge, WeightBridgeDto>().ReverseMap();
            CreateMap<TruckEntryType, TruckEntryTypeDto>().ReverseMap();
            CreateMap<TruckJobType, TruckJobTypeDto>().ReverseMap();
            CreateMap<WaitingArea, WaitingAreaDto>().ReverseMap();
            CreateMap<PCategory, PCategoryDto>().ReverseMap();
            CreateMap<PCard, PCardDto>().ReverseMap();
            CreateMap<ICD_InBoundCheck, ICD_InBoundCheckDto>().ReverseMap();
            CreateMap<ICD_InBoundCheck_Document, ICD_InBoundCheck_DocumentDto>().ReverseMap();
            CreateMap<OperationArea, OperationAreaDto>().ReverseMap();
            CreateMap<DocumentSetting, DocumentSettingDto>().ReverseMap();
            CreateMap<ICD_TruckProcess,ICD_InBoundCheck>().ReverseMap();
            CreateMap<ICD_OutBoundCheck, ICD_OutBoundCheckDto>().ReverseMap();
            CreateMap<ICD_OutBoundCheck_Document, ICD_OutBoundCheck_DocumentDto>().ReverseMap();
            CreateMap<ICD_TruckProcess, ICD_OutBoundCheck>().ReverseMap();



            //For Proposal
            CreateMap<TMS_Proposal,TMS_ProposalDto>().ReverseMap();




            ///Weight Bridge
            CreateMap<WeightServiceBill, WeightServiceBillDto>().ReverseMap();


        }
    }
}
