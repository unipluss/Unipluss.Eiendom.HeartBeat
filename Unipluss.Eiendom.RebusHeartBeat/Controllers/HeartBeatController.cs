using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unipluss.Eiendom.RebusHeartBeat.Dapper;

namespace Unipluss.Eiendom.RebusHeartBeat.Controllers
{
    public class HeartBeatController : ApiController
    {
        private readonly IHeartBeatRepository _heartBeatRepository;
        private List<HeartBeat> _heartBeats;
        private List<Message> _customerDetails;

        public HeartBeatController()
        {
            _heartBeatRepository = new HeartBeatRepository();
            _heartBeats = new List<HeartBeat>();
        }

        public HttpResponseMessage Get()
        { 
            _heartBeats = _heartBeatRepository.GetLastBeats();

            return _heartBeats != null ? Request.CreateResponse(HttpStatusCode.OK, _heartBeats) : 
                Request.CreateErrorResponse(HttpStatusCode.NoContent, "Missing content");
        }

        public HttpResponseMessage Get(int id)
        {
            _customerDetails = _heartBeatRepository.GetDetails(id);

            return _customerDetails != null ? Request.CreateResponse(HttpStatusCode.OK, _customerDetails) : 
                Request.CreateErrorResponse(HttpStatusCode.NoContent, "Missing content");
        }
    }
}
