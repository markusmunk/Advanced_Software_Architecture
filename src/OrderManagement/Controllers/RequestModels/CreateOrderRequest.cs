using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Controllers.RequestModels;

public class CreateOrderRequest
{
    [Required] public string Name { get; set; } = null!;
}