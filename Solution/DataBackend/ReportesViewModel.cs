namespace DataBackend
{
    public class ReportesViewModel
    {
        public ClienteViewModel Cliente { get; set; }
        public List<CuentaMovimientoViewModel> Cuentas { get; set; }
        public decimal nTotalCredito { get; set; }
        public decimal nTotalDebito { get; set; }
    }
}