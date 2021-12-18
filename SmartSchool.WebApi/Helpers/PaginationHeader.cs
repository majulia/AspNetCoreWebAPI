namespace SmartSchool.WebApi.Helpers
{
  public class PaginationHeader
  {
    public int CurrentPage { get; set; }
    public int ItemsPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalCount { get; set; }
    
    public PaginationHeader(int currentPage, int itemsPage, int totalItems, int totalCount)
    {
      this.CurrentPage = currentPage;
      this.ItemsPage = itemsPage;
      this.TotalItems = totalItems;
      this.TotalCount = totalCount;

    }
  }
}