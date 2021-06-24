using Eliza.Model.SaveData;

namespace Eliza.Model
{
    public class Save
    {
        public SaveDataHeader header;
        public RF5SaveData saveData;
        private SaveDataFooter footer;

        public SaveDataFooter Footer { get => footer; set => footer = value; }
    }
}
