using Core.Presentation.Base;
using Entities.Models;
using Service.Contracts.ServiceBase;
using Shared.DataTransferObjects.RoomDto;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.Controllers
{
    public class RoomController : BaseController<Room, RoomDto, CreateUpdateRoom, CreateUpdateRoom, RequestParameters>
    {
        public RoomController(IServiceBase<Room, RoomDto, CreateUpdateRoom, CreateUpdateRoom> service) : base(service)
        {
        }
    }
}
