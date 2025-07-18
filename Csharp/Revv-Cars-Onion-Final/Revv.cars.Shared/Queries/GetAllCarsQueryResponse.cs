namespace Revv.cars.Shared.Queries
{
    public class GetAllCarsQueryResponse
    {
        // Fix CS0118 by fully qualifying the type name if 'Car' is ambiguous
        public List<Car> Cars { get; set; } = new(); // Fix IDE0028 by simplifying collection initialization
    }
}
