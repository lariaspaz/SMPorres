using NotificacionesDeuda.Controller;

namespace NotificacionesDeuda
{
    class Program
    {
        static void Main(string[] args)
        {
            new NotificacionDeudaController().Notificar();
        }

    }
}
