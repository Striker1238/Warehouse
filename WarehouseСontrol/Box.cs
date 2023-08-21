namespace WarehouseСontrol
{
    internal class Box: WarehouseObject
    {
        //Срок годности
        public DateOnly ExpirationDate;
        //Дата производства
        public DateOnly ВateOfManufacture;

        public Box(DateOnly _ВateOfManufacture, string _Id, float _Height, float _Width, float _Depth, float _Weight) : 
            this(_ВateOfManufacture, _ВateOfManufacture.AddDays(100), _Id, _Height, _Width, _Depth, _Weight) { }
        public Box(DateOnly _ВateOfManufacture, DateOnly _ExpirationDate, string _Id, float _Height, float _Width, float _Depth, float _Weight) 
        {
            Id = _Id;
            Height = _Height;
            Width = _Width;
            Depth = _Depth;
            Weight = _Weight;

            Volume = Height * Width * Depth;

            ExpirationDate = _ExpirationDate;
            ВateOfManufacture = _ВateOfManufacture;
        }
    }
}
