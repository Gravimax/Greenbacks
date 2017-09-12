
namespace Greenbacks.Models
{
    public class GenericComboItem
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public decimal Value2 { get; set; }
        public bool Value3 { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
