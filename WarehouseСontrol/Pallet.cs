namespace WarehouseСontrol
{
    internal class Pallet: WarehouseObject
    {
        public List<Box>? Boxes;
        //Минимальная дата на одной из коробок
        public DateOnly GeneralMinExpirationDate = new DateOnly(2023,01,01);
        public DateOnly GeneralMaxExpirationDate = new DateOnly(2023,01,01);
        public Pallet(string _Id, float _Height, float _Width, float _Depth, List<Box> _Boxes = null) 
        {
            Id = _Id; 
            Height = _Height;
            Width = _Width;
            Depth = _Depth;
            Weight = 30;
            Volume = Height * Width * Depth;

            Boxes = _Boxes;

            if (_Boxes is null) return;
            
            GeneralMinExpirationDate = Boxes.Min(x => x.ExpirationDate);
            GeneralMaxExpirationDate = Boxes.Max(x => x.ExpirationDate);
            foreach (var box in Boxes)
            {
                Volume += box.Volume;
                Weight += box.Weight;
            }
        }
    }
}
