namespace FastRestaurant
{
        public class DataCollection<T> where T : class
        {
            public IEnumerable<T> Items { get; set; }
        }

        public class Pagination
        {
            public int Total { get; set; }
            public int page { get; set; }
            public int pages { get; set; }
            public string column { get; set; }
            public string order { get; set; }
        }
}
