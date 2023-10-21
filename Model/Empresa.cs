namespace SERVICIO_DE_FACTURACION.Model
{
    public class Empresa
    {
        public string? NombreCliente { get; set; }
        public string? ApellidoCliente { get; set; }
        public int? EdadCliente { get; set; }
        public string? RutCliente { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? RutEmpresa { get; set; }
        public string? GiroEmpresa { get; set; }
        public float TotalVentas { get; set; }
        public float MontoVentas { get; set; }
        public float MontoIva { get; set; }
        public float MontoUtilidades { get; set; }
    }
}
