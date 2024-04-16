using System;
using System.Collections.Generic;

// Definición de la clase Producto
class Producto
{
    public string Nombre { get; set; }
    public float Precio { get; set; }
    public int Stock { get; set; }

    public Producto(string nombre, float precio, int stock)
    {
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }

    public virtual string Descripcion()
    {
        return $"Nombre: {Nombre}, Precio: {Precio}, Stock: {Stock}";
    }
}

// Definición de las subclases Tela y Arcilla
class Tela : Producto
{
    public Tela(string nombre, float precio, int stock) : base(nombre, precio, stock) { }

    public override string Descripcion()
    {
        return $"Tipo: Tela, {base.Descripcion()}";
    }
}

class Arcilla : Producto
{
    public Arcilla(string nombre, float precio, int stock) : base(nombre, precio, stock) { }

    public override string Descripcion()
    {
        return $"Tipo: Arcilla, {base.Descripcion()}";
    }
}

// Clase principal del programa
class TiendaArtesanias
{
    private float dineroDisponible = 200.0f;
    private List<Producto> carrito = new List<Producto>();
    private List<Producto> tienda = new List<Producto>(); // Lista de productos disponibles en la tienda
    private List<Producto> inventario = new List<Producto>(); // Lista de productos en el inventario del usuario

    public TiendaArtesanias()
    {
        // Agregar productos a la tienda
        tienda.Add(new Tela("Tela A", 20.0f, 10));
        tienda.Add(new Tela("Tela B", 25.0f, 15));
        tienda.Add(new Tela("Tela C", 30.0f, 20));
        tienda.Add(new Tela("Tela D", 35.0f, 25));
        tienda.Add(new Tela("Tela E", 40.0f, 30));
        tienda.Add(new Arcilla("Arcilla A", 15.0f, 8));
        tienda.Add(new Arcilla("Arcilla B", 18.0f, 12));
        tienda.Add(new Arcilla("Arcilla C", 20.0f, 15));
        tienda.Add(new Arcilla("Arcilla D", 22.0f, 20));
        tienda.Add(new Arcilla("Arcilla E", 25.0f, 25));
    }

    // Método para mostrar el inventario
    public void MostrarInventario()
    {
        Console.WriteLine("Inventario:");
        foreach (var producto in inventario)
        {
            Console.WriteLine(producto.Descripcion());
        }
    }

    // Método para mostrar el carrito de compras
    public void MostrarCarrito()
    {
        Console.WriteLine("Carrito de compras:");
        float total = 0;
        foreach (var producto in carrito)
        {
            Console.WriteLine(producto.Descripcion());
            total += producto.Precio;
        }
        Console.WriteLine($"Precio total del carrito: {total}");
    }

    // Método para mostrar los productos disponibles en la tienda y permitir la selección para agregar al carrito
    public void MostrarTienda()
    {
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("Productos disponibles en la tienda:");
            for (int i = 0; i < tienda.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tienda[i].Descripcion()}");
            }

            // Opciones adicionales en la lista de la tienda
            Console.WriteLine("\nOpciones:");
            Console.WriteLine("0. Ir al carrito de compras");
            Console.WriteLine("99. Volver al menú principal");

            int opcion = Convert.ToInt32(Console.ReadLine());

            if (opcion > 0 && opcion <= tienda.Count)
            {
                RealizarCompra(opcion - 1); // Restar 1 para ajustar al índice de la lista
            }
            else if (opcion == 0)
            {
                salir = true;
            }
            else if (opcion == 99)
            {
                salir = true;
                return; // Salir del método y regresar al menú principal
            }
            else
            {
                Console.WriteLine("Opción no válida. Volviendo a la tienda.");
            }
        }
    }

    // Método para realizar una compra
    public void RealizarCompra(int indiceProducto)
    {
        if (indiceProducto >= 0 && indiceProducto < tienda.Count)
        {
            Producto productoComprado = tienda[indiceProducto];
            if (dineroDisponible >= productoComprado.Precio)
            {
                carrito.Add(productoComprado);
                dineroDisponible -= productoComprado.Precio;
                Console.WriteLine($"¡{productoComprado.Nombre} añadido al carrito!");
            }
            else
            {
                Console.WriteLine("¡Dinero insuficiente para realizar la compra!");
            }
        }
        else
        {
            Console.WriteLine("¡Producto no válido!");
        }
    }

    // Método para finalizar la compra
    public void FinalizarCompra()
    {
        Console.WriteLine("Compra finalizada. Recibo:");
        float total = 0;
        foreach (var producto in carrito)
        {
            inventario.Add(producto); // Agregar producto al inventario del usuario
            Console.WriteLine(producto.Descripcion());
            total += producto.Precio;
        }
        Console.WriteLine($"Precio total de la compra: {total}");
        carrito.Clear();
    }

    // Método principal del programa
    static void Main(string[] args)
    {
        TiendaArtesanias tienda = new TiendaArtesanias();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("Bienvenido a la tienda de artesanías.");
            Console.WriteLine("1. Ver inventario");
            Console.WriteLine("2. Ver carrito de compras");
            Console.WriteLine("3. Ver productos disponibles en la tienda");
            Console.WriteLine("4. Salir de la tienda");
            Console.WriteLine("5. Realizar compra");
            Console.WriteLine("6. Finalizar compra");

            int opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    tienda.MostrarInventario();
                    break;
                case 2:
                    tienda.MostrarCarrito();
                    break;
                case 3:
                    tienda.MostrarTienda();
                    break;
                case 4:
                    salir = true;
                    break;
                case 5:
                    Console.WriteLine("Seleccione el número de producto que desea comprar:");
                    int indiceProducto = Convert.ToInt32(Console.ReadLine());
                    tienda.RealizarCompra(indiceProducto);
                    break;
                case 6:
                    tienda.FinalizarCompra();
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }
        }
    }
}
