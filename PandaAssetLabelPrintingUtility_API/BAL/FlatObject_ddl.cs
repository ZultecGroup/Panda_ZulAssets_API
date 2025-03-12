namespace PandaAssetLabelPrintingUtility_API.BAL
{
    public class FlatObject_ddl
    {

        public string Id { get; set; }
        public string ParentId { get; set; }
        public string title { get; set; }

        public string href { get; set; }
        public FlatObject_ddl(string name, string id, string parentId)
        {
            title = name;
            Id = id;
            ParentId = parentId;
        }
        public static List<RecursiveObject_ddl> FillRecursive_ddl(List<FlatObject_ddl> flatObjects, string parentId)
        {
            List<RecursiveObject_ddl> recursiveObjects = new List<RecursiveObject_ddl>();
            foreach (var item in flatObjects.Where(x => x.ParentId.Equals(parentId)))
            {
                recursiveObjects.Add(new RecursiveObject_ddl
                {
                    title = item.title,
                    id = item.Id,
                    href = item.href,
                    attr = new FlatTreeAttribute { id = item.Id.ToString(), selected = false },
                    subs = FillRecursive_ddl(flatObjects, item.Id)
                });
            }
            return recursiveObjects;
        }
    }
    public class RecursiveObject_ddl
    {
        public string title { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public FlatTreeAttribute attr { get; set; }
        public List<RecursiveObject_ddl> subs { get; set; }
    }
    public class FlatTreeAttribute_ddl
    {
        public string id;
        public bool selected;
    }


}
