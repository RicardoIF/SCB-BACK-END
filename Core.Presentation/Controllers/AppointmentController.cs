using Core.Presentation.Base;
using Entities.Models;
using Service.Contracts.ServiceBase;
using Shared.DataTransferObjects.AppointmentsDto;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.Controllers
{
    public class AppointmentController : BaseController<Appointment, AppointmentDto, CreateUpdateAppointment, CreateUpdateAppointment, RequestParameters>
    {
        public AppointmentController(IServiceBase<Appointment, AppointmentDto, CreateUpdateAppointment, CreateUpdateAppointment> service) : base(service)
        {
        }
    }
}
