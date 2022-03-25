namespace HCM.Models
{
    public class SfGridPosition
    {
        public double RowIndex { get; set; }
        public int PageNr { get; set; }

        public SfGridPosition()
        {
            RowIndex = 0;
            PageNr = 1;
        }
    }
}
