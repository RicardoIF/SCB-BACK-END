using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.RoomStatusDto;

namespace SCB.Mappers
{
    public class RoomStatusMapper : Profile
    {
        public RoomStatusMapper()
        {
            CreateMap<RoomStatus, RoomStatusDto>().ReverseMap();
            CreateMap<RoomStatus, CreateUpdateRoomStatusDto>().ReverseMap();
        }
    }
}
