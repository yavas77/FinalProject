namespace Building.Application.Features.Queries.IncomeAndExpenditure
{
    public class ApartmentExpenseListModel
    {
        public int Id { get; set; }
        public string Bill { get; set; }
        public decimal Amount { get; set; }
        public string Apartment { get; set; }
        public string User { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
