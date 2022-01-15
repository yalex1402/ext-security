namespace secure_lib.Helpers.Interfaces
{
    public interface IConverterHelper
    {
        OutputModel ConvertTo<OutputModel,InputModel>(InputModel model);
    }
}