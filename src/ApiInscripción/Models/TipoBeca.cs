namespace ApiInscripción.Models
{
    public enum TipoBeca
    {
        /// <summary>
        /// La beca se cobra hasta la fecha de vencimiento de la cuota.
        /// </summary>
        AplicaHastaVto = 1,

        /// <summary>
        /// La beca se cobra siempre, aún después de la fecha de vencimiento de la cuota.
        /// </summary>
        AplicaSiempre = 2
    }
}
