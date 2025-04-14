namespace Denso.HotSheet.AS400.Dto
{
    public class AS400ParameterDto
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public AS400ParameterDto(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
