namespace Eliza.Model.SaveData
{
    class SaveDataFooter
    {
        private int bodyLength;
        private int length;
        private uint sum;
        private int blank;

        public int BodyLength { get => bodyLength; set => bodyLength = value; }
        public int Length { get => length; set => length = value; }
        public uint Sum { get => sum; set => sum = value; }
        public int Blank { get => blank; set => blank = value; }
    }
}
