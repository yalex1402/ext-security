using AutoMapper;
using secure_lib.Helpers.Interfaces;

namespace secure_lib.Helpers.Conversors
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly IMapper _mapper;

        public ConverterHelper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public OutputModel ConvertTo<OutputModel,InputModel>(InputModel model){
            var modelConverted = _mapper.Map<OutputModel>(model);
            return modelConverted;
        }
    }
}