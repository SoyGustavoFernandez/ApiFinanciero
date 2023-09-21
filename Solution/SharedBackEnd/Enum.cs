namespace SharedBackEnd
{
    public enum Tipo_Sexo : int
    {
        GRUPO = 1,
        MASCULINO = 1,
        FEMENINO = 2,
        NO_ESPECIFICA = 3
    }

    public enum Tipo_Cuenta : int
    {
        GRUPO = 2,
        AHORROS = 4,
        CORRIENTE = 5
    }

    public enum Tipo_Movimiento : int
    {
        GRUPO = 3,
        RETIRO = 6,
        DEPOSITO = 7
    }

    public enum Limite_Diario_Retiro : int
    {
        GRUPO = 4,
        PARAMETRO = 8
    }
}