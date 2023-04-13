using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.RoomDto;

namespace SCB.Mappers
{
    public class RoomMapper : Profile
    {
        public RoomMapper()
        {
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Room, CreateUpdateRoom>().ReverseMap();

        }
    }
}
