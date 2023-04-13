using Core.Presentation.Base;
using Entities.Models;
using Service.Contracts.ServiceBase;
using Shared.DataTransferObjects.RoomStatusDto;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.Controllers
{
    public class RoomStatusController : BaseController<RoomStatus, RoomStatusDto, CreateUpdateRoomStatusDto, CreateUpdateRoomStatusDto, RequestParameters>
    {
        public RoomStatusController(IServiceBase<RoomStatus, RoomStatusDto, CreateUpdateRoomStatusDto, CreateUpdateRoomStatusDto> service) : base(service)
        {
        }
    }
}
