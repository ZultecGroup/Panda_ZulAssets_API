namespace PandaAssetLabelPrintingUtility_API.BAL
{
    public class FlatObject
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string text { get; set; }

        public string href { get; set; }
        public FlatObject(string name, string id, string parentId, string href)
        {
            text = name;
            Id = id;
            ParentId = parentId;
            this.href = href;
        }
        public static List<RecursiveObject> FillRecursive(List<FlatObject> flatObjects, string parentId)
        {
            List<RecursiveObject> recursiveObjects = new List<RecursiveObject>();
            foreach (var item in flatObjects.Where(x => x.ParentId.Equals(parentId)))
            {
                recursiveObjects.Add(new RecursiveObject
                {
                    text = item.text,
                    id = item.Id,
                    href = item.href,
                    attr = new FlatTreeAttribute { id = item.Id.ToString(), selected = false },
                    children = FillRecursive(flatObjects, item.Id)
                });
            }
            return recursiveObjects;
        }
    }
    public class RecursiveObject
    {
        public string text { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public FlatTreeAttribute attr { get; set; }
        public List<RecursiveObject> children { get; set; }
    }
    public class FlatTreeAttribute
    {
        public string id;
        public bool selected;
    }
}
