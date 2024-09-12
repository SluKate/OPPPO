namespace NewStorageLab.Domain.DTOs;

public class ProductReplanishDTO
{
    public string Name { get; set; } = null!;
    public int Count { get; set; }
    public double PricePerPiece { get; set; } = 0!;
}
