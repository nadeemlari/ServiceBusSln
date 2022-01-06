using System.Text.Json;

namespace Common;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Qty { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}